using AutoMapper;
using JEPCO.Application.Exceptions;
using JEPCO.Application.Interfaces.IAM;
using JEPCO.Application.Models.Users.Internal;
using JEPCO.Shared;
using JEPCO.Shared.Constants;
using JEPCO.Shared.ModelsAbstractions;
using Keycloak.AuthServices.Common;
using Keycloak.AuthServices.Sdk;
using Keycloak.AuthServices.Sdk.Admin;
using Keycloak.AuthServices.Sdk.Admin.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace JEPCO.Infrastructure.IAM
{
    public partial class KeycloakUserManager : IIdentityUserManager
    {
        /// <summary>
        /// To directly send http requests to Rest Admin API for the actions not implemented in AuthServices SDK.
        /// Handling tokens are done with a library configured in program.cs
        /// </summary>
        private readonly HttpClient _httpkeyCloakClient;

        /// <summary>
        /// From AuthServices SDK, for easier communication with rest APIs to perform actions [Not all actions are implemented]
        /// </summary>
        private readonly IKeycloakClient _keycloakClient;
        private readonly IConfiguration _configuration;
        private readonly IStringLocalizer<SharedResource> _localizer;
        private readonly IMapper _mapper;

        private readonly KeycloakAdminClientOptions _keycloakConfiguration;
        private readonly string _realm;

        public KeycloakUserManager(IStringLocalizer<SharedResource> localizer, IMapper mapper, IConfiguration configuration,
            IKeycloakClient client, IHttpClientFactory httpClientFactory)
        {
            _localizer = localizer;
            _mapper = mapper;
            _configuration = configuration;
            _keycloakClient = client;


            _httpkeyCloakClient = httpClientFactory.CreateClient(KeycloakInternalDefinedConstants.KeycloakHttpClientName);
            _keycloakConfiguration = configuration.GetKeycloakOptions<KeycloakAdminClientOptions>()!;
            _realm = _keycloakConfiguration.Realm ??
                throw new CustomException(FailResponseStatus.Failed, _localizer.GetString(LocalizationKeysConstant.InternalServerErrorDefaultMessage));
        }

        public async Task<IAMUser> GetUserAsync(Guid userId)
        {

            var representationUser = await _keycloakClient.GetUserAsync(_realm, userId.ToString());
            if (representationUser == null)
            {
                throw new CustomException(FailResponseStatus.Failed, _localizer.GetString(LocalizationKeysConstant.InternalServerErrorDefaultMessage));
            }

            var user = _mapper.Map<IAMUser>(representationUser);
            return user;
        }

        public async Task<Guid> CreateUserAsync(IAMUser user)
        {
            var userRepresentation = _mapper.Map<UserRepresentation>(user);
            var response = await _keycloakClient.CreateUserWithResponseAsync(_realm, userRepresentation);
            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                {
                    throw new CustomException(FailResponseStatus.Conflict, _localizer.GetString(LocalizationKeysConstant.ConflictDefaultMessage));
                }

                throw new CustomException(FailResponseStatus.Failed, _localizer.GetString(LocalizationKeysConstant.InternalServerErrorDefaultMessage));
            }

            // get user to return the generated userId
            var createdUser = await _keycloakClient.GetUsersAsync(_realm, new Keycloak.AuthServices.Sdk.Admin.Requests.Users.GetUsersRequestParameters()
            { Username = userRepresentation.Username, Email = userRepresentation.Email });

            var createdUserId = new Guid(createdUser.First().Id);

            //TODO: remove the comment bellow when smtp is ready on dev env
            //if (!userRepresentation.EmailVerified.HasValue || !userRepresentation.EmailVerified.Value)
            //{
            //    await SendVerifyEmailAsync(createdUserId);
            //}
            return createdUserId;
        }

        public async Task UpdateUserAsync(IAMUser user)
        {
            var userRepresentation = _mapper.Map<UserRepresentation>(user);

            var response = await _keycloakClient.UpdateUserWithResponseAsync(_realm, userRepresentation.Id!, userRepresentation);
            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                {
                    throw new CustomException(FailResponseStatus.Conflict, _localizer.GetString(LocalizationKeysConstant.ConflictDefaultMessage));
                }

                throw new CustomException(FailResponseStatus.Failed, _localizer.GetString(LocalizationKeysConstant.InternalServerErrorDefaultMessage));
            }
        }

        public async Task DeleteUserAsync(Guid userId)
        {
            var response = await _keycloakClient.DeleteUserWithResponseAsync(_realm, userId.ToString());
            if (!response.IsSuccessStatusCode)
            {
                throw new CustomException(FailResponseStatus.Failed, _localizer.GetString(LocalizationKeysConstant.InternalServerErrorDefaultMessage));
            }
        }
        public async Task SendVerifyEmailWithResponseAsync(Guid userId)
        {
            var response = await _keycloakClient.SendVerifyEmailWithResponseAsync(_realm, userId.ToString());
            if (!response.IsSuccessStatusCode)
            {
                throw new CustomException(FailResponseStatus.Failed, _localizer.GetString(LocalizationKeysConstant.InternalServerErrorDefaultMessage));
            }
        }
        public async Task SendVerifyEmailAsync(Guid userId)
        {
            var response = await _keycloakClient.SendVerifyEmailWithResponseAsync(_realm, userId.ToString());
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Verify email does not send from keycloak to the user: {userId}");
            }
        }

        public async Task SetUserPasswordAsync(Guid userId, string password, bool isTemporary)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"/admin/realms/{_realm}/users/{userId}/reset-password");

            var passwordData = new
            {
                type = "password",
                temporary = isTemporary,
                value = password
            };

            request.Content = new StringContent(JsonSerializer.Serialize(passwordData), Encoding.UTF8, "application/json");

            var response = await _httpkeyCloakClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }
        public async Task<Response<bool>> ResetUserPasswordAsync(Guid userId)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"/admin/realms/{_realm}/users/{userId}/reset-password-email");

            var response = await _httpkeyCloakClient.SendAsync(request);
            return new(response.IsSuccessStatusCode, response.ReasonPhrase);
        }


        public async Task DisableUserAsync(Guid userId)
        {
            var user = await GetUserAsync(userId);
            user.Enabled = false;
            await UpdateUserAsync(user);
        }
        public async Task EnableUserAsync(Guid userId)
        {
            var user = await GetUserAsync(userId);
            user.Enabled = true;
            await UpdateUserAsync(user);
        }



        public async Task<List<IAMUserSessionData>> GetUserSessionsAsync(Guid userId)
        {
            // Define the endpoint for fetching user events
            var requestUri = $"/admin/realms/{_realm}/users/{userId}/sessions";

            // Send the GET request to the Keycloak server
            var response = await _httpkeyCloakClient.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();

            // Read the response content as a list of login events
            var sessionEvents = await response.Content.ReadFromJsonAsync<List<IAMUserSessionData>>();

            return sessionEvents ?? new List<IAMUserSessionData>();
        }

        public async Task<Response<bool>> LogoutUserAsync(Guid userId)
        {

            var request = new HttpRequestMessage(HttpMethod.Post, $"/admin/realms/{_realm}/users/{userId}/logout");

            var response = await _httpkeyCloakClient.SendAsync(request);

            return new(response.IsSuccessStatusCode, response.ReasonPhrase);
        }



    }
}

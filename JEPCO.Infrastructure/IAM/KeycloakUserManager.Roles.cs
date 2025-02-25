using Keycloak.AuthServices.Sdk.Admin.Models;
using System.Text;
using System.Text.Json;

namespace JEPCO.Infrastructure.IAM
{
    public partial class KeycloakUserManager
    {

        public async Task SetUserRolesAsync(Guid userId, IEnumerable<string> roles)
        {
            var roleRepresentations = await GetRolesRepresentation(roles);
            var request = new HttpRequestMessage(HttpMethod.Post, $"/admin/realms/{_realm}/users/{userId}/role-mappings/realm");

            request.Content = new StringContent(JsonSerializer.Serialize(roleRepresentations), Encoding.UTF8, "application/json");

            var response = await _httpkeyCloakClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }


        private async Task<List<RoleRepresentation>> GetRolesRepresentation(IEnumerable<string> roles)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"/admin/realms/{_realm}/roles");
            var response = await _httpkeyCloakClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var allRoles = JsonSerializer.Deserialize<List<RoleRepresentation>>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            var roleRepresentations = allRoles?.Where(r => roles.Contains(r.Name)).ToList() ?? new List<RoleRepresentation>();

            return roleRepresentations;
        }
    }
}

using AutoMapper;
using JEPCO.Application.Models.Users.Internal;
using Keycloak.AuthServices.Sdk.Admin.Models;

namespace JEPCO.Infrastructure.Mapper;

public class KeycloakMapper : Profile
{
    public KeycloakMapper()
    {
        CreateMap<IAMUser, UserRepresentation>()
            .ForMember(des => des.Id, opt => opt.MapFrom(src => src.Id.ToString()))
            .ForMember(des => des.Username, opt => opt.MapFrom(src => src.UserName))
            .ForMember(des => des.FirstName, opt => opt.MapFrom(src => src.Name))
            .ForMember(des => des.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(des => des.EmailVerified, opt => opt.MapFrom(src => src.EmailVerified))
            .ForMember(des => des.Enabled, opt => opt.MapFrom(src => src.Enabled))
            ;

        CreateMap<UserRepresentation, IAMUser>()
                .ForMember(des => des.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(des => des.Name, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(des => des.UserName, opt => opt.MapFrom(src => src.Username))
                .ForMember(des => des.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(des => des.Enabled, opt => opt.MapFrom(src => src.Enabled))
                ;

    }
}

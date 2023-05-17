using Envisia.Application.Dtos;
using Envisia.Application.Interfaces.Services;
using Envisia.Application.Services;
using Envisia.Data.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Envisia.Core
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMappers(this IServiceCollection services)
        {
            services.AddAutoMapper(config =>
            {
                config.CreateMap<Organisation, OrganisationDto>().ReverseMap()
                    .ForMember(x => x.Id, x => x.Ignore())
                    .ForMember(x => x.ModifiedBy, x => x.Ignore())
                    .ForMember(x => x.ModifiedDateTime, x => x.Ignore())
                    .ForMember(x => x.DeletedBy, x => x.Ignore())
                    .ForMember(x => x.DeletedDateTime, x => x.Ignore());

                config.CreateMap<User, UserDto>().ReverseMap()
                    .ForMember(x => x.Id, x => x.Ignore())
                    .ForMember(x => x.ModifiedBy, x => x.Ignore())
                    .ForMember(x => x.ModifiedDateTime, x => x.Ignore())
                    .ForMember(x => x.DeletedBy, x => x.Ignore())
                    .ForMember(x => x.DeletedDateTime, x => x.Ignore());

                config.CreateMap<UserRole, UserRoleDto>().ReverseMap()
                    .ForMember(x => x.Id, x => x.Ignore())
                    .ForMember(x => x.ModifiedBy, x => x.Ignore())
                    .ForMember(x => x.ModifiedDateTime, x => x.Ignore())
                    .ForMember(x => x.DeletedBy, x => x.Ignore())
                    .ForMember(x => x.DeletedDateTime, x => x.Ignore());

                config.CreateMap<Client, ClientDto>().ReverseMap()
                    .ForMember(x => x.Id, x => x.Ignore())
                    .ForMember(x => x.ModifiedBy, x => x.Ignore())
                    .ForMember(x => x.ModifiedDateTime, x => x.Ignore())
                    .ForMember(x => x.DeletedBy, x => x.Ignore())
                    .ForMember(x => x.DeletedDateTime, x => x.Ignore());

                config.CreateMap<RefreshToken, RefreshTokenDto>().ReverseMap()
                    .ForMember(x => x.Id, x => x.Ignore());

                config.CreateMap<Store, StoreDto>().ReverseMap()
                    .ForMember(x => x.Id, x => x.Ignore())
                    .ForMember(x => x.ModifiedBy, x => x.Ignore())
                    .ForMember(x => x.ModifiedDateTime, x => x.Ignore())
                    .ForMember(x => x.DeletedBy, x => x.Ignore())
                    .ForMember(x => x.DeletedDateTime, x => x.Ignore());

                config.CreateMap<Formula, FormulaDto>().ReverseMap()
                    .ForMember(x => x.Id, x => x.Ignore())
                    .ForMember(x => x.ModifiedBy, x => x.Ignore())
                    .ForMember(x => x.ModifiedDateTime, x => x.Ignore())
                    .ForMember(x => x.DeletedBy, x => x.Ignore())
                    .ForMember(x => x.DeletedDateTime, x => x.Ignore());

                config.CreateMap<News, NewsDto>().ReverseMap()
                    .ForMember(x => x.Id, x => x.Ignore())
                    .ForMember(x => x.ModifiedBy, x => x.Ignore())
                    .ForMember(x => x.ModifiedDateTime, x => x.Ignore())
                    .ForMember(x => x.DeletedBy, x => x.Ignore())
                    .ForMember(x => x.DeletedDateTime, x => x.Ignore());

                config.CreateMap<Feed, FeedDto>().ReverseMap()
                    .ForMember(x => x.Id, x => x.Ignore());

                config.CreateMap<Logo, LogoDto>().ReverseMap()
                    .ForMember(x => x.Id, x => x.Ignore())
                    .ForMember(x => x.ModifiedBy, x => x.Ignore())
                    .ForMember(x => x.ModifiedDateTime, x => x.Ignore())
                    .ForMember(x => x.DeletedBy, x => x.Ignore())
                    .ForMember(x => x.DeletedDateTime, x => x.Ignore());
            });

            return services;
        }

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMappers();
            services.AddScoped<IOrganisationService, OrganisationService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IFormulaService, FormulaService>();
            services.AddScoped<IStoreService, StoreService>();
            services.AddScoped<IFeedNewsService, FeedNewsService>();

            return services;
        }
    }
}

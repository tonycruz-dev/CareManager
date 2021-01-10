using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Extensions
{
    public static class ScopedServicesExtensions
    {
        public static IServiceCollection AddScopedServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAgencyServices, AgentService>();
            services.AddScoped<IClientLocation, ClientLocationRepository>();
            services.AddScoped<IJobRequestRepository, JobRequestRepository>();
            services.AddScoped<IInvitedCandidateRepository, InvitedCandidateRepository>();
            services.AddScoped<IRepositoryHelper, RepositoryHelper>();
            services.AddScoped<IJobConfirmedRepository, JobConfirmedRepository>();
            services.AddScoped<ICandidatePhotoRepository, CandidatePhotoRepository>();
            services.AddScoped<ICandidateDocumentRepository, CandidateDocumentRepository>();
            services.AddScoped<IAgencyPhotoRepository, AgencyPhotoRepository>();
            services.AddScoped<IAgencyDocumentRepository, AgencyDocumentRepository>();
            services.AddScoped<ICandidateManageRepository, CandidateManageRepository>();
            services.AddScoped<IAgencyRepository, AgencyRepository>();
            services.AddScoped<IHRManagerRepository, HRManagerRepository>();

            //

            return services;
        }
    }
}

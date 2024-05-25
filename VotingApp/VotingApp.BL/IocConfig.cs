using Microsoft.Extensions.DependencyInjection;
using VotingApp.BL.Interfaces;
using VotingApp.BL.Manager;

namespace VotingApp.BL
{
    /// <summary>
    /// Configures dependency injection for the Business Logic (BL) layer.
    /// </summary>
    public static class IocConfig
    {
        /// <summary>
        /// Registers services required by the BL layer with the dependency injection container.
        /// </summary>
        /// <param name="services">The IServiceCollection instance used for registration.</param>
        public static void ConfigureServices(ref IServiceCollection services)
        {
            // Register BL services with transient lifetime (created per request)
            services.AddTransient<ICandidateServiceManager, CandidateServiceManager>();
            services.AddTransient<IVoterServiceManager, VoterServiceManager>();
            services.AddTransient<IVoteServiceManager, VoteServiceManager>();

            // Configure services from the Data Layer (DL)
            DL.IocConfig.ConfigureServices(ref services);
        }
    }
}
using Microsoft.Extensions.DependencyInjection;
using VotingApp.DL.Interfaces;
using VotingApp.DL.Repository;

namespace VotingApp.DL
{
    /// <summary>
    /// Configures dependency injection for the Data Layer (DL) services.
    /// </summary>
    public static class IocConfig
    {
        /// <summary>
        /// Registers repositories required by the DL with the dependency injection container.
        /// </summary>
        /// <param name="services">The IServiceCollection instance used for registration.</param>
        public static void ConfigureServices(ref IServiceCollection services)
        {
            // Register data layer repositories with transient lifetime (created per request)
            services.AddTransient<ICandidateRepository, CandidateRepository>();
            services.AddTransient<IVoteRepository, VoteRepository>();
            services.AddTransient<IVoterRepository, VoterRepository>();
        }
    }
}
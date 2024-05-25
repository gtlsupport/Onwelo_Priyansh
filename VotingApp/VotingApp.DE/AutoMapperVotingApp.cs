using AutoMapper;
using VotingApp.DE.BusinessModels;
using VotingApp.DE.DataModels;

namespace VotingApp.DE
{
    /// <summary>
    /// An AutoMapper profile specifically for mapping between VotingApp data models and business models (DTOs).
    /// </summary>
    public class AutoMapperVotingApp : Profile
    {
        public AutoMapperVotingApp()
        {
            /// Mapping between Candidate and CandidateDto

            // Create a map from Candidate to CandidateDto
            CreateMap<Candidate, CandidateDto>()
                .ReverseMap(); // Automatically create a reverse map for DTO to Vote

            /// Mapping between Voter and VoterDto

            // Create a map from Voter to VoterDto
            CreateMap<Voter, VoterDto>()
                .ReverseMap(); // Automatically create a reverse map for DTO to Vote

            /// Mapping between Vote and VoteDto

            // Create a map from Vote to VoteDto
            CreateMap<Vote, VoteDto>()
                .ReverseMap(); // Automatically create a reverse map for DTO to Vote
        }
    }
}
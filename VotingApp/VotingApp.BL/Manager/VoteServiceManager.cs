using AutoMapper;
using VotingApp.BL.Interfaces;
using VotingApp.DE.BusinessModels;
using VotingApp.DE.DataModels;
using VotingApp.DL.Interfaces;

namespace VotingApp.BL.Manager
{
    /// <summary>
    /// Manages operations related to votes.
    /// </summary>
    public class VoteServiceManager : IVoteServiceManager
    {
        #region Variables

        private readonly IVoteRepository _voteRepository;
        private readonly IVoterRepository _voterRepository;
        private readonly ICandidateRepository _candidateRepository;
        private readonly IMapper _mapper;

        #endregion Variables

        #region Constructor

        /// <summary>
        /// Constructor for VoteServiceManager.
        /// </summary>
        /// <param name="voteRepository">The injected instance of IVoteRepository.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="voterRepository">The injected instance of IVoterRepository.</param>
        /// <param name="candidateRepository">The injected instance of ICandidateRepository.</param>
        public VoteServiceManager(IVoteRepository voteRepository, IMapper mapper, IVoterRepository voterRepository, ICandidateRepository candidateRepository)
        {
            _voteRepository = voteRepository ?? throw new ArgumentNullException(nameof(voteRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _voterRepository = voterRepository ?? throw new ArgumentNullException(nameof(voterRepository));
            _candidateRepository = candidateRepository ?? throw new ArgumentNullException(nameof(candidateRepository));
        }

        #endregion Constructor

        /// <summary>
        /// Records a cast vote.
        /// </summary>
        /// <param name="vote">The VoteDto object representing the cast vote.</param>
        /// <exception cref="ArgumentNullException">Thrown if the vote object or required IDs (Voter and Candidate) are null.</exception>
        /// <exception cref="ArgumentException">Thrown if the voter or candidate is not found based on provided IDs.</exception>
        /// <exception cref="InvalidOperationException">Thrown if the voter has already cast a vote.</exception>
        public void CastVote(VoteDto vote)
        {
            #region Validate Vote Data

            if (vote?.VoterId == null || vote.CandidateId == null)
            {
                throw new ArgumentNullException(nameof(vote), "Vote object or required IDs (Voter and Candidate) cannot be null");
            }

            #endregion Validate Vote Data

            // Validate voter existence
            var existingVoter = _voterRepository.GetById(vote.VoterId.Value);
            if (existingVoter == null)
            {
                throw new ArgumentException($"Voter not found. VoterId: {vote.VoterId}");
            }

            // Validate candidate existence
            var existingCandidate = _candidateRepository.GetById(vote.CandidateId.Value);
            if (existingCandidate == null)
            {
                throw new ArgumentException($"Candidate not found. CandidateId: {vote.CandidateId}");
            }

            // Check for duplicate vote from the same voter
            if (_voteRepository.GetVoteByVoterId(vote.VoterId.Value))
            {
                throw new InvalidOperationException("Voter has already cast a vote.");
            }

            #region Record Vote

            var voteDetail = _mapper.Map<Vote>(vote);
            _voteRepository.Add(voteDetail);

            #endregion Record Vote
        }
    }
}
using AutoMapper;
using VotingApp.BL.Interfaces;
using VotingApp.DE.BusinessModels;
using VotingApp.DE.DataModels;
using VotingApp.DL.Interfaces;

namespace VotingApp.BL.Manager
{
    /// <summary>
    /// Manages operations related to candidates.
    /// </summary>
    public class CandidateServiceManager : ICandidateServiceManager
    {
        #region Variables

        private readonly ICandidateRepository _candidateRepository;
        private readonly IMapper _mapper;

        #endregion Variables

        #region Constructor

        /// <summary>
        /// Constructor for CandidateServiceManager.
        /// </summary>
        /// <param name="candidateRepository">The injected instance of ICandidateRepository.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        public CandidateServiceManager(ICandidateRepository candidateRepository, IMapper mapper)
        {
            _candidateRepository = candidateRepository ?? throw new ArgumentNullException(nameof(candidateRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion Constructor

        /// <summary>
        /// Retrieves all candidates with optional pagination.
        /// </summary>
        /// <param name="pageNumber">The page number to retrieve (optional).</param>
        /// <param name="pageSize">The number of candidates per page (optional).</param>
        /// <returns>An IEnumerable of CandidateDto objects representing the retrieved candidates with vote counts.</returns>
        public IEnumerable<CandidateDto> GetAllCandidates(int? pageNumber, int? pageSize)
        {
            // Get all candidates from the repository
            var candidates = _candidateRepository.GetAll();

            // Map candidates to DTOs using AutoMapper
            var candidateDtos = _mapper.Map<IEnumerable<CandidateDto>>(candidates);

            #region Add Vote Count to Candidate DTOs

            // Loop through each DTO and retrieve the vote count from the repository
            foreach (var candidateDto in candidateDtos)
            {
                candidateDto.VotesCount = _candidateRepository.GetVoteCount(candidateDto.Id);
            }

            #endregion Add Vote Count to Candidate DTOs

            // Apply pagination if necessary
            var skipResult = (pageNumber - 1) * pageSize;
            candidateDtos = candidateDtos.Skip(skipResult ?? 0).Take(pageSize ?? 100);

            return candidateDtos;
        }

        /// <summary>
        /// Adds a new candidate.
        /// </summary>
        /// <param name="candidate">The CandidateDto object representing the candidate to be added.</param>
        public void AddCandidate(CandidateDto candidate)
        {
            if (candidate == null)
            {
                throw new ArgumentNullException(nameof(candidate), "Candidate object cannot be null");
            }

            // Map the DTO to a Candidate data model for repository interaction
            var candidateDetail = _mapper.Map<Candidate>(candidate);
            _candidateRepository.Add(candidateDetail);
        }

        /// <summary>
        /// Gets the total number of candidates.
        /// </summary>
        /// <returns>The count of all candidates.</returns>
        public int GetCandidatesCount()
        {
            return _candidateRepository.GetCandidatesCount();
        }
    }
}
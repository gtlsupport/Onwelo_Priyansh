using AutoMapper;
using VotingApp.BL.Interfaces;
using VotingApp.DE.BusinessModels;
using VotingApp.DE.DataModels;
using VotingApp.DL.Interfaces;

namespace VotingApp.BL.Manager
{
    /// <summary>
    /// Manages operations related to voters.
    /// </summary>
    public class VoterServiceManager : IVoterServiceManager
    {
        #region Variables

        private readonly IVoterRepository _voterRepository;
        private readonly IMapper _mapper;

        #endregion Variables

        #region Constructor

        /// <summary>
        /// Constructor for VoterServiceManager.
        /// </summary>
        /// <param name="voterRepository">The injected instance of IVoterRepository.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        public VoterServiceManager(IVoterRepository voterRepository, IMapper mapper)
        {
            _voterRepository = voterRepository ?? throw new ArgumentNullException(nameof(voterRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion Constructor

        /// <summary>
        /// Retrieves all voters with optional pagination.
        /// </summary>
        /// <param name="pageNumber">The page number to retrieve (optional).</param>
        /// <param name="pageSize">The number of voters per page (optional).</param>
        /// <returns>An IEnumerable of VoterDto objects representing the retrieved voters.</returns>
        public IEnumerable<VoterDto> GetAllVoters(int? pageNumber, int? pageSize)
        {
            // Get all voters from the repository
            var voters = _voterRepository.GetAll();

            // Map voters to DTOs using AutoMapper
            var voterDtos = _mapper.Map<IEnumerable<VoterDto>>(voters);

            // Apply pagination if necessary
            var skipResult = (pageNumber - 1) * pageSize;
            voterDtos = voterDtos.Skip(skipResult ?? 0).Take(pageSize ?? 100);

            return voterDtos;
        }

        /// <summary>
        /// Adds a new voter.
        /// </summary>
        /// <param name="voter">The VoterDto object representing the voter to be added.</param>
        /// <exception cref="ArgumentNullException">Thrown if the voter object is null.</exception>
        public void AddVoter(VoterDto voter)
        {
            if (voter == null)
            {
                throw new ArgumentNullException(nameof(voter), "Voter object cannot be null");
            }

            // Map the DTO to a Voter data model for repository interaction
            var voterDetail = _mapper.Map<Voter>(voter);

            // Set the initial voting status to 'not voted'
            voterDetail.HasVoted = false;

            _voterRepository.Add(voterDetail);
        }

        /// <summary>
        /// Sets the voted flag for a specific voter.
        /// </summary>
        /// <param name="voterId">The ID of the voter who has voted.</param>
        /// <exception cref="ArgumentException">Thrown if the voter is not found based on the provided ID.</exception>
        public void SetVoterHasVoted(int voterId)
        {
            var voter = _voterRepository.GetById(voterId);

            if (voter == null)
            {
                throw new ArgumentException("Voter not found", nameof(voterId));
            }
            voter.HasVoted = true;
            _voterRepository.Update(voter);
        }

        /// <summary>
        /// Retrieves a list of voters who haven't voted yet.
        /// </summary>
        /// <returns>An IEnumerable of VoterDto objects representing non-voted voters.</returns>
        public IEnumerable<VoterDto> GetNonVotedVoters()
        {
            // Get all voters and filter for those who haven't voted
            var nonVotedVoters = _voterRepository.GetAll().Where(v => v.HasVoted != true);

            // Map non-voted voters to DTOs
            return _mapper.Map<IEnumerable<VoterDto>>(nonVotedVoters);
        }

        /// <summary>
        /// Gets the total number of voters.
        /// </summary>
        /// <returns>The count of all voters.</returns>
        public int GetVotersCount()
        {
            return _voterRepository.GetVotersCount();
        }
    }
}
using VotingApp.DE;
using VotingApp.DE.DataModels;
using VotingApp.DL.Interfaces;

namespace VotingApp.DL.Repository
{
    /// <summary>
    /// Concrete implementation of ICandidateRepository for interacting with candidate data in the database.
    /// </summary>
    public class CandidateRepository : ICandidateRepository
    {
        #region Variables

        private readonly VotingAppDbContext _context;

        #endregion Variables

        #region Constructor

        /// <summary>
        /// Constructor for CandidateRepository.
        /// </summary>
        /// <param name="context">An instance of VotingAppDbContext for database access.</param>
        public CandidateRepository(VotingAppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        #endregion Constructor

        /// <summary>
        /// Retrieves all candidates from the database, ordered by ID descending.
        /// </summary>
        /// <returns>An IEnumerable of Candidate objects representing all retrieved candidates.</returns>
        public IEnumerable<Candidate> GetAll()
        {
            // Get all candidates and order by ID descending
            return _context.Candidates.OrderByDescending(x => x.Id).ToList();
        }

        /// <summary>
        /// Adds a new candidate to the database.
        /// </summary>
        /// <param name="candidate">The Candidate object representing the candidate to be added.</param>
        public void Add(Candidate candidate)
        {
            _context.Candidates.Add(candidate); // Add the candidate to the DbSet for Candidates
            _context.SaveChanges();// Save changes to the database
        }

        /// <summary>
        /// Gets the total number of votes cast for a specific candidate.
        /// </summary>
        /// <param name="candidateId">The ID of the candidate.</param>
        /// <returns>The integer count of votes for the specified candidate.</returns>
        public int GetVoteCount(int candidateId)
        {
            // Count votes where the CandidateId matches the provided ID
            return _context.Votes.Count(v => v.CandidateId == candidateId);
        }

        /// <summary>
        /// Retrieves a candidate from the database based on its ID.
        /// </summary>
        /// <param name="id">The ID of the candidate to retrieve.</param>
        /// <returns>A Candidate object representing the retrieved candidate, or null if not found.</returns>
        public Candidate? GetById(int id)
        {
            // Find the first candidate with matching ID
            return _context.Candidates.FirstOrDefault(v => v.Id == id);
        }

        /// <summary>
        /// Gets the total number of candidates in the database.
        /// </summary>
        /// <returns>The integer count of all candidates.</returns>
        public int GetCandidatesCount()
        {
            // Get the count of all candidates
            return _context.Candidates.Count();
        }
    }
}
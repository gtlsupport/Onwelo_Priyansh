using VotingApp.DE;
using VotingApp.DE.DataModels;
using VotingApp.DL.Interfaces;

namespace VotingApp.DL.Repository
{
    /// <summary>
    /// Concrete implementation of IVoterRepository for interacting with voter data in the database.
    /// </summary>
    public class VoterRepository : IVoterRepository
    {
        #region Variables

        private readonly VotingAppDbContext _context;

        #endregion Variables

        #region Constructor

        /// <summary>
        /// Constructor for VoterRepository.
        /// </summary>
        /// <param name="context">An instance of VotingAppDbContext for database access.</param>
        public VoterRepository(VotingAppDbContext context)
        {
            _context = context;
        }

        #endregion Constructor

        /// <summary>
        /// Retrieves all voters from the database, ordered by ID descending.
        /// </summary>
        /// <returns>An IEnumerable of Voter objects representing all retrieved voters.</returns>
        public IEnumerable<Voter> GetAll()
        {
            return _context.Voters.OrderByDescending(x => x.Id).ToList(); // Get all voters and order by ID descending
        }

        /// <summary>
        /// Retrieves a voter from the database based on its ID.
        /// </summary>
        /// <param name="id">The ID of the voter to retrieve.</param>
        /// <returns>A Voter object representing the retrieved voter, or null if not found.</returns>
        public Voter? GetById(int id)
        {
            return _context.Voters.FirstOrDefault(v => v.Id == id); // Find the first voter with matching ID
        }

        /// <summary>
        /// Adds a new voter to the database.
        /// </summary>
        /// <param name="voter">The Voter object representing the voter to be added.</param>
        public void Add(Voter voter)
        {
            _context.Voters.Add(voter); // Add the voter to the DbSet for Voters
            _context.SaveChanges();// Save changes to the database
        }

        /// <summary>
        /// Updates an existing voter record in the database.
        /// </summary>
        /// <param name="voter">The Voter object with updated information.</param>
        public void Update(Voter voter)
        {
            _context.Voters.Update(voter); // Mark the voter entity as modified for update tracking
            _context.SaveChanges(); // Save changes to the database
        }

        /// <summary>
        /// Gets the total number of voters in the database.
        /// </summary>
        /// <returns>The integer count of all voters.</returns>
        public int GetVotersCount() // Get the count of all voters
        {
            return _context.Voters.Count();
        }
    }
}
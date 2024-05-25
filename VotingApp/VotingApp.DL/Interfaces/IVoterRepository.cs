using VotingApp.DE.DataModels;

namespace VotingApp.DL.Interfaces
{
    /// <summary>
    /// Interface defining methods for interacting with voter data storage.
    /// </summary>
    public interface IVoterRepository
    {
        /// <summary>
        /// Retrieves all voters.
        /// </summary>
        /// <returns>An IEnumerable of Voter objects representing all voters.</returns>
        IEnumerable<Voter> GetAll();

        /// <summary>
        /// Retrieves a voter by its ID.
        /// </summary>
        /// <param name="id">The ID of the voter to retrieve.</param>
        /// <returns>A Voter object representing the retrieved voter, or null if not found.</returns>
        Voter? GetById(int id);

        /// <summary>
        /// Adds a new voter.
        /// </summary>
        /// <param name="voter">The Voter object representing the voter to be added.</param>
        void Add(Voter voter);

        /// <summary>
        /// Updates an existing voter record.
        /// </summary>
        /// <param name="voter">The Voter object with updated information.</param>
        void Update(Voter voter);

        /// <summary>
        /// Gets the total number of voters.
        /// </summary>
        /// <returns>The integer count of all voters.</returns>
        int GetVotersCount();
    }
}
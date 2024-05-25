using VotingApp.DE.DataModels;

namespace VotingApp.DL.Interfaces
{
    /// <summary>
    /// Interface defining methods for interacting with vote data storage.
    /// </summary>
    public interface IVoteRepository
    {
        /// <summary>
        /// Retrieves all recorded votes.
        /// </summary>
        /// <returns>An IEnumerable of Vote objects representing all recorded votes.</returns>
        IEnumerable<Vote> GetAll();

        /// <summary>
        /// Adds a new vote record.
        /// </summary>
        /// <param name="vote">The Vote object representing the vote to be added.</param>
        void Add(Vote vote);

        /// <summary>
        /// Checks if a vote record exists for a specific voter.
        /// </summary>
        /// <param name="voterId">The ID of the voter to check.</param>
        /// <returns>True if a vote record exists for the provided voter ID, False otherwise.</returns>
        bool GetVoteByVoterId(int voterId);
    }
}
using VotingApp.DE.DataModels;

namespace VotingApp.DL.Interfaces
{
    /// <summary>
    /// Interface defining methods for interacting with candidate data storage.
    /// </summary>
    public interface ICandidateRepository
    {
        /// <summary>
        /// Retrieves all candidates.
        /// </summary>
        /// <returns>An IEnumerable of Candidate objects representing all candidates.</returns>
        IEnumerable<Candidate> GetAll();

        /// <summary>
        /// Adds a new candidate.
        /// </summary>
        /// <param name="candidate">The Candidate object representing the candidate to be added.</param>
        void Add(Candidate candidate);

        /// <summary>
        /// Gets the total number of votes cast for a specific candidate.
        /// </summary>
        /// <param name="candidateId">The ID of the candidate.</param>
        /// <returns>The integer count of votes for the specified candidate.</returns>
        int GetVoteCount(int candidateId);

        /// <summary>
        /// Retrieves a candidate by its ID.
        /// </summary>
        /// <param name="id">The ID of the candidate to retrieve.</param>
        /// <returns>A Candidate object representing the retrieved candidate, or null if not found.</returns>
        Candidate? GetById(int id);

        /// <summary>
        /// Gets the total number of candidates.
        /// </summary>
        /// <returns>The integer count of all candidates.</returns>
        int GetCandidatesCount();
    }
}
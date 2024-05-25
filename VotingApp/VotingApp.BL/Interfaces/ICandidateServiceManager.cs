using VotingApp.DE.BusinessModels;

namespace VotingApp.BL.Interfaces
{
    /// <summary>
    /// Interface defining methods for managing candidate data.
    /// </summary>
    public interface ICandidateServiceManager
    {
        /// <summary>
        /// Retrieves all candidates with optional pagination.
        /// </summary>
        /// <param name="pageNumber">The page number to retrieve (optional).</param>
        /// <param name="pageSize">The number of candidates per page (optional).</param>
        /// <returns>An IEnumerable of CandidateDto objects representing the retrieved candidates.</returns>
        IEnumerable<CandidateDto> GetAllCandidates(int? pageNumber, int? pageSize);

        /// <summary>
        /// Adds a new candidate.
        /// </summary>
        /// <param name="candidate">The CandidateDto object representing the candidate to be added.</param>
        void AddCandidate(CandidateDto candidate);

        /// <summary>
        /// Gets the total number of candidates.
        /// </summary>
        /// <returns>The count of all candidates.</returns>
        int GetCandidatesCount();
    }
}
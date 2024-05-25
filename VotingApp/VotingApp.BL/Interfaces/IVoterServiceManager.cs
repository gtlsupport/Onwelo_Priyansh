using VotingApp.DE.BusinessModels;

namespace VotingApp.BL.Interfaces
{
    /// <summary>
    /// Interface defining methods for managing voter data.
    /// </summary>
    public interface IVoterServiceManager
    {
        /// <summary>
        /// Retrieves all voters with optional pagination.
        /// </summary>
        /// <param name="pageNumber">The page number to retrieve (optional).</param>
        /// <param name="pageSize">The number of voters per page (optional).</param>
        /// <returns>An IEnumerable of VoterDto objects representing the retrieved voters.</returns>
        IEnumerable<VoterDto> GetAllVoters(int? pageNumber, int? pageSize);

        /// <summary>
        /// Adds a new voter.
        /// </summary>
        /// <param name="voter">The VoterDto object representing the voter to be added.</param>
        void AddVoter(VoterDto voter);

        /// <summary>
        /// Sets the voted flag for a specific voter.
        /// </summary>
        /// <param name="voterId">The ID of the voter who has voted.</param>
        void SetVoterHasVoted(int voterId);

        /// <summary>
        /// Retrieves a list of voters who haven't voted yet.
        /// </summary>
        /// <returns>An IEnumerable of VoterDto objects representing non-voted voters.</returns>
        IEnumerable<VoterDto> GetNonVotedVoters();

        /// <summary>
        /// Gets the total number of voters.
        /// </summary>
        /// <returns>The count of all voters.</returns>
        int GetVotersCount();
    }
}
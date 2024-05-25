using VotingApp.DE.BusinessModels;

namespace VotingApp.BL.Interfaces
{
    /// <summary>
    /// Interface defining methods for managing votes.
    /// </summary>
    public interface IVoteServiceManager
    {
        /// <summary>
        /// Records a cast vote.
        /// </summary>
        /// <param name="vote">The VoteDto object representing the cast vote.</param>
        void CastVote(VoteDto vote);
    }
}
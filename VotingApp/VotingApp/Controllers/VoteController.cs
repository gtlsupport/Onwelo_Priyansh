using Microsoft.AspNetCore.Mvc;
using VotingApp.BL.Interfaces;
using VotingApp.DE.BusinessModels;

namespace VotingApp.Controllers
{
    /// <summary>
    /// Controller for managing votes.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class VoteController : ControllerBase
    {
        #region Variables

        private readonly IVoteServiceManager _voteServiceManager;
        private readonly IVoterServiceManager _voterServiceManager;

        #endregion Variables

        #region Constructor

        /// <summary>
        /// Constructor for VoteController.
        /// </summary>
        /// <param name="voteServiceManager">The injected instance of IVoteServiceManager.</param>
        /// <param name="voterServiceManager">The injected instance of IVoterServiceManager.</param>
        public VoteController(IVoteServiceManager voteServiceManager, IVoterServiceManager voterServiceManager)
        {
            _voteServiceManager = voteServiceManager ?? throw new ArgumentNullException(nameof(voteServiceManager));
            _voterServiceManager = voterServiceManager ?? throw new ArgumentNullException(nameof(voterServiceManager));
        }

        #endregion Constructor

        #region Actions

        /// <summary>
        /// Casts a vote.
        /// </summary>
        /// <param name="voteDto">The vote data to be casted.</param>
        /// <returns>Returns an empty response if successful, otherwise a BadRequest with an error message.</returns>
        [HttpPost]
        public IActionResult CastVote([FromBody] VoteDto voteDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (voteDto is null)
            {
                return BadRequest("Vote input object is null");
            }

            if (voteDto.Id != 0)
            {
                return BadRequest("Vote ID should not be passed while casting a new vote");
            }

            if (voteDto.VoterId == null)
            {
                return BadRequest("Voter ID should be passed while casting a new vote");
            }

            try
            {
                _voteServiceManager.CastVote(voteDto);
                _voterServiceManager.SetVoterHasVoted(voteDto.VoterId.Value);
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        #endregion Actions
    }
}
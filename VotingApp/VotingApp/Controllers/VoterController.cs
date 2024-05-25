using Microsoft.AspNetCore.Mvc;
using VotingApp.BL.Interfaces;
using VotingApp.DE.BusinessModels;

namespace VotingApp.Controllers
{
    /// <summary>
    /// Controller for managing voters.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class VoterController : ControllerBase
    {
        #region Variables

        private readonly IVoterServiceManager _voterServiceManager;

        #endregion Variables

        #region Constructor

        /// <summary>
        /// Constructor for VoterController.
        /// </summary>
        /// <param name="voterServiceManager">The injected instance of IVoterServiceManager.</param>
        public VoterController(IVoterServiceManager voterServiceManager)
        {
            _voterServiceManager = voterServiceManager ?? throw new ArgumentNullException(nameof(voterServiceManager));
        }

        #endregion Constructor

        #region Actions

        /// <summary>
        /// Retrieves all voters with pagination support.
        /// </summary>
        /// <param name="pageNumber">The page number to retrieve (optional).</param>
        /// <param name="pageSize">The number of candidates per page (optional).</param>
        /// <returns>A paginated list of voters.</returns>
        [HttpGet]
        public IActionResult GetVoters([FromQuery] int? pageNumber, [FromQuery] int? pageSize)
        {
            var voters = _voterServiceManager.GetAllVoters(pageNumber, pageSize);
            return Ok(voters);
        }

        /// <summary>
        /// Adds a new voter.
        /// </summary>
        /// <param name="voterDto">The voter data to be added.</param>
        /// <returns>Created status code (201) on success, otherwise BadRequest with an error message.</returns>
        [HttpPost]
        public IActionResult AddVoter([FromBody] VoterDto voterDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (voterDto is null)
            {
                return BadRequest("Voter input object is null");
            }

            if (voterDto.Id != 0)
            {
                return BadRequest("Voter ID should not be passed while creating a new voter");
            }

            try
            {
                _voterServiceManager.AddVoter(voterDto);
                return StatusCode(201, new { Message = "Voter created successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a list of non-voted voters.
        /// </summary>
        /// <returns>Returns a list of non-voted voters.</returns>
        [HttpGet("nonvoted")]
        public IActionResult GetNonVotedVoters()
        {
            var nonVotedVoters = _voterServiceManager.GetNonVotedVoters();
            return Ok(nonVotedVoters);
        }

        /// <summary>
        /// Gets the total number of candidates.
        /// </summary>
        /// <returns>The count of all candidates.</returns>
        [HttpGet]
        [Route("count")]
        public IActionResult GetVotersCount()
        {
            var count = _voterServiceManager.GetVotersCount();
            return Ok(count);
        }

        #endregion Actions
    }
}
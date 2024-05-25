using Microsoft.AspNetCore.Mvc;
using VotingApp.BL.Interfaces;
using VotingApp.DE.BusinessModels;

namespace VotingApp.Controllers
{
    /// <summary>
    /// Controller for managing candidates.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        #region Variables

        private readonly ICandidateServiceManager _candidateServiceManager;

        #endregion Variables

        #region Constructor

        /// <summary>
        /// Constructor for CandidateController.
        /// </summary>
        /// <param name="candidateServiceManager">The injected instance of ICandidateServiceManager.</param>
        public CandidateController(ICandidateServiceManager candidateServiceManager)
        {
            _candidateServiceManager = candidateServiceManager ?? throw new ArgumentNullException(nameof(candidateServiceManager));
        }

        #endregion Constructor

        #region Actions

        /// <summary>
        /// Retrieves all candidates with pagination support.
        /// </summary>
        /// <param name="pageNumber">The page number to retrieve (optional).</param>
        /// <param name="pageSize">The number of candidates per page (optional).</param>
        /// <returns>A paginated list of candidates.</returns>
        [HttpGet]
        public IActionResult GetCandidates([FromQuery] int? pageNumber, [FromQuery] int? pageSize)
        {
            var candidates = _candidateServiceManager.GetAllCandidates(pageNumber, pageSize);
            return Ok(candidates);
        }

        /// <summary>
        /// Adds a new candidate.
        /// </summary>
        /// <param name="candidateDto">The candidate data to be added.</param>
        /// <returns>Created status code (201) on success, otherwise BadRequest with an error message.</returns>
        [HttpPost]
        public IActionResult AddCandidate([FromBody] CandidateDto candidateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (candidateDto is null)
            {
                return BadRequest("Candidate input object is null");
            }

            if (candidateDto.Id != 0)
            {
                return BadRequest("Candidate ID should not be passed while creating a new candidate");
            }

            try
            {
                _candidateServiceManager.AddCandidate(candidateDto);
                return StatusCode(201, new { Message = "Candidate created successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Gets the total number of candidates.
        /// </summary>
        /// <returns>The count of all candidates.</returns>
        [HttpGet]
        [Route("count")]
        public IActionResult GetCandidatesCount()
        {
            var count = _candidateServiceManager.GetCandidatesCount();

            return Ok(count);
        }

        #endregion Actions
    }
}
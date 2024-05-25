using System.ComponentModel.DataAnnotations;

namespace VotingApp.DE.BusinessModels
{
    /// <summary>
    /// Data Transfer Object (DTO) representing a vote cast by a voter for a candidate.
    /// Used for transferring vote data between layers without exposing data models directly.
    /// </summary>
    public class VoteDto
    {
        /// <summary>
        /// Unique identifier for the vote record.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The ID of the candidate the voter chose.
        /// Required field.
        /// </summary>
        [Required]
        public int? CandidateId { get; set; }

        /// <summary>
        /// The ID of the voter who cast the vote.
        /// Required field.
        /// </summary>
        [Required]
        public int? VoterId { get; set; }
    }
}
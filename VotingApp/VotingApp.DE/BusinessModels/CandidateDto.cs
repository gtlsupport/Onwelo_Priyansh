using System.ComponentModel.DataAnnotations;

namespace VotingApp.DE.BusinessModels
{
    /// <summary>
    /// Data Transfer Object (DTO) representing a candidate.
    /// Used for transferring candidate data between layers without exposing data models directly.
    /// </summary>
    public class CandidateDto
    {
        /// <summary>
        /// Unique identifier for the candidate.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Candidate's name.
        /// Required field.
        /// </summary>
        [Required]
        public string? Name { get; set; }

        /// <summary>
        /// Total number of votes cast for this candidate.
        /// </summary>
        public int VotesCount { get; set; }
    }
}
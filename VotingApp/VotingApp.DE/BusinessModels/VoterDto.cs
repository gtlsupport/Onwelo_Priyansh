using System.ComponentModel.DataAnnotations;

namespace VotingApp.DE.BusinessModels
{
    /// <summary>
    /// Data Transfer Object (DTO) representing a voter.
    /// Used for transferring voter data between layers without exposing data models directly.
    /// </summary>
    public class VoterDto
    {
        /// <summary>
        /// Unique identifier for the voter.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Voter's name.
        /// Required field.
        /// </summary>
        [Required]
        public string? Name { get; set; }

        /// <summary>
        /// Indicates whether the voter has cast their vote.
        /// </summary>
        public bool? HasVoted { get; set; }
    }
}
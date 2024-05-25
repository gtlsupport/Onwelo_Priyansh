using System.ComponentModel.DataAnnotations;

namespace VotingApp.DE.DataModels
{
    /// <summary>
    /// Represents a voter in the voting system.
    /// This class is used for persisting data in the database.
    /// </summary>
    public class Voter
    {
        /// <summary>
        /// Unique identifier for the voter. (Primary Key)
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Voter's name.
        /// Required field.
        /// </summary>
        [Required]
        public string? Name { get; set; }

        /// <summary>
        /// Indicates whether the voter has cast a vote.
        /// </summary>
        public bool? HasVoted { get; set; }
    }
}
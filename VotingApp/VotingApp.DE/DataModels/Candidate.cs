using System.ComponentModel.DataAnnotations;

namespace VotingApp.DE.DataModels
{
    /// <summary>
    /// Represents a candidate in the voting system.
    /// This class is used for persisting data in the database.
    /// </summary>
    public class Candidate
    {
        /// <summary>
        /// Unique identifier for the candidate. (Primary Key)
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Candidate's name.
        /// Required field.
        /// </summary>
        [Required]
        public string? Name { get; set; }
    }
}
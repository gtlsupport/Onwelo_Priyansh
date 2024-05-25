using System.ComponentModel.DataAnnotations;

namespace VotingApp.DE.DataModels
{
    /// <summary>
    /// Represents a vote cast by a voter for a candidate in the voting system.
    /// This class is used for persisting data in the database with relationships.
    /// </summary>
    public class Vote
    {
        /// <summary>
        /// Unique identifier for the vote record. (Primary Key)
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

        /// <summary>
        /// Navigation property referencing the Candidate object this vote is associated with.
        /// (Foreign Key: CandidateId)
        /// </summary>
        public Candidate? Candidate { get; set; }

        /// <summary>
        /// Navigation property referencing the Voter object who cast this vote.
        /// (Foreign Key: VoterId)
        /// </summary>
        public Voter? Voter { get; set; }
    }
}
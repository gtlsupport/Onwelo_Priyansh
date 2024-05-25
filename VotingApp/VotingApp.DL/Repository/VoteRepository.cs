using VotingApp.DE;
using VotingApp.DE.DataModels;
using VotingApp.DL.Interfaces;

namespace VotingApp.DL.Repository
{
    public class VoteRepository : IVoteRepository
    {
        private readonly VotingAppDbContext _context;

        public VoteRepository(VotingAppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Vote> GetAll()
        {
            return _context.Votes.ToList();
        }

        public void Add(Vote vote)
        {
            _context.Votes.Add(vote);
            _context.SaveChanges();
        }

        public bool GetVoteByVoterId(int voterId)
        {
            return _context.Votes.Any(v => v.VoterId == voterId);
        }
    }
}
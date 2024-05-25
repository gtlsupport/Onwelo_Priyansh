using Microsoft.EntityFrameworkCore;
using VotingApp.DE;
using VotingApp.DE.DataModels;
using VotingApp.DL.Repository;

namespace VotingApp.Tests
{
    public class VoteRepositoryTests
    {
        private readonly VoteRepository _repository;
        private readonly VotingAppDbContext _context;

        public VoteRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<VotingAppDbContext>()
                .UseInMemoryDatabase(databaseName: "VotingApp")
                .Options;

            _context = new VotingAppDbContext(options);
            _repository = new VoteRepository(_context);

            // Ensure database is cleaned up before each test and seed data is added
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
            SeedData(_context);
        }

        private static void SeedData(VotingAppDbContext context)
        {
            var votes = new List<Vote>
            {
                new Vote { Id = 20, Voter = new Voter { Id= 25, Name="Voter 1"}, Candidate = new Candidate { Id= 30, Name="Candidate 1"} },
                new Vote { Id = 21, Voter = new Voter { Id= 26, Name="Voter 2"}, Candidate = new Candidate { Id= 31, Name="Candidate 2"} },
            };

            context.Votes.AddRange(votes);
            context.SaveChanges();
        }

        [Fact]
        public void GetAll_ReturnsAllVotes()
        {
            // Act
            var result = _repository.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }
    }
}
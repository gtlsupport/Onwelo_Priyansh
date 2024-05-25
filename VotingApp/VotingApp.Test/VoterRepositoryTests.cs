using Microsoft.EntityFrameworkCore;
using VotingApp.DE;
using VotingApp.DE.DataModels;
using VotingApp.DL.Repository;

namespace VotingApp.Tests
{
    public class VoterRepositoryTests
    {
        private readonly VoterRepository _repository;
        private readonly VotingAppDbContext _context;

        public VoterRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<VotingAppDbContext>()
                .UseInMemoryDatabase(databaseName: "VotingApp")
                .Options;

            _context = new VotingAppDbContext(options);
            _repository = new VoterRepository(_context);

            // Ensure database is cleaned up before each test and seed data is added
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
            SeedData(_context);
        }

        private static void SeedData(VotingAppDbContext context)
        {
            var voters = new List<Voter>
            {
                new Voter { Id = 1, Name = "Voter 1" },
                new Voter { Id = 2, Name = "Voter 2" }
            };

            context.Voters.AddRange(voters);
            context.SaveChanges();
        }

        [Fact]
        public void GetAll_ReturnsAllVoters()
        {
            // Act
            var result = _repository.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void Add_AddsVoterToDatabase()
        {
            // Arrange
            var voter = new Voter { Name = "New Voter" };

            // Act
            _repository.Add(voter);
            var result = _context.Voters.FirstOrDefault(v => v.Name == "New Voter");

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Update_SetsVoterHasVotedToTrue()
        {
            // Arrange
            var voter = _context.Voters.First();
            voter.HasVoted = true; // Manually set HasVoted to true

            // Act
            _repository.Update(voter);
            var updatedVoter = _context.Voters.First(v => v.Id == voter.Id);

            // Assert
            Assert.True(updatedVoter.HasVoted);
        }

        [Fact]
        public void GetById_ReturnsCorrectVoter()
        {
            // Act
            var result = _repository.GetById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }
    }
}
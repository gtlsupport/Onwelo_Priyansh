using Microsoft.EntityFrameworkCore;
using VotingApp.DE;
using VotingApp.DE.DataModels;
using VotingApp.DL.Repository;

namespace VotingApp.Tests
{
    public class CandidateRepositoryTests
    {
        private readonly CandidateRepository _repository;
        private readonly VotingAppDbContext _context;

        public CandidateRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<VotingAppDbContext>()
                .UseInMemoryDatabase(databaseName: "VotingAppDb")
                .Options;

            _context = new VotingAppDbContext(options);
            _repository = new CandidateRepository(_context);

            // Ensure database is cleaned up before each test
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            // Seed data
            SeedData(_context);
        }

        private static void SeedData(VotingAppDbContext context)
        {
            context.Candidates.AddRange(new List<Candidate>
            {
                new Candidate { Id = 1, Name = "Candidate 1" },
                new Candidate { Id = 2, Name = "Candidate 2" }
            });

            context.Votes.AddRange(new List<Vote>
            {
                new Vote { Id = 1, CandidateId = 1, VoterId = 1 },
                new Vote { Id = 2, CandidateId = 1, VoterId = 2 },
                new Vote { Id = 3, CandidateId = 2, VoterId = 3 }
            });

            context.SaveChanges();
        }

        [Fact]
        public void GetAll_ReturnsAllCandidates()
        {
            // Act
            var result = _repository.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void Add_AddsCandidateToDatabase()
        {
            // Arrange
            var candidate = new Candidate { Name = "New Candidate" };

            // Act
            _repository.Add(candidate);
            var result = _context.Candidates.FirstOrDefault(c => c.Name == "New Candidate");

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void GetVoteCount_ReturnsCorrectVoteCount()
        {
            // Act
            var result = _repository.GetVoteCount(1);

            // Assert
            Assert.Equal(2, result);
        }

        [Fact]
        public void GetById_ReturnsCorrectCandidate()
        {
            // Act
            var result = _repository.GetById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }
    }
}
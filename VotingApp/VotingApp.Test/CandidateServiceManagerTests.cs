using AutoMapper;
using Moq;
using VotingApp.BL.Manager;
using VotingApp.DE.BusinessModels;
using VotingApp.DE.DataModels;
using VotingApp.DL.Interfaces;

namespace VotingApp.Tests
{
    public class CandidateServiceManagerTests
    {
        [Fact]
        public void GetAllCandidates_Returns_Correct_Candidates_With_VotesCount()
        {
            // Arrange
            var mockRepository = new Mock<ICandidateRepository>();
            var candidates = new List<Candidate>
            {
                new Candidate { Id = 1, Name = "Candidate 1" },
                new Candidate { Id = 2, Name = "Candidate 2" }
            };
            var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<Candidate, CandidateDto>());
            var mapper = new Mapper(mapperConfig);
            mockRepository.Setup(repo => repo.GetAll()).Returns(candidates);
            mockRepository.Setup(repo => repo.GetVoteCount(It.IsAny<int>())).Returns(0);
            var serviceManager = new CandidateServiceManager(mockRepository.Object, mapper);

            // Act
            var result = serviceManager.GetAllCandidates(null, null);

            // Assert
            Assert.NotNull(result);
            Assert.Collection(result,
                item =>
                {
                    Assert.Equal(1, item.Id);
                    Assert.Equal("Candidate 1", item.Name);
                    Assert.Equal(0, item.VotesCount);
                },
                item =>
                {
                    Assert.Equal(2, item.Id);
                    Assert.Equal("Candidate 2", item.Name);
                    Assert.Equal(0, item.VotesCount);
                });
        }

        [Fact]
        public void AddCandidate_Throws_Exception_When_Candidate_Null()
        {
            // Arrange
            var mockRepository = new Mock<ICandidateRepository>();
            var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<CandidateDto, Candidate>());
            var mapper = new Mapper(mapperConfig);
            var serviceManager = new CandidateServiceManager(mockRepository.Object, mapper);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => serviceManager.AddCandidate(null!));
        }

        [Fact]
        public void GetCandidatesCount_Returns_Correct_Count()
        {
            // Arrange
            var mockRepository = new Mock<ICandidateRepository>();
            mockRepository.Setup(repo => repo.GetCandidatesCount()).Returns(3);
            var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<Candidate, CandidateDto>());
            var mapper = new Mapper(mapperConfig);
            var serviceManager = new CandidateServiceManager(mockRepository.Object, mapper);

            // Act
            var result = serviceManager.GetCandidatesCount();

            // Assert
            Assert.Equal(3, result);
        }
    }
}
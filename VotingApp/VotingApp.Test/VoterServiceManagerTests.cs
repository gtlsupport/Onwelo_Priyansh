using AutoMapper;
using Moq;
using VotingApp.BL.Manager;
using VotingApp.DE;
using VotingApp.DE.BusinessModels;
using VotingApp.DE.DataModels;
using VotingApp.DL.Interfaces;

namespace VotingApp.Tests
{
    public class VoterServiceManagerTests
    {
        private readonly VoterServiceManager _service;
        private readonly Mock<IVoterRepository> _mockRepository;
        private readonly IMapper _mapper;

        public VoterServiceManagerTests()
        {
            _mockRepository = new Mock<IVoterRepository>();

            var config = new MapperConfiguration(cfg => cfg.AddProfile(new AutoMapperVotingApp()));
            _mapper = config.CreateMapper();

            _service = new VoterServiceManager(_mockRepository.Object, _mapper);
        }

        [Fact]
        public void GetAllVoters_ReturnsAllVoters()
        {
            // Arrange
            var mockVoters = new List<Voter>
            {
                new Voter { Id = 1, Name = "Voter 1" },
                new Voter { Id = 2, Name = "Voter 2" }
            };

            _mockRepository.Setup(repo => repo.GetAll()).Returns(mockVoters);

            // Act
            var result = _service.GetAllVoters(null, null);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void AddVoter_CallsRepositoryAdd()
        {
            // Arrange
            var voterDto = new VoterDto { Name = "New Voter" };

            // Act
            _service.AddVoter(voterDto);

            // Assert
            _mockRepository.Verify(repo => repo.Add(It.Is<Voter>(v => v.Name == "New Voter" && v.HasVoted == false)), Times.Once);
        }

        [Fact]
        public void SetVoterHasVoted_CallsRepositoryUpdate()
        {
            // Arrange
            var mockVoter = new Voter { Id = 1, Name = "Voter 1", HasVoted = false };

            _mockRepository.Setup(repo => repo.GetById(1)).Returns(mockVoter);

            // Act
            _service.SetVoterHasVoted(1);

            // Assert
            _mockRepository.Verify(repo => repo.Update(It.Is<Voter>(v => v.Id == 1 && v.HasVoted == true)), Times.Once);
        }

        [Fact]
        public void GetNonVotedVoters_ReturnsNonVotedVoters()
        {
            // Arrange
            var mockVoters = new List<Voter>
            {
                new Voter { Id = 1, Name = "Voter 1", HasVoted = false },
                new Voter { Id = 2, Name = "Voter 2", HasVoted = true },
                new Voter { Id = 3, Name = "Voter 3", HasVoted = false }
            };

            _mockRepository.Setup(repo => repo.GetAll()).Returns(mockVoters);

            // Act
            var result = _service.GetNonVotedVoters();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }
    }
}
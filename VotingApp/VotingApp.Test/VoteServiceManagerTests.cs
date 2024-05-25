using AutoMapper;
using Moq;
using VotingApp.BL.Manager;
using VotingApp.DE.BusinessModels;
using VotingApp.DE.DataModels;
using VotingApp.DL.Interfaces;

namespace VotingApp.Tests
{
    public class VoteServiceManagerTests
    {
        private readonly VoteServiceManager _service;
        private readonly Mock<IVoteRepository> _mockVoteRepository;
        private readonly Mock<IVoterRepository> _mockVoterRepository;
        private readonly Mock<ICandidateRepository> _mockCandidateRepository;
        private readonly IMapper _mapper;

        public VoteServiceManagerTests()
        {
            _mockVoteRepository = new Mock<IVoteRepository>();
            _mockVoterRepository = new Mock<IVoterRepository>();
            _mockCandidateRepository = new Mock<ICandidateRepository>();

            var config = new MapperConfiguration(cfg => cfg.CreateMap<VoteDto, Vote>());
            _mapper = config.CreateMapper();

            _service = new VoteServiceManager(_mockVoteRepository.Object, _mapper, _mockVoterRepository.Object, _mockCandidateRepository.Object);
        }

        [Fact]
        public void CastVote_WithValidData_AddsVoteToRepository()
        {
            // Arrange
            var voteDto = new VoteDto { VoterId = 1, CandidateId = 1 };
            _mockVoterRepository.Setup(repo => repo.GetById(voteDto.VoterId.Value)).Returns(new Voter { Id = voteDto.VoterId.Value });
            _mockCandidateRepository.Setup(repo => repo.GetById(voteDto.CandidateId.Value)).Returns(new Candidate { Id = voteDto.CandidateId.Value });

            // Act
            _service.CastVote(voteDto);

            // Assert
            _mockVoteRepository.Verify(repo => repo.Add(It.IsAny<Vote>()), Times.Once);
        }

        [Fact]
        public void CastVote_WithNullVoteDto_ThrowsException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _service.CastVote(null!));
        }

        // Add more test cases as needed...
    }
}
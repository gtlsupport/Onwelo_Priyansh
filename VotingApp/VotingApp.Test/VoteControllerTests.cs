using Microsoft.AspNetCore.Mvc;
using Moq;
using VotingApp.BL.Interfaces;
using VotingApp.Controllers;
using VotingApp.DE.BusinessModels;

namespace VotingApp.Tests
{
    public class VoteControllerTests
    {
        private readonly VoteController _controller;
        private readonly Mock<IVoteServiceManager> _mockVoteService;
        private readonly Mock<IVoterServiceManager> _mockVoterService;

        public VoteControllerTests()
        {
            _mockVoteService = new Mock<IVoteServiceManager>();
            _mockVoterService = new Mock<IVoterServiceManager>();
            _controller = new VoteController(_mockVoteService.Object, _mockVoterService.Object);
        }

        [Fact]
        public void CastVote_WithValidData_ReturnsOkResult()
        {
            // Arrange
            var voteDto = new VoteDto { VoterId = 1, CandidateId = 1 };
            _mockVoteService.Setup(service => service.CastVote(voteDto)).Verifiable();
            _mockVoterService.Setup(service => service.SetVoterHasVoted(voteDto.VoterId.Value)).Verifiable();

            // Act
            var result = _controller.CastVote(voteDto);

            // Assert
            Assert.IsType<OkResult>(result);
            _mockVoteService.Verify();
            _mockVoterService.Verify();
        }

        [Fact]
        public void CastVote_WithNullVoteDto_ReturnsBadRequest()
        {
            // Arrange
            VoteDto voteDto = null!;

            // Act
            var result = _controller.CastVote(voteDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Vote input object is null", badRequestResult.Value);
        }

        [Fact]
        public void CastVote_WithNonZeroId_ReturnsBadRequest()
        {
            // Arrange
            var voteDto = new VoteDto { Id = 1, VoterId = 1, CandidateId = 1 };

            // Act
            var result = _controller.CastVote(voteDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Vote ID should not be passed while casting a new vote", badRequestResult.Value);
        }

        [Fact]
        public void CastVote_WithNullVoterId_ReturnsBadRequest()
        {
            // Arrange
            var voteDto = new VoteDto { CandidateId = 1 };

            // Act
            var result = _controller.CastVote(voteDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Voter ID should be passed while casting a new vote", badRequestResult.Value);
        }

        // Add more test cases as needed...
    }
}
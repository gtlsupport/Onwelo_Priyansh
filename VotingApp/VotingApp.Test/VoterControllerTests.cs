using Microsoft.AspNetCore.Mvc;
using Moq;
using VotingApp.BL.Interfaces;
using VotingApp.Controllers;
using VotingApp.DE.BusinessModels;

namespace VotingApp.Tests
{
    public class VoterControllerTests
    {
        private readonly VoterController _controller;
        private readonly Mock<IVoterServiceManager> _mockService;

        public VoterControllerTests()
        {
            _mockService = new Mock<IVoterServiceManager>();
            _controller = new VoterController(_mockService.Object);
        }

        [Fact]
        public void GetVoters_ReturnsOkResult_WithListOfVoters()
        {
            // Arrange
            var mockVoters = new List<VoterDto>
            {
                new VoterDto { Id = 1, Name = "Voter 1" },
                new VoterDto { Id = 2, Name = "Voter 2" }
            };

            _mockService.Setup(service => service.GetAllVoters(null,null)).Returns(mockVoters);

            // Act
            var result = _controller.GetVoters(null,null);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<VoterDto>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public void AddVoter_ReturnsBadRequest_WhenModelIsInvalid()
        {
            // Arrange
            _controller.ModelState.AddModelError("Name", "Required");

            // Act
            var result = _controller.AddVoter(new VoterDto());

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }

        [Fact]
        public void AddVoter_ReturnsBadRequest_WhenVoterIsNull()
        {
            // Act
            var result = _controller.AddVoter(null!);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Voter input object is null", badRequestResult.Value);
        }

        [Fact]
        public void AddVoter_ReturnsBadRequest_WhenVoterIdIsNotZero()
        {
            // Act
            var result = _controller.AddVoter(new VoterDto { Id = 1 });

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Voter ID should not be passed while creating a new voter", badRequestResult.Value);
        }

        [Fact]
        public void AddVoter_ReturnsStatusCode201_WhenVoterIsAdded()
        {
            // Arrange
            var voterDto = new VoterDto { Name = "New Voter" };

            // Act
            var result = _controller.AddVoter(voterDto);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(201, statusCodeResult.StatusCode);

            var resultValue = statusCodeResult.Value;
            var messageProperty = resultValue?.GetType().GetProperty("Message");
            var message = messageProperty?.GetValue(resultValue, null) as string;

            Assert.Equal("Voter created successfully.", message);
        }

        [Fact]
        public void GetNonVotedVoters_ReturnsOkResult_WithListOfNonVotedVoters()
        {
            // Arrange
            var mockVoters = new List<VoterDto>
            {
                new VoterDto { Id = 1, Name = "Voter 1", HasVoted = false },
                new VoterDto { Id = 2, Name = "Voter 2", HasVoted = false }
            };

            _mockService.Setup(service => service.GetNonVotedVoters()).Returns(mockVoters);

            // Act
            var result = _controller.GetNonVotedVoters();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<VoterDto>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }
    }
}
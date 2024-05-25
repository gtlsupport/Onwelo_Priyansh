using Microsoft.AspNetCore.Mvc;
using Moq;
using VotingApp.BL.Interfaces;
using VotingApp.Controllers;
using VotingApp.DE.BusinessModels;

namespace VotingApp.Tests
{
    public class CandidateControllerTests
    {
        [Fact]
        public void GetCandidates_Returns_OkResult_With_Candidates()
        {
            // Arrange
            var mockServiceManager = new Mock<ICandidateServiceManager>();
            var candidates = new List<CandidateDto> { new CandidateDto { Id = 1, Name = "Candidate 1" } };
            mockServiceManager.Setup(manager => manager.GetAllCandidates(null, null)).Returns(candidates);
            var controller = new CandidateController(mockServiceManager.Object);

            // Act
            var result = controller.GetCandidates(null, null) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(candidates, result.Value);
        }

        [Fact]
        public void AddCandidate_Returns_CreatedStatus_When_Successful()
        {
            // Arrange
            var mockServiceManager = new Mock<ICandidateServiceManager>();
            var candidateDto = new CandidateDto { Name = "New Candidate" };
            mockServiceManager.Setup(manager => manager.AddCandidate(candidateDto));
            var controller = new CandidateController(mockServiceManager.Object);

            // Act
            var result = controller.AddCandidate(candidateDto) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(201, result.StatusCode);
        }

        [Fact]
        public void AddCandidate_Returns_BadRequest_When_ModelState_Invalid()
        {
            // Arrange
            var mockServiceManager = new Mock<ICandidateServiceManager>();
            var controller = new CandidateController(mockServiceManager.Object);
            controller.ModelState.AddModelError("Name", "Name is required");

            // Act
            var result = controller.AddCandidate(null!) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public void AddCandidate_Returns_BadRequest_When_CandidateDto_Null()
        {
            // Arrange
            var mockServiceManager = new Mock<ICandidateServiceManager>();
            var controller = new CandidateController(mockServiceManager.Object);

            // Act
            var result = controller.AddCandidate(null!) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("Candidate input object is null", result.Value);
        }

        [Fact]
        public void AddCandidate_Returns_BadRequest_When_CandidateId_NotZero()
        {
            // Arrange
            var mockServiceManager = new Mock<ICandidateServiceManager>();
            var controller = new CandidateController(mockServiceManager.Object);
            var candidateDto = new CandidateDto { Id = 1, Name = "Existing Candidate" };

            // Act
            var result = controller.AddCandidate(candidateDto) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("Candidate ID should not be passed while creating a new candidate", result.Value);
        }

        [Fact]
        public void GetCandidatesCount_Returns_OkResult_With_Count()
        {
            // Arrange
            var mockServiceManager = new Mock<ICandidateServiceManager>();
            var count = 5;
            mockServiceManager.Setup(manager => manager.GetCandidatesCount()).Returns(count);
            var controller = new CandidateController(mockServiceManager.Object);

            // Act
            var result = controller.GetCandidatesCount() as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(count, result.Value);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Configuration;
using FluentAssertions;
using Moq;
using OTP_server;
using OTP_server.Context;
using OTP_server.Models;
using OTP_server.Services.Interfaces;
using OTP_server.Services.Services;
namespace OTP_tests
{
    public class OTPGeneratorServiceTest
    {
        private readonly Mock<IOTPGeneratorService> _otpGeneratorMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly Mock<DatabaseContext> _dbContextMock;
        private readonly OTPGeneratorService _otpGeneratorService;

        public OTPGeneratorServiceTest()
        {
            _otpGeneratorMock = new Mock<IOTPGeneratorService>();
            _configurationMock = new Mock<IConfiguration>();
            _dbContextMock = new Mock<DatabaseContext>(_configurationMock.Object);
            _otpGeneratorService = new OTPGeneratorService(_dbContextMock.Object);
        }

        [Fact]
        public void GenerateOTP_ValidEmailAndMinutesActive_ReturnsOTPEntity()
        {
            // Arrange
            string validEmail = "test@example.com";
            int minutesActive = 5;

            _otpGeneratorMock.Setup(otp => otp.GenerateOTP(It.IsAny<string>(), It.IsAny<int>()))
                             .Returns(new OTPEntity { OTP = "testOTP" });

            // Act
            var otpEntity = _otpGeneratorService.GenerateOTP(validEmail, minutesActive);

            // Assert
            otpEntity.Should().NotBeNull();
            otpEntity.OTP.Should().NotBeNullOrEmpty();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("invalid_email")]
        [InlineData("test@example")]
        public void GenerateOTP_InvalidEmail_ThrowsException(string invalidEmail)
        {
            // Arrange
            int minutesActive = 5;

            // Act
            Action action = () => _otpGeneratorService.GenerateOTP(invalidEmail, minutesActive);

            // Assert
            action.Should().Throw<Exception>().WithMessage("Email is not valid");
        }

        [Fact]
        public void GenerateOTP_ShouldStoreUserInDatabase()
        {
            // Arrange
            string validEmail = "test@example.com";
            int minutesActive = 5;

            // Setup mock behavior if needed
            _otpGeneratorMock.Setup(otp => otp.GenerateOTP(It.IsAny<string>(), It.IsAny<int>()));

            // Act
            _otpGeneratorService.GenerateOTP(validEmail, minutesActive);

            // Assert
            var storedUser = _dbContextMock.Object.OneTimePasswordDetails.FirstOrDefault(u => u.Email == validEmail);
            storedUser.Should().NotBeNull();
            storedUser.HashedOTP.Should().NotBeNullOrEmpty();
            storedUser.ExpirationDate.Should().BeCloseTo(DateTime.UtcNow.AddMinutes(minutesActive), precision: TimeSpan.FromSeconds(5));
        }

        [Fact]
        public void GenerateOTP_ShouldReplaceExistingUserInDatabase()
        {
            // Arrange
            var existingUser = new OneTimePasswordDetails { Email = "test@example.com" };
            _dbContextMock.Object.OneTimePasswordDetails.Add(existingUser);
            _dbContextMock.Object.SaveChanges();
            string validEmail = "test@example.com";
            int minutesActive = 5;

            // Setup mock behavior if needed
            _otpGeneratorMock.Setup(otp => otp.GenerateOTP(It.IsAny<string>(), It.IsAny<int>()));

            // Act
            _otpGeneratorService.GenerateOTP(validEmail, minutesActive);

            // Assert
            var storedUser = _dbContextMock.Object.OneTimePasswordDetails.FirstOrDefault(u => u.Email == validEmail);
            storedUser.Should().NotBeNull();
            storedUser.HashedOTP.Should().NotBeNullOrEmpty();
            storedUser.ExpirationDate.Should().BeCloseTo(DateTime.UtcNow.AddMinutes(minutesActive), precision: TimeSpan.FromSeconds(5));
            storedUser.Id.Should().NotBe(existingUser.Id);
        }

        [Fact]
        public void GenerateOTP_ShouldGenerateValidOTP()
        {
            // Arrange
            string validEmail = "test@example.com";
            int minutesActive = 5;

            _otpGeneratorMock.Setup(otp => otp.GenerateOTP(It.IsAny<string>(), It.IsAny<int>()))
                             .Returns(new OTPEntity { OTP = "testOTP" });

            // Act
            var otpEntity = _otpGeneratorService.GenerateOTP(validEmail, minutesActive);

            // Assert
            otpEntity.OTP.Should().NotBeNullOrEmpty();
            otpEntity.OTP.Length.Should().BeGreaterThan(0);
            otpEntity.OTP.All(char.IsLetterOrDigit).Should().BeTrue();
        }
    }
}

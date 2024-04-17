using FluentAssertions;
using Moq;
using OTP_server.Context;
using OTP_server.Helpers;
using OTP_server.Models;
using OTP_server.Services.Interfaces;
using OTP_server.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTP_tests
{
    public class OTPValidatorServiceTest
    {
        private readonly Mock<DatabaseContext> _dbContextMock;
        private readonly IOTPValidatorService _otpValidatorService;

        public OTPValidatorServiceTest()
        {
            _dbContextMock = new Mock<DatabaseContext>();
            _otpValidatorService = new OTPValidatorService(_dbContextMock.Object);
        }

        [Theory]
        [InlineData(null, "123456")]
        [InlineData("test@example.com", null)]
        [InlineData(null, null)]
        [InlineData("", "123456")]
        [InlineData("test@example.com", "")]
        [InlineData("", "")]
        public void ValidateOTP_NullOrEmptyEmailOrOTP_ReturnsFalse(string email, string otp)
        {
            // Act
            var result = _otpValidatorService.ValidateOTP(email, otp);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void ValidateOTP_ValidEmailNoUser_ReturnsFalse()
        {
            // Arrange
            string email = "test@example.com";
            string otp = "123456";
            _dbContextMock.Setup(db => db.Users)
                          .Returns((Microsoft.EntityFrameworkCore.DbSet<User>)new List<User>().AsQueryable());

            // Act
            var result = _otpValidatorService.ValidateOTP(email, otp);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void ValidateOTP_ValidEmailUserNoOTP_ReturnsFalse()
        {
            // Arrange
            string email = "test@example.com";
            string otp = "123456";
            _dbContextMock.Setup(db => db.Users)
                          .Returns((Microsoft.EntityFrameworkCore.DbSet<User>)new List<User> { new User { Email = email } }.AsQueryable());

            // Act
            var result = _otpValidatorService.ValidateOTP(email, otp);

            // Assert
            result.Should().BeFalse();
        }

    }
}

using FluentAssertions;
using OTP_server.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTP_tests
{
    public class PasswordEncryptTest
    {
        [Fact]
        public void HashPassword_ReturnsValidHash()
        {
            // Arrange
            string password = "password123";

            // Act
            var hash = PasswordEncryptHelper.HashPassword(password);

            // Assert
            hash.Should().NotBeNullOrEmpty();
            hash.Should().NotBe(password); // Ensure password is not stored directly
        }

        [Fact]
        public void VerifyPassword_CorrectPassword_ReturnsTrue()
        {
            // Arrange
            string password = "password123";
            string hash = PasswordEncryptHelper.HashPassword(password);

            // Act
            bool result = PasswordEncryptHelper.VerifyPassword(password, hash);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void VerifyPassword_IncorrectPassword_ReturnsFalse()
        {
            // Arrange
            string correctPassword = "password123";
            string incorrectPassword = "wrongpassword";
            string hash = PasswordEncryptHelper.HashPassword(correctPassword);

            // Act
            bool result = PasswordEncryptHelper.VerifyPassword(incorrectPassword, hash);

            // Assert
            result.Should().BeFalse();
        }
    }
}

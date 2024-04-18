# Secure One-Time Password (OTP) Generator for Banking Application

This project aims to develop a secure, efficient, and user-friendly system for generating one-time passwords (OTPs) for a banking application. The OTP system is designed to enhance the user experience while ensuring the confidentiality and security of customers' data.

## Business Requirements

1. **Security**: The OTP system must ensure the security of customers' confidential data. OTPs should be generated randomly and should not be predictable. Encryption during OTP transmission must be implemented to prevent interception by unauthorized parties.

2. **Time-Bound OTPs**: OTPs should have a limited validity period. Once generated, an OTP should be automatically invalidated after a certain customizable period of time to prevent misuse.

3. **User-Friendly Interface**: The OTP input interface should be intuitive and easy to use, minimizing user confusion and errors during OTP input.

4. **Error Handling**: The system should provide clear and understandable error messages to users in case of any issues, ensuring a smooth user experience.

5. **Notification**: Users should receive the OTP through a toast message, visible as long as the OTP is valid, to streamline the authentication process.

## Technical Requirements

1. **Web Application**: Used .NET Framework core (.net 8) for server implementation

2. **Frontend Framework**: Used Angular 16 for implementation

3. **Unit Testing**: On backend used xUnit, Moq, FluentAssertions 

## Installation and Running Instructions

### .NET Core API Server

1. Clone the repository to your local machine:
2. Navigate to OTP_server folder and launch OTP_server.sln
3. Run dotnet restore to restore dependencies and build the project.
4. In Package-Manager-Console run update-database to update database schema in your SQL Server Management Studio
5. Run the API Server

6. Navigate to OTP_client folder and open it with visual studio code
7. In terminal run npm run start to start the project then open https://localhost:4200/

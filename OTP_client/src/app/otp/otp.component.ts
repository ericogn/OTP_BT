import { Component } from '@angular/core';
import { OtpService } from '../services/service';
import { OtpResponse } from '../models/otpResponse';

@Component({
  selector: 'app-otp',
  templateUrl: './otp.component.html',
  styleUrls: ['./otp.component.css']
})
export class OtpComponent {
  email: string = '';
  otp: string = '';
  otpReceived: boolean = false;
  otpResponse: OtpResponse ={expirationDate:'',otp:''};
  constructor(private otpService: OtpService) {}

  generateOTP() {
    this.otpService.generateOTP(this.email)
      .subscribe(response => {
        this.otpResponse = response
        if (response) {
          alert(`OTP generated successfully! otp: ${response.otp}`);
          this.otpReceived = true;
          setTimeout(() => {
            this.otpReceived = false;
          }, 2 * 60 * 1000); // 2 minutes in milliseconds
        } else {
          alert('Failed to generate OTP.');
        }
      }, error => {
        console.error('Failed to generate OTP:', error);
        alert('Failed to generate OTP.');
      });
  }

  validateOTP() {
    this.otpService.validateOTP(this.email, this.otp)
      .subscribe(response => {
        if (response) {
          alert('OTP is valid.');
        } else {
          alert('Invalid OTP.');
        }
      }, error => {
        console.error('Failed to validate OTP:', error);
        alert('Failed to validate OTP.');
      });
  }
}

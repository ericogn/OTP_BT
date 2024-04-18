import { Component } from '@angular/core';
import { OtpService } from '../services/service';
import { OtpResponse } from '../models/otpResponse';
import { MatSnackBar } from '@angular/material/snack-bar';
import { environment } from 'src/environments/environment.development';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-otp',
  templateUrl: './otp.component.html',
  styleUrls: ['./otp.component.css']
})
export class OtpComponent {
  email: string = '';
  otp: string = '';
  otpReceived: boolean = false;
  otpResponse: OtpResponse ={expirationTime:'',otp:''};
  validationResponse:boolean = false;
  minutesActive:number = 0;
  constructor(private otpService: OtpService, private snackBar:MatSnackBar) {}

  generateOTP() {
    if(this.minutesActive <= 0){
      alert('Value must be at least 1');
      return;
    }
    this.otpService.generateOTP(this.email,this.minutesActive)
      .subscribe(response => {
        this.otpResponse = response
        if (response) {
          const currentDate = new Date();
          const expirationDate = new Date(response.expirationTime);
          const differenceInMilliseconds = expirationDate.getTime() - currentDate.getTime();
          this.snackBar.open(`OTP generated successfully! otp: ${response.otp}`, 'Close', {
            duration: differenceInMilliseconds,
          });
          this.otpReceived = true;
        } else {
          this.snackBar.open('Failed to generate OTP.', 'Close', {
            duration: 2000, 
          });
        }
      }, error => {
        console.error('Failed to generate OTP:', error);
        alert(`Failed to generate OTP.`);
      });
  }

  validateOTP() {
    this.otpService.validateOTP(this.email, this.otp)
      .subscribe((isValid: any) => {
        if (isValid) {
          this.snackBar.dismiss();
          alert('OTP is valid.');
        } else {
          alert('Invalid OTP.');
        }
      }, (error: any) => { 
        console.error('Failed to validate OTP:', error);
        alert('Failed to validate OTP.');
      });
  }
}

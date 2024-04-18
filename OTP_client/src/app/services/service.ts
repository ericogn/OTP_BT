import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpStatusCode } from "@angular/common/http";
import { environment } from 'src/environments/environment.development';
import { OtpResponse } from '../models/otpResponse';

@Injectable({
  providedIn: 'root'
})
export class OtpService {
    private httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    private baseUrl = `${environment.baseUrl}`;
    
    constructor (private http: HttpClient){

    }

  generateOTP(email: string, minutesActive:number) {
    return this.http.post<OtpResponse>(`${environment.baseUrl}OTPGenerator/GenerateOTP?email=${email}&minutesActive=${minutesActive}`, this.httpOptions);
  }

  validateOTP(email: string, otp: string) {
    return this.http.post(`${environment.baseUrl}OTPValidator/ValidateOTP?email=${email}&otp=${otp}`, this.httpOptions)
  }
}


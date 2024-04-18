// never did tests on angular. this is just an attempt
import { TestBed, inject } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { environment } from 'src/environments/environment.development';
import { OtpService } from './services/service';

describe('OtpService', () => {
  let service: OtpService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [OtpService]
    });
    service = TestBed.inject(OtpService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should generate OTP', () => {
    const email = 'test@example.com';
    const minutesActive = 5;
    const mockResponse = { otp: '123456' };

    service.generateOTP(email, minutesActive).subscribe((response: any) => {
      expect(response).toEqual(mockResponse);
    });

    const request = httpMock.expectOne(`${environment.baseUrl}OTPGenerator/GenerateOTP?email=${email}&minutesActive=${minutesActive}`);
    expect(request.request.method).toBe('POST');
    request.flush(mockResponse);
  });
});

describe('OtpService', () => {
    let service: OtpService;
    let httpMock: HttpTestingController;
  
    beforeEach(() => {
      TestBed.configureTestingModule({
        imports: [HttpClientTestingModule],
        providers: [OtpService]
      });
      service = TestBed.inject(OtpService);
      httpMock = TestBed.inject(HttpTestingController);
    });
  
    afterEach(() => {
      httpMock.verify();
    });
  
    it('should be created', () => {
      expect(service).toBeTruthy();
    });
  
    it('should validate OTP', () => {
      const email = 'test@example.com';
      const otp = '123456';
      const mockResponse = { valid: true };
  
      service.validateOTP(email, otp).subscribe(response => {
        expect(response).toEqual(mockResponse);
      });
  
      const request = httpMock.expectOne(`${environment.baseUrl}OTPValidator/ValidateOTP?email=${email}&otp=${otp}`);
      expect(request.request.method).toBe('POST');
      request.flush(mockResponse);
    });
  });
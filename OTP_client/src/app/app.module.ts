import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { OtpComponent } from './otp/otp.component';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { OtpService } from './services/service';

@NgModule({
  declarations: [
    AppComponent,
    OtpComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule // Import FormsModule
  ],
  providers: [
    HttpClient,
    OtpService // Provide the OtpService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

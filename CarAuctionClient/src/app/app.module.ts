import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {HttpClientModule} from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CarAuctionComponent } from './car-auction/car-auction.component';
import { LoginRegisterComponent } from './login-register/login-register.component';
import { MyBidsComponent } from './my-bids/my-bids.component';
import { MyCarsComponent } from './my-cars/my-cars.component';
import { ReactiveFormsModule } from '@angular/forms';
import { RegisterComponent } from './login-register/register/register.component';
import { LoginComponent } from './login-register/login/login.component';
import { GetCarComponent } from './car-auction/get-car/get-car.component';

@NgModule({
  declarations: [
    AppComponent,
    CarAuctionComponent,
    LoginRegisterComponent,
    MyBidsComponent,
    MyCarsComponent,
    RegisterComponent,
    LoginComponent,
    GetCarComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

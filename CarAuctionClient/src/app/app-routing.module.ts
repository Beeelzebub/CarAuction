import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CarAuctionComponent } from './car-auction/car-auction.component';
import { LoginRegisterComponent } from './login-register/login-register.component';
import { LoginComponent } from './login-register/login/login.component';
import { RegisterComponent } from './login-register/register/register.component';
import { MyBidsComponent } from './my-bids/my-bids.component';
import { MyCarsComponent } from './my-cars/my-cars.component';
import { GetCarComponent } from './car-auction/get-car/get-car.component';
const routes: Routes = [
  { path: '', component: CarAuctionComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'myBids', component: MyBidsComponent },
  { path: 'myCars', component: MyCarsComponent },
  { path: ':id/info', component: GetCarComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

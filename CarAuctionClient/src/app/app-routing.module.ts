import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CarAuctionComponent } from './car-auction/car-auction.component';
import { LoginComponent } from './login-register/login/login.component';
import { RegisterComponent } from './login-register/register/register.component';
import { MyBidsComponent } from './my-bids/my-bids.component';
import { MyCarsComponent } from './my-cars/my-cars.component';
import { GetCarComponent } from './car-auction/get-car/get-car.component';
import { AdminComponent } from './admin/admin.component';
import { GetOneCarComponent } from './admin/get-one-car/get-one-car.component';
import { AddCarComponent } from './my-cars/add-car/add-car.component';
import { GetOneCarProfileComponent } from './my-cars/get-one-car-profile/get-one-car-profile.component';
const routes: Routes = [
  { path: '', component: CarAuctionComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'myBids', component: MyBidsComponent },
  { path: 'myCars', component: MyCarsComponent },
  { path: ':id/info', component: GetCarComponent },
  { path: 'admin/cars', component: AdminComponent },
  { path: 'admin/cars/:id', component: GetOneCarComponent },
  { path: 'myCars/addCar', component: AddCarComponent },
  { path: 'myCars/:id/info', component: GetOneCarProfileComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

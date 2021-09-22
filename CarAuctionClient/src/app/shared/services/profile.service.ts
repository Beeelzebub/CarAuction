import { Injectable } from '@angular/core';
import{HttpClient} from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';
import { Lot } from '../models/enums/lot';
import { Observable } from 'rxjs';
import { ModelForCreatingLot } from '../models/model-for-creating-lot.=model';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {


  constructor(public http: HttpClient) { }

  readonly apiURL = "https://localhost:5001/api";
  

  getToken(){
    var currentUser = JSON.parse(localStorage.getItem('currentUser') || '');
    var token = currentUser.token;
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return headers;
  }
  
  GetUserCars(): Observable<any>{
   
    return this.http.get(this.apiURL + '/profile/myCars', { headers: this.getToken() });
  }

  GetUserCarsWithStatus(status: Lot): Observable<any>{
    return this.http.get(this.apiURL + '/profile/myCars', {params:{status:status}, headers: this.getToken()});
  }

  public addCar(model: ModelForCreatingLot){
    console.log(model);
    const formData = new FormData();
    formData.append('Year', model.Year.toString());
    formData.append('Image', model.Image);
    formData.append('Fuel', model.Fuel);
    formData.append('CarBody', model.CarBody);
    formData.append('DriveUnit', model.DriveUnit);
    formData.append('Name', model.Name);
    formData.append('BrandName', model.BrandName);
    formData.append('MinimalStep', model.MinimalStep.toString());
    formData.append('StartingPrice', model.StartingPrice.toString());
    formData.append('RedemptionPrice', model.RedemptionPrice.toString());
    return this.http.post(this.apiURL + '/Profile/AddLot', formData, {headers: this.getToken()});
  }
  getBids(): Observable<any>{
    return this.http.get(this.apiURL + '/Profile/myBids', {headers: this.getToken()}) ;
  }

  getOneCar(id: number): Observable<any>{
    return this.http.get(this.apiURL + '/profile/myCars/' + id, {headers: this.getToken()})
  }
  deleteCar(id: number){
    return this.http.delete(this.apiURL + '/Profile/MyCars/' + id, {headers: this.getToken()})
  }

}

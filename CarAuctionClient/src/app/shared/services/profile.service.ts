import { Injectable } from '@angular/core';
import{HttpClient} from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';
import { Lot } from '../models/enums/lot';
import { ModelForCreatingLot } from '../models/model-for-creating-lot.=model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {


  constructor(public http: HttpClient) { }

  readonly apiURL = "https://localhost:44364/api";
  

  GetUserCars(){
    
    var currentUser = JSON.parse(localStorage.getItem('currentUser') || '');
    var token = currentUser.token;
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.get(this.apiURL + '/profile/myCars', { headers: headers });
  }
  GetUserCarsWithStatus(status: Lot){
    
    var currentUser = JSON.parse(localStorage.getItem('currentUser') || '');
    var token = currentUser.token;
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.get(this.apiURL + '/profile/myCars', {params:{status:status}, headers: headers});
  }
  addCar(model: ModelForCreatingLot){
    var currentUser = JSON.parse(localStorage.getItem('currentUser') || '');
    var token = currentUser.token;
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.post(this.apiURL + '/Profile/AddLot', model, {headers: headers}) ;
  }
}

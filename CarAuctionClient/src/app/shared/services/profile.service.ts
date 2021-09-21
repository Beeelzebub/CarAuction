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

  addCar(model: ModelForCreatingLot){
    return this.http.post(this.apiURL + '/Profile/AddLot', model, {headers: this.getToken()});
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

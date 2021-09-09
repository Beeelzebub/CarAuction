import { Injectable } from '@angular/core';
import{HttpClient} from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';

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
}

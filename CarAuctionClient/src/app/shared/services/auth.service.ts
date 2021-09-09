import { Injectable } from '@angular/core';
import{HttpClient} from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(public http: HttpClient) { }

  readonly apiURL = "https://localhost:44364/api";

  registration(name:string, userName:string, password:string ) {
    return this.http.post(this.apiURL + '/register', {name, userName, password});
  }
  login(userName:string, password:string ) {
    return this.http.post(this.apiURL + '/login', { userName, password});
  }
}

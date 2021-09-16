import { Injectable } from '@angular/core';
import{HttpClient} from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient) { }

  readonly apiURL = "https://localhost:44364/api";
  isAuthorithed:boolean = true;

  registration(name:string, userName:string, password:string ) {
    return this.http.post(this.apiURL + '/register', {name, userName, password});
  }
  login(userName:string, password:string ) {
   
    return this.http.post(this.apiURL + '/login', { userName, password});
    
  }
  logoutClick(){
    localStorage.clear();
    this.isAuthorithed= false;
    location.reload();
  }
}

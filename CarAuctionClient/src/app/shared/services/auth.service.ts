import { Injectable } from '@angular/core';
import{HttpClient} from '@angular/common/http';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient, private router: Router) { }

  readonly apiURL = "https://localhost:5001/api";
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
    this.router.navigate(['/cars']).then(()=>location.reload());
  }
}

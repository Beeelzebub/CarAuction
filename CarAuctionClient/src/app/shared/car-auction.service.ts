import { Injectable } from '@angular/core';
import{HttpClient} from '@angular/common/http';
import{Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CarAuctionService {

  constructor(private http: HttpClient) { }

  readonly apiURL = "https://localhost:44364/api";

  listCars(): Observable<any[]>{
    return this.http.get<any>(this.apiURL + '/Cars');
  }
  registration(name:string, userName:string, password:string ) {
    return this.http.post(this.apiURL + '/register', {name, userName, password});
  }
  login(userName:string, password:string ) {
    return this.http.post(this.apiURL + '/login', { userName, password});
  }
  GetUserCars(){
    return this.http.get(this.apiURL + '/profile/myCars')
  }
  getOneCar(id: number): Observable<any>{
    return this.http.get(this.apiURL + '/cars/' + id)
  }
}

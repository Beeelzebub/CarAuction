import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs';
import { HttpHeaders } from '@angular/common/http';
import { Lot } from '../models/enums/lot';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  constructor(private http: HttpClient) { }

  readonly apiURL = "https://localhost:44364/api";
  getToken(){
    var currentUser = JSON.parse(localStorage.getItem('currentUser') || '');
    var token = currentUser.token;
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return headers;
  }



  getCarsAdmin(): Observable<any>{
    return this.http.get(this.apiURL + '/Admin/cars/', { headers: this.getToken() })
  }

  setStatusLot(id: number, newStatus: Lot){ 
    const body = [{
      op: "replace",
      path: "/status",
      value: newStatus
  }];
    return this.http.patch(this.apiURL + '/admin/cars/' + id, body, {headers: this.getToken()});
  }

  getOneCarForAdmin(id: number): Observable<any>{
    return this.http.get(this.apiURL + '/admin/cars/' + id,{ headers: this.getToken() })
  }
}

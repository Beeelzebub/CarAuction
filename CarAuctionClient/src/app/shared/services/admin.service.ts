import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { HttpHeaders } from '@angular/common/http';
import { Lot } from '../models/enums/lot';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  constructor(private http: HttpClient) { }

  readonly apiURL = "https://localhost:44364/api";

  getCarsAdmin(): Observable<any>{
    var currentUser = JSON.parse(localStorage.getItem('currentUser') || '');
    var token = currentUser.token;
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.get(this.apiURL + '/Admin/cars/', { headers: headers })
  }
  setStatusLot(id: number, status: Lot){
    console.log(id, status);
    console.log(this.apiURL + '/admin/cars/' + id);
    return this.http.put(this.apiURL + '/admin/cars/' + id, status);
  }

  getOneCarForAdmin(id: number): Observable<any>{
    var currentUser = JSON.parse(localStorage.getItem('currentUser') || '');
    var token = currentUser.token;
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.get(this.apiURL + '/admin/cars/' + id,{ headers: headers })
  }
}

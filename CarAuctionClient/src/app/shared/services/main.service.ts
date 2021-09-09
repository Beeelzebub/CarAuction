import { Injectable } from '@angular/core';
import{HttpClient} from '@angular/common/http';
import{Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MainService {

  constructor(public http: HttpClient) { }

  readonly apiURL = "https://localhost:44364/api";

  listCars(): Observable<any[]>{
    return this.http.get<any>(this.apiURL + '/Cars');
  }
  getOneCar(id: number): Observable<any>{
    return this.http.get(this.apiURL + '/cars/' + id)
  }
}

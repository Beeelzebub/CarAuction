import { Injectable } from '@angular/core';
import{HttpClient} from '@angular/common/http';
import{Observable} from 'rxjs';
import { CarsParameters } from '../models/cars-parameters.model';
import { HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class MainService {

  constructor(public http: HttpClient) { }

  readonly apiURL = "https://localhost:5001/api";
  getToken(){
    var currentUser = JSON.parse(localStorage.getItem('currentUser') || '');
    var token = currentUser.token;
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return headers;
  }

  listCars(): Observable<any>{
    return this.http.get<any>(this.apiURL + '/Cars');
  }
  listCarsByCondition(carParam:CarsParameters): Observable<any>{
    return this.http.get<any>(this.apiURL + '/Cars', {params:{
      modelName: carParam.ModelName, 
      brandName: carParam.BrandName,
      minYear: carParam.minYear,
      maxYear: carParam.maxYear
    }});
  }
  getOneCar(id: number): Observable<any>{
    return this.http.get(this.apiURL + '/cars/' + id)
  }
  getModelsWithBrands(): Observable<any>{
   return this.http.get(this.apiURL+'/GetModelsWithBrands')
  }

  placeBid(id:number): Observable<any>{
    return this.http.post(this.apiURL + '/cars/' + id, {}, {headers: this.getToken()});
  }
  redemption(id:number): Observable<any>{
    return this.http.post(this.apiURL + '/cars/redemption/' + id, {}, {headers: this.getToken()});
  }
}

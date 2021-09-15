import { Injectable } from '@angular/core';
import{HttpClient} from '@angular/common/http';
import{Observable} from 'rxjs';
import { CarsParameters } from '../models/cars-parameters.model';

@Injectable({
  providedIn: 'root'
})
export class MainService {

  constructor(public http: HttpClient) { }

  readonly apiURL = "https://localhost:44364/api";

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
   return this.http.get(this.apiURL+'/Cars/models')
  }
}

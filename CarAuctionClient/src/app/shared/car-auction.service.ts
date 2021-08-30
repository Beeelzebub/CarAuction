import { Injectable } from '@angular/core';
import{HttpClient} from '@angular/common/http';
import{Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CarAuctionService {

  constructor(private client: HttpClient) { }

  readonly apiURL = "https://localhost:44364/api/Cars";

  listEmployees(): Observable<any[]>{
    return this.client.get<any>(this.apiURL);
  }
}

import { Component, Input, OnInit } from '@angular/core';
import { CarAuctionService } from 'src/app/shared/car-auction.service';
import { ActivatedRoute } from '@angular/router';
import { Car } from 'src/app/shared/models/car.model';

@Component({
  selector: 'app-get-car',
  templateUrl: './get-car.component.html',
  styleUrls: ['./get-car.component.css']
})
export class GetCarComponent implements OnInit {

  constructor(private service: CarAuctionService, private _activatedRoute: ActivatedRoute) { }

   car: Car;
  
   id!: number;
   
  ngOnInit(): void {
    this._activatedRoute.paramMap.subscribe(
      params => { 
        this.id = parseInt(params.get('id') || '{}') ; }
     );
     this.getCar(this.id);
  }

  getCar(id:number){

    this.service.getOneCar(id).subscribe(
      data=>
      this.car= data
    )
  }


}

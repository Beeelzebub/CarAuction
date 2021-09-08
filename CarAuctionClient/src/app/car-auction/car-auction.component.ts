import { Component, OnInit } from '@angular/core';
import {CarAuctionService} from 'src/app/shared/car-auction.service';
import { Car } from '../shared/models/car.model';

@Component({
  selector: 'app-car-auction',
  templateUrl: './car-auction.component.html',
  styles: [
  ]
})
export class CarAuctionComponent implements OnInit {

  constructor(private service: CarAuctionService) { }
  carsList:Car[];

  ngOnInit(): void {
    this.refreshList();
  }


  refreshList(){
    this.service.listCars().subscribe(data =>{
      this.carsList = data;
    });
  }

}

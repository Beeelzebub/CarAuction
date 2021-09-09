import { Component, OnInit } from '@angular/core';
import { MainService } from '../shared/services/main.service'; 
import { Car } from '../shared/models/car.model';

@Component({
  selector: 'app-car-auction',
  templateUrl: './car-auction.component.html',
  styles: [
  ]
})
export class CarAuctionComponent implements OnInit {

  constructor(private service: MainService) { }
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

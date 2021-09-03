import { Component, OnInit } from '@angular/core';
import { CarAuctionService } from '../shared/car-auction.service';

@Component({
  selector: 'app-my-cars',
  templateUrl: './my-cars.component.html',
  styleUrls: ['./my-cars.component.css']
})
export class MyCarsComponent implements OnInit {

  constructor(public service: CarAuctionService) { }

  carsList:any=[];

  ngOnInit(): void {
    this.refreshList();
  }

  refreshList(){
    this.service.GetUserCars().subscribe(data =>{
      this.carsList = data;
    });
  }

}

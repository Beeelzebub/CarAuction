import { Component, OnInit } from '@angular/core';
import { CarAuctionService } from 'src/app/car-auction.service';

@Component({
  selector: 'app-cars',
  templateUrl: './cars.component.html',
  styleUrls: ['./cars.component.css']
})
export class CarsComponent implements OnInit {

  constructor(private service: CarAuctionService) { }

  carList:any=[];
  ngOnInit(): void {
    this.refreshList;
  }

  refreshList(){
    this.service.listCars().subscribe(data =>{
      this.carList=data;
      console.log(data);
    });
  }

}

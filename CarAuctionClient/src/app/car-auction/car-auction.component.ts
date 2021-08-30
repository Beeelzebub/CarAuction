import { Component, OnInit } from '@angular/core';
import {CarAuctionService} from 'src/app/shared/car-auction.service'

@Component({
  selector: 'app-car-auction',
  templateUrl: './car-auction.component.html',
  styles: [
  ]
})
export class CarAuctionComponent implements OnInit {

  constructor(private service: CarAuctionService) { }
  carsList:any=[];

  ngOnInit(): void {
    this.refreshList();
  }


  refreshList(){
    this.service.listEmployees().subscribe(data =>{
      this.carsList = data;
    });
  }
}

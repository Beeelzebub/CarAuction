import { Component, OnInit } from '@angular/core';
import { ProfileService } from '../shared/services/profile.service';
import {Lot} from 'src/app/shared/models/enums/lot';

@Component({
  selector: 'app-my-cars',
  templateUrl: './my-cars.component.html',
  styleUrls: ['./my-cars.component.css']
})
export class MyCarsComponent implements OnInit {

  constructor(public service: ProfileService) { }

  carsList:any=[];

  ngOnInit(): void {
    this.allClick();
  }

  allClick(){
    this.service.GetUserCars().subscribe(data =>{
      this.carsList = data.data;
    });
  }
  filterClick(status: Lot){
    this.service.GetUserCarsWithStatus(status).subscribe(data =>{
      this.carsList = data.data;
    });
  }

}

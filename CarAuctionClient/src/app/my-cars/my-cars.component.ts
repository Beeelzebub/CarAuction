import { Component, OnInit } from '@angular/core';
import { ProfileService } from '../shared/services/profile.service';
import {Lot} from 'src/app/shared/models/enums/lot';
import { Car } from '../shared/models/car.model';

@Component({
  selector: 'app-my-cars',
  templateUrl: './my-cars.component.html',
  styleUrls: ['./my-cars.component.css']
})
export class MyCarsComponent implements OnInit {

  constructor(public service: ProfileService) { }

  carsList:Car[];

  ngOnInit(): void {
    this.allClick();
  }

  allClick(){
    this.service.GetUserCars().subscribe(data =>{
      this.carsList = data.data;
      console.log(this.carsList);
    });
  }
  filterClick(status: Lot){
    this.service.GetUserCarsWithStatus(status).subscribe(data =>{
      this.carsList = data.data;
    });
  }


}



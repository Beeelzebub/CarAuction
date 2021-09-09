import { Component, OnInit } from '@angular/core';
import { ProfileService } from '../shared/services/profile.service';

@Component({
  selector: 'app-my-cars',
  templateUrl: './my-cars.component.html',
  styleUrls: ['./my-cars.component.css']
})
export class MyCarsComponent implements OnInit {

  constructor(public service: ProfileService) { }

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

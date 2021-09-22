import { Component, OnInit } from '@angular/core';
import { AdminService } from '../shared/services/admin.service';
import { Car } from '../shared/models/car.model';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {

  constructor(public service: AdminService) { }

  cars: Car[];
  

  ngOnInit(): void {
    this.service.getCarsAdmin().subscribe(data =>{ 
      console.log(data.data)
      this.cars = data.data
    });
  }

  
}

import { Component, OnInit } from '@angular/core';
import { ModelForCreatingLot } from 'src/app/shared/models/model-for-creating-lot.=model';
import { DriveUnit } from 'src/app/shared/models/enums/drive-unit';
import { Fuel } from 'src/app/shared/models/enums/fuel';
import { CarBody } from 'src/app/shared/models/enums/car-body';
import { ProfileService } from 'src/app/shared/services/profile.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-car',
  templateUrl: './add-car.component.html',
  styleUrls: ['./add-car.component.css']
})
export class AddCarComponent implements OnInit {

  driveUnit = DriveUnit;
  fuel = Fuel;
  carBody = CarBody;
  constructor(public service: ProfileService, private router: Router) {}

  

  modelForCreatingLot: ModelForCreatingLot = new ModelForCreatingLot();
  ngOnInit(): void {
  }

  getFiles(event:any){  
    this.modelForCreatingLot.Image = event.target.files[0];  
}
  
  addCar(model: ModelForCreatingLot){
    model = this.modelForCreatingLot;
    this.service.addCar(model).subscribe(
      ()=>{
        this.router.navigate(['/myCars']);
      }
    );
  }

}

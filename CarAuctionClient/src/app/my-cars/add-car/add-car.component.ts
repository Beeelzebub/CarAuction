import { Component, OnInit } from '@angular/core';
import { ModelForCreatingLot } from 'src/app/shared/models/model-for-creating-lot.=model';
import { Fuel } from 'src/app/shared/models/enums/fuel';

@Component({
  selector: 'app-add-car',
  templateUrl: './add-car.component.html',
  styleUrls: ['./add-car.component.css']
})
export class AddCarComponent implements OnInit {

  constructor() {}

  modelForCreatingLot: ModelForCreatingLot;
  fuel: Fuel;
  imageUrl:string;
  ngOnInit(): void {
  }

}

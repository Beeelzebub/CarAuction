import { Component, OnInit } from '@angular/core';
import { Car } from 'src/app/shared/models/car.model';
import { AdminService } from 'src/app/shared/services/admin.service'; 
import { ActivatedRoute } from '@angular/router';
import { Lot } from 'src/app/shared/models/enums/lot';

@Component({
  selector: 'app-get-one-car',
  templateUrl: './get-one-car.component.html',
  styleUrls: ['./get-one-car.component.css']
})
export class GetOneCarComponent implements OnInit {

  constructor(public service: AdminService, private _activatedRoute: ActivatedRoute) { }

  car: Car;
  
   id!: number;

  ngOnInit(): void {
    this._activatedRoute.paramMap.subscribe(
      params => { 
        this.id = parseInt(params.get('id') || '{}') ; }
     );
     this.getCar(this.id);
  }

  getCar(id:number){

    this.service.getOneCarForAdmin(id).subscribe(
      data=> this.car= data
    )
  }
  approvedClick(){
    console.log(this.id)
    this.service.setStatusLot(this.id, Lot.Approved);
    
  }
}

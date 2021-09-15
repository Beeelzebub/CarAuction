import { Component, OnInit } from '@angular/core';
import { Car } from 'src/app/shared/models/car.model';
import { AdminService } from 'src/app/shared/services/admin.service'; 
import { ActivatedRoute } from '@angular/router';
import { Lot } from 'src/app/shared/models/enums/lot';
import { Router } from '@angular/router';

@Component({
  selector: 'app-get-one-car',
  templateUrl: './get-one-car.component.html',
  styleUrls: ['./get-one-car.component.css']
})
export class GetOneCarComponent implements OnInit {

  constructor(public service: AdminService, private _activatedRoute: ActivatedRoute, private router: Router) { }

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
    this.service.setStatusLot(this.id, Lot.Approved).subscribe(data=>{
      this.router.navigate(['admin/cars']);
    });
    
  }
  deniedClick(){
    this.service.setStatusLot(this.id, Lot.Denied).subscribe(data=>{
      this.router.navigate(['admin/cars']);
    });
    
  }
}

import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Car } from 'src/app/shared/models/car.model';
import { ProfileService } from 'src/app/shared/services/profile.service';
import { Router } from '@angular/router';
import {MatDialog} from '@angular/material/dialog';
import { ModalDeleteComponent } from './modal-delete/modal-delete.component';

@Component({
  selector: 'app-get-one-car-profile',
  templateUrl: './get-one-car-profile.component.html',
  styleUrls: ['./get-one-car-profile.component.css']
})
export class GetOneCarProfileComponent implements OnInit {

  constructor(private service: ProfileService,private dialog: MatDialog, private _activatedRoute: ActivatedRoute) { }

  car: Car;
  
   id: number;

  ngOnInit(): void {
    this._activatedRoute.paramMap.subscribe(
      params => { 
        this.id = parseInt(params.get('id') || '{}') ; }
     );
     this.getCar(this.id);
  }

  getCar(id:number){
 
    this.service.getOneCar(id).subscribe(
      data=>
      this.car= data.data
    )
  }

  openModalDelete(){
    this.dialog.open(ModalDeleteComponent,{
      data: {id:this.id}
    });
  }

  

}

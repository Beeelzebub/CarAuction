import { Component, Input, OnInit } from '@angular/core';
import { MainService } from 'src/app/shared/services/main.service'; 
import { ActivatedRoute } from '@angular/router';
import { Car } from 'src/app/shared/models/car.model';
import {MatDialog} from '@angular/material/dialog';
import { ModalWindowComponent } from './modal-window/modal-window.component';

@Component({
  selector: 'app-get-car',
  templateUrl: './get-car.component.html',
  styleUrls: ['./get-car.component.css']
})
export class GetCarComponent implements OnInit {

  constructor(private service: MainService, private _activatedRoute: ActivatedRoute,public dialog: MatDialog) { }

   car: Car;
  
   id: number;
   message:string
   
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
  clickPlaceBid(id:number){
    
    this.service.placeBid(id).subscribe(
      data=>{
        this.message = "Ваша ставка принята!"
          this.dialog.open(ModalWindowComponent, {
            data: {message:this.message}
          });
      },
      error=>{
        if(error.error.errorCode != ""){
          this.message = "Вы не можете сделать ставку!"
          this.dialog.open(ModalWindowComponent, {
            data: {message:this.message}
          });
        }
          
        
        
      }
    );
    
    
    
  }


}

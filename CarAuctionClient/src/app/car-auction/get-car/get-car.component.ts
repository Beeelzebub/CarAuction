import { Component, Input, OnInit } from '@angular/core';
import { MainService } from 'src/app/shared/services/main.service'; 
import { ActivatedRoute } from '@angular/router';
import { Car } from 'src/app/shared/models/car.model';

@Component({
  selector: 'app-get-car',
  templateUrl: './get-car.component.html',
  styleUrls: ['./get-car.component.css']
})
export class GetCarComponent implements OnInit {

  constructor(private service: MainService, private _activatedRoute: ActivatedRoute) { }

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

    this.service.getOneCar(id).subscribe(
      data=>
      this.car= data.data
    )
  }
  clickPlaceBid(id:number){
    
    this.service.placeBid(id).subscribe(
      data=>{},
      error=>{
        console.log(`error status : ${error.error.errorCode}`);
        if(error.status == 400){
          confirm(`fuck, status code: ${error.error.errorCode}!!!!!!`);
        }
        
      }
    );
  }


}

import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Car } from 'src/app/shared/models/car.model';
import { ProfileService } from 'src/app/shared/services/profile.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-get-one-car-profile',
  templateUrl: './get-one-car-profile.component.html',
  styleUrls: ['./get-one-car-profile.component.css']
})
export class GetOneCarProfileComponent implements OnInit {

  constructor(private service: ProfileService, private _activatedRoute: ActivatedRoute, private router: Router) { }

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
  deleteClick(id: number){
    this.service.deleteCar(id).subscribe(data =>{
      this.router.navigate(['/myCars']);
    });
  }

}

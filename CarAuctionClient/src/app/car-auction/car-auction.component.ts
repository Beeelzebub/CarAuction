import { Component, OnInit } from '@angular/core';
import { MainService } from '../shared/services/main.service'; 
import { Car } from '../shared/models/car.model';
import { CarsParameters } from '../shared/models/cars-parameters.model';

@Component({
  selector: 'app-car-auction',
  templateUrl: './car-auction.component.html',
  styles: [
  ]
})
export class CarAuctionComponent implements OnInit {

  constructor(private service: MainService) { }
  carsList:Car[];
  carsListByCondition:Car[];
  carListCount: boolean;
  carParam: CarsParameters;
  models:string[];
  brands:string[];
  mbdata:any;
  brandName:string = "";
  modelName:string = "";
  minYear:number=0;
  maxYear:number=2021;


  ngOnInit(): void {
    this.refreshList();
    this.getModelsWithBrands();
    var currentUser = JSON.parse(localStorage.getItem('currentUser') || '');
  }


  refreshList(){
    this.service.listCars().subscribe(data =>{
      this.carsList = data;
      
      if(this.carsList.length < 1){
        this.carListCount = false;
      }
      else{
        this.carListCount = true;
      }
    });
    
  }
  getCarByCondition(modelName: string, brandName:string, minYear:number, maxYear: number){
    this.carParam = {ModelName: modelName, BrandName: brandName, minYear: minYear, maxYear: maxYear}; 
    this.service.listCarsByCondition(this.carParam).subscribe(data =>{
      this.carsList = data;
      
      if(this.carsList.length < 1){
        this.carListCount = false;
      }
      else{
        this.carListCount = true;
      }
      console.log(minYear, maxYear)
    });
    
  }
  getModelsWithBrands(){
    this.service.getModelsWithBrands().subscribe(data=>{
       this.models = data.modelNames;
       this.brands = data.brandNames;
       
    });
  }
  dropClick(){
    this.brandName = "";
    this.modelName = "";
    this.minYear=0;
    this.maxYear=2021;
    this.refreshList();
  }

}

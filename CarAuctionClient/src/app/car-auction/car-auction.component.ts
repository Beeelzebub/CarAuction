import { Component, OnInit } from '@angular/core';
import { MainService } from '../shared/services/main.service'; 
import { Car } from '../shared/models/car.model';
import { CarsParameters } from '../shared/models/cars-parameters.model';

@Component({
  selector: 'app-car-auction',
  templateUrl: './car-auction.component.html',
  styleUrls: ['./car-auction.component.css']
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
  totalPages:number;
  currentPage:number;
  isFiltering: boolean = false;


  ngOnInit(): void {
    this.refreshList();
    this.getModelsWithBrands();
  }

  createRange(){
    var items: number[] = [];
    for(var i = 1; i <= this.totalPages; i++){
      items.push(i);
    }
    return items;
    
  }


  refreshList(page?: number){
    this.isFiltering = false;
    this.currentPage = page || 1;
    this.service.listCars(this.currentPage).subscribe(data =>{
      this.carsList = data.data.items;
      this.totalPages = data.data.totalPages
      if(this.carsList.length < 1){
        this.carListCount = false;
      }
      else{
        this.carListCount = true;
      }
    });
    
  }
  getCarByCondition(modelName: string, brandName:string, minYear:number, maxYear: number, page?: number){
    this.isFiltering = true;
    this.currentPage = page || 1;
    this.carParam = {ModelName: modelName, BrandName: brandName, minYear: minYear, maxYear: maxYear}; 
    this.service.listCarsByCondition(this.carParam, this.currentPage).subscribe(data =>{
      this.carsList = data.data.items;
      this.totalPages = data.data.totalPages
      if(this.carsList.length < 1){
        this.carListCount = false;
      }
      else{
        this.carListCount = true;
      }
    });
    
  }
  getModelsWithBrands(){
    this.service.getModelsWithBrands().subscribe(data=>{
       this.models = data.data.modelNames;
       this.brands = data.data.brandNames;
       
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

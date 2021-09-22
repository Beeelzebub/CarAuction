import { Fuel } from "./enums/fuel";
import { CarBody } from "./enums/car-body";
import { DriveUnit } from "./enums/drive-unit";

export class  ModelForCreatingLot {
        Year:number;
        Image:File;
        Fuel: Fuel;

        CarBody: CarBody;
        DriveUnit: DriveUnit;
        
        Name:string ;
        BrandName:string;
        MinimalStep: number;
        StartingPrice:number;
        RedemptionPrice:number;
}

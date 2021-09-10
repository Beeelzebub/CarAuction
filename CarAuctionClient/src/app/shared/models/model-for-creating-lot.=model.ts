import { Fuel } from "./enums/fuel";
import { CarBody } from "./enums/car-body";
import { DriveUnit } from "./enums/drive-unit";

export class ModelForCreatingLot {
        Year:number
        ImageUrl:string
        Fuel: Fuel

        CarBody: CarBody
        DriveUnit: DriveUnit
        
        ModelId:number
        BrandId:number
        MinimalStep: number
        StartingPrice:number
        RedemptionPrice:number
}

export class BidDto {
    bidStatus: number = 0;
    image: string = '';
    year: number = 0;
    fuel: string = '';
    modelName:string = '';
    brandName: string = '';
    carBody: string = '';
    driveUnit: string = '';
    startDate: Date;
    endDate: Date;
    minimalStep: number;
    startingPrice: number;
    currentCost: number;
    redemptionPrice:number;
}

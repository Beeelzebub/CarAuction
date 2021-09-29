import { Component, OnInit } from '@angular/core';
import { ProfileService } from '../shared/services/profile.service';
import { BidDto } from '../shared/models/bid-dto.model';

@Component({
  selector: 'app-my-bids',
  templateUrl: './my-bids.component.html',
  styleUrls: ['./my-bids.component.css']
})
export class MyBidsComponent implements OnInit {

  constructor(private service: ProfileService) { }

  bids:BidDto[];

  ngOnInit(): void {
    this.getBids();
  }

  getBids(){
    this.service.getBids().subscribe(data=>{
      this.bids = data.data;
    });
  }


}

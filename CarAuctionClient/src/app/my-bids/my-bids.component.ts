import { Component, OnInit } from '@angular/core';
import { ProfileService } from '../shared/services/profile.service';

@Component({
  selector: 'app-my-bids',
  templateUrl: './my-bids.component.html',
  styleUrls: ['./my-bids.component.css']
})
export class MyBidsComponent implements OnInit {

  constructor(private service: ProfileService) { }

  bids:any[];

  ngOnInit(): void {
    this.getBids();
  }

  getBids(){
    this.service.getBids().subscribe(data=>{
      console.log(data.data);
      this.bids = data.data;
    });
  }


}

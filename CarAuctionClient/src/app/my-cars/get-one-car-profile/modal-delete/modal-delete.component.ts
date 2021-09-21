import { Component, Inject, OnInit } from '@angular/core';
import { ProfileService } from 'src/app/shared/services/profile.service';
import { Router } from '@angular/router';
import {MAT_DIALOG_DATA} from '@angular/material/dialog';
import { GetOneCarComponent } from 'src/app/admin/get-one-car/get-one-car.component';

@Component({
  selector: 'app-modal-delete',
  templateUrl: './modal-delete.component.html',
  styleUrls: ['./modal-delete.component.css']
})
export class ModalDeleteComponent implements OnInit {

  id:number;

  constructor(private service: ProfileService, private router: Router, @Inject(MAT_DIALOG_DATA) public data: GetOneCarComponent) {
    this.id = data.id;
   }

  ngOnInit(): void {
  }

  deleteClick(){
    this.service.deleteCar(this.id).subscribe(() =>{
      this.router.navigate(['/myCars']);
    });
  }

}

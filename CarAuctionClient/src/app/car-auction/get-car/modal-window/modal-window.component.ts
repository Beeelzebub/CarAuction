import { Component, Inject, Input, OnInit } from '@angular/core';
import {MAT_DIALOG_DATA} from '@angular/material/dialog';
import { GetCarComponent } from '../get-car.component';

@Component({
  selector: 'app-modal-window',
  templateUrl: './modal-window.component.html',
  styleUrls: ['./modal-window.component.css']
})
export class ModalWindowComponent implements OnInit {
  message:string;


  constructor(@Inject(MAT_DIALOG_DATA) public data: GetCarComponent) {
    this.message = data.message;
   }

  ngOnInit(): void {
  }

}

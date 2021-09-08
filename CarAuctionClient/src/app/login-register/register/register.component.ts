import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { FormBuilder } from '@angular/forms';
import { Validators } from '@angular/forms';
import { CarAuctionService } from 'src/app/shared/car-auction.service'; 

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  public form:FormGroup;

  constructor(private fb:FormBuilder, public service: CarAuctionService) {
    this.form = this.fb.group({
      name: ['', Validators.required],
      userName: ['', Validators.required],
      password: ['', Validators.required]
  });
   }

  ngOnInit(): void {
  }

  register() {
    const val = this.form.value;

    if (val.name && val.userName && val.password) {
        this.service.registration(val.name, val.userName, val.password)
            .subscribe(
                () => {
                    console.log("User is register"); 
                }
            );
    }
    this.form.reset();
  }

}
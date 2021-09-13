import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { FormBuilder } from '@angular/forms';
import { Validators } from '@angular/forms';
import { AuthService } from 'src/app/shared/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  public form:FormGroup;

  constructor(private fb:FormBuilder, public service: AuthService, private router: Router) {
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
                    if (val.userName === "admin") {
                      this.router.navigate(['admin/cars']);
                    }
                    else{
                      this.router.navigate(['']);
                
                    }
                }
            );
    }
    this.form.reset();
  }

}

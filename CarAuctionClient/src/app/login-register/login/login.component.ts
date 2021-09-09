import { createUrlResolverWithoutPackagePrefix } from '@angular/compiler';
import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { FormBuilder } from '@angular/forms';
import { Validators } from '@angular/forms';
import { AuthService } from 'src/app/shared/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  public form:FormGroup;

  token:any;
  tokenString: string;

  constructor(private fb:FormBuilder, public service: AuthService) {
    this.form = this.fb.group({
      userName: ['', Validators.required],
      password: ['', Validators.required]
  });
   }

  ngOnInit(): void {
  }



  login() {
    const val = this.form.value;

    if (val.userName && val.password) {
        this.service.login(val.userName, val.password)
            .subscribe(
                data => {
                    this.token = data
                    this.tokenString = this.token.token
                    localStorage.setItem('currentUser', JSON.stringify({ token: this.tokenString, name: this.tokenString }));
                    console.log(this.tokenString);
                }
            );
    } 
    this.form.reset();
  }
}

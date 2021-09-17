import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { AuthService } from 'src/app/shared/services/auth.service';
import { Router } from '@angular/router';
import { DictionaryError } from 'src/app/shared/models/dictionary-error.dictionary';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {


  token:any;
  tokenString: string;
  userName:string;
  password:string;
  message:string;
  dictionary: DictionaryError = new DictionaryError();
  

  constructor(public service: AuthService, private router: Router) {
   }

  ngOnInit(): void {
  }



  login(userName: string, password: string) {
    this.service.login(userName, password).subscribe(
                data => {
                    this.token = data
                    this.tokenString = this.token.data.token
                    localStorage.setItem('currentUser', JSON.stringify({ token: this.tokenString, name: this.tokenString }));
                    this.service.isAuthorithed = true;
                    if (userName === "admin") {
                      this.router.navigate(['admin/cars']).then(()=>window.location.reload());
                    }
                    else  this.router.navigate(['']).then(()=>window.location.reload()); 
                },
                error=>{
                  if(error.error.errorCode != ""){
                    this.message = this.dictionary.dictionary['4']
                  }
                }
            );
    
  }
}

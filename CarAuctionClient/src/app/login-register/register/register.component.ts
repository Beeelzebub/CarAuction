import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/shared/services/auth.service';
import { Router } from '@angular/router';
import { DictionaryError } from 'src/app/shared/models/dictionary-error.dictionary';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {


  token:any;
  tokenString: string;
  userName:string;
  name:string;
  password:string;
  message:string;
  dictionary: DictionaryError = new DictionaryError();

  constructor(public service: AuthService, private router: Router) {};
   

  ngOnInit(): void {
  }

  register(name:string, userName:string, password:string) {
 
        this.service.registration(name, userName, password)
            .subscribe(
                data => {
                  this.token = data
                  this.tokenString = this.token.data.token
                  localStorage.setItem('currentUser', JSON.stringify({ token: this.tokenString, name: this.tokenString }));
                  this.service.isAuthorithed = true;
                  this.router.navigate(['']).then(()=>window.location.reload());
                },
                error=>{
                  if(error.error.errorCode != ""){
                    this.message = this.dictionary.dictionary['5']
                  }
                }
            );
  }

}

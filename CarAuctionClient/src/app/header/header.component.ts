import { Component, OnInit } from '@angular/core';
import { LoginComponent } from '../login-register/login/login.component';
import { AuthService } from '../shared/services/auth.service';
import {MatDialog} from '@angular/material/dialog';
import { RegisterComponent } from '../login-register/register/register.component';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  constructor( public service: AuthService,public dialog: MatDialog) { }

  

  ngOnInit(): void {
    this.checkIsAuthorithed();
  }


  checkIsAuthorithed(){
    var currentUser = JSON.parse(localStorage.getItem('currentUser') || '');
    if(currentUser.token == ''){
      this.service.isAuthorithed = true;
    }
    else this.service.isAuthorithed= false;
  }
  openLogin(){
    this.dialog.open(LoginComponent, {width:'500px'});
  }
  openRegister(){
    this.dialog.open(RegisterComponent, {width:'500px'});
  }
  

}

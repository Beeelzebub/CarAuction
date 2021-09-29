import { Component, OnInit } from '@angular/core';
import { LoginComponent } from '../login-register/login/login.component';
import { AuthService } from '../shared/services/auth.service';
import {MatDialog} from '@angular/material/dialog';
import { RegisterComponent } from '../login-register/register/register.component';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  constructor( public service: AuthService,public dialog: MatDialog, private router: Router) { }

  

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
  openMyCars(){
    if(this.service.isAuthorithed==true){
      this.dialog.open(LoginComponent, {width:'500px'});
    }
    else{
      this.router.navigate(['profile/myCars']);
    }
  }
  openMyBids(){
    if(this.service.isAuthorithed==true){
      this.dialog.open(LoginComponent, {width:'500px'});
    }
    else{
      this.router.navigate(['profile/myBids']);
    }
  }
  

}

import { Component, OnInit } from '@angular/core';
import { AuthService } from '../shared/services/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  constructor( public service: AuthService) { }

  

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
  

}

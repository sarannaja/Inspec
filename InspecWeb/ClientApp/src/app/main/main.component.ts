import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { UserManager } from 'oidc-client';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent implements OnInit {
  email:string =''
  role_id:any;
  constructor(private router:Router , private authorize:AuthorizeService, private userManager: UserManager) { }

  ngOnInit() {
  
    
    this.authorize.getUser()
    .subscribe(result=>{
      this.email = result.name
      this.role_id = result.role_id
      //alert(this.role_id);
      console.log("user",result);
    })
  }
  Logout(){
    this.authorize.signOut({ local: true })
  }
  
}

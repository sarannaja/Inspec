import { Component, OnInit } from '@angular/core';
import { superAdmin } from './_nav';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
@Component({
  selector: 'app-default-layout',
  templateUrl: './default-layout.component.html',
  styleUrls: ['./default-layout.component.css']
})
export class DefaultLayoutComponent implements OnInit {
  classIcon = "align-middle mr-2 fas fa-fw "
  urlActive = ""
  classtap = 'sidebar-header'
  userid : any
  role_id : any
  nav : any
  // childClassIcon = "align-middle mr-2 fas fa-fw 
  constructor( private authorize: AuthorizeService) { }
  // 0C-54-15-66-C2-D6
  ngOnInit() {
    this.authorize.getUser()
    .subscribe(result => {
      this.userid = result.sub
      this.role_id = result.role_id

      if(this.role_id == 1){
        this.nav = superAdmin
      }else if(this.role_id == 2 ){
        this.nav = superAdmin
      }else if(this.role_id ==3){
        this.nav = superAdmin
      }else if(this.role_id ==4){
        this.nav = superAdmin
      }else if(this.role_id == 5){
        this.nav = superAdmin
      }else if(this.role_id == 6){
        this.nav = superAdmin
   
       }else{
        this.nav = superAdmin
       }
      console.log(result);
     // alert(this.Role_id)
    })
   this.urlActive = this.nav[0].url
  }
  checkactive(url){ 
    this.urlActive = url
    console.log('in');
    
  }
 Logout(){
    this.authorize.signOut({ local: true })
  }
}

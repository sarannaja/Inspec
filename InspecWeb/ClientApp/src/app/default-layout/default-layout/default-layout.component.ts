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
  Role_id : any
  //this.Role_id;
    // if(this.Role_id == 1){
     nav = superAdmin
    //  }else if(){
    //  }else if(){
    //  }else if(){
    //  }else if(){
    //  }else if(){
  
    //  }else{
  
    //  }

  // childClassIcon = "align-middle mr-2 fas fa-fw 
  constructor( private authorize: AuthorizeService) { }
  // 0C-54-15-66-C2-D6
  ngOnInit() {
    this.authorize.getUser()
    .subscribe(result => {
      this.userid = result.sub,
      this.Role_id = result.Role_id
      console.log(result);
     // alert(this.Role_id)
    })
   this.urlActive = this.nav[0].url
  }
  checkactive(url){ 
    this.urlActive = url
    console.log('in');
    
  }

}

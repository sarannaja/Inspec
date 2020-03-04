import { Component, OnInit } from '@angular/core';
import { superAdmin } from './_nav'
@Component({
  selector: 'app-default-layout',
  templateUrl: './default-layout.component.html',
  styleUrls: ['./default-layout.component.css']
})
export class DefaultLayoutComponent implements OnInit {
  nav = superAdmin
  classIcon = "align-middle mr-2 fas fa-fw "
  urlActive = ""
  classtap = 'sidebar-header'
  // childClassIcon = "align-middle mr-2 fas fa-fw 
  constructor() { }
  // 0C-54-15-66-C2-D6
  ngOnInit() {
   this.urlActive = this.nav[0].url
  }
  checkactive(url){ 
    this.urlActive = url
    console.log('in');
    
  }

}

import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { superAdmin } from './_nav';
import { Centraladmin } from './_nav';
import { Inspector } from './_nav';
import { Provincialgovernor } from './_nav';
import { Adminprovince } from './_nav';
import { InspectorMinistry } from './_nav';
import { publicsector } from './_nav';
import { president } from './_nav';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { UserService } from 'src/app/services/user.service';
// import { Router } from '@angular/router';
@Component({
  selector: 'app-default-layout',
  templateUrl: './default-layout.component.html',
  styleUrls: ['./default-layout.component.css']
})
export class DefaultLayoutComponent implements OnInit {
  classIcon = "align-middle mr-2 fas fa-fw "
  urlActive = ""
  classtap = 'sidebar-header'
  userid: any
  role_id: any
  nav: any
  // childClassIcon = "align-middle mr-2 fas fa-fw

  constructor(
    private authorize: AuthorizeService,
    private userService: UserService,
    private router: Router
  ) { }
  // 0C-54-15-66-C2-D6
  ngOnInit() {
    this.nav = superAdmin
    this.authorize.getUser()

      .subscribe(result => {
        this.userid = result.sub
        this.role_id = result.role_id
        if (this.role_id == 1) {
          this.nav = superAdmin
        } else if (this.role_id == 2) {
          this.nav = Centraladmin
        } else if (this.role_id == 3) {
          this.nav = Inspector
        } else if (this.role_id == 4) {
          this.nav = Provincialgovernor
        } else if (this.role_id == 5) {
          this.nav = Adminprovince
        } else if (this.role_id == 6) {
          this.nav = InspectorMinistry
        } else if (this.role_id == 7) {
          this.nav = publicsector
        } else if (this.role_id == 8) {
          this.nav = president
        }
        // console.log(result);
      })
    this.checkactive(this.nav[0].url);
    // this.urlActive = this.nav[0].url
  }
  checkactive(url) {
    this.urlActive = url
    // console.log('in');
  }
  userNav(url, id): void {
    this.router.navigate([url])
    // send message to subscribers via observable subject
    this.userService.sendNav(id);
  }
  Logout() {
    this.authorize.signOut({ local: true })
  }
}

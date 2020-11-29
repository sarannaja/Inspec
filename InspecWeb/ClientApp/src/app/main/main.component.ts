import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { ExcelService } from '../services/excel.service';
import { WordService } from '../services/word.service';
import { CookieService } from 'ngx-cookie-service';
import { NotofyService } from '../services/notofy.service';
import { ExternalOrganizationService } from '../services/external-organization.service';
import * as moment from 'moment';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent implements OnInit {
  exportregistration: any = []
  email: string = ''
  role_id: any;
  userid: any;
  name = 'Angular 6';

  data: any = [{
    eid: 'e101',
    ename: 'ravi',
    esal: 1000
  },
  {
    eid: 'e102',
    ename: 'ram',
    esal: 2000
  },
  {
    eid: 'e103',
    ename: 'rajesh',
    esal: 3000
  }];


  constructor(
    private router: Router,
    private authorize: AuthorizeService,
    private wordService: WordService,
    private excelService: ExcelService,
    private _CookieService: CookieService,
    private _external: ExternalOrganizationService,

    private _notify: NotofyService
  ) {
    this.authorize.getUser()
      .subscribe(result => {
        this.email = result.name
        this.role_id = result.role_id
        this.userid = result.sub
        // window.postMessage(result.sub, result.sub)
        moment.fn.toISOString = function () {
          return '/Date(' + (+this) + this.format('ZZ') + ')';
        }
        result ? console.log(
          new Date(Date.now()),
          moment("/Date(" + moment(new Date(result.auth_time).toUTCString()).valueOf() + moment(new Date(result.auth_time).toUTCString()).format("ZZ") + ")/").format("YYYY-MM-DD hh:mmA")

          ,
          'result.auth_time')
          : ''
        this._CookieService.set('UserIdMobile', result.sub)
        // console.log('UserMMo', result);

        // this._external.putUserToken(result).subscribe(result => {
        //  // console.log('resultUserMMo', result);

        // })
        // this.setUserCookie(result.sub)
        // console.log("this._CookieService.get('idsrv.session')", this._CookieService.get('idsrv.session'));


        //alert(this.role_id);
        //console.log("user", result);
      })
  }

  ngOnInit() {

    // alert(this.role_id)
    // this.exportExcel();

  }
  Logout() {
    this.authorize.signOut({ local: true })
  }
  exportAsXLSX(): void {
    this.excelService.exportAsExcelFile(this.exportregistration, 'sample');
  }

  exportExcel() {
    this.wordService.exportExcel().subscribe(results => {
      // alert("1" + JSON.stringify(results))
      this.exportregistration = results
      // alert("2" + JSON.stringify(this.exportregistration))
      // console.log("res: ", this.exportregistration);

    })
  }

  async setUserCookie(userid: string) {
    // console.log("in UserIdMobiasle", userid);

    await this._CookieService.set('UserIdMobile', userid)
    console.log(this._CookieService.get('UserIdMobile'));

  }




  test() {
    this._notify.onSuccess('test')
  }
}
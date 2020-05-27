import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
// import { UserManager } from 'oidc-client';
import { ExcelService } from '../services/excel.service';
import { WordService } from '../services/word.service';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent implements OnInit {
  exportregistration: any = []
  email: string = ''
  role_id: any;
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
  
  constructor(private router:Router , private authorize:AuthorizeService,private excelService:ExcelService) { }

  constructor(private router: Router,
    private authorize: AuthorizeService,
    private userManager: UserManager,
    private wordService: WordService,
    private excelService: ExcelService) { }

  ngOnInit() {

    this.authorize.getUser()
      .subscribe(result => {
        this.email = result.name
        this.role_id = result.role_id
        //alert(this.role_id);
        console.log("user", result);
      })
      this.exportExcel();

  }
  Logout() {
    this.authorize.signOut({ local: true })
  }
  exportAsXLSX(): void {
    this.excelService.exportAsExcelFile(this.exportregistration, 'sample');
  }

  exportExcel(){

    this.wordService.exportExcel().subscribe(results => {
      // alert("1" + JSON.stringify(results))
      this.exportregistration = results
      // alert("2" + JSON.stringify(this.exportregistration))
      console.log("res: ", this.exportregistration);


    })
  }
}

import { Component, OnInit } from '@angular/core';
import { ProvinceService } from '../services/province.service';
import { BsModalRef,  } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgxSpinnerService } from "ngx-spinner";
import { AuthorizeService } from 'src/api-authorization-new/authorize.service';

@Component({
  selector: 'app-infomation-province',
  templateUrl: './infomation-province.component.html',
  styleUrls: ['./infomation-province.component.css']
})
export class InfomationProvinceComponent implements OnInit {
  resultprovince: any[] = []
  delid: any
  name: any
  link: any
  modalRef: BsModalRef;
  Form: FormGroup;
  EditForm: FormGroup;
  loading = false;
  dtOptions: any = {};
  forbiddenUsernames = ['admin', 'test', 'xxxx'];
  url = "";
  userid: any;

  constructor(
    private fb: FormBuilder,
    private provinceservice: ProvinceService,
    public share: ProvinceService,
    private router: Router,
    private spinner: NgxSpinnerService,
    private authorize: AuthorizeService,

  ) { }

  ngOnInit() {
    this.getdata();
    this.authorize.getUser()
      .subscribe(result => {
        this.userid = result.sub

    })
  }

  getdata(){
    this.spinner.show();
    this.provinceservice.getprovincedata()
      .subscribe(result => {
        this.spinner.hide();
        this.resultprovince = result
        this.loading = true
       // console.log("momo",this.resultprovince);
      })
  }

  District(id) {
    if (this.userid == null) {
      this.router.navigate(['/infodistrictmain', id])
    } else {
      this.router.navigate(['/infodistrict', id])
    }

  }

   //<!-- excel -->
   excel(){
    window.location.href = '/api/province/excelprovince';
  }
  //<!-- END excel -->

  //<!-- Word -->
  word() {
    this.provinceservice.wordprovince()
      .subscribe(result => {
        //alert(result.data);
        //console.log('result', result);
        window.open("reportprovince/" + result.data);
        this.getdata();
      })
  }
  //<!-- END Word -->
}

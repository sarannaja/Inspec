import { Component, OnInit } from '@angular/core';
import { ProvinceService } from '../services/province.service';
import { BsModalRef,  } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgxSpinnerService } from "ngx-spinner";

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
  dtOptions: DataTables.Settings = {};
  forbiddenUsernames = ['admin', 'test', 'xxxx'];
  url = "";

  constructor(
    private fb: FormBuilder,
    private provinceservice: ProvinceService,
    public share: ProvinceService,
    private router: Router,
    private spinner: NgxSpinnerService
    
  ) { }

  ngOnInit() {
    this.getdata();
  }

  getdata(){
    this.spinner.show();
    this.provinceservice.getprovincedata()
      .subscribe(result => {
        this.spinner.hide();
        this.resultprovince = result
        this.loading = true
       // console.log(this.resultprovince);
      })
  }

  District(id) {
    this.router.navigate(['/infodistrict', id])
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

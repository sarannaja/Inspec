import { Component, OnInit } from '@angular/core';
import { ProvinceService } from '../services/province.service';
import { BsModalRef,  } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgxSpinnerService } from "ngx-spinner";

@Component({
  selector: 'app-province',
  templateUrl: './province.component.html',
  styleUrls: ['./province.component.css']
})
export class ProvinceComponent implements OnInit {
  resultprovince: any[] = []
  delid: any
  name: any
  link: any
  modalRef: BsModalRef;
  EditForm: FormGroup;
  loading = false;
  dtOptions: DataTables.Settings = {};
  forbiddenUsernames = ['admin', 'test', 'xxxx'];

  constructor(
    private fb: FormBuilder,
    private provinceservice: ProvinceService,
    public share: ProvinceService,
    private router: Router,
    private spinner: NgxSpinnerService
    
  ) { }

  ngOnInit() {
    console.log("in");
    // this.onSuccess()
    // this.snotifyService.onSuccess("test")
    this.spinner.show();

    //แก้ไข


    this.provinceservice.getprovincedata()
      .subscribe(result => {
        this.spinner.hide();
        this.resultprovince = result
        this.loading = true
        console.log(this.resultprovince);
      })
  }

  District(id) {
    this.router.navigate(['/district', id])
  }

}

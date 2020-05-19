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

    this.Form = this.fb.group({
      "provincename": new FormControl(null, [Validators.required]),
      "provincelink": new FormControl(null, [Validators.required])
      // "test" : new FormControl(null,[Validators.required,this.forbiddenNames.bind(this)])
    })

    //แก้ไข


    this.provinceservice.getprovincedata()
      .subscribe(result => {
        this.spinner.hide();
        this.resultprovince = result
        this.loading = true
        console.log(this.resultprovince);
      })
    this.Form.patchValue({
      // test: "testest"
    })
  }

  District(id) {
    this.router.navigate(['/infodistrict', id])
  }

}

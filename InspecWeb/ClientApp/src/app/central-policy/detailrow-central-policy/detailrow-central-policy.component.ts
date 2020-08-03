import { Component, OnInit, Inject } from '@angular/core';
import { CentralpolicyService } from 'src/app/services/centralpolicy.service';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from 'src/app/services/user.service';
import { FormControl, Validators, FormGroup, FormBuilder, FormArray } from '@angular/forms';

import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FiscalyearService } from 'src/app/services/fiscalyear.service';
import { ProvinceService } from 'src/app/services/province.service';
import { IMyOptions, IMyDateModel } from 'mydatepicker-th';
import { NgxSpinnerService } from 'ngx-spinner';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import * as _ from 'lodash';

@Component({
  selector: 'app-detailrow-central-policy',
  templateUrl: './detailrow-central-policy.component.html',
  styleUrls: ['./detailrow-central-policy.component.css']
})
export class DetailrowCentralPolicyComponent implements OnInit {

  id: any;
  fiscalYearId: any;
  year: any;
  resultdetailcentralpolicy: any = []
  resultfiscalyear: any = []
  resultfiscalyearId: any = []
  fiscalYearIdString: any = [];
  resultprovince: any = []
  EditForm: FormGroup;
  selectdataprovince: Array<any>
  provinceId: any[];
  form: FormGroup;
  fileStatus = false;
  loading = false;
  startDate: any;
  endDate: any;
  delid: any;
  modalRef: BsModalRef;
  userid: string;
  oldProvince: any = [];
  addProvince: any = [];
  removeProvince: any = [];
  disable = true;
  selected: any = [];
  downloadUrl: any;

  constructor(
    private fb: FormBuilder,
    private modalService: BsModalService,
    private centralpolicyservice: CentralpolicyService,
    private userservice: UserService,
    private activatedRoute: ActivatedRoute,
    private fiscalyearservice: FiscalyearService,
    private provinceservice: ProvinceService,
    private router: Router,
    private spinner: NgxSpinnerService,
    private authorize: AuthorizeService,
    @Inject('BASE_URL') baseUrl: string
  ) {
    this.id = activatedRoute.snapshot.paramMap.get('id')
    this.form = this.fb.group({
      name: [''],
      files: [null]
    })
    this.downloadUrl = baseUrl + '/Uploads';
  }
  get f() { return this.EditForm.controls }
  get t() { return this.f.input as FormArray }
  get d() { return this.f.inputdate as FormArray }

  ngOnInit() {
    this.spinner.show();
    this.EditForm = this.fb.group({
      title: new FormControl(null),
      year: new FormControl(null),
      type: new FormControl(null),
      files: new FormControl(null),
      ProvinceId: new FormControl(null),
      status: new FormControl(),
      input: new FormArray([]),
      inputdate: new FormArray([])
    });
    this.t.push(this.fb.group({
      date: '',
      subject: '',
      questions: []
    }))

    this.getDetailCentralpolicy();
    this.getDataProvince();
  }
  getDetailCentralpolicy() {
    this.centralpolicyservice.getdetailcentralpolicydata(this.id)
      .subscribe(result => {
        this.resultdetailcentralpolicy = result;
        console.log("RES EDIT: ", this.resultdetailcentralpolicy);

        this.fiscalYearId = this.resultdetailcentralpolicy.fiscalYearId.toString();


        this.resultdetailcentralpolicy.centralPolicyDates.forEach(element => {
          console.log("element: ", element.startDate)
          const checkTimeStart = <FormArray>this.EditForm.get('inputdate') as FormArray;
          let sDate: Date = new Date(element.startDate);
          let eDate: Date = new Date(element.endDate)
          console.log("EEE", sDate);
        });

        this.resultdetailcentralpolicy.centralPolicyProvinces.forEach(element => {
          console.log("element: ", element);
          if (element.active == 1) {
            this.selected.push(element.provinceId);
            this.oldProvince.push(element.provinceId);
          }

        });
        console.log("SELECTED: ", this.selected);



        console.log("year: ", this.resultdetailcentralpolicy.fiscalYearId);


        this.EditForm.patchValue({
          title: this.resultdetailcentralpolicy.title,
          year: this.resultdetailcentralpolicy.fiscalYearId.toString(),
          type: this.resultdetailcentralpolicy.type,
          status: this.resultdetailcentralpolicy.status,
          ProvinceId: this.selected
        });
        this.EditForm.controls.ProvinceId.disable();
      });
  }
  getProvince() {
    this.provinceservice.getprovincedata().subscribe(result => {
      this.resultprovince = result
      this.selectdataprovince = this.resultprovince.map((item, index) => {
        return { value: item.id, label: item.name }
      })
      // this.spinner.hide();
    })
    this.loading = true;
  }
  back() {
    window.history.back();
  }
  getDataProvince() {
    this.provinceservice.getprovincedata()
      .subscribe(result => {
        this.selectdataprovince = result.map(result => {
          // console.log(
          //   result.name
          // );
          var region = this.provinceservice.getRegionMock()
            .filter(
              (thing, i, arr) => arr.findIndex(t => t.name === result.name) === i
            )[0].region
          // console.log(
          //   region
          // );
          return { ...result, region: region, label: result.name, value: result.id }
        })
        // console.log(this.selectdataprovince);
        this.spinner.hide();
      })
  }
}

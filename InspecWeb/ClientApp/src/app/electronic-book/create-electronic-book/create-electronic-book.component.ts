import { Component, OnInit, Inject, TemplateRef } from '@angular/core';
import { IMyOptions } from 'mydatepicker-th';
import { FormGroup, FormBuilder, FormArray, FormControl, Validators } from '@angular/forms';

import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { Router } from '@angular/router';
import { InspectionplaneventService } from 'src/app/services/inspectionplanevent.service';
import { UserService } from 'src/app/services/user.service';
import { CentralpolicyService } from 'src/app/services/centralpolicy.service';
import { SubjectService } from 'src/app/services/subject.service';
import { ProvinceService } from 'src/app/services/province.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ElectronicbookService } from 'src/app/services/electronicbook.service';
import { DepartmentService } from 'src/app/services/department.service';
import { FiscalyearService } from 'src/app/services/fiscalyear.service';
import { NotificationService } from 'src/app/services/notification.service';
import { InspectionplanService } from 'src/app/services/inspectionplan.service';

interface addInput {
  id: number;
  name: string;
}

@Component({
  selector: 'app-create-electronic-book',
  templateUrl: './create-electronic-book.component.html',
  styleUrls: ['./create-electronic-book.component.css']
})
export class CreateElectronicBookComponent implements OnInit {

  url: any;
  userid: any;
  form: FormGroup;
  EbookForm: FormGroup;
  fileStatus: any;
  centralPolicyEbook: Array<any>;
  inputdate: any = [{ start_date: '', end_date: '' }];
  fileType: any;

  get f() { return this.EbookForm.controls }
  get d() { return this.f.inputdate as FormArray }

  constructor(
    private fb: FormBuilder,
    private authorize: AuthorizeService,
    private router: Router,
    private inspectionplaneventservice: InspectionplaneventService,
    private userservice: UserService,
    private centralpolicyservice: CentralpolicyService,
    private subjectservice: SubjectService,
    private provinceservice: ProvinceService,
    private spinner: NgxSpinnerService,
    private modalService: BsModalService,
    private electronicBookService: ElectronicbookService,
    private departmentservice: DepartmentService,
    private fiscalyearservice: FiscalyearService,
    private notificationService: NotificationService,
    private inspectionplanservice: InspectionplanService,
    @Inject('BASE_URL') baseUrl: string
  ) {
    this.url = baseUrl;
  }

  ngOnInit() {
    this.authorize.getUser()
      .subscribe(result => {
        this.userid = result.sub
        console.log(result);
        // alert(this.userid)
      })

    this.form = this.fb.group({
      files: [null]
    })

    this.EbookForm = this.fb.group({
      centralPolicyEventId: new FormControl(null, [Validators.required]),
      inputdate: new FormArray([]),
      checkDetail: new FormControl(null, [Validators.required]),
      Problem: new FormControl(null, [Validators.required]),
      Suggestion: new FormControl(null, [Validators.required]),
      Status: new FormControl("ร่างกำหนดการ", [Validators.required]),
      fileType: new FormControl("เลือกประเภทเอกสารแนบ", [Validators.required]),
      description: new FormControl(null, [Validators.required]),
    })

    this.d.push(this.fb.group({
      start_date: '',
      end_date: '',
    }))

    this.getCentralPolicy();
  }

  goBack() {
    window.history.back();
  }

  uploadFile(event) {
    this.fileStatus = true;
    const file = (event.target as HTMLInputElement).files;
    this.form.patchValue({
      files: file
    });
    console.log("fff:", this.form.value.files)
    this.form.get('files').updateValueAndValidity()
  }

  getCentralPolicy() {
    this.electronicBookService.getCentralPolicyEbook().subscribe(res => {
      console.log("cenData: ", res);

      this.centralPolicyEbook = res.map((item, index) => {
        return {
          value: item.id,
          label: item.centralPolicy.title + "  -  " + "จังหวัด: " + item.inspectionPlanEvent.province.name
        }
      })
    })
  }

  storeElectronicBook(value) {
    console.log("Form: ", value);
    this.electronicBookService.createElectronicBook(value, this.form.value.files, this.userid).subscribe(res => {
      console.log("eBookRes: ", res);
      window.history.back();
    })
  }

  checkType(type) {
    // alert(type)
    this.fileType = type;
  }

  back() {
    window.history.back();
  }
}

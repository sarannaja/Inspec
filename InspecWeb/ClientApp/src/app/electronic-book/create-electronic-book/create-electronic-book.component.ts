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
import { ExternalOrganizationService } from 'src/app/services/external-organization.service';
import { NotofyService } from 'src/app/services/notofy.service';

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
  form2: FormGroup;
  EbookForm: FormGroup;
  EbookForm2: FormGroup;
  fileStatus: any;
  centralPolicyEbook: Array<any>;
  inputdate: any = [{ start_date: '', end_date: '' }];
  fileType: any;
  checkTypeCreate: any;
  provincialDepartmentData: any = [];
  listfiles: any = [];
  listfiles2: any = [];
  listfiles3: any = [];
  fileData: any = [{ ebookFile: '', fileDescription: '', type: '' }];
  fileData2: any = [{ ebookFile: '', fileDescription: '', type: '' }];
  province: any[] = [];
  selectedProvince = [];
  provinceData: any = [];
  submitted = false;
  submitted2 = false;

  get f() { return this.EbookForm.controls }
  get d() { return this.f.inputdate as FormArray }

  get s() { return this.f.fileData as FormArray }
  get g() { return this.f.fileData2 as FormArray }

  get f2() { return this.EbookForm2.controls }
  get d2() { return this.f2.inputdate as FormArray }

  get s2() { return this.f2.fileData as FormArray }
  get g2() { return this.f2.fileData2 as FormArray }

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
    private external: ExternalOrganizationService,
    private _NotofyService: NotofyService,
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

    this.form2 = this.fb.group({
      files2: [null]
    })

    this.EbookForm = this.fb.group({
      centralPolicyEventId: new FormControl("", [Validators.required]),
      inputdate: new FormArray([]),
      checkDetail: new FormControl("", [Validators.required]),
      Problem: new FormControl("", [Validators.required]),
      Suggestion: new FormControl("", [Validators.required]),
      Status: new FormControl("ร่างกำหนดการ", [Validators.required]),
      // fileType: new FormControl("เลือกประเภทเอกสารแนบ"),
      // description: new FormControl(null, [Validators.required]),
      // ebookType: new FormControl("สร้างจากแผนการตรวจราชการ", [Validators.required]),
      // provincialDepartment: new FormControl(null, [Validators.required]),
      // centralPolicy: new FormControl("", [Validators.required]),
      fileData: new FormArray([]),
      fileData2: new FormArray([]),
      SendToProvince: new FormControl(""),
      // ProvinceId: new FormControl(null, [Validators.required]),
    });

    this.EbookForm2 = this.fb.group({
      // centralPolicyEventId: new FormControl("", [Validators.required]),
      inputdate: new FormArray([]),
      checkDetail: new FormControl("", [Validators.required]),
      Problem: new FormControl("", [Validators.required]),
      Suggestion: new FormControl("", [Validators.required]),
      Status: new FormControl("ร่างกำหนดการ", [Validators.required]),
      // fileType: new FormControl("เลือกประเภทเอกสารแนบ", [Validators.required]),
      // description: new FormControl(null, [Validators.required]),
      // ebookType: new FormControl("สร้างจากแผนการตรวจราชการ", [Validators.required]),
      provincialDepartment: new FormControl("", [Validators.required]),
      centralPolicy: new FormControl("", [Validators.required]),
      fileData: new FormArray([]),
      fileData2: new FormArray([]),
      SendToProvince: new FormControl(""),
      ProvinceId: new FormControl("", [Validators.required]),
    });

    this.d.push(this.fb.group({
      start_date: '',
      end_date: '',
    }))

    this.d2.push(this.fb.group({
      start_date: '',
      end_date: '',
    }))

    // this.getCentralPolicy();
    this.getProvincialDepartment();
    this.getDataProvince();
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
    this.electronicBookService.getCentralPolicyEbook(this.userid).subscribe(res => {
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
    // console.log("File with desctiption: ", value);
    // console.log("checkType: ", this.checkTypeCreate);
    this.submitted = true;
    if (this.EbookForm.invalid) {
      console.log("in1");
      return;
    } else {
      // true = สร้างจากแผนการตรวจ
      if (this.checkTypeCreate == true) {
        console.log("Form: ", value);
        this.electronicBookService.createElectronicBook2(value, this.userid).subscribe(res => {
          console.log("eBookRes: ", res);

          this._NotofyService.onSuccess("เพื่มข้อมูล",);

          // if (value.fileData != null) {
          //   for (var iii = 0; iii < value.fileData.length; iii++) {
          //     console.log("Loop: ", value.fileData[iii]);
          //     const formData = new FormData();
          //     formData.append("files2", value.fileData[iii].ebookFile);
          //     formData.append("fileDescription2", value.fileData[iii].fileDescription);
          //     this.electronicBookService.addElectronicBookImage(formData, res.eBookID).subscribe(res => {
          //       console.log("imageRes: ", res);

          //     })
          //   }
          // }

          window.history.back();

        })
      } else {
        console.log("Form: ", value);
        this.electronicBookService.createElectronicBookOwn(value, this.form.value.files, this.userid)
          .subscribe(res => {
            console.log("eBookRes: ", res);
            this._NotofyService.onSuccess("เพื่มข้อมูล",);
            window.history.back();
          })
      }
    }
  }

  storeElectronicBook2(value) {
    // console.log("File with desctiption: ", value);
    // console.log("checkType: ", this.checkTypeCreate);
    this.submitted2 = true;
    if (this.EbookForm2.invalid) {
      console.log("in2");
      return;
    } else {
      // true = สร้างจากแผนการตรวจ
      if (this.checkTypeCreate == true) {
        console.log("Form: ", value);
        this.electronicBookService.createElectronicBook2(value, this.userid).subscribe(res => {
          console.log("eBookRes: ", res);

          this._NotofyService.onSuccess("เพื่มข้อมูล",);

          // if (value.fileData != null) {
          //   for (var iii = 0; iii < value.fileData.length; iii++) {
          //     console.log("Loop: ", value.fileData[iii]);
          //     const formData = new FormData();
          //     formData.append("files2", value.fileData[iii].ebookFile);
          //     formData.append("fileDescription2", value.fileData[iii].fileDescription);
          //     this.electronicBookService.addElectronicBookImage(formData, res.eBookID).subscribe(res => {
          //       console.log("imageRes: ", res);

          //     })
          //   }
          // }

          window.history.back();

        })
      } else {
        console.log("Form: ", value);
        this.electronicBookService.createElectronicBookOwn(value, this.form.value.files, this.userid)
          .subscribe(res => {
            console.log("eBookRes: ", res);
            this._NotofyService.onSuccess("เพื่มข้อมูล",);
            window.history.back();
          })
      }
    }
  }

  // storeElectronicBook2(value) {
  //   console.log("File with desctiption: ", value);
  //   if (this.checkTypeCreate == true) {
  //     console.log("Form: ", value);
  //     this.electronicBookService.createElectronicBook(value, this.userid).subscribe(res => {
  //       console.log("eBookRes: ", res);

  //       // if (value.fileData != null) {
  //       //   for (var iii = 0; iii < value.fileData.length; iii++) {
  //       //     console.log("Loop: ", value.fileData[iii]);
  //       //     const formData = new FormData();
  //       //     formData.append("files2", value.fileData[iii].ebookFile);
  //       //     formData.append("fileDescription2", value.fileData[iii].fileDescription);
  //       //     // this.electronicBookService.addElectronicBookImage(formData, res.eBookID).subscribe(res => {
  //       //     //   console.log("imageRes: ", res);

  //       //     // })
  //       //   }
  //       // }

  //       // window.history.back();

  //     })
  //   } else {
  //     console.log("Form: ", value);
  //     this.electronicBookService.createElectronicBookOwn(value, this.form.value.files, this.userid).subscribe(res => {
  //       console.log("eBookRes: ", res);
  //       // window.history.back();
  //     })
  //   }
  // }

  checkType(type) {
    // alert(type)
    this.fileType = type;
  }

  back() {
    window.history.back();
  }

  createFromCentralpolicy() {
    this.checkTypeCreate = true;
  }
  createOwn() {
    this.checkTypeCreate = false;
  }

  getProvincialDepartment() {
    this.electronicBookService.getProvincialDepartment().subscribe(res => {
      console.log("ProvincialDepartment: ", res);
      this.provincialDepartmentData = res.provincialDepartmentData.map((item, index) => {
        return {
          value: item.id,
          label: item.name
        }
      })
      this.provinceData = res.provinceData.map((item, index) => {
        return {
          value: item.id,
          label: item.name
        }
      })
    })
  }

  uploadFile2(event) {
    var file = (event.target as HTMLInputElement).files;
    for (let i = 0, numFiles = file.length; i < numFiles; i++) {
      this.listfiles.push(file[i])
      this.s.push(this.fb.group({
        ebookFile: file[i],
        fileDescription: '',
        type: file[i].type,
      }))
    }
    console.log("listfiles: ", this.EbookForm.value);
    console.log("eiei: ", this.s.controls);


    this.form.patchValue({
      files: this.listfiles
    });

    // console.log("listfiles", this.Formfile.get('files'));
    // this.Formfile.get('files').updateValueAndValidity()
  }

  uploadFile3(event) {
    var file = (event.target as HTMLInputElement).files;
    for (let i = 0, numFiles = file.length; i < numFiles; i++) {
      this.listfiles2.push(file[i])
      this.g.push(this.fb.group({
        ebookFile: file[i],
        fileDescription: '',
        type: file[i].type,
      }))
    }

    this.form2.patchValue({
      files2: this.listfiles
    });

    // console.log("listfiles", this.Formfile.get('files'));
    // this.Formfile.get('files').updateValueAndValidity()
  }

  uploadFile4(event) {
    var file = (event.target as HTMLInputElement).files;
    for (let i = 0, numFiles = file.length; i < numFiles; i++) {
      this.listfiles2.push(file[i])
      this.g2.push(this.fb.group({
        ebookFile: file[i],
        fileDescription: '',
        type: file[i].type,
      }))
    }

    this.form2.patchValue({
      files2: this.listfiles
    });

    // console.log("listfiles", this.Formfile.get('files'));
    // this.Formfile.get('files').updateValueAndValidity()
  }

  uploadFile5(event) {
    var file = (event.target as HTMLInputElement).files;
    for (let i = 0, numFiles = file.length; i < numFiles; i++) {
      this.listfiles.push(file[i])
      this.s2.push(this.fb.group({
        ebookFile: file[i],
        fileDescription: '',
        type: file[i].type,
      }))
    }
    console.log("listfiles: ", this.EbookForm.value);
    console.log("eiei: ", this.s.controls);


    this.form.patchValue({
      files: this.listfiles
    });

    // console.log("listfiles", this.Formfile.get('files'));
    // this.Formfile.get('files').updateValueAndValidity()
  }

  onStartDateChanged(value) {
    console.log("startDateChange: ", value);
    var startDate: any;
    startDate = value.date.year + '-' + value.date.month + '-' + value.date.day;
    console.log("Date ja: ", startDate);

    this.electronicBookService.getCentralPolicyEbook2(startDate, this.userid).subscribe(res => {
      console.log("cenData: ", res);

      this.centralPolicyEbook = res.map((item, index) => {
        return {
          value: item.id,
          label: item.centralPolicy.title + "  -  " + "จังหวัด: " + item.inspectionPlanEvent.province.name
        }
      })
      var selectedCentralPolicy = this.centralPolicyEbook.map(item => item.value);
      this.EbookForm.get('centralPolicyEventId').patchValue(selectedCentralPolicy);
    })
  }

  public onSelectAll() {
    var selected = this.province.map(item => item.id);
    this.EbookForm2.get('ProvinceId').patchValue(selected);
    this.selectedProvince = selected;
  }

  public onClearAll() {
    this.EbookForm2.get('ProvinceId').patchValue([]);
  }
  getDataProvince() {
    this.provinceservice.getprovincedata2()
      .subscribe(result => {
        this.external.getProvinceRegion()
          .subscribe(result2 => {
            this.province = result.map(result => {
              // console.log(
              //   result.name
              // );
              var region = result2.filter(
                (thing, i, arr) => arr.findIndex(t => t.name === result.name) === i
              )[0].region
              // console.log(
              //   region
              // );
              return { ...result, region: region, label: result.name, value: result.id }
            })
            // console.log(this.province);
          })
      })
    // console.log(this.provinceservice.getRegionMock());
  }

}

import { Component, OnInit, TemplateRef, Inject } from '@angular/core';
import { CentralpolicyService } from 'src/app/services/centralpolicy.service';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from 'src/app/services/user.service';
import { FormControl, Validators, FormGroup, FormBuilder, FormArray } from '@angular/forms';

import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FiscalyearService } from 'src/app/services/fiscalyear.service';
import { ProvinceService } from 'src/app/services/province.service';
import { IMyOptions, IMyDateModel } from 'mydatepicker-th';
import { NgxSpinnerService } from 'ngx-spinner';
import { AuthorizeService } from 'src/api-authorization-new/authorize.service';
import * as _ from 'lodash';
import { ExternalOrganizationService } from 'src/app/services/external-organization.service';
import { NotofyService } from 'src/app/services/notofy.service';
import { TypeexamibationplanService } from 'src/app/services/typeexamibationplan.service';
import { FiscalyearnewService } from 'src/app/services/fiscalyearnew.service';

@Component({
  selector: 'app-edit-central-policy',
  templateUrl: './edit-central-policy.component.html',
  styleUrls: ['./edit-central-policy.component.css']
})
export class EditCentralPolicyComponent implements OnInit {

  myDatePickerOptions: IMyOptions = {
    // other options...
    dateFormat: 'dd/mm/yyyy',
    showClearDateBtn: false
  };

  id: any;
  fiscalYearId: any;
  typeexamibationplanId: any
  year: any;
  resultdetailcentralpolicy: any = []
  resultfiscalyear: any = []
  resultfiscalyearId: any = []
  fiscalYearIdString: any = [];
  resultprovince: any = []
  resulttypeexamibationplan: any = []
  typeexamibationplanIdString: any = []
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
  oldSelected: any[] = []
  selected: any = [];
  downloadUrl: any;
  fileData: any = [{ CentralpolicyFile: '', fileDescription: '' }];
  listfiles: any = [];
  submitted = false;
  statusfile = true;

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
    private external: ExternalOrganizationService,
    private _NotofyService: NotofyService,
    private typeexamibationplanservice: TypeexamibationplanService,
    private fiscalyearnewservice: FiscalyearnewService,
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
  get s() { return this.f.fileData as FormArray }

  ngOnInit() {
    this.spinner.show();
    this.EditForm = this.fb.group({
      title: new FormControl("", [Validators.required]),
      year: new FormControl("", [Validators.required]),
      type: new FormControl("", [Validators.required]),
      files: new FormControl(null),
      ProvinceId: new FormControl(null, [Validators.required]),
      status: new FormControl(),
      input: new FormArray([]),
      inputdate: new FormArray([]),
      fileData: new FormArray([]),
      fileType: new FormControl("เลือกประเภทเอกสารแนบ", [Validators.required]),
    });


    this.t.push(this.fb.group({
      date: '',
      subject: '',
      questions: []
    }))

    // this.d.push(this.fb.group({
    //   start_date: '',
    //   end_date: '',
    // }))

    this.authorize.getUser()
      .subscribe(result => {
        this.userid = result.sub
        console.log(result);
        // alert(this.userid)
      })
    this.getDetailCentralpolicy()
    this.getFiscalyear()
    this.getTypeexamibationplan()
  }

  getDetailCentralpolicy() {

    this.centralpolicyservice.getdetailcentralpolicydata(this.id)
      .subscribe(result => {
        this.resultdetailcentralpolicy = result;
        console.log("RES EDIT: ", this.resultdetailcentralpolicy);

        this.fiscalYearId = this.resultdetailcentralpolicy.fiscalYearNewId.toString();
        this.typeexamibationplanId = this.resultdetailcentralpolicy.typeexaminationplanId.toString();

        if (this.statusfile == true) {
          this.resultdetailcentralpolicy.centralPolicyDates.forEach(element => {
            console.log("element: ", element.startDate)
            const checkTimeStart = <FormArray>this.EditForm.get('inputdate') as FormArray;
            let sDate: Date = new Date(element.startDate);
            let eDate: Date = new Date(element.endDate)
            console.log("EEE", sDate);

            this.d.push(this.fb.group({
              start_date: {
                year: sDate.getFullYear(),
                month: sDate.getMonth() + 1,
                day: sDate.getDate()
              },
              end_date: {
                year: eDate.getFullYear(),
                month: eDate.getMonth() + 1,
                day: eDate.getDate()
              }
            }))
            // console.log("check: ", this.d.controls);
          });
        }

        this.resultdetailcentralpolicy.centralPolicyProvinces.forEach(element => {
          console.log("element: ", element);
          if (element.active == 1) {
            this.selected.push(element.provinceId);
            this.oldSelected.push(element.provinceId);

            this.oldProvince.push(element.provinceId);
          }

        });
        console.log("SELECTED: ", this.selected);


        console.log("year: ", this.resultdetailcentralpolicy.fiscalYearNewId);


        this.EditForm.patchValue({
          title: this.resultdetailcentralpolicy.title,
          year: this.resultdetailcentralpolicy.fiscalYearNewId.toString(),
          type: this.resultdetailcentralpolicy.typeexaminationplanId.toString(),
          status: this.resultdetailcentralpolicy.status,
          ProvinceId: this.selected
        });
        this.getDataProvince()
      });
  }
  getFiscalyear() {
    this.fiscalyearnewservice.getdata().subscribe(result => {
      this.resultfiscalyear = result
      this.fiscalYearIdString = this.resultfiscalyear.map((item, index) => {
        return {
          id: item.id.toString(),
          year: item.year
        }
      })
      // console.log("fiscalyearString: ", this.fiscalYearIdString);

    })
  }
  getTypeexamibationplan() {
    this.typeexamibationplanservice.getdata().subscribe(result => {
      this.resulttypeexamibationplan = result
      this.typeexamibationplanIdString = this.resulttypeexamibationplan.map((item, index) => {
        return {
          id: item.id.toString(),
          name: item.name
        }
      })
    })
  }
  getProvince() {
    this.provinceservice.getprovincedata2().subscribe(result => {
      this.resultprovince = result
      this.selectdataprovince = this.resultprovince.map((item, index) => {
        return { value: item.id, label: item.name }
      })
      console.log("spinner");

      this.spinner.hide();
    })
    this.loading = true;
  }


  EditCentralpolicy(value) {
    console.log("Old Province: ", this.oldProvince);
    console.log("New Province: ", this.selected);
    console.log("files: ", this.form.value.files);

    this.removeProvince = _.differenceBy(this.oldProvince, this.selected);
    console.log("Remove Value => ", this.removeProvince);

    this.addProvince = _.differenceBy(this.selected, this.oldProvince);
    console.log("Add Value => ", this.addProvince);
    this.submitted = true;
    if (this.EditForm.invalid) {
      console.log("in1");
      return;
    } else {
      this.spinner.show();
      this.centralpolicyservice.editCentralpolicy(value, this.form.value.files, this.id, this.userid, this.removeProvince, this.addProvince)
        .subscribe(response => {
          console.log("res: ", response);
          this.EditForm.reset()
          this.spinner.hide();
          // this.router.navigate(['centralpolicy'])
          this._NotofyService.onSuccess("แก้ไขข้อมูล")
          window.history.back();
        })
    }

  }

  appenddate() {
    let date: Date = new Date();
    this.d.push(this.fb.group({
      // start_date: new FormControl("", [Validators.required]),
      // end_date: new FormControl("", [Validators.required]),
      // start_date: {
      //   year: date.getFullYear(),
      //   month: date.getMonth() + 1,
      //   day: date.getDate()
      // },
      // end_date: {
      //   year: date.getFullYear(),
      //   month: date.getMonth() + 1,
      //   day: date.getDate()
      // }
    }))
  }
  public onSelectAll() {
    var selected = this.selectdataprovince.map(item => item.id);
    this.EditForm.get('ProvinceId').patchValue(selected);
    this.selected = selected
  }

  public onClearAll() {
    this.EditForm.get('ProvinceId').patchValue([]);
  }
  public onClearUndo() {
    this.EditForm.get('ProvinceId').patchValue(this.oldSelected);
  }

  // deleteDate(i) {
  //   let date: Date = new Date();
  //   let dArray: any = [];
  //   this.d.value = this.fb.group({
  //     inputdate: this.fb.array([]),
  //   });
  //   this.d.value.forEach((element, index) => {
  //     if (index != i) {

  //       this.d.push(this.fb.group({
  //        element
  //       }))
  //     }
  //   });
  // }

  uploadFile(event) {
    this.fileStatus = true;
    const file = (event.target as HTMLInputElement).files;

    this.form.patchValue({
      files: file
    });
    console.log("fff:", this.form.value.files)
    this.form.get('files').updateValueAndValidity()
  }

  uploadFile2(event) {
    var file = (event.target as HTMLInputElement).files;
    for (let i = 0, numFiles = file.length; i < numFiles; i++) {
      this.listfiles.push(file[i])
      this.s.push(this.fb.group({
        CentralpolicyFile: file[i],
        fileDescription: '',
      }))
    }
    console.log("listfiles: ", this.EditForm.value);
    console.log("eiei: ", this.s.controls);


    this.form.patchValue({
      files: this.listfiles
    });

    // console.log("listfiles", this.Formfile.get('files'));
    // this.Formfile.get('files').updateValueAndValidity()
  }
  onStartDateChanged(event: IMyDateModel, index) {
    this.startDate = event.date;
    console.log("SS: ", this.startDate);

    this.d.value[index].start_date = this.startDate;
    console.log("check: ", this.d.value);
  }

  onEndDateChanged(event: IMyDateModel, index) {
    this.endDate = event.date;
    console.log("EE: ", this.endDate);

    this.d.value[index].end_date = this.endDate;
    console.log("check: ", this.d.value);
  }

  openModal(template: TemplateRef<any>, id) {
    this.delid = id;
    this.modalRef = this.modalService.show(template);
  }

  deleteFile() {
    // alert("onDelete");
    this.statusfile = false;
    this.centralpolicyservice.deleteFile(this.delid)
      .subscribe(response => {
        console.log("res: ", response);
        this.modalRef.hide();
        this.getDetailCentralpolicy();
      })
  }

  removeY(iy: any) {
    // this.d.removeAt(iy);
    console.log("iy: ", iy);

    this.d.removeAt(iy)
  }

  back() {
    window.history.back();
  }

  // getDataProvince() {
  //   this.provinceservice.getprovincedata2()
  //     .subscribe(result => {
  //       this.selectdataprovince = result.map(result => {
  //         console.log(
  //           result.name
  //         );
  //         var region = this.provinceservice.getRegionMock()
  //           .filter(
  //             (thing, i, arr) => arr.findIndex(t => t.name === result.name) === i
  //           )[0].region
  //         console.log(
  //           region
  //         );


  //         return { ...result, region: region, label: result.name, value: result.id }
  //       })
  //       console.log(this.selectdataprovince);

  //     })
  //   this.spinner.hide();
  //   console.log(this.provinceservice.getRegionMock());
  // }
  getDataProvince() {
    this.provinceservice.getprovincedata2()
      .subscribe(result => {
        this.external.getProvinceRegion()
          .subscribe(result2 => {
            this.selectdataprovince = result.map(result => {
              console.log(
                result.name
              );
              var region = result2.filter(
                (thing, i, arr) => arr.findIndex(t => t.name === result.name) === i
              )[0].region
              console.log(
                region
              );
              return { ...result, region: region, label: result.name, value: result.id }
            })
            this.selectdataprovince.sort((a, b) => {
              if(a.provincesGroupId > b.provincesGroupId) {
                return 1;
              } else if(a.provincesGroupId < b.provincesGroupId) {
                return -1;
              } else {
                return 0;
              }
            });
            console.log("test2: ", this.selectdataprovince);
            console.log(this.selectdataprovince);
            this.spinner.hide();
          })
      })
    // console.log(this.provinceservice.getRegionMock());
  }

}

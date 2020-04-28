import { Component, OnInit, TemplateRef, Inject } from '@angular/core';
import { CentralpolicyService } from 'src/app/services/centralpolicy.service';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from 'src/app/services/user.service';
import { FormControl, Validators, FormGroup, FormBuilder, FormArray } from '@angular/forms';
import { IOption } from 'ng-select';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FiscalyearService } from 'src/app/services/fiscalyear.service';
import { ProvinceService } from 'src/app/services/province.service';
import { IMyOptions, IMyDateModel } from 'mydatepicker-th';
import { NgxSpinnerService } from 'ngx-spinner';
import { AuthorizeService } from 'src/api-authorization/authorize.service';

@Component({
  selector: 'app-edit-central-policy',
  templateUrl: './edit-central-policy.component.html',
  styleUrls: ['./edit-central-policy.component.css']
})
export class EditCentralPolicyComponent implements OnInit {

  private myDatePickerOptions: IMyOptions = {
    // other options...
    dateFormat: 'dd/mm/yyyy',
  };

  id: any;
  fiscalYearId: any;
  year: any;
  resultdetailcentralpolicy: any = []
  resultfiscalyear: any = []
  resultfiscalyearId: any = []
  fiscalYearIdString: any = [];
  resultprovince: any = []
  EditForm: FormGroup;
  selectdataprovince: Array<IOption>
  provinceId: any[];
  form: FormGroup;
  fileStatus = false;
  loading = false;
  startDate: any;
  endDate: any;
  delid: any;
  modalRef: BsModalRef;
  userid: string

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
    this.getProvince()

  }

  getDetailCentralpolicy() {
    this.centralpolicyservice.getdetailcentralpolicydata(this.id)
      .subscribe(result => {
        this.resultdetailcentralpolicy = result;
        console.log("RES: ", this.resultdetailcentralpolicy);

        this.fiscalYearId = this.resultdetailcentralpolicy.fiscalYearId.toString();


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

        this.resultdetailcentralpolicy.centralPolicyProvinces.forEach(element => {
          //  console.log("element: ", element.province.id);
          this.selected.push(element.province.id)
        });
        console.log("SELECTED: ", this.selected);

        console.log("year: ", this.resultdetailcentralpolicy.fiscalYearId,);


        this.EditForm.patchValue({
          title: this.resultdetailcentralpolicy.title,
          year: this.resultdetailcentralpolicy.fiscalYearId.toString(),
          type: this.resultdetailcentralpolicy.type,
          status: this.resultdetailcentralpolicy.status
        });
      });
  }

  getFiscalyear() {
    this.fiscalyearservice.getfiscalyeardata().subscribe(result => {
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
  getProvince() {
    this.provinceservice.getprovincedata().subscribe(result => {
      this.resultprovince = result
      this.selectdataprovince = this.resultprovince.map((item, index) => {
        return { value: item.id, label: item.name }
      })
      this.spinner.hide();
    })
    this.loading = true;
  }


  EditCentralpolicy(value) {
    console.log("SUBMIT: ", value);
    console.log("files: ", this.form.value.files);

    this.centralpolicyservice.editCentralpolicy(value, this.form.value.files, this.id, this.userid)
    .subscribe(response => {
      console.log("res: ", response);
      this.EditForm.reset()
      this.router.navigate(['centralpolicy'])
    })
  }

  appenddate() {
    let date: Date = new Date();
    this.d.push(this.fb.group({
      start_date: {
        year: date.getFullYear(),
        month: date.getMonth() + 1,
        day: date.getDate()
      },
      end_date: {
        year: date.getFullYear(),
        month: date.getMonth() + 1,
        day: date.getDate()
      }
    }))
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
    // alert(this.delid);
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

}

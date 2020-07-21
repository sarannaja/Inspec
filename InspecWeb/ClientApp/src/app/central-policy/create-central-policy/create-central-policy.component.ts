import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators, FormArray } from '@angular/forms';
import { CentralpolicyService } from 'src/app/services/centralpolicy.service';
import { IMyOptions } from 'mydatepicker-th';
import { Router } from '@angular/router';
import { FiscalyearService } from 'src/app/services/fiscalyear.service';
import { ProvinceService, Province } from 'src/app/services/province.service';

import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { ExternalOrganizationService } from 'src/app/services/external-organization.service';

interface addInput {
  id: number;
  name: string;
}

@Component({
  selector: 'app-create-central-policy',
  templateUrl: './create-central-policy.component.html',
  styleUrls: ['./create-central-policy.component.css']
})
export class CreateCentralPolicyComponent implements OnInit {

  private myDatePickerOptions: IMyOptions = {
    // other options...
    dateFormat: 'dd/mm/yyyy',
  };

  // Initialized to specific date (09.10.2018).
  // private model: Object = { date: { year: 2018, month: 10, day: 9 } };
  files: string[] = []
  resultcentralpolicy: any = []
  resultfiscalyear: any = []
  resultprovince: any = []
  delid: any
  title: any
  start_date: any
  end_date: any
  Form: FormGroup;
  ProvinceId: any;
  selectdataprovince: Array<any>
  input: any = [{ date: '', subject: '', questions: '' }]
  inputdate: any = [{ start_date: '', end_date: '' }]
  form: FormGroup;
  progress: number = 0;
  userid: string
  province: any[] = [];
  selectedProvince = [];
  fileStatus: any;
  fileData: any = [{ CentralpolicyFile: '', fileDescription: '' }];
  listfiles: any = [];

  constructor(
    private fb: FormBuilder,
    private centralpolicyservice: CentralpolicyService,
    public share: CentralpolicyService,
    private router: Router,
    private fiscalyearservice: FiscalyearService,
    private provinceservice: ProvinceService,
    private authorize: AuthorizeService,
    private external: ExternalOrganizationService
  ) {
    this.form = this.fb.group({
      name: [''],
      files: [null]
    })
  }

  ngOnInit() {

    this.authorize.getUser()
      .subscribe(result => {
        this.userid = result.sub
        console.log(result);
        // alert(this.userid)
      })

    this.Form = this.fb.group({
      title: new FormControl(null, [Validators.required]),
      // start_date: new FormControl(null, [Validators.required]),
      // end_date: new FormControl(null, [Validators.required]),
      year: new FormControl(null, [Validators.required]),
      type: new FormControl(null, [Validators.required]),
      files: new FormControl(null, [Validators.required]),
      ProvinceId: new FormControl(null, [Validators.required]),
      status: new FormControl("ร่างกำหนดการ", [Validators.required]),
      input: new FormArray([]),
      inputdate: new FormArray([]),
      Class: new FormControl("แผนการตรวจประจำปี", [Validators.required]),
      fileData: new FormArray([]),
      fileType: new FormControl("เลือกประเภทเอกสารแนบ", [Validators.required]),
    })
    this.t.push(this.fb.group({
      date: '',
      subject: '',
      questions: []
    }))

    this.d.push(this.fb.group({
      start_date: '',
      end_date: '',
    }))
    //แก้ไข

    // this.provinceservice.getprovincedata()
    // .subscribe(result => {
    //   this.resultprovince = result

    //   this.selectdataprovince = this.resultprovince.map((item, index) => {
    //     return { value: item.id, label: item.name }
    //   })

    //   console.log(this.resultprovince);
    // })
    this.getDataProvince();
    this.fiscalyearservice.getfiscalyeardata().subscribe(result => {
      // alert(JSON.stringify(result))
      this.resultfiscalyear = result
      console.log(this.resultcentralpolicy);
    })

    this.Form.patchValue({
      // กรณีจะแก้ไข
    })
    // this.addInput()
  }
  inspec(event) {
    console.log(event);

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
  uploadFile2(event) {
    var file = (event.target as HTMLInputElement).files;
    for (let i = 0, numFiles = file.length; i < numFiles; i++) {
      this.listfiles.push(file[i])
      this.s.push(this.fb.group({
        CentralpolicyFile: file[i],
        fileDescription: '',
      }))
    }
    console.log("listfiles: ", this.Form.value);
    console.log("eiei: ", this.s.controls);


    this.form.patchValue({
      files: this.listfiles
    });

    // console.log("listfiles", this.Formfile.get('files'));
    // this.Formfile.get('files').updateValueAndValidity()
  }
  storeCentralpolicy(value) {
    console.log(value);
    // // alert(JSON.stringify(value))
    this.centralpolicyservice.addCentralpolicy(value, this.form.value.files, this.userid)
      .subscribe(response => {
        console.log(response);
        this.Form.reset()
        this.router.navigate(['centralpolicy'])
        // this.centralpolicyservice.getcentralpolicydata().subscribe(result => {
        //   this.centralpolicyservice = result
        //   console.log(this.resultcentralpolicy);
        // })
      })
  }

  addFile(event) {
    this.files = event.target.files
  }

  get f() { return this.Form.controls }
  get t() { return this.f.input as FormArray }
  get d() { return this.f.inputdate as FormArray }
  get s() { return this.f.fileData as FormArray }

  append() {
    this.t.push(this.fb.group({
      date: '',
      subject: '',
      questions: []
    }))
  }
  appendquestion() {
  }

  back() {
    window.history.back();
  }

  appenddate() {
    this.d.push(this.fb.group({
      start_date: '',
      end_date: '',
    }))
  }
  remove(index: number) {
    this.d.removeAt(index);
  }

  public onSelectAll() {
    var selected = this.province.map(item => item.id);
    this.Form.get('ProvinceId').patchValue(selected);
    this.selectedProvince = selected
  }

  public onClearAll() {
    this.Form.get('ProvinceId').patchValue([]);
  }
  getDataProvince() {
    this.provinceservice.getprovincedata()
      .subscribe(result => {
        this.external.getProvinceRegion()
          .subscribe(result2 => {
            this.province = result.map(result => {
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
            console.log(this.province);
          })



      })
    // console.log(this.provinceservice.getRegionMock());
  }
}

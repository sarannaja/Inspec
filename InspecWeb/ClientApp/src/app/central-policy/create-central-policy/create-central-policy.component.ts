import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators, FormArray } from '@angular/forms';
import { CentralpolicyService } from 'src/app/services/centralpolicy.service';
import { IMyOptions } from 'mydatepicker-th';
import { Router } from '@angular/router';
import { FiscalyearService } from 'src/app/services/fiscalyear.service';
import { ProvinceService } from 'src/app/services/province.service';
import { IOption } from 'ng-select';
import { AuthorizeService } from 'src/api-authorization/authorize.service';

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
  selectdataprovince: Array<IOption>
  input: any = [{ date: '', subject: '', questions: '' }]
  inputdate: any = [{ start_date: '', end_date: '' }]
  form: FormGroup;
  progress: number = 0;
  userid: string
  constructor(
    private fb: FormBuilder,
    private centralpolicyservice: CentralpolicyService,
    public share: CentralpolicyService,
    private router: Router,
    private fiscalyearservice: FiscalyearService,
    private provinceservice: ProvinceService,
    private authorize: AuthorizeService,) {
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
      // "test" : new FormControl(null,[Validators.required,this.forbiddenNames.bind(this)])
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

    this.provinceservice.getprovincedata().subscribe(result => {
      this.resultprovince = result

      this.selectdataprovince = this.resultprovince.map((item, index) => {
        return { value: item.id, label: item.name }
      })

      console.log(this.resultprovince);
    })

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
  inspec(event){
    console.log(event);

  }
  uploadFile(event) {
    const file = (event.target as HTMLInputElement).files;
    this.form.patchValue({
      files: file
    });
    this.form.get('files').updateValueAndValidity()

  }
  storeCentralpolicy(value) {
    // console.log(this.form.value.files);
    // alert(JSON.stringify(value))
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
    const selected = this.resultprovince.map(item => item.id);
    this.Form.get('ProvinceId').patchValue(selected);
  }

  public onClearAll() {
    this.Form.get('ProvinceId').patchValue([]);
  }
}

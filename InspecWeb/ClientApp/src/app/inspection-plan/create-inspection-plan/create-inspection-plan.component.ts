import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators, FormArray } from '@angular/forms';
import { CentralpolicyService } from 'src/app/services/centralpolicy.service';
import { IMyOptions } from 'mydatepicker-th';
import { Router, ActivatedRoute } from '@angular/router';
import { FiscalyearService } from 'src/app/services/fiscalyear.service';
import { ProvinceService } from 'src/app/services/province.service';
import { IOption } from 'ng-select';
import { InspectionplanService } from '../../services/inspectionplan.service';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { UserService } from 'src/app/services/user.service';

interface addInput {
  id: number;
  name: string;
}

@Component({
  selector: 'app-create-inspection-plan',
  templateUrl: './create-inspection-plan.component.html',
  styleUrls: ['./create-inspection-plan.component.css']
})
export class CreateInspectionPlanComponent implements OnInit {
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
  id
  userid: string
  provinceid
  resultinspectionplan: any = []

  constructor(
    private fb: FormBuilder,
    private centralpolicyservice: CentralpolicyService,
    public share: CentralpolicyService,
    private router: Router,
    private fiscalyearservice: FiscalyearService,
    private provinceservice: ProvinceService,
    private activatedRoute: ActivatedRoute,
    private authorize: AuthorizeService,
    private userservice: UserService,
    private inspectionplanservice: InspectionplanService) {
    this.id = activatedRoute.snapshot.paramMap.get('id')
    this.provinceid = activatedRoute.snapshot.paramMap.get('proid')
  }

  ngOnInit() {
    this.authorize.getUser()
      .subscribe(result => {
        this.userid = result.sub
        console.log(result);
        // alert(this.userid)
      })

    this.getinspectionplanservice();

    this.Form = this.fb.group({
      title: new FormControl(null, [Validators.required]),
      start_date: new FormControl(null, [Validators.required]),
      end_date: new FormControl(null, [Validators.required]),
      year: new FormControl(null, [Validators.required]),
      type: new FormControl(null, [Validators.required]),
      // files: new FormControl(null, [Validators.required]),
      ProvinceId: new FormControl(null, [Validators.required]),
      // status: new FormControl("ร่างกำหนดการ", [Validators.required]),
      input: new FormArray([])
    })
    this.t.push(this.fb.group({
      date: '',
      subject: '',
      questions: []
    }))
    //แก้ไข

    // this.provinceservice.getprovincedata().subscribe(result => {
    //   this.resultprovince = result

    //   this.selectdataprovince = this.resultprovince.map((item, index) => {
    //     return { value: item.id, label: item.name }
    //   })

    //   console.log(this.resultprovince);
    // })
    this.userservice.getprovincedata(this.userid).subscribe(result => {
      this.resultprovince = result

      this.selectdataprovince = this.resultprovince.map((item, index) => {
        return { value: item.province.id, label: item.province.name }
      })

      console.log(this.resultprovince);
    })

    this.fiscalyearservice.getfiscalyeardata().subscribe(result => {
      this.resultfiscalyear = result
      console.log(this.resultcentralpolicy);
    })


    this.Form.patchValue({
      // กรณีจะแก้ไข
    })
    // this.addInput()
  }

  storeInspectionPlan(value) {
    console.log("FORM: ", value);
    // alert(JSON.stringify(value))
    this.inspectionplanservice.addInspectionPlan(value, this.userid,this.id).subscribe(response => {
      console.log(value);
      this.Form.reset()
      window.history.back();
      // this.router.navigate(['inspectionplanevent'])
      // this.router.navigate(['inspectionplan', this.id])
    })
  }

  addFile(event) {
    this.files = event.target.files
  }

  get f() { return this.Form.controls }
  get t() { return this.f.input as FormArray }

  append() {
    this.t.push(this.fb.group({
      date: '',
      subject: '',
      questions: []
    }))
  }
  appendquestion() {
  }

  getinspectionplanservice() {
    this.inspectionplanservice.getinspectionplandata(this.id, this.provinceid).subscribe(result => {
      console.log("result", result);

      this.resultinspectionplan = result.inspectionplandata

      this.Form.patchValue({
        start_date: this.resultinspectionplan[0].startDate,
        end_date:  this.resultinspectionplan[0].endDate,
        ProvinceId:  this.resultinspectionplan[0].province.id,
      })

      // alert(JSON.stringify(this.resultinspectionplan))
    })
  }

}

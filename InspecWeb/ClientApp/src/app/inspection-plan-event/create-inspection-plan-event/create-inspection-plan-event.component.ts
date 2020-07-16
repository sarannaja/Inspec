import { Component, OnInit, Inject } from '@angular/core';
import { IMyOptions } from 'mydatepicker-th';
import { FormGroup, FormBuilder, FormControl, Validators, FormArray } from '@angular/forms';
import { Router } from '@angular/router';
import { InspectionplaneventService } from 'src/app/services/inspectionplanevent.service';
import { ProvinceService } from 'src/app/services/province.service';

import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { UserService } from 'src/app/services/user.service';
import { CentralpolicyService } from 'src/app/services/centralpolicy.service';
import { SubjectService } from 'src/app/services/subject.service';
import { InspectionplanService } from 'src/app/services/inspectionplan.service';

interface addInput {
  id: number;
  name: string;
}

@Component({
  selector: 'app-create-inspection-plan-event',
  templateUrl: './create-inspection-plan-event.component.html',
  styleUrls: ['./create-inspection-plan-event.component.css']
})
export class CreateInspectionPlanEventComponent implements OnInit {
  url = "";
  private myDatePickerOptions: IMyOptions = {
    // other options...
    dateFormat: 'dd/mm/yyyy',
  };
  userid: string
  files: string[] = []
  resultinspectionplan: any = []
  resultprovince: any = []
  resultcentralpolicy: any = []
  resultdetailcentralpolicy: any = []
  province: any = []
  delid: any
  title: any
  start_date: any
  end_date: any
  Form: FormGroup;
  selectdataprovince: Array<any>
  selectdatacentralpolicy: Array<any>
  id: any = 1
  provincename: any = []
  provinceid: any = []
  input: any = [{ start_date_plan: '', end_date_plan: '', province: '' }]
  start_date_plan_i: any = []
  end_date_plan_i: any = []

  constructor(
    private fb: FormBuilder, private authorize: AuthorizeService,
    private router: Router, private inspectionplaneventservice: InspectionplaneventService,
    private userservice: UserService,
    private centralpolicyservice: CentralpolicyService,
    private subjectservice: SubjectService,
    private provinceservice: ProvinceService,
    private inspectionplanservice: InspectionplanService,
    @Inject('BASE_URL') baseUrl: string) {
    this.url = baseUrl;
  }

  ngOnInit() {
    this.authorize.getUser()
      .subscribe(result => {
        this.userid = result.sub
        console.log(result);
        // alert(this.userid)
      })

    this.Form = this.fb.group({
      //title: new FormControl(null, [Validators.required]),
      input: new FormArray([])
      // start_date: new FormControl(null, [Validators.required]),
      // end_date: new FormControl(null, [Validators.required]),
    });
    this.t.push(this.fb.group({
      start_date_plan: '',
      end_date_plan: '',
      provinces: [],
      centralpolicies: [],
      resultcentralpolicy: [],
      resultdetailcentralpolicy: ''
    }))

    this.userservice.getprovincedata(this.userid).subscribe(result => {
      this.resultprovince = result

      this.selectdataprovince = this.resultprovince.map((item, index) => {
        return { value: item.province.id, label: item.province.name }
      })

      console.log(this.resultprovince);
    })

  }

  get f() { return this.Form.controls }
  get t() { return this.f.input as FormArray }

  // get y(){return this.t.controls}
  // get p(){return this.y.provinces as FormArray}

  DetailCentralPolicy(id: any, i) {
    // alert(this.province[i].value)
    // this.router.navigate(['/centralpolicy/detailcentralpolicy', id])
    // alert(this.url)

    this.subjectservice.storesubjectprovince(id, this.province[i].value)
      .subscribe(result => {
        console.log("storesubjectprovince : " + result);

        // this.router.navigate([]).then(result => {  window.open(this.url + 'centralpolicy/detailcentralpolicyprovince/' + result, '_blank'); });
        window.open(this.url + 'centralpolicy/detailcentralpolicyprovince/' + result);
      })

  }

  storeInspectionPlanEvent(value) {
    console.log("Store : ", value);
    // alert(JSON.stringify(value))
    this.inspectionplaneventservice.addInspectionplanevent(value, this.userid).subscribe(response => {
      console.log(value);
      this.Form.reset()
      this.router.navigate(['inspectionplanevent'])
    })
  }

  append() {
    this.t.push(this.fb.group({
      start_date_plan: '',
      end_date_plan: '',
      provinces: [],
      centralpolicies: [],
      resultcentralpolicy: [],
      resultdetailcentralpolicy: '',
    }))
    // this.input.push({
    //   date: '',
    //   province: ''
    // });
  }

  selectedprovince(event, i, item) {
    this.province[i] = event;
    this.provincename[i] = event.label;
    this.provinceid[i] = event.value;

    // alert(JSON.stringify(event));
    console.log("item", item);
    this.t.at(i).get('resultcentralpolicy').patchValue([1, 2])

    this.centralpolicyservice.getcentralpolicyfromprovince(event.value)
      .subscribe(result => {
        this.resultcentralpolicy = result
        // alert(JSON.stringify(this.resultcentralpolicy))
        this.selectdatacentralpolicy = this.resultcentralpolicy.map((item, index) => {
          return { value: item.centralPolicy.id, label: item.centralPolicy.title }
        })
        this.t.at(i).get('resultcentralpolicy').patchValue(this.selectdatacentralpolicy)
        console.log("t", this.t.value);
      })
  }

  selectedcentralpolicy(event, i) {
    // this.t.at(i).get('resultdetailcentralpolicy').patchValue([1, 2])
    // alert(JSON.stringify(event.value))
    this.centralpolicyservice.getdetailcentralpolicydata(event.value)
      .subscribe(result => {
        this.resultdetailcentralpolicy = result
        console.log(this.resultdetailcentralpolicy.title);
        this.t.at(i).get('resultdetailcentralpolicy').patchValue({ id: this.resultdetailcentralpolicy.id, title: this.resultdetailcentralpolicy.title })
        // alert(JSON.stringify(this.resultdetailcentralpolicy))
        // this.resultdetailcentralpolicy = this.resultdetailcentralpolicy.map((item, index) => {
        //   return { value: item.id, label: item.title }
        // })

        // this.t.at(i).get('resultdetailcentralpolicy').patchValue(this.resultdetailcentralpolicy)
        console.log("t", this.t.value);
      })
  }

  remove(index: number) {
    this.t.removeAt(index);
  }
  back() {
    window.history.back();
  }

  Gotoinspecplan(provinceid, i) {

    // alert(this.start_date_plan[i])
    // alert(this.end_date_plan[i])
    // alert(provinceid)

    this.inspectionplanservice.inspectionplansprovince(provinceid, this.userid, this.start_date_plan_i[i], this.end_date_plan_i[i])
      .subscribe(result => {
        console.log("storesubjectprovince : " + result);
        var id = result
        window.open(this.url + 'inspectionplan/' + id + '/' + provinceid);
      })
  }

  startdate(event, i) {
    this.start_date_plan_i[i] = event.date.year + '-' + event.date.month + '-' + event.date.day;
    // alert(JSON.stringify(event))
  }
  enddate(event, i) {
    // this.start_date_plan_i[i] = event;
    this.end_date_plan_i[i] = event.date.year + '-' + event.date.month + '-' + event.date.day;
    // alert(JSON.stringify(event))
  }
}

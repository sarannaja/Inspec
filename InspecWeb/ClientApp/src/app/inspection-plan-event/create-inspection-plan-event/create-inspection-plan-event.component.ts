import { Component, OnInit, Inject } from '@angular/core';
import { IMyOptions } from 'mydatepicker-th';
import { FormGroup, FormBuilder, FormControl, Validators, FormArray } from '@angular/forms';
import { Router } from '@angular/router';
import { Location } from '@angular/common';
import { InspectionplaneventService } from 'src/app/services/inspectionplanevent.service';
import { ProvinceService } from 'src/app/services/province.service';

import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { UserService } from 'src/app/services/user.service';
import { CentralpolicyService } from 'src/app/services/centralpolicy.service';
import { SubjectService } from 'src/app/services/subject.service';
import { InspectionplanService } from 'src/app/services/inspectionplan.service';
import { ConfirmationDialogService } from 'src/app/services/confirmation-dialog/confirmation-dialog.service';
import { NotofyService } from 'src/app/services/notofy.service';

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
  myDatePickerOptions: IMyOptions = {
    // other options...
    // dateFormat: 'dd/mm/yyyy',

  };
  myDatePickerOptions_i: IMyOptions[] = []

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
  provinceid: any[] = []
  input: any = [{ start_date_plan: '', end_date_plan: '', province: '' }]
  start_date_plan_i: Date[] = []
  start_time_plan_i: Date[] = []
  end_time_plan_i: Date[] = []
  end_date_plan_i: Date[] = []
  bsValue: Date = new Date()
  mytime: Date = new Date()
  boxId: any = 0
  constructor(
    private fb: FormBuilder, private authorize: AuthorizeService,
    private router: Router, private inspectionplaneventservice: InspectionplaneventService,
    private userservice: UserService,
    private centralpolicyservice: CentralpolicyService,
    private subjectservice: SubjectService,
    private provinceservice: ProvinceService,
    private inspectionplanservice: InspectionplanService,
    private _dialog: ConfirmationDialogService,
    private _NotofyService: NotofyService,
    private _location: Location,
    @Inject('BASE_URL') baseUrl: string) {
    this.url = baseUrl;
  }

  ngOnInit() {
    this.authorize.getUser()
      .subscribe(result => {
        this.userid = result.sub
        //console.log(result);
        // alert(this.userid)
      })

    this.Form = this.fb.group({
      //title: new FormControl(null, [Validators.required]),
      input: new FormArray([])

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

      //console.log(this.resultprovince);
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
        //console.log("storesubjectprovince : " + result);

        // this.router.navigate([]).then(result => {  window.open(this.url + 'centralpolicy/detailcentralpolicyprovince/' + result, '_blank'); });
        window.open(this.url + 'centralpolicy/detailcentralpolicyprovince/' + result);
      })

  }

  storeInspectionPlanEvent(value) {
    //console.log("Store : ", value);
    // alert(JSON.stringify(value))
    let start_date_plan_i: any[] = this.start_date_plan_i
    let end_date_plan_i: any[] = this.end_date_plan_i
    var count = 0
    for (let i = 0; i < value.input.length; i++) {
      if (this.dateChecked(this.start_date_plan_i[i], this.end_date_plan_i[i])) {


        this.inspectionplanservice.inspectionplansprovince(value.input[i].provinces, this.userid, start_date_plan_i[i], end_date_plan_i[i])
          .subscribe(result => {
            //console.log("storesubjectprovince : " + result);
            var id = result
            var watch = 0;
            // value.input.length == 1 ? this.removeThem() : count != value.input.length - 1 ? count++ : this.removeThem()
            this._NotofyService.onSuccess("เพื่มข้อมูล",)
          })
      } else {
        // addT.push(this.t.at(i).value)
      }
    }
    this.removeThem()

  }
  removeThem() {
    let count = 0
    for (let i = 0; i < this.t.length; i++) {
      if (this.dateChecked(this.start_date_plan_i[i], this.end_date_plan_i[i])) {


        this.t.removeAt(i)

        let starts = new Set(this.start_date_plan_i);
        starts.delete(this.start_date_plan_i[i]);
        this.start_date_plan_i = Array.from(starts);

        let ends = new Set(this.end_date_plan_i);
        ends.delete(this.end_date_plan_i[i]);
        this.end_date_plan_i = Array.from(ends);

        count++
      } else {
      }
    }
    // alert(this.t.length)
    if (this.t.length == 0) {
      // setTimeout(()=>thi)
      this._dialog.confirm('ต้องการเพิ่มข้อมูลต่อหรือไม่', 'ตอนนี้ข้อมูลของคุณเพิ่มเรียบร้อยแล้วคุณต้องการเพิ่มข้อมูลต่อหรือไม่', 'เพิ่มต่อ', 'ไม่เพิ่มต่อ')
        .then((result: any) => {
          result.status ? this.t.push(this.fb.group({
            start_date_plan: '',
            end_date_plan: '',
            provinces: [],
            centralpolicies: [],
            resultcentralpolicy: [],
            resultdetailcentralpolicy: ''
          })) : this.back()

        })

    } else {
      this._dialog.confirm('ยังมีข้อมูลบางตัวที่มีวันและเวลาเริ่มต้นน้อยกว่าวันที่สิ้นสุด', 'ยังมีข้อมูลบางตัวที่มีวันและเวลาเริ่มต้นน้อยกว่าวันที่สิ้นสุด', false, 'ปิด')
        .then(result => {

        })
    }
  }
  checkfoGotothem() {

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
    //console.log("item", item);
    this.t.at(i).get('resultcentralpolicy').patchValue([1, 2])

    this.centralpolicyservice.getcentralpolicyfromprovince(event.value)
      .subscribe(result => {
        this.resultcentralpolicy = result
        // alert(JSON.stringify(this.resultcentralpolicy))
        this.selectdatacentralpolicy = this.resultcentralpolicy.map((item, index) => {
          return { value: item.centralPolicy.id, label: item.centralPolicy.title }
        })
        this.t.at(i).get('resultcentralpolicy').patchValue(this.selectdatacentralpolicy)
        //console.log("t", this.t.value);
      })
  }

  selectedcentralpolicy(event, i) {
    // this.t.at(i).get('resultdetailcentralpolicy').patchValue([1, 2])
    // alert(JSON.stringify(event.value))
    this.centralpolicyservice.getdetailcentralpolicydata(event.value)
      .subscribe(result => {
        this.resultdetailcentralpolicy = result
        //console.log(this.resultdetailcentralpolicy.title);
        this.t.at(i).get('resultdetailcentralpolicy').patchValue({ id: this.resultdetailcentralpolicy.id, title: this.resultdetailcentralpolicy.title })


      })
  }

  remove(index: number) {
    this.t.removeAt(index);
  }
  back() {
    this._location.back()
    // window.history.back();
  }
  //inpecplanevent
  Gotoinspecplan(provinceid, i) {



    this.dateChecked(this.start_date_plan_i[i], this.end_date_plan_i[i])
      ? this.inspectionplanservice.inspectionplansprovince(provinceid, this.userid, this.start_date_plan_i[i], this.end_date_plan_i[i])
        .subscribe(result => {
          this._NotofyService.onSuccess("เพื่มข้อมูล",)
          var id = result
          var watch = 0;
          this.t.length == 1 ? this.append() : null

          this.t.removeAt(i)

          var starts = new Set(this.start_date_plan_i);
          starts.delete(this.start_date_plan_i[i]);
          this.start_date_plan_i = Array.from(starts);

          var ends = new Set(this.end_date_plan_i);
          ends.delete(this.end_date_plan_i[i]);
          this.end_date_plan_i = Array.from(ends);

          window.open(this.url + 'inspectionplan/' + id + '/' + provinceid + '/' + watch);
        }) : this._dialog.confirm('ข้อมูลชุดนี้มีวันและเวลาเริ่มต้นน้อยกว่าวันที่และเวลาสิ้นสุด', 'ข้อมูลชุดนี้มีวันและเวลาเริ่มต้นน้อยกว่าวันที่และเวลาสิ้นสุด', false, 'ปิด')
          .then(result => {

          })
  }

  startdate(event, i) {
    this.end_date_plan_i ? this.checkStarttoResetEndDate(i) : null
    this.start_date_plan_i[i] = event
    this.disableDate_i(event, i)

    //console.log(this.start_date_plan_i[i]);

    // alert(JSON.stringify(event))
  }
  starttime(event, i) {
    this.start_date_plan_i[i] = event

  }

  enddate(event, i) {

    this.end_date_plan_i[i] = event


  }

  async endtime(event: any, i) {
    //console.log('this.end_date_plan_i[i]', this.end_date_plan_i[i]);
  }

  dateChecked(startDate: Date, endDate: Date) {
    //console.log(startDate.toString(), endDate.toString());

    return Date.parse(startDate.toString()) <= Date.parse(endDate.toString())
    // ? true : false
  }

  checkStarttoResetEndDate(index) {
    this.t.at(index).get('end_date_plan').reset()
    this.end_date_plan_i[index] = void 0;
  }





  setDateTime(date: Date) {
    var time = new Date(date)
    time.setHours(time.getHours() + 7)
    return time
  }

  disableDate_i(date: any, i) {
    //console.log(date.getFullYear(), date.getMonth() + 1, date.getDate());

    this.myDatePickerOptions_i[i] = {
      disableDateRanges: [{
        end: { year: date.getFullYear(), month: date.getMonth() + 1, day: date.getDate() - 1 },
        begin: { year: date.getFullYear() - 10, month: date.getMonth() + 1, day: date.getDate() - 1 }
      }]
    }
  }
}

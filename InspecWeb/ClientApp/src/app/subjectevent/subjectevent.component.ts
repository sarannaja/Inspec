import { Component, OnInit, TemplateRef } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { SubjectService } from '../services/subject.service';
import { CentralpolicyService } from '../services/centralpolicy.service';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { IOption } from 'ng-select';
import { UserService } from '../services/user.service';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { InspectionplanService } from '../services/inspectionplan.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-subjectevent',
  templateUrl: './subjectevent.component.html',
  styleUrls: ['./subjectevent.component.css']
})
export class SubjecteventComponent implements OnInit {

  dtOptions: DataTables.Settings = {};
  loading = false;
  modalRef: BsModalRef;
  selectcentralpolicy: any = [];
  resultcentralpolicy: any[] = [];
  resultcentralpolicy2: any = [];
  resultsubjectevent: any = [];
  Form: FormGroup;
  Form2: FormGroup;
  checkInspec: Boolean;
  selectdataprovince: Array<IOption>
  userid: string;
  resultprovince: any = [];
  province: any = []
  provincename: any = []
  provinceid: any = []
  selectdatacentralpolicy: Array<IOption>

  selectdataprovince2: Array<IOption>
  selectdatacentralpolicy2: Array<any>
  province2: any = []
  provincename2: any = []
  provinceid2: any = []

  CentralPolicyEvents: Array<any> = []
  subjectgroupsdatas: Array<any> = []

  // ceneventid

  constructor(
    private spinner: NgxSpinnerService,
    private modalService: BsModalService,
    private subjectservice: SubjectService,
    private centralpolicyservice: CentralpolicyService,
    private userservice: UserService,
    private authorize: AuthorizeService,
    private fb: FormBuilder,
    private inspectionplanservice: InspectionplanService,
    private router: Router,
  ) { }

  ngOnInit() {
    this.authorize.getUser()
      .subscribe(result => {
        this.userid = result.sub
        console.log(result);
        // alert(this.userid)
      })
    // this.spinner.show();
    this.dtOptions = {
      pagingType: 'full_numbers',
      // columnDefs: [
      //   {
      //     targets: [3],
      //     orderable: false
      //   }
      // ]

    };

    this.Form = this.fb.group({
      "CentralpolicyId": new FormControl(null, [Validators.required]),
      "startdate": new FormControl(null, [Validators.required]),
      "enddate": new FormControl(null, [Validators.required]),
      "province": new FormControl(null, [Validators.required]),
      "land": new FormControl(null)
    })

    this.Form2 = this.fb.group({
      "CentralpolicyId2": new FormControl(null, [Validators.required]),
      "province2": new FormControl(null, [Validators.required]),
    })

    this.getCentralPolicy();
    this.getSubjectevent();


    this.userservice.getprovincedata(this.userid).subscribe(result => {
      this.resultprovince = result

      this.selectdataprovince = this.resultprovince.map((item, index) => {
        return { value: item.province.id, label: item.province.name }
      })

      console.log(this.resultprovince);
    })

  }
  openModal(template: TemplateRef<any>) {

    this.modalRef = this.modalService.show(template);
  }

  getCentralPolicy() {
    this.centralpolicyservice.getcentralpolicydata()
      .subscribe(result => {
        this.resultcentralpolicy = result
        this.selectcentralpolicy = this.resultcentralpolicy.map((item, index) => {
          return { value: item.id, label: item.title }
        })
      })
  }

  getSubjectevent() {
    this.subjectservice.getsubjectevent().subscribe(result => {
      this.resultsubjectevent = result
      this.loading = true;
    })
  }

  selectedprovince(event, i, item) {
    this.province[i] = event;
    this.provincename[i] = event.label;
    this.provinceid[i] = event.value;

    // alert(JSON.stringify(event));
    // console.log("item", item);
    // this.t.at(i).get('resultcentralpolicy').patchValue([1, 2])

    this.centralpolicyservice.getcentralpolicyfromprovince(event.value)
      .subscribe(result => {
        this.resultcentralpolicy = result
        // alert(JSON.stringify(this.resultcentralpolicy))

        this.selectdatacentralpolicy = this.resultcentralpolicy.filter((item, index) => {
          return item.centralPolicy.status == "ใช้งานจริง"
        }).map((item, index) => {
          return { value: item.centralPolicy.id, label: item.centralPolicy.title }
        })
        // this.t.at(i).get('resultcentralpolicy').patchValue(this.selectdatacentralpolicy)
        // console.log("t", this.t.value);
      })
  }


  selectedprovince2(event, i, item) {
    // alert("123")
    // this.ceneventid = 0
    this.province2[i] = event;
    this.provincename2[i] = event.label;
    this.provinceid2[i] = event.value;

    this.subjectservice.geteventfromcalendar(event.value)
      .subscribe(result => {

        // alert(JSON.stringify(result.centralPolicyEvents))

        this.CentralPolicyEvents = result.centralPolicyEvents
        this.subjectgroupsdatas = result.subjectgroupsdatas

        this.selectdatacentralpolicy2 = this.CentralPolicyEvents.filter((item, index) => {
          return item.haveSubject == 0
        }).map((item, index) => {
          return { value:{centralPolicyId:item.centralPolicy.id,centralPolicyeventId:item.id} , label: item.centralPolicy.title }
        })

        // alert(JSON.stringify(this.selectdatacentralpolicy2))
        // this.getRecycled()



      })
  }
  getRecycled() {
    // alert(JSON.stringify(this.CentralPolicyEvents))
    this.selectdatacentralpolicy2 = []

    // if (this.subjectgroupsdatas.length == 0) {

    //   for (var i = 0; i < this.CentralPolicyEvents.length; i++) {
    //     // alert(JSON.stringify(this.CentralPolicyEvents[i].centralPolicy.title))
    //     this.selectdatacentralpolicy2.push({ value: this.CentralPolicyEvents[i].centralPolicy.id, label: this.CentralPolicyEvents[i].centralPolicy.title })
    //   }
    // }

    // else {
    for (var i = 0; i < this.CentralPolicyEvents.length; i++) {
      var n = 0;
      for (var ii = 0; ii < this.subjectgroupsdatas.length; ii++) {
        if (this.CentralPolicyEvents[i].haveSubject == 1) {
          // alert("1")
          n++;
        }
      }
      if (n == 0) {
        // alert("push")
        // alert(JSON.stringify(this.CentralPolicyEvents[i].centralPolicy.title))
        this.selectdatacentralpolicy2.push({ value: this.CentralPolicyEvents[i].centralPolicy.id, label: this.CentralPolicyEvents[i].centralPolicy.title })
      }
    }
    // }
  }
  storeCentralPolicy(value) {
    // alert(JSON.stringify(value))
    if (value.land == "ลงพื้นที่") {
      this.subjectservice.subjectevent(value, this.userid)
        .subscribe(result => {
          this.loading = false;
          this.modalRef.hide();
          this.Form.reset()
          // this.resultcentralpolicy = result
          this.getSubjectevent();
        })
    } else if (value.land == "ไม่ลงพื้นที่") {
      this.subjectservice.subjecteventnoland(value, this.userid)
        .subscribe(result => {
          this.loading = false;
          this.modalRef.hide();
          this.Form.reset()
          // this.resultcentralpolicy = result
          this.getSubjectevent();
        })
    }
  }

  storeCentralPolicy2(value) {
    // alert(JSON.stringify(value))
    this.subjectservice.postsubjecteventfromcalendar(value, this.userid)
    .subscribe(result => {
      this.loading = false;
      this.modalRef.hide();
      this.Form2.reset()
      // this.resultcentralpolicy = result
      this.getSubjectevent();
    })
  }

  inspect(myradio) {
    // alert(myradio)
    this.checkInspec = true;
  }
  notInspec(value) {
    // alert(value)
    this.checkInspec = false;
  }
  Subjectevent(id, centralPolicyId, provinceId) {
    this.inspectionplanservice.getcentralpolicyprovinceid(centralPolicyId, provinceId).subscribe(result => {
      // this.centralpolicyprovinceid = result
      this.router.navigate(['/subjectevent/detail/' + result, { subjectgroupid: id, }])
    })
  }
}

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
  resultcentralpolicy: any = [];
  resultsubjectevent: any = [];
  Form: FormGroup;
  checkInspec: Boolean;
  selectdataprovince: Array<IOption>
  userid: string;
  resultprovince: any = [];
  province: any = []
  provincename: any = []
  provinceid: any = []
  selectdatacentralpolicy: Array<IOption>
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
      // "status": new FormControl(null)
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
        this.selectdatacentralpolicy = this.resultcentralpolicy.map((item, index) => {
          return { value: item.centralPolicy.id, label: item.centralPolicy.title }
        })
        // this.t.at(i).get('resultcentralpolicy').patchValue(this.selectdatacentralpolicy)
        // console.log("t", this.t.value);
      })
  }

  storeCentralPolicy(value) {
    // alert(JSON.stringify(value))
    this.subjectservice.subjectevent(value, this.userid)
      .subscribe(result => {
        this.loading = false;
        this.modalRef.hide();
        this.Form.reset()
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

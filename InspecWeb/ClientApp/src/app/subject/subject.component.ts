import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { SubjectService } from '../services/subject.service';
import { ActivatedRoute, Router } from '@angular/router';
import { IMyOptions, IMyDate } from 'mydatepicker-th';
import { IOption } from 'ng-select';
import * as moment from 'moment';
import { CentralpolicyService } from '../services/centralpolicy.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-subject',
  templateUrl: './subject.component.html',
  styleUrls: ['./subject.component.css']
})
export class SubjectComponent implements OnInit {

  private myDatePickerOptions: IMyOptions = {
    // other options...
    dateFormat: 'dd/mm/yyyy',
  };

  private startDate: IMyDate = { year: 0, month: 0, day: 0 };
  private endDate: IMyDate = { year: 0, month: 0, day: 0 };

  resultsubject: any = []
  resultcentralpolicy: any = []
  id
  userid
  role_id
  delid: any
  name: any
  start_date: any
  end_date: any
  modalRef: BsModalRef;
  // router: any
  Form: FormGroup;
  times: IOption[] = [];
  loading = false;
  dtOptions: DataTables.Settings = {};

  constructor(
    private modalService: BsModalService,
    private fb: FormBuilder,
    private subjectservice: SubjectService,
    private centralpolicyservice: CentralpolicyService,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    public share: SubjectService,
    private spinner: NgxSpinnerService,
    private authorize: AuthorizeService,
    private userService: UserService) {
    this.id = activatedRoute.snapshot.paramMap.get('id')
    this.name = activatedRoute.snapshot.paramMap.get('name')
  }

  ngOnInit() {
    
    this.spinner.show();

    this.dtOptions = {
      pagingType: 'full_numbers',
      columnDefs: [
        {
          targets: [4],
          orderable: false
        }
      ]

    };

    // this.Form = this.fb.group({
    //   name: new FormControl(null, [Validators.required]),
    //   centralpolicydateid: new FormControl(null, [Validators.required]),
    // })

    this.getTimeCentralPolicy()
    this.getSubject()

    this.authorize.getUser()
      .subscribe(result => {
        this.userid = result.sub
        console.log(result);
        // alert(this.userid)
        this.userService.getuserfirstdata(this.userid)
        .subscribe(result => {
          // this.resultuser = result;
          //console.log("test" , this.resultuser);
          this.role_id = result[0].role_id
          // alert(this.role_id)
        })
      })

    // this.subjectservice.getsubjectdata(this.id).subscribe(result => {
    //   this.resultsubject = result
    //   this.times = []
    //   // let StartDate = ImyDate = {year:  0}
    //   for (var i = 0; i < this.resultsubject[0].centralPolicy.centralPolicyDates.length; i++) {
    //     let d: Date = new Date(this.resultsubject[0].centralPolicy.centralPolicyDates[i].startDate)
    //     // alert(this.resultsubject[0].centralPolicy.centralPolicyDates[i].startDate)
    //     let e: Date = new Date(this.resultsubject[0].centralPolicy.centralPolicyDates[i].endDate)
    //     this.startDate = {
    //       year: d.getFullYear()+543,
    //       month: d.getMonth()+1,
    //       day: d.getDate()
    //     }
    //     this.endDate = {
    //       year: e.getFullYear()+543,
    //       month: e.getMonth()+1,
    //       day: e.getDate()
    //     }
    //     let test = this.startDate.day + '/' + this.startDate.month + '/' + this.startDate.year + 
    //     "  ถึง  " + this.endDate.day + '/' + this.endDate.month + '/' + this.endDate.year


    //     this.times.push({
    //       value: this.resultsubject[0].centralPolicy.centralPolicyDates[i].id,
    //       label: test,
    //     })
    //     console.log(this.resultsubject[0].centralPolicy.centralPolicyDates[i].startDate);
    //   }

    // })
  }
  Subquestion() {
    this.router.navigate(['/subquestion', this.id])
  }
  DetailSubject(id){
    this.router.navigate(['/subject/detailsubject', id])
  }
  EditSubject(id) {
    this.router.navigate(['/subject/editsubject', id])
  }

  getTimeCentralPolicy() {
    this.centralpolicyservice.getdetailcentralpolicydata(this.id).subscribe(result => {
      this.resultcentralpolicy = result
      this.times = []
      // let StartDate = ImyDate = {year:  0}
      for (var i = 0; i < this.resultcentralpolicy.centralPolicyDates.length; i++) {
        let d: Date = new Date(this.resultcentralpolicy.centralPolicyDates[i].startDate)
        // alert(this.resultsubject[0].centralPolicy.centralPolicyDates[i].startDate)
        let e: Date = new Date(this.resultcentralpolicy.centralPolicyDates[i].endDate)
        this.startDate = {
          year: d.getFullYear() + 543,
          month: d.getMonth() + 1,
          day: d.getDate()
        }
        this.endDate = {
          year: e.getFullYear() + 543,
          month: e.getMonth() + 1,
          day: e.getDate()
        }
        let test = this.startDate.day + '/' + this.startDate.month + '/' + this.startDate.year +
          "  ถึง  " + this.endDate.day + '/' + this.endDate.month + '/' + this.endDate.year


        this.times.push({
          value: this.resultcentralpolicy.centralPolicyDates[i].id,
          label: test,
        })
        console.log(this.resultcentralpolicy.centralPolicyDates[i].startDate);
      }

    })
  }

  getSubject() {
    this.subjectservice.getsubjectdata(this.id).subscribe(result => {
      this.resultsubject = result
      this.loading = true;
      this.spinner.hide();
    }
    )
  }

  openModal(template: TemplateRef<any>, id, name) {
    this.delid = id;
    this.name = name;
    console.log(this.delid);
    console.log(this.name);

    this.modalRef = this.modalService.show(template);
  }

  // storeSubject(value) {
  //   console.log(value);
  //   this.subjectservice.addSubject(value, this.id).subscribe(response => {
  //     this.Form.reset()
  //     this.modalRef.hide()
  //     this.getSubject()
  //     // this.subjectservice.getsubjectdata(this.id).subscribe(result => {
  //     //   this.resultsubject = result
  //     //   console.log(this.resultsubject);
  //     // })
  //   })
  // }
  deleteSubject(value) {
    this.subjectservice.deleteSubject(value).subscribe(result => {
      console.log(result);
      this.modalRef.hide()
      this.subjectservice.getsubjectdata(this.id).subscribe(result => {
        this.resultsubject = result
        console.log(this.resultsubject);
      })
    })
  }
}

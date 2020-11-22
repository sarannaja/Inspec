import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { SubjectService } from '../services/subject.service';
import { ActivatedRoute, Router } from '@angular/router';
import { IMyOptions, IMyDate } from 'mydatepicker-th';

import * as moment from 'moment';
import { CentralpolicyService } from '../services/centralpolicy.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { UserService } from '../services/user.service';
import { NotofyService } from '../services/notofy.service';

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

  resultsubject: any[] = []
  resultcentralpolicy: any = []
  id
  userid
  role_id
  userministryId
  delid: any
  name: any
  start_date: any
  end_date: any
  modalRef: BsModalRef;
  // router: any
  Form: FormGroup;
  times: any[] = [];
  loading = false;
  dtOptions: any = {};
  subquestion: any = []
  boxcount: any = []

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
    private userService: UserService,
    private _NotofyService: NotofyService) {
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
      ],
      "language": {
        "lengthMenu": "แสดง  _MENU_  รายการ",
        "search": "ค้นหา:",
        "info": "แสดง _START_ ถึง _END_ จาก _TOTAL_ แถว",
        "infoEmpty": "แสดง 0 ของ 0 รายการ",
        "zeroRecords": "ไม่พบข้อมูล",
        "paginate": {
          "first": "หน้าแรก",
          "last": "หน้าสุดท้าย",
          "next": "ต่อไป",
          "previous": "ย้อนกลับ"
        },
      }
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
            this.userministryId = result[0].ministryId
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
  DetailSubject(id) {
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
    this.resultsubject = []
    this.subjectservice.getsubjectdata(this.id).subscribe(result => {
      result.forEach(element => {
        var arrayBox: any[] = []
        element.subquestionCentralPolicyProvinces.forEach(element2 => {
          arrayBox.push(element2.box)
          // console.log("cccc", element2.box);
        });
        setTimeout(() => {
          var count = arrayBox.filter(
            (thing, i, arr) => arr.findIndex(t => t === thing) === i
          ).length
          this.resultsubject.push({ ...element, count })
          console.log(count);
        }, 10 * result.length)

        // console.log(arrayBox);
      });
      setTimeout(() => {
        this.loading = true;
      }, 10 * result.length)

      // result.forEach(element => {
      //   this.subquestion = element.subquestionCentralPolicyProvinces.filter(
      //     (thing, i, arr) => arr.findIndex(t => t.box === thing.box) === i
      //   )
      // });
      // this.boxcount = this.subquestion.filter(
      //   (thing, i, arr) => arr.findIndex(t => t.box === thing.box) === i
      // );
      // console.log("CCCCCCC: ", this.resultsubject);

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
    this.loading = false;
    this.subjectservice.deleteSubject(value, this.userid).subscribe(result => {
      console.log(result);
      this.modalRef.hide()
      this.getSubject();
      this._NotofyService.onSuccess("ลบข้อมูล")
    })
  }
}

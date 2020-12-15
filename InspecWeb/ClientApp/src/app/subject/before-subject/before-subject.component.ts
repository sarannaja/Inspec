import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { IMyDate, IMyOptions } from 'mydatepicker-th';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { AuthorizeService } from 'src/api-authorization-new/authorize.service';
import { CentralpolicyService } from 'src/app/services/centralpolicy.service';
import { NotofyService } from 'src/app/services/notofy.service';
import { SubjectService } from 'src/app/services/subject.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-before-subject',
  templateUrl: './before-subject.component.html',
  styleUrls: ['./before-subject.component.css']
})
export class BeforeSubjectComponent implements OnInit {

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
    private _NotofyService: NotofyService
  ) {
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

    this.getTimeCentralPolicy()
    this.getSubject()
  }

  DetailSubject(id) {
    this.router.navigate(['/mainsubject/detailsubject', id])
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
        });
        setTimeout(() => {
          var count = arrayBox.filter(
            (thing, i, arr) => arr.findIndex(t => t === thing) === i
          ).length
          this.resultsubject.push({ ...element, count })
          console.log(count);
        }, 10 * result.length)
      });
      setTimeout(() => {
        this.loading = true;
      }, 10 * result.length)
      this.spinner.hide();
    }
    )
  }
}

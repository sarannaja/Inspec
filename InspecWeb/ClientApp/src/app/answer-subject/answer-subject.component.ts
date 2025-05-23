import { Component, OnInit } from '@angular/core';
import { AuthorizeService } from 'src/api-authorization-new/authorize.service';
import { AnswersubjectService } from '../services/answersubject.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { Router } from '@angular/router';

@Component({
  selector: 'app-answer-subject',
  templateUrl: './answer-subject.component.html',
  styleUrls: ['./answer-subject.component.css']
})
export class AnswerSubjectComponent implements OnInit {

  userid: string
  resultuserdetail: any = []
  loading = false;
  dtOptions: any = {};

  constructor(
    private authorize: AuthorizeService,
    private answersubjectservice: AnswersubjectService,
    private spinner: NgxSpinnerService,
    private router: Router,
  ) { }

  ngOnInit() {
    this.spinner.show();

    this.dtOptions = {
      pagingType: 'full_numbers',
      columnDefs: [
        {
          targets: [5],
          orderable: false
        }
      ],
      "language": {
        "lengthMenu": "แสดง  _MENU_  รายการ",
        "search": "ค้นหา:",
        "info": "แสดง _PAGE_ ของ _PAGES_ รายการ",
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

    this.authorize.getUser()
      .subscribe(result => {
        this.userid = result.sub
        console.log(result);
      })

    this.getUserDetail()
  }
  getUserDetail() {
    this.answersubjectservice.getuseredata(this.userid)
      .subscribe(result => {
        this.resultuserdetail = result
        this.spinner.hide();
        this.loading = true
        console.log("test", this.resultuserdetail);
      })
  }
  Subjectlist(id) {
    this.router.navigate(['/answersubject/list', id])
  }
}

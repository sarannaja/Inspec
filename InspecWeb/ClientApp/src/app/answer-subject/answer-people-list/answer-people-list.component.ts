import { Component, OnInit } from '@angular/core';
import { AnswersubjectService } from 'src/app/services/answersubject.service';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthorizeService } from 'src/api-authorization/authorize.service';

@Component({
  selector: 'app-answer-people-list',
  templateUrl: './answer-people-list.component.html',
  styleUrls: ['./answer-people-list.component.css']
})
export class AnswerPeopleListComponent implements OnInit {

  id: any
  userid: string
  resultsubjectlist: any[]
  resultcentralpolicy: any[]
  loading = false;
  dtOptions: any = {};

  constructor(
    private authorize: AuthorizeService,
    private answersubjectservice: AnswersubjectService,
    private activatedRoute: ActivatedRoute,
    private router:Router, 
  ) { 
    this.id = activatedRoute.snapshot.paramMap.get('id')
  }

  ngOnInit() {
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
    this.getSubjectlist()
  }
  getSubjectlist() {
    this.answersubjectservice.getsubjectlistdata(this.id,this.userid).subscribe(result => {
      this.resultsubjectlist = result
      this.resultcentralpolicy = result[0].centralPolicyProvince.centralPolicy.title
      this.loading = true
      console.log(this.resultcentralpolicy);
      // this.loading = true;
      // this.spinner.hide();
    }
    )
  }
  Subjectdetail(id) {
    this.router.navigate(['/answerpeople/detail', id])
  }
}

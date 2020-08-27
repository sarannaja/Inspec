import { Component, OnInit } from '@angular/core';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { AnswersubjectService } from 'src/app/services/answersubject.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { Router } from '@angular/router';

@Component({
  selector: 'app-answer-recommendationin-spector',
  templateUrl: './answer-recommendationin-spector.component.html',
  styleUrls: ['./answer-recommendationin-spector.component.css']
})
export class AnswerRecommendationinSpectorComponent implements OnInit {

  userid: string
  resultrecommendationinspector: any = []
  resultanswerrecommendationinspector: any = []
  loading = false;
  dtOptions: DataTables.Settings = {};
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
          targets: [3],
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

    this.getRecommendationinspector()
    this.getAnswerRecommendationinspectoruser()
  }
  getRecommendationinspector() {
    this.answersubjectservice.getRecommendationinspector(this.userid)
      .subscribe(result => {
        this.resultrecommendationinspector = result
        this.spinner.hide();
        this.loading = true
        console.log("test", this.resultrecommendationinspector);
      })
  }
  getAnswerRecommendationinspectoruser() {
    this.answersubjectservice.getRecommendationinspectoruser(this.userid).subscribe(result => {
      this.resultanswerrecommendationinspector = result
      console.log("result", result);
    })
  }
  Recommendationinspector(id) {
    console.log(id);
    // this.router.navigate(['/answerrecommendationinspector/detail', id])
    if (this.resultanswerrecommendationinspector.length == 0) {
      // alert("maime")
      this.router.navigate(['/answerrecommendationinspector/detail', id])
    }
    else {
      for(var i = 0; i < this.resultanswerrecommendationinspector.length; i++){
        if(this.resultanswerrecommendationinspector[i].subjectGroupId == id){
          this.router.navigate(['/answerrecommendationinspector/edit', id])
          break;
        }
        else {
          // alert("me2 indetail")
          this.router.navigate(['/answerrecommendationinspector/detail', id])
        }
      }
    }
  }
}

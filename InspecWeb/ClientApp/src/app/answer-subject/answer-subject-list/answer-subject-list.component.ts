import { Component, OnInit, Inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AnswersubjectService } from 'src/app/services/answersubject.service';
import { AuthorizeService } from 'src/api-authorization-new/authorize.service';

@Component({
  selector: 'app-answer-subject-list',
  templateUrl: './answer-subject-list.component.html',
  styleUrls: ['./answer-subject-list.component.css']
})
export class AnswerSubjectListComponent implements OnInit {

  id: any
  userid: string
  resultsubjectlist: any[]
  loading = false;
  dtOptions: any = {};
  resultanswersubject: any[]
  urllink

  constructor(
    private authorize: AuthorizeService,
    private answersubjectservice: AnswersubjectService,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    @Inject('BASE_URL') baseUrl: string,
  ) {
    this.id = activatedRoute.snapshot.paramMap.get('id')
    this.urllink = baseUrl + 'answersubject/outsider/';
  }

  ngOnInit() {
    this.dtOptions = {
      pagingType: 'full_numbers',
      columnDefs: [
        {
          targets: [2],
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
    this.getAnsweruser()
  }
  getSubjectlist() {
    this.answersubjectservice.getsubjectlistdata(this.id, this.userid).subscribe(result => {
      this.resultsubjectlist = result
      this.loading = true
      console.log(this.resultsubjectlist);
      // this.loading = true;
      // this.spinner.hide();
    }
    )
  }
  getAnsweruser() {
    this.answersubjectservice.getansweruserdata(this.userid).subscribe(result => {
      this.resultanswersubject = result
      console.log("result" , result);
    })
  }
  Subjectdetail(id) {
    console.log(id);
    if (this.resultanswersubject.length == 0) {
      // alert("maime")
      this.router.navigate(['/answersubject/detail', id])
    }
    else {
      for (var i = 0; i < this.resultanswersubject.length; i++) {
        if (this.resultanswersubject[i].subquestionCentralPolicyProvince.subjectCentralPolicyProvinceId == id) {
          // alert("me1 inedit")
          this.router.navigate(['/answersubject/edit', id])
          break;
        } 
        else {
          // alert("me2 indetail")
          this.router.navigate(['/answersubject/detail', id])
        }
        }
      }
    }
    /* To copy Text from Textbox */
    copyInputMessage(inputElement){
      inputElement.select();
      document.execCommand('copy');
      inputElement.setSelectionRange(0, 0);
    }
    back() {
      window.history.back();
    }
  }

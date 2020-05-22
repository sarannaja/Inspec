import { Component, OnInit } from '@angular/core';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
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
  dtOptions: DataTables.Settings = {};

  constructor(
    private authorize: AuthorizeService,
    private answersubjectservice: AnswersubjectService,
    private spinner: NgxSpinnerService,
    private router:Router, 
  ) { }

  ngOnInit() {
    // this.spinner.show();

    this.dtOptions = {
      pagingType: 'full_numbers',
      // columnDefs: [
      //   {
      //     targets: [4],
      //     orderable: false
      //   }
      // ]

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
        console.log("test",this.resultuserdetail);
      })
  }
  Subjectlist(id) {
    this.router.navigate(['/answersubject/list', id])
  }
}

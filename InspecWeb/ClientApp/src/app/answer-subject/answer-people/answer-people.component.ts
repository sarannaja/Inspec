import { Component, OnInit } from '@angular/core';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { AnswersubjectService } from 'src/app/services/answersubject.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { Router } from '@angular/router';
import { InspectionplanService } from 'src/app/services/inspectionplan.service';

@Component({
  selector: 'app-answer-people',
  templateUrl: './answer-people.component.html',
  styleUrls: ['./answer-people.component.css']
})
export class AnswerPeopleComponent implements OnInit {

  userid: string
  resultuserdetail: any = []
  resultanswersubject: any = []
  loading = false;
  dtOptions: DataTables.Settings = {};

  constructor(
    private authorize: AuthorizeService,
    private answersubjectservice: AnswersubjectService,
    private spinner: NgxSpinnerService,
    private router: Router,
    private inspectionplanservice: InspectionplanService
  ) { }

  ngOnInit() {
    this.spinner.show();

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
    this.getAnsweruserrole7()
  }
  getUserDetail() {
    this.answersubjectservice.getuserpeopleedata(this.userid)
      .subscribe(result => {
        this.resultuserdetail = result
        this.spinner.hide();
        this.loading = true
        console.log("test", this.resultuserdetail);
      })
  }
  getAnsweruserrole7() {
    this.answersubjectservice.getansweruserrole7data(this.userid).subscribe(result => {
      this.resultanswersubject = result
      console.log("result", result);
    })
  }
  // getCentralpolicyprovince(cenid, proid){
  //   this.inspectionplanservice.getcentralpolicyprovinceid(cenid, proid).subscribe(result => {
  //     this.router.navigate(['/answerpeople/centralpolicyprovince/' + result])
  //   })
  // }
  // Subjectlist(cenid, proid) {
  //   this.inspectionplanservice.getcentralpolicyprovinceid(cenid, proid).subscribe(result => {
  //     var id = result
  //     this.router.navigate(['/answerpeople/list/' + id])
  //   })
  //   // this.router.navigate(['/answerpeople/list', id])
  // }
  getCentralpolicyprovince(cenid, proid) {
    this.inspectionplanservice.getcentralpolicyprovinceid(cenid, proid).subscribe(result => {
      if (this.resultanswersubject.length == 0) {
        this.router.navigate(['/answerpeople/centralpolicyprovince/' + result])
      }
      else {
        for (var i = 0; i < this.resultanswersubject.length; i++) {
          if (this.resultanswersubject[i].centralPolicyProvinceId == result) {
            // alert("me1 inedit")
            this.router.navigate(['/answerpeople/editcentralpolicyprovince/' + result])
            break;
          }
          else {
            // alert("me2 indetail")
            this.router.navigate(['/answerpeople/centralpolicyprovince/' + result])
          }
        }
      }
    })
  }
}

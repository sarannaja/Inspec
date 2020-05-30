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
  dtOptions: DataTables.Settings = {};

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
      ]

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
    this.router.navigate(['/answersubject/detail', id])
  }
}

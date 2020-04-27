import { Component, OnInit } from '@angular/core';
import { SubjectService } from 'src/app/services/subject.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-detail-subject',
  templateUrl: './detail-subject.component.html',
  styleUrls: ['./detail-subject.component.css']
})
export class DetailSubjectComponent implements OnInit {

  
  resultsubjectdetail: any = []
  questionsopen: any = []
  departments: any = []
  id: any

  constructor(
    private subjectservice: SubjectService,
    private activatedRoute: ActivatedRoute,
  ) {
    this.id = activatedRoute.snapshot.paramMap.get('id')
  }

  ngOnInit() {
    this.getSubjectDetail()
  }
  getSubjectDetail() {
    this.subjectservice.getsubjectdetaildata(this.id)
      .subscribe(result => {
        this.resultsubjectdetail = result
        this.questionsopen = this.resultsubjectdetail.subquestions
        this.departments = this.resultsubjectdetail.subjectDepartments
        console.log("res: ", this.departments);

      })
  }
  back() {
    window.history.back();
  }
}

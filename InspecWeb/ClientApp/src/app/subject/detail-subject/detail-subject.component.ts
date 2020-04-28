import { Component, OnInit } from '@angular/core';
import { SubjectService } from 'src/app/services/subject.service';
import { ActivatedRoute } from '@angular/router';
import { CentralpolicyService } from 'src/app/services/centralpolicy.service';

@Component({
  selector: 'app-detail-subject',
  templateUrl: './detail-subject.component.html',
  styleUrls: ['./detail-subject.component.css']
})
export class DetailSubjectComponent implements OnInit {

  
  resultsubjectdetail: any = []
  questionsopen: any = []
  departments: any = []
  resultprovince: any = []
  id: any
  centralpolicyid: any

  constructor(
    private subjectservice: SubjectService,
    private activatedRoute: ActivatedRoute,
    private centralpolicyservice: CentralpolicyService,
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
        this.centralpolicyid = this.resultsubjectdetail.centralPolicyId
        console.log("res: ", this.centralpolicyid);
        this.getCentralPolicyProvincesl()

      })
  }
  getCentralPolicyProvincesl() {
    this.centralpolicyservice.getdetailcentralpolicydata(this.centralpolicyid)
      .subscribe(result => {
        this.resultprovince = result
      })
  }
  back() {
    window.history.back();
  }
}

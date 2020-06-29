import { Component, OnInit } from '@angular/core';
import { SupportsubjectService } from 'src/app/services/supportsubject.service';
import { ActivatedRoute } from '@angular/router';
import { SupportcentralpolicyService } from 'src/app/services/supportcentralpolicy.service';

@Component({
  selector: 'app-support-detailsubject',
  templateUrl: './support-detailsubject.component.html',
  styleUrls: ['./support-detailsubject.component.css']
})
export class SupportDetailsubjectComponent implements OnInit {
  resultsubjectdetail: any = []
  question: any = []
  departments: any = []
  filterboxdepartments: any = []
  resultprovince: any = []
  id: any
  centralpolicyid: any
  boxcount = 0

  constructor(
    private subjectservice: SupportsubjectService,
    private activatedRoute: ActivatedRoute,
    private centralpolicyservice: SupportcentralpolicyService,
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
        this.question = this.resultsubjectdetail.subquestionCentralPolicyProvinces
        this.departments = this.resultsubjectdetail.subquestionCentralPolicyProvinces
        this.centralpolicyid = this.resultsubjectdetail.centralPolicyProvince.centralPolicyId
        //console.log("res: ", this.question);
        this.getCentralPolicyProvincesl()
        this.getboxsubquestion()
 
  
        
      })
  }
  getCentralPolicyProvincesl() {
    this.centralpolicyservice.getdetailcentralpolicydata(this.centralpolicyid)
      .subscribe(result => {
        this.resultprovince = result
      })
  }
  getboxsubquestion() {
    var departments = this.departments
   // console.log("test1: ", this.departments);
    var question = this.question
    this.filterboxdepartments = departments.filter(
      (thing, i, arr) => arr.findIndex(t => t.box === thing.box) === i
    ); 
    //console.log("test2: ", this.filterboxdepartments);

    var test: any = [];

    this.departments.forEach(element => {
      element.subjectCentralPolicyProvinceGroups.forEach(element2 => {

        if (element.id == element2.subquestionCentralPolicyProvinceId) {
          test.push({
            departmentID: element2.provincialDepartment.id,
            departmentName: element2.provincialDepartment.name,
            question: element.name,
            type: element.type,
            subquestionChoice: element.subquestionChoiceCentralPolicyProvinces,
            box: element.box
          })
        }
      });
    });
    //console.log("TEST: ", test);
  }
}

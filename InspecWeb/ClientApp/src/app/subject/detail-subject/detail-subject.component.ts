import { Component, Inject, OnInit } from '@angular/core';
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
  question: any = []
  departments: any = []
  filterboxdepartments: any = []
  resultprovince: any = []
  id: any
  centralpolicyid: any
  boxcount = 0
  downloadUrl: any;

  constructor(
    private subjectservice: SubjectService,
    private activatedRoute: ActivatedRoute,
    private centralpolicyservice: CentralpolicyService,
    @Inject('BASE_URL') baseUrl: string
  ) {
    this.id = activatedRoute.snapshot.paramMap.get('id')
    this.downloadUrl = baseUrl + '/Uploads';
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
        console.log("res: ", this.question);
        this.getCentralPolicyProvincesl()
        this.getboxsubquestion()
      })
  }
  getCentralPolicyProvincesl() {
    this.centralpolicyservice.getdetailcentralpolicydata(this.centralpolicyid)
      .subscribe(result => {
        this.resultprovince = result
        console.log("RES PROVINCE: ", this.resultprovince);

      })
  }
  getboxsubquestion() {
    var departments = this.departments
    console.log("aaaaa: ", this.departments);
    var question = this.question
    this.filterboxdepartments = departments.filter(
      (thing, i, arr) => arr.findIndex(t => t.box === thing.box) === i
    ); //song
    console.log("CCCCCCC: ", this.filterboxdepartments);

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
    console.log("TEST: ", test);



    // for (var i = 0; i < departments.length; i++) {
    //   if (departments[i].box == departments[i].box) {
    //     test.push({
    //       departments: departments[i].subjectCentralPolicyProvinceGroups
    //     })
    //   }
    //   console.log("CCCCCCCC", departments[i].subjectCentralPolicyProvinceGroups);
    // }
  }

  test(){
    this.boxcount + 1
  }
  back() {
    window.history.back();
  }
}

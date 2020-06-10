import { Component, OnInit, TemplateRef } from '@angular/core';
import { CentralpolicyService } from 'src/app/services/centralpolicy.service';
import { Router } from '@angular/router';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { UserService } from 'src/app/services/user.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ReportService } from 'src/app/services/report.service';
import { centralPolicyProvinces, subjectCentralPolicyProvinceGroups } from '../../models/reportnik';

@Component({
  selector: 'app-report-subject',
  templateUrl: './report-subject.component.html',
  styleUrls: ['./report-subject.component.css']
})
export class ReportSubjectComponent implements OnInit {

  dtOptions: DataTables.Settings = {};
  loading = false;
  resultcentralpolicy: any = []
  selectcentralpolicy: any = []
  resultreportdata: any = []
  modalRef: BsModalRef;

  constructor(
    private router: Router,
    private centralpolicyservice: CentralpolicyService,
    private reportservice: ReportService,
    private modalService: BsModalService,
    private authorize: AuthorizeService,
    private userService: UserService,
    private spinner: NgxSpinnerService) { }

  ngOnInit() {

    this.spinner.show();
    this.dtOptions = {
      pagingType: 'full_numbers',

    };

    this.getcentralpolicy()
  }
  getcentralpolicy() {
    this.centralpolicyservice.getcentralpolicydata()
      .subscribe(result => {
        this.resultcentralpolicy = result
        this.loading = true;
        this.spinner.hide();
        //console.log(this.resultcentralpolicy);
        this.selectcentralpolicy = this.resultcentralpolicy.map((item, index) => {
          return { value: item.id, label: item.title }
        })
        //console.log("selectcentralpolicy", this.selectcentralpolicy);

      })
  }
  test(id) {
    //console.log(id);
    var mapData: Array<any> = []
    this.reportservice.getreportsubject(id).subscribe(result => {
      //console.log("report", result);
      var title = result.title
      var centralPolicyProvinces: Array<centralPolicyProvinces> = result.centralPolicyProvinces

      function getDuplicateArrayElements(arr) {
        var sorted_arr = arr.slice().sort();
        var results = [];
        for (var i = 0; i < sorted_arr.length - 1; i++) {
          if (sorted_arr[i + 1].departmentId != sorted_arr[i].departmentId) {
            results.push(sorted_arr[i]);
          }
        }
        return results;
      }


      centralPolicyProvinces.forEach(item => {
        mapData.push(
          item.subjectCentralPolicyProvinces
            .filter(item => {
              return item.type == "Master"
            })
            .map(
              result => {
                var department: Array<any> = []

                result.subquestionCentralPolicyProvinces
                  .forEach((reuult, index) => {
                    reuult.subjectCentralPolicyProvinceGroups.forEach(element => {
                      department.push(element.provincialDepartment)
                    });
                  })


                return {
                  data: {
                    title,
                    subjectCentralPolicyProvinces: result.name,
                    department: getDuplicateArrayElements(department).map(result => { return result.name }),
                    province: item.province.name,
                    status: result.status
                  },
                  value: [Object.values({
                    title,
                    subjectCentralPolicyProvinces: result.name,
                    department: getDuplicateArrayElements(department).map(result => { return result.name }).toString().replace(',', '\n'),
                    province: item.province.name,
                    status: result.status
                  })],
                  column: ['co1', 'co2', 'co3', 'co4', 'co5']
                }
              })
        )
        console.log("mapData", mapData);
      })

    })
  }
  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }
}





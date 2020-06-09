import { Component, OnInit, TemplateRef } from '@angular/core';
import { CentralpolicyService } from 'src/app/services/centralpolicy.service';
import { Router } from '@angular/router';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { UserService } from 'src/app/services/user.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ReportService } from 'src/app/services/report.service';

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
    this.reportservice.getreportsubject(id).subscribe(result => {
      //console.log("report", result);
      var data:Array<any> = []
      var title = result.title
      var centralPolicyProvinces: Array<any> = result.centralPolicyProvinces
      var subjectCentralPolicyProvinces: Array<any> = []
      centralPolicyProvinces.forEach(item => {

        subjectCentralPolicyProvinces.push(
          item.subjectCentralPolicyProvinces
            .filter(item => {
              return item.type == "Master"
            }).map(result=>{return [result.name]})
        )
      })
      console.log("subjectCentralPolicyProvinces", subjectCentralPolicyProvinces);

    })
  }
  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }
}

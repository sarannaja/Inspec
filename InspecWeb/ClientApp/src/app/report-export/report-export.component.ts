import { Component, OnInit, Inject, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { ElectronicbookService } from '../services/electronicbook.service';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { InspectionplanService } from '../services/inspectionplan.service';
import { UserService } from '../services/user.service';
import { ExportReportService } from '../services/export-report.service';

@Component({
  selector: 'app-report-export',
  templateUrl: './report-export.component.html',
  styleUrls: ['./report-export.component.css']
})
export class ReportExportComponent implements OnInit {
  electronicBookData: any = [];
  loading = false;
  dtOptions: DataTables.Settings = {};
  userid: string;
  delid: any;
  modalRef: BsModalRef;
  centralpolicyprovinceid: any;
  reportData: any = [];
  role_id;
  exportData: any = [];
  url = ""

  constructor(
    private router: Router,
    private electronicBookService: ElectronicbookService,
    private authorize: AuthorizeService,
    private modalService: BsModalService,
    private inspectionplanservice: InspectionplanService,
    private spinner: NgxSpinnerService,
    private userService: UserService,
    private exportReportService: ExportReportService,
    @Inject('BASE_URL') baseUrl: string,
  ) {
    this.url = baseUrl
  }

  ngOnInit() {

    this.authorize.getUser()
      .subscribe(result => {
        this.userid = result.sub
        console.log(result);
        this.userService.getuserfirstdata(this.userid)
          .subscribe(result => {
            this.role_id = result[0].role_id
          })
      })
    this.dtOptions = {
      pagingType: 'full_numbers',
      columnDefs: [
        {
          targets: [2],
          orderable: false
        }
      ]
    };
    this.getexportport();
  }

  getexportport() {
    this.electronicBookService.getexportport(this.userid).subscribe(results => {
      console.log("res: ", results);
      this.electronicBookData = results;
      console.log("ELECTDATA: ", this.electronicBookData);

      var test: any = [];

      this.electronicBookData.forEach(element => {
        test.push({
          centralPolicyName: element.centralPolicyProvince.centralPolicy.title
        })
      });

      this.electronicBookData.forEach(element => {
        element.centralPolicyProvince.subjectCentralPolicyProvinces
      });

      this.reportData = this.electronicBookData.map((item, index) => {
        return {
          centralPolicyName: item.centralPolicyProvince.centralPolicy
        }
      })
      console.log("test: ", test);
      console.log("report: ", this.reportData);

      this.loading = true;
    })
  }

  gotoDetail(id, elecId) {
    this.router.navigate(['/electronicbook/detail/' + id, { electronicBookId: elecId }])
  }

  exportReport(elecId, cenProId) {
    // alert("eiei");
    this.exportReportService.exportReport(this.userid, elecId, cenProId).subscribe(res => {
      console.log("export: ", res);
      this.createReport(res, cenProId);
    })
  }

  createReport(res, cenProId) {
    this.exportReportService.createReport(res, cenProId).subscribe(results => {
      console.log("aaa: ", res);
      // alert("Success");
      window.open(this.url + "Uploads/" + results.data);
    })
  }


}

import { Component, OnInit, TemplateRef } from '@angular/core';
import { CentralpolicyService } from 'src/app/services/centralpolicy.service';
import { Router } from '@angular/router';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { UserService } from 'src/app/services/user.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ReportService } from 'src/app/services/report.service';
import { centralPolicyProvinces, subjectCentralPolicyProvinceGroups } from '../../models/reportnik';

import * as Excel from "exceljs/dist/exceljs.min.js";
import * as ExcelProper from "exceljs";
import * as fs from 'file-saver';
import { map } from 'rxjs/operators';

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
  userid: any

  constructor(
    private router: Router,
    private centralpolicyservice: CentralpolicyService,
    private reportservice: ReportService,
    private modalService: BsModalService,
    private authorize: AuthorizeService,
    private userService: UserService,
    private spinner: NgxSpinnerService) { }

  ngOnInit() {
    this.test(1)
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

      })
  }
  test(id) {
    //console.log(id);
    var mapData: Array<any> = []
    var dataEx: Array<any> = []
    this.reportservice.getreportsubject(id).subscribe(async result => {
      //console.log("report", result);

      var title = result.title
      var nameuser
      var centralPolicyProvinces: Array<centralPolicyProvinces> = result.centralPolicyProvinces
      console.log("nameuser", nameuser);

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


      await centralPolicyProvinces.forEach(item => {
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
                  column: ['ลำดับที่', 'หัวข้อการตรวจติดตาม', 'ประเด็นการตรวจติดตาม', 'หน่วยงานที่ได้รับประเด็นการตรวจติดตาม(ไม่ระบุจังหวัด)', 'จังหวัด', 'เจ้าของเรื่อง', 'สถานะประเด็น']
                }
              })
        )
      })
      this.exportExcel2(mapData.map((result, index) => {
        if (result.lenght != 0) {
          console.log(result);
          return result
        }

      }).map(result => {


        return [Object.values({
          title: result.data.title,
          subjectCentralPolicyProvinces: result.data.subjectCentralPolicyProvinces,
          department: result.data.department,
          province: result.data.province,
          status: result.data.status
        })]
      }), mapData[0].column)
      await mapData.forEach((element, index) => {
        console.log("mapData", element);
        dataEx.push(element.value)



      });

      // this.exportExcel2(mapData.value,mapData.column)

    })
  }
  exportExcel2(data: Array<any>, column) {
    // Excel Title, Header, Data
    const title = 'ทะเบียนรายงานผลการตรวจราชการ';
    // const header = ['Year', 'Month', 'Make', 'Model', 'Quantity', 'Pct'];
    const header = [
      'ลำดับที่',
      'วัน/เดือน/ปี ที่มีรายงาน',
      'ประเด็น/เรื่อง', 'จังหวัด', 'ผู้สร้างรายงาน',
      'สถานะรายงาน',
      'วัน/เดือน/ปี ที่ส่งรายงาน',
      'ข้อสั่งการของผู้บังคับบัญชา (มี/ไม่มี)',
      'วัน/เดือน/ปี ที่มีข้อสั่งการ',
      'ไฟล์/เอกสารแนบ (เอกสาร/ภาพ/เสียง/วีดิทัศน์)'
    ];
    // Create workbook and worksheet
    // const workbook = new Workbook();
    let workbook: ExcelProper.Workbook = new Excel.Workbook();
    let worksheet = workbook.addWorksheet('Sales Data');
    // Add Row and formatting
    const titleRow = worksheet.addRow([title]);
    titleRow.font = { name: 'Comic Sans MS', family: 4, size: 16, underline: 'double', bold: true };
    worksheet.addRow([]);
    worksheet.mergeCells('A1:F2');

    worksheet.addRow([]);

    const headerRow = worksheet.addRow(column);

    headerRow.eachCell((cell, number) => {
      cell.fill = {
        type: 'pattern',
        pattern: 'solid',
        fgColor: { argb: 'FFFFFF' },
        bgColor: { argb: 'FFFFFF' }
      };
      cell.border = { top: { style: 'thin' }, left: { style: 'thin' }, bottom: { style: 'thin' }, right: { style: 'thin' } };
    });
    data.forEach(d => {
      const row = worksheet.addRow(d);
      const qty = row.getCell(5);
    });

    workbook.xlsx.writeBuffer().then((data) => {
      let blob = new Blob([data], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' });
      fs.saveAs(blob, title + '.xlsx');
    })
    // const subTitleRow = worksheet.addRow(['Date : ' + this.datePipe.transform(new Date(), 'medium')]);
  }
  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }
}





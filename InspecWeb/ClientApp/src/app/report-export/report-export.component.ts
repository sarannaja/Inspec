import { Component, OnInit, Inject, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { ElectronicbookService } from '../services/electronicbook.service';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { InspectionplanService } from '../services/inspectionplan.service';
import { UserService } from '../services/user.service';
import { ExportReportService } from '../services/export-report.service';
import { ExcelGeneraterService } from '../services/excel-generater.service';

import * as _ from 'lodash';
import * as Excel from "exceljs/dist/exceljs.min.js";
import * as ExcelProper from "exceljs";
import * as fs from 'file-saver';

@Component({
  selector: 'app-report-export',
  templateUrl: './report-export.component.html',
  styleUrls: ['./report-export.component.css']
})
export class ReportExportComponent implements OnInit {
  electronicBookData: any = [];
  loading = false;
  dtOptions: any = {};
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
    this.testCompare();
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
    // this.excellService.generateExcel()
  }

  createReport(res, cenProId) {
    this.exportReportService.createReport(res, cenProId).subscribe(results => {
      console.log("aaa: ", res);
      // alert("Success");
      window.open(this.url + "Uploads/" + results.data);
    })
  }
  testExcel(id) {
    var monthNamesThai = ["มกราคม", "กุมภาพันธ์", "มีนาคม", "เมษายน", "พฤษภาคม", "มิถุนายน",
      "กรกฎาคม", "สิงหาคม", "กันยายน", "ตุลาคม", "พฤษจิกายน", "ธันวาคม"];

    var dayNames = ["วันอาทิตย์ที่", "วันจันทร์ที่", "วันอังคารที่", "วันพุทธที่", "วันพฤหัสบดีที่", "วันศุกร์ที่", "วันเสาร์ที่"];

    var monthNamesEng = ["January", "February", "March", "April", "May", "June",
      "July", "August", "September", "October", "November", "December"];

    var dayNamesEng = ['Sunday', 'Monday', 'tuesday', 'wednesday', 'thursday', 'friday', 'saturday'];

    var d = new Date();

    var dateNow = d.getDate() + " " + monthNamesThai[d.getMonth()] + " " + (d.getFullYear() + 543)
    console.log('generateExcel');
    this.electronicBookService.getReportExcelElectronicBook(id).subscribe(data => {
      var exe = []
      var user = []
      var date2 = []
      data.exe.forEach((item, index) => {
        exe.push(item.detailExecutiveOrder)
      })
      data.user.forEach((item, index) => {
        user.push(item.user.prefix + " " + item.user.name)
      })
      data.exe.forEach((item, index) => {
        var ddd = new Date(item.createdAt)
        date2.push(ddd.getDate() + " " + monthNamesThai[ddd.getMonth()] + " " + (ddd.getFullYear() + 543))
      })

      var subjectCentralPolicyProvinces = data.ebook.centralPolicyProvince.subjectCentralPolicyProvinces
      subjectCentralPolicyProvinces = subjectCentralPolicyProvinces.filter((item, index) => {
        return item.type == "NoMaster"
      }).map((result, index) => {

        return [index + 1, dateNow, result.name, data.ebook.centralPolicyProvince.province.name, user.toString(), "ร่าง", "วัน/เดือน/ปี ที่ส่งรายงาน", exe.toString(), date2.toString(), "ไฟล์/เอกสารแนบ (เอกสาร/ภาพ/เสียง/วีดิทัศน์)"]
      })
      console.log("data", subjectCentralPolicyProvinces)
      this.exportExcel2(subjectCentralPolicyProvinces, "");
    })
    // this.excellService.generateExcel()
  }

  exportExcel2(data: Array<any>, filename) {
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
    // Add Image
    // const logo = workbook.addImage({
    //     //   base64: logoFile.logoBase64,
    //     extension: 'png',
    // });

    // worksheet.addImage(logo, 'E1:F3');
    worksheet.mergeCells('A1:F2');
    // worksheet.mergeCells('G5:G8');

    // Blank Row
    worksheet.addRow([]);

    // Add Header Row
    const headerRow = worksheet.addRow(header);

    // Cell Style : Fill and Border
    headerRow.eachCell((cell, number) => {
      cell.fill = {
        type: 'pattern',
        pattern: 'solid',
        fgColor: { argb: 'FFFFFF' },
        bgColor: { argb: 'FFFFFF' }
      };
      cell.border = { top: { style: 'thin' }, left: { style: 'thin' }, bottom: { style: 'thin' }, right: { style: 'thin' } };
    });
    // worksheet.addRows(data);


    // Add Data and Conditional Formatting
    data.forEach(d => {
      const row = worksheet.addRow(d);
      const qty = row.getCell(5);
      // let color = 'FFFFFF';
      // if (+qty.value < 500) {
      //   color = 'FFFFFF';
      // }
      // qty.fill = {
      //   type: 'pattern',
      //   pattern: 'solid',
      //   fgColor: { argb: color }
      // };
    });

    // worksheet.getColumn(3).width = 30;
    // worksheet.getColumn(4).width = 30;
    // worksheet.addRow([]);

    // Footer Row
    // const footerRow = worksheet.addRow(['This is system generated excel sheet.']);
    // footerRow.getCell(1).fill = {
    //     type: 'pattern',
    //     pattern: 'solid',
    //     fgColor: { argb: 'FFFFFF' }
    // };
    // footerRow.getCell(1).border = { top: { style: 'thin' }, left: { style: 'thin' }, bottom: { style: 'thin' }, right: { style: 'thin' } };

    // // Merge Cells
    // worksheet.mergeCells(`A${footerRow.number}:F${footerRow.number}`);

    // Generate Excel File with given name
    workbook.xlsx.writeBuffer().then((data) => {
      let blob = new Blob([data], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' });
      fs.saveAs(blob, title + '.xlsx');
    })
    // const subTitleRow = worksheet.addRow(['Date : ' + this.datePipe.transform(new Date(), 'medium')]);
  }

  testCompare() {

    //default value
    var a = [{ 'id': 1 }, { 'id': 2 }, { 'id': 3 }, { 'id': 4 }, { 'id': 5 }, { 'id': 6 }, { 'id': 7 }];
    //new value
    var b = [{ 'id': 1 }, { 'id': 2 }, { 'id': 3 }, { 'id': 8 }];
    var test: any = [];
    test = _.differenceBy(a, b, 'id');
    console.log("Less than Default => ", test);

     //default value
     var a2 = [{ 'id': 1 }, { 'id': 2 }, { 'id': 3 }, { 'id': 4 }, { 'id': 5 }, { 'id': 6 }, { 'id': 7 }];
     //new value
     var b2 = [{ 'id': 1 }, { 'id': 2 }, { 'id': 3 }, { 'id': 8 }];
     var test2: any = [];
     test2 = _.differenceBy(b2, a2, 'id');
     console.log("More than Default => ", test2);

  }

}

import { Component, OnInit } from '@angular/core';
import * as XLSX from 'xlsx';
import * as Excel from "exceljs/dist/exceljs.min.js";
import * as ExcelProper from "exceljs";

import * as fs from 'file-saver';
import { UserService } from 'src/app/services/user.service';
@Component({
  selector: 'app-test',
  templateUrl: './test.component.html',
  styleUrls: ['./test.component.css']
})
export class TestComponent implements OnInit {

  constructor(private TestUserService: UserService) { }
  /*name of the excel-file which will be downloaded. */
  fileName = 'ExcelSheet.xlsx';
  testUser:any[] = [
    {
      id: 1,
    },
    {
      id: 2
    }
  ]
  resultUser: Array<any> = []
  exportexcel(): void {
    /* table id is passed over here */
    let element = document.getElementById('excel-table');
    const ws: XLSX.WorkSheet = XLSX.utils.table_to_sheet(element);

    /* generate workbook and add the worksheet */
    const wb: XLSX.WorkBook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, 'Sheet1');

    /* save to file */
    XLSX.writeFile(wb, this.fileName);
    // let workbook = new Workbook();
  }

  exportExcel2(data: Array<any>, filename) {
    // Excel Title, Header, Data
    const title = 'ทะเบียนรายงานผลการตรวจราชการ';
    // const header = ['Year', 'Month', 'Make', 'Model', 'Quantity', 'Pct'];
    const header = [
      'ลำดับที่',
      'วัน/เดือน/ปี ที่มีรายงาน',
      'ประเด็น/เรื่อง', 'ผู้สร้างรายงาน',
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
    worksheet.mergeCells('A1:D2');
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
      let color = 'FFFFFF';
      if (+qty.value < 500) {
        color = 'FFFFFF';
      }
      qty.fill = {
        type: 'pattern',
        pattern: 'solid',
        fgColor: { argb: color }
      };
    });

    worksheet.getColumn(3).width = 30;
    worksheet.getColumn(4).width = 30;
    worksheet.addRow([]);

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
  ngOnInit() {
    // this.exportexcel()
    this.testUser.forEach((item) => {
      this.getUserServiceLoop('001e26c1-0d87-4508-bd1a-e3b0e166f7d8')
    })
    console.log("in test component");

  }

  getUserServiceLoop(id): void {
    this.TestUserService.getuserfirstdata(id)
      .subscribe(result => {
        this.testUser = this.AddUserTest(this.testUser, result)
        // console.log();
        // return Array.from(s);
        console.log(this.testUser);
        
      })
  }

  AddUserTest(array: any, value) {
    var s = new Set(array);
    s.add(value);
    return Array.from(s);
  }

}

import { Component, OnInit, TemplateRef } from '@angular/core';
import { CentralpolicyService } from 'src/app/services/centralpolicy.service';
import { Router } from '@angular/router';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { UserService } from 'src/app/services/user.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ReportService } from 'src/app/services/report.service';
import { CentralPolicyProvince, SubjectCentralPolicyProvinceGroup, Report1, Subjectdatum } from '../../models/reportnik';

import * as Excel from "exceljs/dist/exceljs.min.js";
import * as ExcelProper from "exceljs";
import * as fs from 'file-saver';
import { map } from 'rxjs/operators';
import { ReportCentralPolicy } from 'src/app/services/nikmodel/reportCentralpolicy';

@Component({
  selector: 'app-report-subject',
  templateUrl: './report-subject.component.html',
  styleUrls: ['./report-subject.component.css']
})
export class ReportSubjectComponent implements OnInit {

  dtOptions: any = {};
  loading = false;
  resultcentralpolicy: any = []
  selectcentralpolicy: any = []
  resultreportdata: any = []
  modalRef: BsModalRef;
  userid: any
  id: any[] = []

  reportsubject: Subjectdatum[] = []
  arr: any[] = []
  constructor(
    private router: Router,
    private centralpolicyservice: CentralpolicyService,
    private reportservice: ReportService,
    private modalService: BsModalService,
    private authorize: AuthorizeService,
    private userService: UserService,
    private spinner: NgxSpinnerService) { }

  ngOnInit() {
    // this.test(1)
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
        ////console.log(this.resultcentralpolicy);
        this.selectcentralpolicy = this.resultcentralpolicy.map((item, index) => {
          return { value: item.id, label: item.title }
        })

      })
  }
  get(id: any[]) {
    // ส่งค่า promise ออกไป
    const self = this
    return new Promise((resolve, reject) => {
      // resolve(req.response);

    });
  }
  select() {
    // this.reportsubject = []
    // this.id = id
    //console.log("select", this.id);

    const doAsync = () => {
      return new Promise((resolve, reject) => {
        setTimeout(() => {
          var reportsubject: any[] = []
          for (var i1 = 0; i1 < this.id.length; i1++) {
            //console.log('this.id[i1]', this.id[i1]);
            console.log("result", this.id[0]);
            // setTimeout(() => {
            this.reportservice.getreportsubject(this.id[i1])
              .subscribe(async result => {
                // var reportsubject =
                result.subjectdata
                  .forEach(result => {
                    // return { ...result }
                    reportsubject.push(result)
                  })
                // reportsubject.push(reportsubject)
              })

            // }, 100 * i1 + 1)
          }
          resolve(reportsubject)
          // return 
        }, 1000)
      })
    }

    const doAsync2 = (reportsubject) => {
      return new Promise((resolve, reject) => {
        setTimeout(() => {
          var arr: any[] = []
          for (var i2 = 0; i2 < reportsubject.length; i2++) {
            ///////////
            // item.inspectionPlanEvent.centralPolicyUsers.filter(
            //   (thing, i, arr) => { return arr.findIndex(t => t.userId === thing.userId) === i }
            // ).filter(reportsubject => {
            //   return reportsubject.centralPolicyId == item.centralPolicyId
            // }),
            //////////
            arr.push([
              i2 + 1,
              reportsubject[i2].centralPolicyProvince.centralPolicy.title,
              reportsubject[i2].name,
              ///////////
              reportsubject[i2].subquestionCentralPolicyProvinces[0].subjectCentralPolicyProvinceGroups.map(result => {
                return result.provincialDepartment.name
              }).toString().replace(',', "\n"),
              // reportsubject[i2].centralPolicyProvince.province.name,
              // reportsubject[i2].centralPolicyProvince.centralPolicy.createdBy,
              reportsubject[i2].centralPolicyProvince.centralPolicy.status,

              //////////
              //////////

            ])

            console.log('this.arr', arr);
            this.modalRef.hide()
            // j++
          }
          resolve(arr)
          // return 
        }, 1000)
      })
    }
    console.log();
    doAsync().then((result: any[]) => {
      console.log("result", result);

      doAsync2(result).then((result: any[]) => {
        this.exportExcelService(result, 'testTitle',
          ['ลำดับที่',
            'หัวข้อการตรวจติดตาม',
            'ประเด็นการตรวจติดตาม',
            'หน่วยงานที่ได้รับประเด็นการตรวจติดตาม(ไม่ระบุจังหวัด)',
            // 'จังหวัด',
            // 'เจ้าของเรื่อง',
            'สถานะประเด็น'
          ])
      })

    })
    // get(this.id).then(result => {
    //   var j = 1
    //   for (var i2 = 0; i2 < this.reportsubject.length; i2++) {
    //     ///////////
    //     // item.inspectionPlanEvent.centralPolicyUsers.filter(
    //     //   (thing, i, arr) => { return arr.findIndex(t => t.userId === thing.userId) === i }
    //     // ).filter(this.reportsubject => {
    //     //   return this.reportsubject.centralPolicyId == item.centralPolicyId
    //     // }),
    //     //////////
    //     this.arr.push([
    //       i2 + 1,
    //       this.reportsubject[i2].centralPolicyProvince.centralPolicy.title,
    //       this.reportsubject[i2].name,
    //       ///////////
    //       this.reportsubject[i2].subquestionCentralPolicyProvinces[0].subjectCentralPolicyProvinceGroups.map(result => {
    //         return result.provincialDepartment.name
    //       }).toString().replace(',', "\n"),
    //       this.reportsubject[i2].centralPolicyProvince.province.name,
    //       this.reportsubject[i2].centralPolicyProvince.centralPolicy.createdBy,
    //       this.reportsubject[i2].centralPolicyProvince.centralPolicy.status,

    //       //////////
    //       //////////

    //     ])

    //     console.log('this.arr', this.arr);
    //     j++
    //   }
    // }).then(result => {
    //   get2()
    // })
    // function get2() {
    //   // ส่งค่า promise ออกไป
    //   this.exportExcelService(this.arr, 'testTitle',
    //     ['ลำดับที่',
    //       'หัวข้อการตรวจติดตาม',
    //       'ประเด็นการตรวจติดตาม',
    //       'หน่วยงานที่ได้รับประเด็นการตรวจติดตาม(ไม่ระบุจังหวัด)',
    //       'จังหวัด',
    //       'เจ้าของเรื่อง',
    //       'สถานะประเด็น'
    //     ])
    // }
    // setTimeout(() => {

    // }, 1000 * j)

    // this.id.forEach((id) => {

    // })


  }
  // test(id) {
  //   // var id = this.id
  //   //console.log("test", id);
  //   var mapData: Array<any> = []
  //   var dataEx: Array<any> = []
  //   this.reportservice.getreportsubject(id).subscribe(async result => {
  //     ////console.log("report", result);

  //     var title = result.centralpolicydata.title
  //     var createdBy = result.centralpolicydata.createdBy
  //     var nameuser
  //     var centralPolicyProvinces: CentralPolicyProvince[] = result.centralpolicydata.centralPolicyProvinces

  //     function getDuplicateArrayElements(arr) {
  //       var sorted_arr = arr.slice().sort();
  //       var results = [];
  //       for (var i = 0; i < sorted_arr.length - 1; i++) {
  //         if (sorted_arr[i + 1].departmentId != sorted_arr[i].departmentId) {
  //           results.push(sorted_arr[i]);
  //         }
  //       }
  //       return results;
  //     }

  //     let index: number = 0
  //     await centralPolicyProvinces.forEach(item => {
  //       mapData.push(
  //         item.subjectCentralPolicyProvinces
  //           .filter(item => {
  //             return item.type == "Master"
  //           })
  //           .map(
  //             result => {
  //               var department: Array<any> = []
  //               //console.log("result.subquestionCentralPolicyProvinces", result.subquestionCentralPolicyProvinces);

  //               result.subquestionCentralPolicyProvinces
  //                 .forEach((result, index) => {
  //                   result.subjectCentralPolicyProvinceGroups.forEach(element => {
  //                     department.push(element.provincialDepartment)
  //                   });
  //                 })
  //               index = index + 1
  //               return {
  //                 no: index,
  //                 title: title,
  //                 subjectCentralPolicyProvinces: result.name,
  //                 department: getDuplicateArrayElements(department).map(result => { return result.name }),
  //                 province: item.province.name,
  //                 createdBy: createdBy,
  //                 status: result.status
  //                 // column: ['ลำดับที่', 'หัวข้อการตรวจติดตาม', 'ประเด็นการตรวจติดตาม', 'หน่วยงานที่ได้รับประเด็นการตรวจติดตาม(ไม่ระบุจังหวัด)', 'จังหวัด', 'เจ้าของเรื่อง', 'สถานะประเด็น']
  //               }
  //             })[0]
  //       )
  //     })
  //     //console.log(mapData);
  //     this.exportExcelService(mapData, 'testTitle', ['ลำดับที่',
  //       'หัวข้อการตรวจติดตาม',
  //       'ประเด็นการตรวจติดตาม',
  //       'หน่วยงานที่ได้รับประเด็นการตรวจติดตาม(ไม่ระบุจังหวัด)',
  //       'จังหวัด',
  //       'เจ้าของเรื่อง',
  //       'สถานะประเด็น'
  //     ])
  //     // let map2 = mapData.map((result, index) => {
  //     //   if (result.lenght != 0) {
  //     //     ////console.log(result);
  //     //     return result
  //     //   }

  //     // }).map(result8 => {


  //     //   ////console.log("result8", result8[0].data.department);
  //     //   return [Object.values({
  //     //     no: result8[0].no,
  //     //     title: result8[0].data.title,
  //     //     subjectCentralPolicyProvinces: result8[0].data.subjectCentralPolicyProvinces,
  //     //     // department:Object.values(result8[0].data.department) ,
  //     //     department: 'พ่อมึงสิ',
  //     //     province: result8[0].data.province,
  //     //     status: result8[0].data.status,
  //     //   })]
  //     // })
  //     // ////console.log("mapData", mapData);
  //     // this.exportExcel2(map2, mapData[0][0].column)
  //     // await mapData.forEach((element, index) => {
  //     //   dataEx.push(element.value)
  //     // });

  //     // this.exportExcel2(mapData.value,mapData.column)

  //   })
  // }
  // exportExcel2(data: any, column) {
  //   ////console.log(column);

  //   // Excel Title, Header, Data
  //   const title = 'ทะเบียนรายงานผลการตรวจราชการ';
  //   // const header = ['Year', 'Month', 'Make', 'Model', 'Quantity', 'Pct'];
  //   const header = [
  //     'ลำดับที่',
  //     'วัน/เดือน/ปี ที่มีรายงาน',
  //     'ประเด็น/เรื่อง',
  //     'จังหวัด',
  //     'ผู้สร้างรายงาน',
  //     'สถานะรายงาน',
  //     'วัน/เดือน/ปี ที่ส่งรายงาน',
  //     'ข้อสั่งการของผู้บังคับบัญชา (มี/ไม่มี)',
  //     'วัน/เดือน/ปี ที่มีข้อสั่งการ',
  //     'ไฟล์/เอกสารแนบ (เอกสาร/ภาพ/เสียง/วีดิทัศน์)'
  //   ];
  //   // Create workbook and worksheet
  //   // const workbook = new Workbook();
  //   let workbook: ExcelProper.Workbook = new Excel.Workbook();
  //   let worksheet = workbook.addWorksheet('Sales Data');
  //   // Add Row and formatting
  //   const titleRow = worksheet.addRow([title]);
  //   titleRow.font = { name: 'Comic Sans MS', family: 4, size: 16, underline: 'double', bold: true };
  //   worksheet.addRow([]);
  //   worksheet.mergeCells('A1:F2');

  //   worksheet.addRow([]);

  //   const headerRow = worksheet.addRow(column);

  //   headerRow.eachCell((cell, number) => {
  //     cell.fill = {
  //       type: 'pattern',
  //       pattern: 'solid',
  //       fgColor: { argb: 'FFFFFF' },
  //       bgColor: { argb: 'FFFFFF' }
  //     };
  //     cell.border = { top: { style: 'thin' }, left: { style: 'thin' }, bottom: { style: 'thin' }, right: { style: 'thin' } };
  //   });

  //   ////console.log("data", data);
  //   data[0].forEach(d => {

  //     const row = worksheet.addRow(d);
  //     // const qty = row.getCell(5);
  //   });
  //   for (let i = 0; i < header.length; i++) {
  //     worksheet.getColumn(i + 1).width = 30
  //   }
  //   // header
  //   worksheet.addRow([]);
  //   workbook.xlsx.writeBuffer().then((data) => {
  //     let blob = new Blob([data], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' });
  //     fs.saveAs(blob, title + '.xlsx');
  //   })
  //   // const subTitleRow = worksheet.addRow(['Date : ' + this.datePipe.transform(new Date(), 'medium')]);
  // }
  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }
  Export() {

  }
  exportExcelService(
    data: Array<any>,
    title: string,
    column: Array<any>
  ) {

    //console.log("data", data);

    let workbook: ExcelProper.Workbook = new Excel.Workbook();
    let worksheet = workbook.addWorksheet('Sales Data');
    // Add Row and formatting
    const titleRow = worksheet.addRow([title]);
    titleRow.font = { name: 'Angsana New', family: 4, size: 16, underline: 'double', bold: true };
    worksheet.addRow([]);
    // Add Image
    // const logo = workbook.addImage({
    //     //   base64: logoFile.logoBase64,
    //     extension: 'png',
    // });

    // worksheet.addImage(logo, 'E1:F3');
    worksheet.mergeCells('A1:G2');
    // worksheet.mergeCells('G5:G8');

    // Blank Row
    worksheet.addRow([]);

    // Add Header Row
    const headerRow = worksheet.addRow(column);

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
      if (d != null) {
        var obj = Object.values(d)
        const row = worksheet.addRow(obj);
        // const qty = row.getCell(5);
        let color = 'FFFFFF';
        // if (+qty.value < 500) {
        //   color = 'FFFFFF';
        // }
        // qty.fill = {
        //   type: 'pattern',
        //   pattern: 'solid',
        //   fgColor: { argb: color }
        // };
      }

    });

    for (let i = 0; i < column.length; i++) {
      worksheet.getColumn(i + 1).width = 25
    }
    worksheet.addRow([]);


    // Generate Excel File with given name
    workbook.xlsx.writeBuffer().then((data) => {
      let blob = new Blob([data], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' });
      fs.saveAs(blob, title + '.xlsx');
    })
    // const subTitleRow = worksheet.addRow(['Date : ' + this.datePipe.transform(new Date(), 'medium')]);
  }
}





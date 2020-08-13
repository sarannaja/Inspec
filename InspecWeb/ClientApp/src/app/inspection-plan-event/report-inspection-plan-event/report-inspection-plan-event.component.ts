import { Component, OnInit, TemplateRef } from '@angular/core';
import { NgxSpinnerService } from "ngx-spinner";
import { InspectionplaneventService } from 'src/app/services/inspectionplanevent.service';
import { UserService } from 'src/app/services/user.service';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormControl, FormBuilder, FormGroup, Validators } from '@angular/forms';

import * as Excel from "exceljs/dist/exceljs.min.js";
import * as ExcelProper from "exceljs";
import * as fs from 'file-saver';
import { Calendar } from 'src/app/services/modelaof/reportInspectionplan';

@Component({
  selector: 'app-report-inspection-plan-event',
  templateUrl: './report-inspection-plan-event.component.html',
  styleUrls: ['./report-inspection-plan-event.component.css']
})
export class ReportInspectionPlanEventComponent implements OnInit {
  loading = false;
  dtOptions: DataTables.Settings = {};
  resultschedule: any[] = []
  role_id
  userid
  modalRef: BsModalRef;
  selectregion: any = [];
  resultregion: any = [];
  selectprovince: any = [];
  resultprovince: any = [];
  selectpeople: any = [];
  resultpeople: any = [];
  selectprovincialdepartment: any = [];
  resultprovincialdepartment: any = [];
  Form: FormGroup;
  Form2: FormGroup;
  Form3: FormGroup;
  Form4: FormGroup;
  owner: any
  constructor(
    private spinner: NgxSpinnerService,
    private inspectionplanservice: InspectionplaneventService,
    private userService: UserService,
    private authorize: AuthorizeService,
    private modalService: BsModalService,
    private fb: FormBuilder,
  ) { }

  ngOnInit() {
    // this.ExportProvince(1)
    this.spinner.show();

    this.authorize.getUser()
      .subscribe(result => {
        this.userid = result.sub
        this.userService.getuserfirstdata(this.userid)
          .subscribe(result => {
            this.role_id = result[0].role_id
          })
      })

    this.Form = this.fb.group({
      region: new FormControl(null, [Validators.required]),
    });

    this.Form2 = this.fb.group({
      province: new FormControl(null, [Validators.required]),
    });

    this.Form3 = this.fb.group({
      people: new FormControl(null, [Validators.required]),
    });

    this.Form4 = this.fb.group({
      provincialdepartment: new FormControl(null, [Validators.required]),
    });

    this.dtOptions = {
      pagingType: 'full_numbers',
      columnDefs: [
        {
          targets: [2],
          orderable: false
        }
      ]

    };

    this.inspectionplanservice.getscheduledata(this.userid)
      .subscribe(result => {
        // alert(JSON.stringify(result))
        this.spinner.hide();
        this.resultschedule = result
        this.loading = true
      })
  }

  openModal(template: TemplateRef<any>) {
    this.getprovince();
    this.getregion();
    this.getpeople();
    this.getprovincialdepartment();
    this.modalRef = this.modalService.show(template);
  }
  getprovincialdepartment() {
    this.inspectionplanservice.getprovincialdepartment().subscribe(response => {
      this.resultprovincialdepartment = response
      this.selectprovincialdepartment = this.resultprovincialdepartment.map((item, index) => {
        return { value: item.id, label: item.name }
      })
    })
  }

  getprovince() {
    this.inspectionplanservice.getuserregion(this.userid) // province
      .subscribe(result => {
        this.resultprovince = result
        // alert(JSON.stringify(this.resultprovince))
        this.selectprovince = this.resultprovince.map((item, index) => {
          return { value: item.province.id, label: item.province.name }
        })
      })
  }

  getregion() {
    this.inspectionplanservice.getregion(this.userid).subscribe(response => {
      this.resultregion = response
      this.selectregion = this.resultregion.map((item, index) => {
        return { value: item.id, label: item.name }
      })
    })
  }
  getpeople() {
    this.inspectionplanservice.getpeople().subscribe(response => {
      this.resultpeople = response
      this.selectpeople = this.resultpeople.map((item, index) => {
        return { value: item.id, label: item.prefix + " " + item.name }
      })
    })
  }

  ExportProvincialdepartment(value) {
    var monthNamesThai = ["มกราคม", "กุมภาพันธ์", "มีนาคม", "เมษายน", "พฤษภาคม", "มิถุนายน",
      "กรกฎาคม", "สิงหาคม", "กันยายน", "ตุลาคม", "พฤษจิกายน", "ธันวาคม"];
    var dayNames = ["วันอาทิตย์ที่", "วันจันทร์ที่", "วันอังคารที่", "วันพุทธที่", "วันพฤหัสบดีที่", "วันศุกร์ที่", "วันเสาร์ที่"];
    var monthNamesEng = ["January", "February", "March", "April", "May", "June",
      "July", "August", "September", "October", "November", "December"];
    var dayNamesEng = ['Sunday', 'Monday', 'tuesday', 'wednesday', 'thursday', 'friday', 'saturday'];
    var d = new Date();
    var dateNow = d.getDate() + " " + monthNamesThai[d.getMonth()] + " " + (d.getFullYear() + 543)
    // var centralPolicyProvinces: Array<any> = []
    this.inspectionplanservice.exportexcelcalendardepartment(value.provincialdepartment).subscribe(async data => {
      var calendar: Array<Calendar> = data.calendar
      var centralPolicyUser: Array<any> = []
      var maptest: Array<any> =
        // var centralPolicyProvinces: Array<any> = []
        calendar.map((item, index) => {
          var ddd = new Date(item.startDate)
          var startDate = ddd.getDate() + " " + monthNamesThai[ddd.getMonth()] + " " + (ddd.getFullYear() + 543)
          // var date =  item.startDate.getDate() + " " + monthNamesThai[ddd.getMonth()] + " " + (ddd.getFullYear() + 543)
          return [index + 1,
            startDate,
          item.centralPolicy.title,
          item.inspectionPlanEvent.status,
          item.inspectionPlanEvent.user.prefix + " " + item.inspectionPlanEvent.user.name,
          item.inspectionPlanEvent.user.phoneNumber,
          ///////////
          item.inspectionPlanEvent.centralPolicyUsers.filter(
            (thing, i, arr) => { return arr.findIndex(t => t.userId === thing.userId) === i }
          ).filter(result => {
            return result.centralPolicyId == item.centralPolicyId
          }).map(result => {
            return result.user.prefix + " " + result.user.name
          }).toString().replace(',', "\n"),
          //////////
          item.inspectionPlanEvent.centralPolicyUsers.filter(
            (thing, i, arr) => { return arr.findIndex(t => t.userId === thing.userId) === i }
          ).filter(result => {
            return result.centralPolicyId == item.centralPolicyId
          }).map(result => {
            return result.user.phoneNumber
          }).toString().replace(',', "\n"),
          //////////
          item.inspectionPlanEvent.centralPolicyUsers.filter(
            (thing, i, arr) => { return arr.findIndex(t => t.userId === thing.userId) === i }
          ).filter(result => {
            return result.centralPolicyId == item.centralPolicyId
          }).map(result => {
            return result.status
          }).toString().replace(',', "\n")
          ]
        })
      console.log(maptest);

      var column = ["ลำดับที่",
        "วัน/เดือน/ปี",
        "เรื่อง",
        "สถานะเรื่อง",
        "หน่วยงาน/ผต.นร./ผต.กท. (เจ้าของเรื่อง)",
        "หมายเลขติดต่อ",
        "ผู้เข้าร่วม/หน่วยงาน",
        "หมายเลขติดต่อ",
        "สถานะการเข้าร่วม"
      ]
      this.ExportExcel(maptest, column)
    })
  }
  ExportRegion(value) {
    var monthNamesThai = ["มกราคม", "กุมภาพันธ์", "มีนาคม", "เมษายน", "พฤษภาคม", "มิถุนายน",
      "กรกฎาคม", "สิงหาคม", "กันยายน", "ตุลาคม", "พฤษจิกายน", "ธันวาคม"];
    var dayNames = ["วันอาทิตย์ที่", "วันจันทร์ที่", "วันอังคารที่", "วันพุทธที่", "วันพฤหัสบดีที่", "วันศุกร์ที่", "วันเสาร์ที่"];
    var monthNamesEng = ["January", "February", "March", "April", "May", "June",
      "July", "August", "September", "October", "November", "December"];
    var dayNamesEng = ['Sunday', 'Monday', 'tuesday', 'wednesday', 'thursday', 'friday', 'saturday'];
    var d = new Date();
    var dateNow = d.getDate() + " " + monthNamesThai[d.getMonth()] + " " + (d.getFullYear() + 543)
    // var centralPolicyProvinces: Array<any> = []
    this.inspectionplanservice.getexportregion(value.region).subscribe(async data => {
      var calendar: Array<Calendar> = data.calendar
      var centralPolicyUser: Array<any> = []
      var maptest: Array<any> =
        // var centralPolicyProvinces: Array<any> = []
        calendar.map((item, index) => {
          var ddd = new Date(item.startDate)
          var startDate = ddd.getDate() + " " + monthNamesThai[ddd.getMonth()] + " " + (ddd.getFullYear() + 543)
          // var date =  item.startDate.getDate() + " " + monthNamesThai[ddd.getMonth()] + " " + (ddd.getFullYear() + 543)
          return [index + 1,
            startDate,
          item.centralPolicy.title,
          item.inspectionPlanEvent.status,
          item.inspectionPlanEvent.user.prefix + " " + item.inspectionPlanEvent.user.name,
          item.inspectionPlanEvent.user.phoneNumber,
          ///////////
          item.inspectionPlanEvent.centralPolicyUsers.filter(
            (thing, i, arr) => { return arr.findIndex(t => t.userId === thing.userId) === i }
          ).filter(result => {
            return result.centralPolicyId == item.centralPolicyId
          }).map(result => {
            return result.user.prefix + " " + result.user.name
          }).toString().replace(',', "\n"),
          //////////
          item.inspectionPlanEvent.centralPolicyUsers.filter(
            (thing, i, arr) => { return arr.findIndex(t => t.userId === thing.userId) === i }
          ).filter(result => {
            return result.centralPolicyId == item.centralPolicyId
          }).map(result => {
            return result.user.phoneNumber
          }).toString().replace(',', "\n"),
          //////////
          item.inspectionPlanEvent.centralPolicyUsers.filter(
            (thing, i, arr) => { return arr.findIndex(t => t.userId === thing.userId) === i }
          ).filter(result => {
            return result.centralPolicyId == item.centralPolicyId
          }).map(result => {
            return result.status
          }).toString().replace(',', "\n")
          ]
        })
      console.log(maptest);

      var column = ["ลำดับที่",
        "วัน/เดือน/ปี",
        "เรื่อง",
        "สถานะเรื่อง",
        "หน่วยงาน/ผต.นร./ผต.กท. (เจ้าของเรื่อง)",
        "หมายเลขติดต่อ",
        "ผู้เข้าร่วม/หน่วยงาน",
        "หมายเลขติดต่อ",
        "สถานะการเข้าร่วม"
      ]
      this.ExportExcel(maptest, column)
    })
  }

  ExportProvince(value) {
    var monthNamesThai = ["มกราคม", "กุมภาพันธ์", "มีนาคม", "เมษายน", "พฤษภาคม", "มิถุนายน",
      "กรกฎาคม", "สิงหาคม", "กันยายน", "ตุลาคม", "พฤษจิกายน", "ธันวาคม"];
    var dayNames = ["วันอาทิตย์ที่", "วันจันทร์ที่", "วันอังคารที่", "วันพุทธที่", "วันพฤหัสบดีที่", "วันศุกร์ที่", "วันเสาร์ที่"];
    var monthNamesEng = ["January", "February", "March", "April", "May", "June",
      "July", "August", "September", "October", "November", "December"];
    var dayNamesEng = ['Sunday', 'Monday', 'tuesday', 'wednesday', 'thursday', 'friday', 'saturday'];
    var d = new Date();
    var dateNow = d.getDate() + " " + monthNamesThai[d.getMonth()] + " " + (d.getFullYear() + 543)
    // var centralPolicyProvinces: Array<any> = []
    this.inspectionplanservice.getexportprovince(value.province).subscribe(async data => {
      var calendar: Array<Calendar> = data.calendar
      var centralPolicyUser: Array<any> = []
      var maptest: Array<any> =
        // var centralPolicyProvinces: Array<any> = []
        calendar.map((item, index) => {
          var ddd = new Date(item.startDate)
          var startDate = ddd.getDate() + " " + monthNamesThai[ddd.getMonth()] + " " + (ddd.getFullYear() + 543)
          // var date =  item.startDate.getDate() + " " + monthNamesThai[ddd.getMonth()] + " " + (ddd.getFullYear() + 543)
          return [index + 1,
            startDate,
          item.centralPolicy.title,
          item.inspectionPlanEvent.status,
          item.inspectionPlanEvent.user.prefix + " " + item.inspectionPlanEvent.user.name,
          item.inspectionPlanEvent.user.phoneNumber,
          ///////////
          item.inspectionPlanEvent.centralPolicyUsers.filter(
            (thing, i, arr) => { return arr.findIndex(t => t.userId === thing.userId) === i }
          ).filter(result => {
            return result.centralPolicyId == item.centralPolicyId
          }).map(result => {
            return result.user.prefix + " " + result.user.name
          }).toString().replace(',', "\n"),
          //////////
          item.inspectionPlanEvent.centralPolicyUsers.filter(
            (thing, i, arr) => { return arr.findIndex(t => t.userId === thing.userId) === i }
          ).filter(result => {
            return result.centralPolicyId == item.centralPolicyId
          }).map(result => {
            return result.user.phoneNumber
          }).toString().replace(',', "\n"),
          //////////
          item.inspectionPlanEvent.centralPolicyUsers.filter(
            (thing, i, arr) => { return arr.findIndex(t => t.userId === thing.userId) === i }
          ).filter(result => {
            return result.centralPolicyId == item.centralPolicyId
          }).map(result => {
            return result.status
          }).toString().replace(',', "\n")
          ]
        })
      console.log(maptest);

      var column = ["ลำดับที่",
        "วัน/เดือน/ปี",
        "เรื่อง",
        "สถานะเรื่อง",
        "หน่วยงาน/ผต.นร./ผต.กท. (เจ้าของเรื่อง)",
        "หมายเลขติดต่อ",
        "ผู้เข้าร่วม/หน่วยงาน",
        "หมายเลขติดต่อ",
        "สถานะการเข้าร่วม"
      ]
      this.ExportExcel(maptest, column)
      //console.log('test', test);
    })
  }
  ExportPeople(value) {
    var monthNamesThai = ["มกราคม", "กุมภาพันธ์", "มีนาคม", "เมษายน", "พฤษภาคม", "มิถุนายน",
      "กรกฎาคม", "สิงหาคม", "กันยายน", "ตุลาคม", "พฤษจิกายน", "ธันวาคม"];
    var dayNames = ["วันอาทิตย์ที่", "วันจันทร์ที่", "วันอังคารที่", "วันพุทธที่", "วันพฤหัสบดีที่", "วันศุกร์ที่", "วันเสาร์ที่"];
    var monthNamesEng = ["January", "February", "March", "April", "May", "June",
      "July", "August", "September", "October", "November", "December"];
    var dayNamesEng = ['Sunday', 'Monday', 'tuesday', 'wednesday', 'thursday', 'friday', 'saturday'];
    var d = new Date();
    var dateNow = d.getDate() + " " + monthNamesThai[d.getMonth()] + " " + (d.getFullYear() + 543)
    // var centralPolicyProvinces: Array<any> = []
    this.inspectionplanservice.getexportpeople(value.people).subscribe(async data => {
      var calendar: Array<Calendar> = data.calendar
      var centralPolicyUser: Array<any> = []
      var maptest: Array<any> =
        // var centralPolicyProvinces: Array<any> = []
        calendar.map((item, index) => {
          var ddd = new Date(item.startDate)
          var startDate = ddd.getDate() + " " + monthNamesThai[ddd.getMonth()] + " " + (ddd.getFullYear() + 543)
          // var date =  item.startDate.getDate() + " " + monthNamesThai[ddd.getMonth()] + " " + (ddd.getFullYear() + 543)
          return [index + 1,
            startDate,
          item.centralPolicy.title,
          item.inspectionPlanEvent.status,
          item.inspectionPlanEvent.user.prefix + " " + item.inspectionPlanEvent.user.name,
          item.inspectionPlanEvent.user.phoneNumber,
          ///////////
          item.inspectionPlanEvent.centralPolicyUsers.filter(
            (thing, i, arr) => { return arr.findIndex(t => t.userId === thing.userId) === i }
          ).filter(result => {
            return result.centralPolicyId == item.centralPolicyId
          }).map(result => {
            return result.user.prefix + " " + result.user.name
          }).toString().replace(',', "\n"),
          //////////
          item.inspectionPlanEvent.centralPolicyUsers.filter(
            (thing, i, arr) => { return arr.findIndex(t => t.userId === thing.userId) === i }
          ).filter(result => {
            return result.centralPolicyId == item.centralPolicyId
          }).map(result => {
            return result.user.phoneNumber
          }).toString().replace(',', "\n"),
          //////////
          item.inspectionPlanEvent.centralPolicyUsers.filter(
            (thing, i, arr) => { return arr.findIndex(t => t.userId === thing.userId) === i }
          ).filter(result => {
            return result.centralPolicyId == item.centralPolicyId
          }).map(result => {
            return result.status
          }).toString().replace(',', "\n")
          ]
        })
      console.log(maptest);

      var column = ["ลำดับที่",
        "วัน/เดือน/ปี",
        "เรื่อง",
        "สถานะเรื่อง",
        "หน่วยงาน/ผต.นร./ผต.กท. (เจ้าของเรื่อง)",
        "หมายเลขติดต่อ",
        "ผู้เข้าร่วม/หน่วยงาน",
        "หมายเลขติดต่อ",
        "สถานะการเข้าร่วม"
      ]
      this.ExportExcel(maptest, column)
      //console.log('test', test);
    })
  }

  ExportDate() {
    var monthNamesThai = ["มกราคม", "กุมภาพันธ์", "มีนาคม", "เมษายน", "พฤษภาคม", "มิถุนายน",
      "กรกฎาคม", "สิงหาคม", "กันยายน", "ตุลาคม", "พฤษจิกายน", "ธันวาคม"];
    var dayNames = ["วันอาทิตย์ที่", "วันจันทร์ที่", "วันอังคารที่", "วันพุทธที่", "วันพฤหัสบดีที่", "วันศุกร์ที่", "วันเสาร์ที่"];
    var monthNamesEng = ["January", "February", "March", "April", "May", "June",
      "July", "August", "September", "October", "November", "December"];
    var dayNamesEng = ['Sunday', 'Monday', 'tuesday', 'wednesday', 'thursday', 'friday', 'saturday'];
    var d = new Date();
    var dateNow = d.getDate() + " " + monthNamesThai[d.getMonth()] + " " + (d.getFullYear() + 543)
    // var centralPolicyProvinces: Array<any> = []
    this.inspectionplanservice.getexportdate().subscribe(async data => {
      var calendar: Array<Calendar> = data.calendar
      var centralPolicyUser: Array<any> = []
      var maptest: Array<any> =
        // var centralPolicyProvinces: Array<any> = []
        calendar.map((item, index) => {
          var ddd = new Date(item.startDate)
          var startDate = ddd.getDate() + " " + monthNamesThai[ddd.getMonth()] + " " + (ddd.getFullYear() + 543)
          // var date =  item.startDate.getDate() + " " + monthNamesThai[ddd.getMonth()] + " " + (ddd.getFullYear() + 543)
          return [index + 1,
            startDate,
          item.centralPolicy.title,
          item.inspectionPlanEvent.status,
          item.inspectionPlanEvent.user.prefix + " " + item.inspectionPlanEvent.user.name,
          item.inspectionPlanEvent.user.phoneNumber,
          ///////////
          item.inspectionPlanEvent.centralPolicyUsers.filter(
            (thing, i, arr) => { return arr.findIndex(t => t.userId === thing.userId) === i }
          ).filter(result => {
            return result.centralPolicyId == item.centralPolicyId
          }).map(result => {
            return result.user.prefix + " " + result.user.name
          }).toString().replace(',', "\n"),
          //////////
          item.inspectionPlanEvent.centralPolicyUsers.filter(
            (thing, i, arr) => { return arr.findIndex(t => t.userId === thing.userId) === i }
          ).filter(result => {
            return result.centralPolicyId == item.centralPolicyId
          }).map(result => {
            return result.user.phoneNumber
          }).toString().replace(',', "\n"),
          //////////
          item.inspectionPlanEvent.centralPolicyUsers.filter(
            (thing, i, arr) => { return arr.findIndex(t => t.userId === thing.userId) === i }
          ).filter(result => {
            return result.centralPolicyId == item.centralPolicyId
          }).map(result => {
            return result.status
          }).toString().replace(',', "\n")
          ]
        })
      console.log(maptest);

      var column = ["ลำดับที่",
        "วัน/เดือน/ปี",
        "เรื่อง",
        "สถานะเรื่อง",
        "หน่วยงาน/ผต.นร./ผต.กท. (เจ้าของเรื่อง)",
        "หมายเลขติดต่อ",
        "ผู้เข้าร่วม/หน่วยงาน",
        "หมายเลขติดต่อ",
        "สถานะการเข้าร่วม"
      ]
      this.ExportExcel(maptest, column)
      //console.log('test', test);
    })
  }

  ExportExcel(data: Array<any>, column) {
    const title = 'กำหนดการตรวจราชการ';
    const header: Array<any> = column

    let workbook: ExcelProper.Workbook = new Excel.Workbook();
    let worksheet = workbook.addWorksheet('Sales Data');

    const titleRow = worksheet.addRow([title]);
    titleRow.font = { name: 'Comic Sans MS', family: 4, size: 16, underline: 'double', bold: true };
    worksheet.addRow([]);

    worksheet.mergeCells('A1:F2');

    for (let i = 0; i < column.length; i++) {
      worksheet.getColumn(i + 1).width = 25
    }

    worksheet.addRow([]);

    const headerRow = worksheet.addRow(header);

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
  }

  exportExcelService(
    data: Array<any>,
    title: string,
    column: Array<any>
  ) {


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
      var obj = Object.values(d)
      const row = worksheet.addRow(obj);
      // const qty = row.getCell(5);
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

    for (let i = 0; i < column.length; i++) {
      worksheet.getColumn(i + 1).width = 25
    }
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

}

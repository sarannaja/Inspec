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
  selectprovince: any = [];
  resultregion: any = [];
  resultprovince: any = []
  Form2: FormGroup;
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

    this.Form2 = this.fb.group({
      province: new FormControl(null, [Validators.required]),
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

    this.modalRef = this.modalService.show(template);
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
        return { value: item.region.id, label: item.region.name }
      })
    })
  }



  ExportRegion(value) {

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
    var centralPolicyProvinces: Array<any> = []
    this.inspectionplanservice.getexportprovince(value.province).subscribe(async data => {
      var calendar: Array<any> = data.calendar
      var centralPolicyUser: Array<any> = []
      var centralPolicyProvinces: Array<any> = []
      calendar
        .forEach((item, index) => {

          var createdAt = item.createdAt
          // centralPolicyEvents= item.centralPolicyEvents
          var centralPolicy: any
          var centralPolicyEvents: Array<any> = item.centralPolicyEvents

          centralPolicyEvents.forEach((item, idnex) => {
            centralPolicy = item.centralPolicy
            var centralPolicyProvinces: Array<any> = centralPolicy.centralPolicyProvinces
            console.log('item.centralPolicyProvinces', centralPolicy.centralPolicyProvinces);

            var title = centralPolicy.title
            if (centralPolicy.centralPolicyUser.length != 0) {
              //console.log('centralPolicy.centralPolicyUser.length', centralPolicy.centralPolicyUser.length);
              // var test =
              // if(centralPolicyProvinces)

              centralPolicyUser.push(centralPolicy.centralPolicyUser.filter(item => {
                return item.provinceId == value.province

              }).map(item => {


                var name = item.user.prefix + ' ' + item.user.name
                var phone = item.phoneNumber
                var status = item.status
                var ownerId = item.user.id
                var statusPlan = centralPolicyProvinces
                  .filter(result => {
                    return result.provinceId == value.province
                  })[0].status
                //   .catch(err => {
                //   //console.log('error');
                // });
                return { name: name, phone, status, ownerId, createdAt, title, statusPlan }

              }))
              // centralPolicy.centralPolicyUser.forEach(element => {
              //   if (element.provinceId == 1)

              // })
            }
          })

        })
      var column = [
        'ลำดับที่',
        'วัน/เดือน/ปี',
        'เรื่อง',
        // 'เอกสารประกอบ การตรวจราชการ',
        'สถานะเรื่อง',
        'หน่วยงาน/ผต.นร./ผต.กท. (เจ้าของเรื่อง)',
        'หมายเลขติดต่อ',
        'ผู้เข้าร่วม/หน่วยงาน',
        'หมายเลขติดต่อ',
        'สถานะ การเข้าร่วม'
      ];
      var test = centralPolicyUser.map((item, index) => {
        // var d = new Date();
        var ddd = new Date(item[0].createdAt)
        var date = ddd.getDate() + " " + monthNamesThai[ddd.getMonth()] + " " + (ddd.getFullYear() + 543)
        //
        // var data:any
        var name: Array<any> = []
        var phone: Array<any> = []
        var status: Array<any> = []
        item.forEach((item) => {
          name.push(item.name)
          phone.push(item.phone)
          status.push(item.status)

          // return data
        })
        console.log('name', name);

        // date2.push()
        return [index + 1, date, item[0].title, item[0].statusPlan, 'ow', 'หมายเลขติดต่อ ow', name.
        toString().replace(',', "\n"), phone.toString().replace(',', "\n"), status.toString().replace(',', "\n")]
      })

      console.log('test', test);

      this.ExportExcelProvince(test, column)
      // var data: Array<any>
      // item.map((item) => {
      //   data['title'] = item.title
      //   console.log(data);

      //   return {data}
      // })
      console.log('centralPolicyUser', centralPolicyUser);



    })
  }


  ExportExcelProvince(data: Array<any>, column) {
    const title = 'กำหนดการตรวจราชการ';
    const header: Array<any> = column

    let workbook: ExcelProper.Workbook = new Excel.Workbook();
    let worksheet = workbook.addWorksheet('Sales Data');

    const titleRow = worksheet.addRow([title]);
    titleRow.font = { name: 'Comic Sans MS', family: 4, size: 16, underline: 'double', bold: true };
    worksheet.addRow([]);

    worksheet.mergeCells('A1:F2');

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
}

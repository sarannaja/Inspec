import { Component, OnInit } from '@angular/core';
import { ExternalOrganizationService } from 'src/app/services/external-organization.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { UserService } from 'src/app/services/user.service';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { OpmCaseDetailComponent } from '../modals/detail-modal/detail-modal.component';
import { IMyOptions } from 'mydatepicker-th';

@Component({
  selector: 'app-gcc1111-table',
  templateUrl: './gcc1111-table.component.html',
  styleUrls: ['./gcc1111-table.component.css']
})
export class Gcc1111TableComponent implements OnInit {
  results: Array<any>
  // dtOptions: DataTables.Settings = {
  dtOptions: any = {
    // dom: 'Bfrtip',
    buttons: [
      { extend: 'copy', text: 'คัดลอก', className: 'btn btn-primary glyphicon glyphicon-duplicate' },
      { extend: 'csv', text: 'ส่งออก csv', className: 'btn btn-primary glyphicon glyphicon-save-file' },
      { extend: 'excel', text: 'ส่งออก excel', className: 'btn btn-primary glyphicon glyphicon-list-alt' },
      { extend: 'pdf', text: 'ส่งออก pdf', className: 'btn btn-primary glyphicon glyphicon-file' },
      { extend: 'print', text: 'ส่งออกออกจากเครื่องพิมพ์', className: 'btn btn-primary glyphicon glyphicon-print' },
      // {
      //   text: 'My button',
      //   action: function (e, dt, node, config) {
      //     alert('Button activated');
      //   }
      // }
    ]
  };
  loading: boolean = false
  userId
  modalRef: BsModalRef;
  myDatePickerOptions: IMyOptions = {
    // other options...
    dateFormat: 'dd mmm yyyy',
    // markCurrentDay: true,
    // inline: true,
    monthLabels: {
      1: 'ม.ค.',
      2: 'ก.พ.',
      3: 'มี.ค.',
      4: 'เม.ย.',
      5: 'พ.ค.',
      6: 'มิ.ย.',
      7: 'ก.ค.',
      8: 'ส.ค.',
      9: 'ก.ย.',
      10: 'ต.ค.',
      11: 'พ.ย.',
      12: 'ธ.ค.',
    },
    dayLabels: {
      su: 'อา.',
      mo: 'จ.',
      tu: 'อ.',
      we: 'พ.',
      th: 'พฤ.',
      fr: 'ศ.',
      sa: 'ส.',
    }
  };

  // Initialized to specific date (09.10.2018).
  // model: Object = { date: { year: 2018, month: 10, day: 9 } };
  constructor(
    private spinner: NgxSpinnerService,
    private extranalService: ExternalOrganizationService,
    private authorizeService: AuthorizeService,
    private modalService: BsModalService,

  ) { }
  getData() {
    // this.userManager.getUser().then(result=>{
    //   //console.log('result',result);

    // })
    this.extranalService.getGcc1111UserProvince(this.userId)
      .subscribe(result => {
        // //console.log(result);

        this.loading = false
        this.results = result
        this.loading = true
        this.spinner.hide();

      })
    // //console.log("user", );

  }
  openModal(data) {
    this.modalRef = this.modalService.show(OpmCaseDetailComponent, {
      initialState: {
        title: data.objective_text,
        id: data.case_id,
        data: data,
        date: data.date_opened

      },
      class: 'modal-dialog-centered modal-lg'
    });
  }

  // myDatePickerOptions: IMyOptions = {
  //   // other options...
  //   // dateFormat: 'dd/mm/yyyy',

  // };
  myDatePickerOptionsEnddate: IMyOptions = {
    // other options...
    // dateFormat: 'dd/mm/yyyy',

  };
  start_date
  end_date
  startdate(event = null, i = null) {

    this.end_date ? this.checkStarttoResetEndDate(i) : null
    this.start_date = event
    this.disableDate_i(event, i)
    // this.value.emit({ start_date: this.setDateTime(this.start_date), end_date: this.setDateTime(this.end_date) })

    //console.log(this.start_date);

    // alert(JSON.stringify(event))
  }

  checkStarttoResetEndDate(index) {
    this.end_date = void 0;
    // this.value.emit({ start_date: this.setDateTime(this.start_date), end_date: this.setDateTime(this.end_date) })
  }
  starttime(event, i) {
    this.start_date = event
    this.end_date = null
    // this.value.emit({ start_date: this.setDateTime(this.start_date), end_date: this.setDateTime(this.end_date) })

  }
  disableDate_i(date: any, i) {
    this.myDatePickerOptionsEnddate = {
      disableDateRanges: [{
        end: { year: date.getFullYear(), month: date.getMonth() + 1, day: date.getDate() - 1 },
        begin: { year: date.getFullYear() - 10, month: date.getMonth() + 1, day: date.getDate() }
      }]
    }
  }
  ngOnInit() {
    this.spinner.show();
    this.authorizeService.getUser()
      .subscribe(result => {
        this.userId = result.sub

      })
    setTimeout(() => { this.getData() }, 100)
  }
  enddate(event, i) {

    this.end_date = event
    // this.value.emit({ start_date: this.setDateTime(this.start_date), end_date: this.setDateTime(this.end_date) })
    let value = { start_date: this.start_date, end_date: event }
    this.onDateRangeChanged(value)
  }
  onDateRangeChanged(value) {
    // //console.log(value);
    if (value.start_date && value.end_date) {
      this.loading = false
      this.spinner.show();
      this.extranalService.getGcc1111UserProvince(this.userId)
        .subscribe(result => {
          //console.log((Date.parse(result[0].date_opened) / 1000).toString().length,Date.parse(value.beginJsDate));
          console.log(value);

          this.results = result.filter(result => {
            return (Date.parse(result.date_opened)) >= Date.parse(value.start_date) && (Date.parse(result.date_opened)) <= Date.parse(value.end_date)
          })
          //  this.results.forEach(result=>{ //console.log(Date.parse(result.date_opened))});

          this.loading = true
          this.spinner.hide();

        })
    } else {
      return;
    }

  }
}

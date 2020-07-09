import { Component, OnInit } from '@angular/core';
import { ExternalOrganizationService } from 'src/app/services/external-organization.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { UserService } from 'src/app/services/user.service';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { UserManager } from 'oidc-client';
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
  dtOptions: DataTables.Settings = {};
  loading: boolean = false
  userId
  modalRef: BsModalRef;
  myDatePickerOptions: IMyOptions = {
    // other options...
    dateFormat: 'dd mmm yyyy',
    markCurrentDay: true,
    inline: true,
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
    //   console.log('result',result);

    // })
    this.extranalService.getGcc1111UserProvince(this.userId)
      .subscribe(result => {
        // console.log(result);

        this.loading = false
        this.results = result
        this.loading = true
        this.spinner.hide();

      })
    // console.log("user", );

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
  ngOnInit() {
    this.spinner.show();
    this.authorizeService.getUser()
      .subscribe(result => {
        this.userId = result.sub

      })
    setTimeout(() => { this.getData() }, 100)
  }
  onDateRangeChanged(value) {
    // console.log(value);

    this.loading = false
    this.spinner.show();
    this.extranalService.getGcc1111UserProvince(this.userId)
      .subscribe(result => {
        // console.log((Date.parse(result[0].date_opened) / 1000).toString().length);

        this.results = result.filter(result => {
          return (Date.parse(result.date_opened) / 999) >= value.beginEpoc && (Date.parse(result.date_opened) / 999) < value.endEpoc
        })
        // console.log(this.results);

        this.loading = true
        this.spinner.hide();

      })
  }
}

import { Component, OnInit } from '@angular/core';
import { ExternalOrganizationService } from 'src/app/services/external-organization.service';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-gcc-opm-table',
  templateUrl: './gcc-opm-table.component.html',
  styleUrls: ['./gcc-opm-table.component.css']
})

export class GccOpmTableComponent implements OnInit {
  results: Array<any>
  provice: any
  wara: any
  dtOptions: DataTables.Settings = {};
  loading: boolean = false
  provincewara: any = {
    provinces: [],
    wara: [],
  }
  constructor(
    private spinner: NgxSpinnerService,
    private extranalService: ExternalOrganizationService
  ) {

  }
  getData(provinces, wara) {

    this.extranalService.getGccopm(provinces, wara)
      .subscribe(result => {
        this.results = result
        this.loading = true
        this.spinner.hide();

      })
  }

  async getProviceWara() {
    await this.extranalService.getGccProvice()
      .subscribe(result => {
        this.provincewara.provinces = result.filter((result, data) => {
          return result.is_active != 0
        })

        this.extranalService.getGccwara()
          .subscribe(result => {
            this.provincewara.wara = result
            this.getData(this.provincewara.provinces[0].value, this.provincewara.wara[0].value)
            this.provice = this.provincewara.provinces[0].value
            this.wara = this.provincewara.wara[0].value
          })
      })
    console.log(this.provincewara);

  }
  ngOnInit() {
    this.getProviceWara()
    this.spinner.show();
    this.dtOptions = {
      pagingType: 'full_numbers',
      "language": {
        "lengthMenu": "แสดง  _MENU_  รายการ",
        "search": "ค้นหา:",
        "info": "แสดง _START_ ถึง _END_ จาก _TOTAL_ แถว",
        "infoEmpty": "แสดง 0 ของ 0 รายการ",
        "zeroRecords": "ไม่พบข้อมูล",
        "paginate": {
          "first": "หน้าแรก",
          "last": "หน้าสุดท้าย",
          "next": "ต่อไป",
          "previous": "ย้อนกลับ"
        },
      },
      dom: 'Bfrtip',
      buttons: [
        { extend: 'excel', text: 'Excel', className: 'btn btn-success glyphicon glyphicon-list-alt' },
        { extend: 'pdf', text: 'Pdf', className: 'btn btn-primary glyphicon glyphicon-file' },
        { extend: 'print', text: 'Print', className: 'btn btn-primary glyphicon glyphicon-print' }
      ]
    };

    // this.getData()
  }

  ChangeProviceWara(event, type) {
    this.loading = false
    this.spinner.show();
    this.getData(this.provice, this.wara);
  }
  excel(provice,wara){ 
    window.location.href = '/api/ExternalOrganization/excelgccopm/'+provice+'/'+wara;
  }
}

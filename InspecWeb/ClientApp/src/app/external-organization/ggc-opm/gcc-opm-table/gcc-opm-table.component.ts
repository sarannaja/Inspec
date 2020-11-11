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

import { Component, OnInit } from '@angular/core';
import { ExternalOrganizationService } from 'src/app/services/external-organization.service';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-gcc1111-table',
  templateUrl: './gcc1111-table.component.html',
  styleUrls: ['./gcc1111-table.component.css']
})
export class Gcc1111TableComponent implements OnInit {
  results: Array<any>
  dtOptions: DataTables.Settings = {};
  loading: boolean = false
  constructor(
    private spinner: NgxSpinnerService,
    private extranalService: ExternalOrganizationService
  ) { }
  getData() {

    this.extranalService.getGcc1111()
      .subscribe(result => {
        this.results = result
        this.loading = true
        this.spinner.hide();

      })
  }
  ngOnInit() {
    this.getData()
  }

}

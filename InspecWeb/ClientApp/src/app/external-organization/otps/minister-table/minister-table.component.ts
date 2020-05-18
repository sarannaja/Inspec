import { Component, OnInit, Input } from '@angular/core';
import { ExternalOrganizationService } from 'src/app/services/external-organization.service';
import { Ministers } from 'src/app/models/otps';

@Component({
  selector: 'app-minister-table',
  templateUrl: './minister-table.component.html',
  styleUrls: ['./minister-table.component.css']
})
export class MinisterTableComponent implements OnInit {
  dtOptions: DataTables.Settings = {};
  @Input() ministers: Array<Ministers>
  @Input() loading: boolean 
  constructor(private externalOrganizationService: ExternalOrganizationService) { }

  ngOnInit() {
    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 10,
      // processing: true
    };
   
  }

}

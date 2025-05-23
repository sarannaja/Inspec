import { Component, OnInit, Input } from '@angular/core';
import { Cabinets } from '../../models/otps';

@Component({
  selector: 'app-cabinet-table',
  templateUrl: './cabinet-table.component.html',
  styleUrls: ['./cabinet-table.component.css']
})
export class CabinetTableComponent implements OnInit {
  dtOptions: any = {};
  @Input() cabinets: Array<Cabinets>
  @Input() loading: boolean
  constructor() { }

  ngOnInit() {

    
    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 10,
      // processing: true
    };

  }
}

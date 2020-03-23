import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-support-government',
  templateUrl: './support-government.component.html',
  styleUrls: ['./support-government.component.css']
})
export class SupportGovernmentComponent implements OnInit {

  loading = true;
  dtOptions: DataTables.Settings = {};

  constructor() { }

  ngOnInit() {

    this.dtOptions = {
      pagingType: 'full_numbers',
    };
  }

}

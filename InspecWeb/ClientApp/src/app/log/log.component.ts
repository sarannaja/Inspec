import { Component, OnInit } from '@angular/core';
import { LogService } from '../services/log.service';

@Component({
  selector: 'app-log',
  templateUrl: './log.component.html',
  styleUrls: ['./log.component.css']
})
export class LogComponent implements OnInit {
  loading = false;
  resultlog: any[] = [];
  dtOptions: DataTables.Settings = {};
  constructor(private logService: LogService,) { }

  ngOnInit() {
    this.getlogdata();
  }

  getlogdata(){
    this.logService.getLogData()
    .subscribe(result => {
      console.log('momomo',result);
      this.resultlog = result;
      this.loading = true
    })
  }

}

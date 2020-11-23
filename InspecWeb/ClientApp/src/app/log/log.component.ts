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
  dtOptions: any = {};
  constructor(private logService: LogService,) { }

  ngOnInit() {
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
        { extend: 'print', text: 'Print', className: 'btn btn-primary glyphicon glyphicon-print' },
      ]
    };
    this.getlogdata();
  }

  getlogdata(){
    this.logService.getLogData()
    .subscribe(result => {
     // console.log('momomo',result);
      this.resultlog = result;
      this.loading = true
    })
  }

}

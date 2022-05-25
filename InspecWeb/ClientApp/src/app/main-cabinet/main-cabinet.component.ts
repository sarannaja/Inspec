import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-main-cabinet',
  templateUrl: './main-cabinet.component.html',
  styleUrls: ['./main-cabinet.component.css']
})
export class MaincabinetComponent implements OnInit {

  loading = true;
  dtOptions: any = {};
  datasupport: any = [
    {
      id:1,
      name: 'คณะรัฐมนตรี (จากกำหนดสิทธิ์)',
      link:'/cabinetserver',
      forblank:0
    },
    {
      id:2,
      name: 'คณะรัฐมนตรี (จากระบบ ส.กกภ.)',
      link:'/external/otps',
      forblank:0
    },
  ]

  constructor() { }

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
      }

    };
  }

}

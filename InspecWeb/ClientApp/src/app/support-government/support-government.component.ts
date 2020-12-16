import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-support-government',
  templateUrl: './support-government.component.html',
  styleUrls: ['./support-government.component.css']
})
export class SupportGovernmentComponent implements OnInit {

  loading = true;
  dtOptions: any = {};
  datasupport: any = [
    {
      id:1,
      name: 'รายชื่อส่วนราชการส่วนกลาง ได้แก่ กระทรวง กรม หน่วยงานอิสระ ฯลฯ',
      link:'/infoministry',
      forblank:0
    },
    {
      id:2,
      name: 'รายชื่อส่วนราชการส่วนภูมิภาค ได้แก่ กลุ่มจังหวัด จังหวัด อำเภอ ตำบล หมู่บ้าน',
      link:'/informationprovince',
      forblank:0
    },
    {
      id:3,
      name: 'การแบ่งเขตตรวจราชการ',
      link:'/supportgovernment/governmentinspectionarea',
      forblank:0
    },
    {
      id:4,
      name: 'ผลการดำเนินโครงการที่ได้รับจัดสรรงบประมาณจากเงินอุดหนุนทั่วไปโครงการเพิ่มสมรรถนะในการกำกับและติดตามปฎิบัติราชการในภูมิภาค',
      link:'/external/otps',
      forblank:0
    },
    {
      id:5,
      name: 'เรื่องร้องเรียนจากระบบศูนย์รับเรื่องราวร้องทุกข์ของรัฐบาล',
      link:'/external/opm-1111',
      forblank:0
    },
    {
      id:6,
      name: 'กฎหมาย ระเบียบ',
      link:'/supportgovernment/circularletter',
      forblank:0
    },
    {
      id:7,
      name: 'หนังสือเวียนต่างๆ',
      link:'/',
      forblank:0
    },
    {
      id:8,
      name: 'แผนการตรวจราชการประจำปี',
      link:'/supportgovernment/govermentinspectionplan',
      forblank:0
    },
    {
      id:9,
      name: 'ข้อมูลประกอบการตรวจราชการ',
      link:'/supportgovernment/informationinspection',
      forblank:0
    },
    {
      id:10,
      name:'ข้อมูลพื้นฐานรายจังหวัด',
      link:'/external/otps-provinces',
      forblank:0
    },
    {
      id:11,
      name:'ข้อมูลเกี่ยวกับการประชุมต่าง ๆ อาทิ หนังสือเชิญประชุม ระเบียบวาระการประชุม รายงานการประชุม เอกสารประกอบการประชุม',
      link:'/supportgovernment/premierorder',
      forblank:0
    },
    {
      id:12,
      name:'ข้อมูลสำหรับการปฏิบัติงานของเจ้าหน้าที่ประจำเขตตรวจราชการ อาทิ ข้อมูลการติดต่อที่พัก ยานพาหนะ และร้านอาหาร เที่ยวบิน',
      link:'/informationoperation',
      forblank:0
    },
    {
      id:13,
      name: 'แบบขออนุมัติเดินทางไปราชการ แบบขอยืมเงินทดรองราชการ สัญญาขอยืมเงิน และแบบรายงานการเดินทางไปราชการ ฯลฯ',
      link:'/supportgovernment/approvaldocument',
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

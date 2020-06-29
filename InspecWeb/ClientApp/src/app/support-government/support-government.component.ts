import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-support-government',
  templateUrl: './support-government.component.html',
  styleUrls: ['./support-government.component.css']
})
export class SupportGovernmentComponent implements OnInit {

  loading = true;
  dtOptions: DataTables.Settings = {};
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
      name: 'ข้อมูลการแบ่งเขตตรวจราชการ',
      link:'/supportfiscalyear',
      forblank:0
    },
    {
      id:4,
      name: 'ข้อมูลผลการดำเนินโครงการที่ได้รับจัดสรรงบประมาณจากเงินอุดหนุนทั่วไปโครงการเพิ่มขีดสมรรถนะในการกำกับและติดตามการปฏิบัติราชการในภูมิมขีดสมรรถนะในการกำกับและติดตามการปฏิบัติราชการในภูมิภาค',
      link:'/external/otps',
      forblank:0
    },
    {
      id:5,
      name: 'ข้อมูลเรื่องร้องเรียนจากระบบศูนย์รับเรื่องราวร้องทุกข์ของรัฐบาล',
      link:'/external/opm-1111',
      forblank:0
    },
    {
      id:6,
      name: 'กฎหมาย ระเบียบ หนังสือเวียนต่าง ๆ อาทิ ระเบียบสำนักนายกรัฐมนตรีว่าด้วยการตรวจราชการ พ.ศ.2551 ระเบียบสำนักนายกรัฐมนตรีว่าด้วยคณะกรรมการ ธรรมาภิบาลจังหวัด พ.ศ. 2552 และที่แก้ไขเพิ่มเติม ระเบียบสำนักนายกรัฐมนตรีว่าด้วยการกำกับติดตามการปฏิบัติราชการในภูมิภาค',
      link:'http://www.opm.go.th/opmportal/index.asp?pageid=1427&parent=1232&directory=12796&pagename=content1',
      forblank:1
    },
    {
      id:7,
      name: 'แผนการตรวจราชการประจำปี',
      link:'/supportcentralpolicy',
      forblank:0
    },
    {
      id:8,
      name: 'ข้อมูลประกอบการตรวจราชการ',
      link:'',
      forblank:0
    },
    {
      id:9,
      name: 'ข้อมูลพื้นฐานรายจังหวัด',
      link:'/external/otps-provinces',
      forblank:1
    },
    {
      id:10,
      name: 'ข้อมูลเกี่ยวกับการประชุมต่าง ๆ อาทิ หนังสือเชิญประชุม ระเบียบวาระการประชุม รายงานการประชุม เอกสารประกอบการประชุม',
      link:'/meetinginformation',
      forblank:0
    },
    {
      id:11,
      name: 'ข้อมูลสำหรับการปฏิบัติงานของเจ้าหน้าที่ประจำเขตตรวจราชการ อาทิ ข้อมูลการติดต่อที่พัก ยานพาหนะ และร้านอาหาร เที่ยวบิน',
      link:'/informationoperation',
      forblank:0
    },
    {
      id:12,
      name: 'แบบขออนุมัติเดินทางไปราชการ แบบขอยืมเงินทดรองราชการ สัญญาขอยืมเงิน และแบบรายงานการเดินทางไปราชการ ฯลฯ',
      link:'/documenttemplate',
      forblank:0
    },
  ]

  constructor() { }

  ngOnInit() {

    this.dtOptions = {
      // pagingType: 'full_numbers',
    };
  }

}


export interface NavBar {
  icon?: string;
  name?: string;
  url?: string;
  ex_link?: string;
  children?: Array<Children>
  classtap?: string;
  IDchildren?: string;
  bridge?: {name:string,status:boolean}
}

export interface Children {
  icon?: string;
  name?: string;
  url?: string;
  ex_link?: string;
  id?: string;
}
export const superAdmin: NavBar[] = [ // ซุปเปอร์แอดมิน
  {
    icon: 'fa-home',
    url: "/main",
    name: "หน้าหลัก"
  },
  {
    icon: 'fa-archive',
    url: "/centralpolicy",
    name: "แผนการตรวจประจำปี"
  },
  {
    icon: 'fa-calendar',
    url: "/inspectionplanevent",
    name: "ปฏิทินการตรวจราชการ"
  },
  {
    icon: 'fa-book',
    url: "/electronicbook",
    name: "สมุดตรวจอิเล็กทรอนิกส์"
  },
  {
    icon: 'fa-hand-point-up',
    name: "ข้อสั่งการถึงผู้ตรวจราชการ",
    IDchildren: 'executiveorderdata',
    children: [
      {
        icon: 'fa-long-arrow-alt-right',
        url: '/executiveorder',
        name: 'ข้อสั่งการถึงผู้ตรวจราชการ',
        ex_link: '0',
      },
      {
        icon: 'fa-long-arrow-alt-right',
        url: '/executiveorderexport1',
        name: 'รายงานข้อสั่งการของผู้บริหาร',
        ex_link: '0',

      },
      {
        icon: 'fa-long-arrow-alt-right',
        url: '/executiveorderexport3',
        name: 'ทะเบียนข้อสั่งการของผู้บริหาร',
        ex_link: '0',

      }
    ]
  },
  {
    icon: 'fa-hand-point-up',
    name: "แจ้งข้อมูลถึงผู้ตรวจราชการ",
    IDchildren: 'requestorderdata',
    children: [
      {
        icon: 'fa-long-arrow-alt-right',
        url: '/requestorder',
        name: 'แจ้งข้อมูลถึงผู้ตรวจราชการ',
        ex_link: '0',
      },
      {
        icon: 'fa-long-arrow-alt-right',
        url: '/exportrequestorderforadminprovince',
        name: 'รายงานแจ้งข้อมูลถึงผู้ตรวจราชการ',
        ex_link: '0',

      },
      {
        icon: 'fa-long-arrow-alt-right',
        url: '/exportrequestorderforinspector',
        name: 'ทะเบียนแจ้งข้อมูลถึงผู้ตรวจราชการ',
        ex_link: '0',

      }
    ]
  },
  // {
  //     classtap:'sidebar-header',
  //     url:"#",
  //     name:"______________________"
  // },
  {
    icon: 'fa-database',
    name: "ข้อมูลพื้นฐาน",
    IDchildren: 'basicdata',
    children: [
      {
        icon: 'fa-long-arrow-alt-right',
        url: '/fiscalyear',
        name: 'ปีงบประมาณ',
        ex_link: '0'
      },
      {
        icon: 'fa-long-arrow-alt-right',
        url: '/region',
        name: 'เขตตรวจราชการ',
        ex_link: '0'
      },
      {
        icon: 'fa-long-arrow-alt-right',
        url: '/province',
        name: 'จังหวัด',
        ex_link: '0'
      },
      {
        icon: 'fa-long-arrow-alt-right',
        url: '/ministry',
        name: 'กระทรวง/กรม',
        ex_link: '0'
      },
    ]
  },
  {
    icon: 'fa-user-friends',
    name: "จัดการผู้ใช้",
    IDchildren: 'userdata',
    children: [
      {
        icon: 'fa-long-arrow-alt-right',
        url: '/user/1',
        name: 'ผู้ดูแลระบบ',
        ex_link: 'user',
        id: '1'
      },
      {
        icon: 'fa-long-arrow-alt-right',
        url: '/user/2',
        name: 'ผู้ดูแลแผนการตรวจราชการประจำปี',
        ex_link: 'user',
        id: '2'
      },
      {
        icon: 'fa-long-arrow-alt-right',
        url: '/user/3',
        name: 'ผู้ตรวจราชการสำนักนายกรัฐมนตรี',
        ex_link: 'user',
        id: '3'
      },
      {
        icon: 'fa-long-arrow-alt-right',
        url: '/user/6',
        name: 'ผู้ตรวจราชการกระทรวง',
        ex_link: 'user',
        id: '6'
      },
      {
        icon: 'fa-long-arrow-alt-right',
        url: '/user/10',
        name: 'ผู้ตรวจราชการกรม',
        ex_link: 'user',
        id: '10'
      },
      {
        icon: 'fa-long-arrow-alt-right',
        url: '/user/9',
        name: 'หน่วยงานตรวจ',
        ex_link: 'user',
        id: '9'
      },
      {
        icon: 'fa-long-arrow-alt-right',
        url: '/user/4',
        name: 'ผู้ว่าราชการจังหวัด',
        ex_link: 'user',
        id: '4'
      },
      {
        icon: 'fa-long-arrow-alt-right',
        url: '/user/5',
        name: 'หัวหน้าสำนักงานจังหวัด',
        ex_link: 'user',
        id: '5'
      },
      {
        icon: 'fa-long-arrow-alt-right',
        url: '/user/7',
        name: 'ที่ปรึกษาผู้ตรวจราชการภาคประชาชน',
        ex_link: 'user',
        id: '7'
      },
      {
        icon: 'fa-long-arrow-alt-right',
        url: '/user/8',
        name: 'ผู้บริหาร/นายก/รองนายก',
        ex_link: 'user',
        id: '8'
      },
    ]
  },
  {
    icon: 'fa-list-alt',
    url: "/supportgovernment",
    name: "ข้อมูลสนับสนุน"
  },
  {
    IDchildren: 'strategic',
    icon: 'fa-flag',
    name: "นโยบายและแผนยุทธศาสตร์",
    children: [
      {
        ex_link: '0',
        icon: 'fa-long-arrow-alt-right',
        url: '/nationalstrategy',
        name: 'ยุทธศาตร์ชาติ'
      },
      {
        ex_link: '1',
        icon: 'fa-long-arrow-alt-right',
        url: 'http://nscr.nesdb.go.th/%E0%B9%81%E0%B8%9C%E0%B8%99%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%9B%E0%B8%8F%E0%B8%B4%E0%B8%A3%E0%B8%B9%E0%B8%9B%E0%B8%9B%E0%B8%A3%E0%B8%B0%E0%B9%80%E0%B8%97%E0%B8%A8/',
        name: 'แผนการปฏิรูปประเทศ'
      },
      {
        ex_link: '0',
        icon: 'fa-long-arrow-alt-right',
        url: '#',
        name: 'แผนพัฒนาเศรษฐกิจและสังคมแห่งชาติ'
      },
      {
        ex_link: '1',
        icon: 'fa-long-arrow-alt-right',
        url: 'http://nscr.nesdb.go.th/%E0%B9%81%E0%B8%9C%E0%B8%99%E0%B9%81%E0%B8%A1%E0%B9%88%E0%B8%9A%E0%B8%97%E0%B8%A0%E0%B8%B2%E0%B8%A2%E0%B9%83%E0%B8%95%E0%B9%89%E0%B8%A2%E0%B8%B8%E0%B8%97%E0%B8%98%E0%B8%A8%E0%B8%B2%E0%B8%AA%E0%B8%95/',
        name: 'แผนแม่บทต่าง ๆ'
      },
      {
        ex_link: '1',
        icon: 'fa-long-arrow-alt-right',
        url: 'http://www.soc.go.th/bb_main01.htm',
        name: 'นโยบายรัฐบาล'
      },
    ]
  },
  {
    IDchildren: 'command',
    icon: 'fa-bolt',
    name: "คำสั่งต่าง ๆ",
    children: [
      {
        ex_link: '0',
        icon: 'fa-long-arrow-alt-right',
        url: '/instructionorder',
        name: 'คำสั่งรับผิดชอบเขตตรวจราชการ'
      },
      {
        ex_link: '0',
        icon: 'fa-long-arrow-alt-right',
        url: '/inspectionorder',
        name: 'คำสั่งการตรวจราชการประจำปี'
      },
    ]
  },
  {
    IDchildren: 'contactpersonnel',
    icon: 'fa-user-tie',
    name: "ข้อมูลการติดต่อบุคลากร",
    children: [
      {
        ex_link: '0',
        icon: 'fa-long-arrow-alt-right',
        url: '/external/otps',
        name: 'คณะรัฐมนตรี'
      },
      {
        ex_link: '0',
        icon: 'fa-long-arrow-alt-right',
        url: '/inspector',
        name: 'ผู้ตรวจราชการ'
      },
      {
        ex_link: '0',
        icon: 'fa-long-arrow-alt-right',
        url: '/officerinspection',
        name: 'เจ้าหน้าที่ประจำเขตตรวจราชการ'
      },
      {
        ex_link: '0',
        icon: 'fa-long-arrow-alt-right',
        url: '#',
        name: 'หน่วยงานในส่วนภูมิภาค'
      },
      {
        ex_link: '0',
        icon: 'fa-long-arrow-alt-right',
        url: '/external/ggc-opm',
        name: 'คณะกรรมการธรรมมาภิบาลจังหวัด'
      },
      {
        ex_link: '0',
        icon: 'fa-long-arrow-alt-right',
        url: '#',
        name: 'เคลือข่ายคณะกรรมการธรรมมาภิบาลจังหวัด'
      },
      {
        ex_link: '0',
        icon: 'fa-long-arrow-alt-right',
        // url: 'http://www.opm.go.th/opmportal/index.asp?pageid=1427&parent=1232&directory=14727&pagename=content1',
        url: '/advisercivilsector',
        name: 'ที่ปรึกษาผู้ตรวจราชการภาคประชาชน'
      },
    ]
  },
  {
    icon: 'fa-shekel-sign',
    url: "/train",
    name: "จัดอบรมหลักสูตร"
  },
  {
    IDchildren: 'training',
    icon: 'fa-shekel-sign',
    name: "ข้อมูลจัดอบรมหลักสูตร",
    children: [
      {
        ex_link: '0',
        icon: 'fa-long-arrow-alt-right',
        url: '/training/lecturer',
        name: 'วิทยากรอบรม'
      },
      {
        ex_link: '0',
        icon: 'fa-long-arrow-alt-right',
        url: '/training',
        name: 'จัดอบรมหลักสูตร'
      },
      {
        ex_link: '0',
        icon: 'fa-long-arrow-alt-right',
        url: '/training/document',
        name: 'เอกสารประกอบการฝึกอบรม'
      },
      {
        ex_link: '0',
        icon: 'fa-long-arrow-alt-right',
        url: '/training/register',
        name: 'ผู้สมัครเข้าร่วมอบรม'
      },
      {
        ex_link: '0',
        icon: 'fa-long-arrow-alt-right',
        url: '/training/survey',
        name: 'ประเมินผลการฝึกอบรม'
      },
      {
        ex_link: '0',
        icon: 'fa-long-arrow-alt-right',
        url: '/training/report',
        name: 'รายงาน'
      },

    ]
  },
  {
    icon: 'fa-chart',
    url: "/external/thaimap",
    name: "แผนที่"
  },
  {
    icon: 'fa-shield',
    url: "/log",
    name: "LOG"
  },

]
export const Centraladmin: NavBar[] = [ //แอดมินส่วนกลาง
  {
    icon: 'fa-home',
    url: "/main",
    name: "หน้าหลัก"
  },
  {
    icon: 'fa-archive',
    url: "/centralpolicy",
    name: "แผนการตรวจประจำปี"
  },
  {
    icon: 'fa-calendar',
    url: "/inspectionplanevent",
    name: "ปฏิทินการตรวจราชการ"
  },
  {
    icon: 'fa-list-alt',
    url: "/supportgovernment",
    name: "ข้อมูลสนับสนุน"
  },
  {
    IDchildren: 'contactpersonnel',
    icon: 'fa-user-tie',
    name: "ข้อมูลการติดต่อบุคลากร",
    children: [
      {
        ex_link: '1',
        icon: 'fa-long-arrow-alt-right',
        url: 'http://www.opm.go.th/opmportal/index.asp?pageid=1558&parent=1232&directory=13214&pagename=content1',
        name: 'คณะรัฐมนตรี'
      },
      {
        ex_link: '0',
        icon: 'fa-long-arrow-alt-right',
        url: '/inspector',
        name: 'ผู้ตรวจราชการ'
      },
      {
        ex_link: '1',
        icon: 'fa-long-arrow-alt-right',
        url: '/province',
        name: 'เจ้าหน้าที่ประจำเขตตรวจราชการ'
      },
      {
        ex_link: '0',
        icon: 'fa-long-arrow-alt-right',
        url: '#',
        name: 'หน่วยงานในส่วนภูมิภาค'
      },
      {
        ex_link: '0',
        icon: 'fa-long-arrow-alt-right',
        url: '/external/ggc-opm',
        name: 'คณะกรรมการธรรมมาภิบาลจังหวัด'
      },
      {
        ex_link: '0',
        icon: 'fa-long-arrow-alt-right',
        url: '#',
        name: 'เคลือข่ายคณะกรรมการธรรมมาภิบาลจังหวัด'
      },
      {
        ex_link: '1',
        icon: 'fa-long-arrow-alt-right',
        url: 'http://www.opm.go.th/opmportal/index.asp?pageid=1427&parent=1232&directory=14727&pagename=content1',
        name: 'ที่ปรึกษาผู้ตรวจราชการภาคประชาชน'
      },
    ]
  },
  {
    icon: 'fa-shekel-sign',
    url: "/train",
    name: "จัดอบรมหลักสูตร"
  },
  {
    IDchildren: 'training',
    icon: 'fa-shekel-sign',
    name: "ข้อมูลจัดอบรมหลักสูตร",
    children: [
      {
        ex_link: '0',
        icon: 'fa-long-arrow-alt-right',
        url: '/training/lecturer',
        name: 'วิทยากรอบรม'
      },
      {
        ex_link: '0',
        icon: 'fa-long-arrow-alt-right',
        url: '/training',
        name: 'จัดอบรมหลักสูตร'
      },
      {
        ex_link: '0',
        icon: 'fa-long-arrow-alt-right',
        url: '/training/document',
        name: 'เอกสารประกอบการฝึกอบรม'
      },
      {
        ex_link: '0',
        icon: 'fa-long-arrow-alt-right',
        url: '/training/register',
        name: 'ผู้สมัครเข้าร่วมอบรม'
      },
      {
        ex_link: '1',
        icon: 'fa-long-arrow-alt-right',
        url: '/training/survey',
        name: 'ประเมินผลการฝึกอบรม'
      },
      {
        ex_link: '0',
        icon: 'fa-long-arrow-alt-right',
        url: '/training/report',
        name: 'รายงาน'
      },

    ]
  },
  {
    icon: 'fa-chart',
    url: "/external/thaimap",
    name: "แผนที่"
  },
]
export const Inspector: NavBar[] = [ //ผู้ตรวจ
  {
    icon: 'fa-home',
    url: "/main",
    name: "หน้าหลัก"
  },
  {
    icon: 'fa-archive',
    url: "/centralpolicy",
    name: "แผนการตรวจประจำปี"
  },

  {
    IDchildren: 'report',
    icon: 'fa-file',
    name: "รายงาน",
    children: [
      {
        ex_link: '0',
        icon: 'fa-long-arrow-alt-right',
        url: '/reportsubject',
        name: 'รายงานประเด็นการตรวจติดตาม'
      }
    ]
  },
  {
    IDchildren: 'schedule',
    icon: 'fa-calendar',
    name: "กำหนดการตรวจราชการ",
    children: [
      {
        ex_link: '0',
        icon: 'fa-long-arrow-alt-right',
        url: '/subjectevent',
        name: 'ประเด็นการตรวจติดตาม'
      },
      {
        ex_link: '0',
        icon: 'fa-long-arrow-alt-right',
        url: '/inspectionplanevent',
        name: 'ปฏิทินการตรวจราชการ'
      },
      {
        ex_link: '0',
        icon: 'fa-long-arrow-alt-right',
        url: '/reportinspectionplanevent',
        name: 'รายงานกำหนดการตรวจราชการ'
      },
    ]
  },

  // {
  //   icon: 'fa-calendar',
  //   url: "/inspectionplanevent",
  //   name: "ปฏิทินการตรวจราชการ"
  // },
  {
    icon: 'fa-book',
    url: "/electronicbook",
    name: "สมุดตรวจอิเล็กทรอนิกส์"
  },
  {
    IDchildren: 'report',
    icon: 'fa-user-tie',
    name: "รายงานผลการตรวจราชการ",
    children: [
      {
        ex_link: '0',
        icon: 'fa-long-arrow-alt-right',
        url: '/reportexport',
        name: 'Export'
      },
      {
        ex_link: '0',
        icon: 'fa-long-arrow-alt-right',
        url: '/reportimport',
        name: 'Import'
      },
    ]
  },
  {
    icon: 'fa-hand-point-up',
    name: "ข้อสั่งการถึงผู้ตรวจราชการ",
    IDchildren: 'executiveorderdata',
    children: [
      {
        icon: 'fa-long-arrow-alt-right',
        url: '/executiveorder',
        name: 'ข้อสั่งการถึงผู้ตรวจราชการ',
        ex_link: '0',
      },
      {
        icon: 'fa-long-arrow-alt-right',
        url: '/executiveorderexport1',
        name: 'รายงานข้อสั่งการของผู้บริหาร',
        ex_link: '0',

      },
      {
        icon: 'fa-long-arrow-alt-right',
        url: '/executiveorderexport3',
        name: 'ทะเบียนข้อสั่งการของผู้บริหาร',
        ex_link: '0',

      }
    ]
  },
  {
    icon: 'fa-hands',
    name: "แจ้งข้อมูลถึงผู้ตรวจราชการ",
    IDchildren: 'requestorderdata',
    children: [
      {
        icon: 'fa-long-arrow-alt-right',
        url: '/requestorder',
        name: 'แจ้งข้อมูลถึงผู้ตรวจราชการ',
        ex_link: '0',
      },
      {
          icon:'fa-long-arrow-alt-right',
          url:'/exportrequestorderforadminprovince',
          name:'รายงานคำร้องขอของหน่วยรับตรวจ',
          ex_link:'0',

      },
      {
          icon:'fa-long-arrow-alt-right',
          url:'/exportrequestorderforinspector',
          name:'ทะเบียนคำร้องขอของหน่วยรับตรวจ',
          ex_link:'0',

      } 
    ]
  },
  {
    icon: 'fa-list-alt',
    url: "/supportgovernment",
    name: "ข้อมูลสนับสนุน"
  },
  {
    IDchildren: 'contactpersonnel',
    icon: 'fa-user-tie',
    name: "ข้อมูลการติดต่อบุคลากร",
    children: [
      {
        ex_link: '1',
        icon: 'fa-long-arrow-alt-right',
        url: 'http://www.opm.go.th/opmportal/index.asp?pageid=1558&parent=1232&directory=13214&pagename=content1',
        name: 'คณะรัฐมนตรี'
      },
      {
        ex_link: '0',
        icon: 'fa-long-arrow-alt-right',
        url: '/inspector',
        name: 'ผู้ตรวจราชการ'
      },
      {
        ex_link: '1',
        icon: 'fa-long-arrow-alt-right',
        url: '/province',
        name: 'เจ้าหน้าที่ประจำเขตตรวจราชการ'
      },
      {
        ex_link: '0',
        icon: 'fa-long-arrow-alt-right',
        url: '#',
        name: 'หน่วยงานในส่วนภูมิภาค'
      },
      {
        ex_link: '0',
        icon: 'fa-long-arrow-alt-right',
        url: '/external/ggc-opm',
        name: 'คณะกรรมการธรรมมาภิบาลจังหวัด'
      },
      {
        ex_link: '0',
        icon: 'fa-long-arrow-alt-right',
        url: '#',
        name: 'เคลือข่ายคณะกรรมการธรรมมาภิบาลจังหวัด'
      },
      {
        ex_link: '1',
        icon: 'fa-long-arrow-alt-right',
        url: 'http://www.opm.go.th/opmportal/index.asp?pageid=1427&parent=1232&directory=14727&pagename=content1',
        name: 'ที่ปรึกษาผู้ตรวจราชการภาคประชาชน'
      },
    ]
  },
  {
    icon: 'fa-chart',
    url: "/external/thaimap",
    name: "แผนที่"
  },


]
export const Provincialgovernor: NavBar[] = [ //ผู้ว่าราชการจังหวัด
  {
    icon: 'fa-home',
    url: "/main",
    name: "หน้าหลัก"
  },
  {
    icon: 'fa-book',
    url: "/electronicbook",
    name: "สมุดตรวจอิเล็กทรอนิกส์"
  },
  {
    icon: 'fa-list-alt',
    url: "/supportgovernment",
    name: "ข้อมูลสนับสนุน"
  },
  {
    IDchildren: 'contactpersonnel',
    icon: 'fa-user-tie',
    name: "ข้อมูลการติดต่อบุคลากร",
    children: [
      {
        ex_link: '1',
        icon: 'fa-long-arrow-alt-right',
        url: 'http://www.opm.go.th/opmportal/index.asp?pageid=1558&parent=1232&directory=13214&pagename=content1',
        name: 'คณะรัฐมนตรี'
      },
      {
        ex_link: '0',
        icon: 'fa-long-arrow-alt-right',
        url: '/inspector',
        name: 'ผู้ตรวจราชการ'
      },
      {
        ex_link: '1',
        icon: 'fa-long-arrow-alt-right',
        url: '/province',
        name: 'เจ้าหน้าที่ประจำเขตตรวจราชการ'
      },
      {
        ex_link: '0',
        icon: 'fa-long-arrow-alt-right',
        url: '#',
        name: 'หน่วยงานในส่วนภูมิภาค'
      },
      {
        ex_link: '0',
        icon: 'fa-long-arrow-alt-right',
        url: '/external/ggc-opm',
        name: 'คณะกรรมการธรรมมาภิบาลจังหวัด'
      },
      {
        ex_link: '0',
        icon: 'fa-long-arrow-alt-right',
        url: '#',
        name: 'เคลือข่ายคณะกรรมการธรรมมาภิบาลจังหวัด'
      },
      {
        ex_link: '1',
        icon: 'fa-long-arrow-alt-right',
        url: 'http://www.opm.go.th/opmportal/index.asp?pageid=1427&parent=1232&directory=14727&pagename=content1',
        name: 'ที่ปรึกษาผู้ตรวจราชการภาคประชาชน'
      },
    ]
  },
  {
    icon: 'fa-chart',
    url: "/external/thaimap",
    name: "แผนที่"
  },
]
export const Adminprovince: NavBar[] = [ //หัวหน้าสำนักงานจังหวัด
  {
    icon: 'fa-home',
    url: "/main",
    name: "หน้าหลัก"
  },
  {
    icon: 'fa-archive',
    url: "/centralpolicy",
    name: "แผนการตรวจประจำปี"
  },
  {
    icon: 'fa-calendar',
    url: "/inspectionplanevent",
    name: "ปฏิทินการตรวจราชการ"
  },
  {
    icon: 'fa-book',
    url: "/electronicbookprovince",
    name: "สมุดตรวจอิเล็กทรอนิกส์"
  },
  {
    icon: 'fa-hands',
    name: "แจ้งข้อมูลถึงผู้ตรวจราชการ",
    IDchildren: 'requestorderdata',
    children: [
      {
        icon: 'fa-long-arrow-alt-right',
        url: '/requestorder',
        name: 'แจ้งข้อมูลถึงผู้ตรวจราชการ',
        ex_link: '0',
      },
      {
        icon:'fa-long-arrow-alt-right',
        url:'/exportrequestorderforadminprovince',
        name:'รายงานคำร้องขอของหน่วยรับตรวจ',
        ex_link:'0',

    },
    {
        icon:'fa-long-arrow-alt-right',
        url:'exportrequestorderforinspector',
        name:'ทะเบียนคำร้องขอของหน่วยรับตรวจ',
        ex_link:'0',

    } 
    ]
  },
  // {
  //     classtap:'sidebar-header',
  //     url:"#",
  //     name:"______________________"
  // },
  {
    icon: 'fa-list-alt',
    url: "/supportgovernment",
    name: "ข้อมูลสนับสนุน"
  },
  {
    IDchildren: 'contactpersonnel',
    icon: 'fa-user-tie',
    name: "ข้อมูลการติดต่อบุคลากร",
    children: [
      {
        ex_link: '1',
        icon: 'fa-long-arrow-alt-right',
        url: 'http://www.opm.go.th/opmportal/index.asp?pageid=1558&parent=1232&directory=13214&pagename=content1',
        name: 'คณะรัฐมนตรี'
      },
      {
        ex_link: '0',
        icon: 'fa-long-arrow-alt-right',
        url: '/inspector',
        name: 'ผู้ตรวจราชการ'
      },
      {
        ex_link: '1',
        icon: 'fa-long-arrow-alt-right',
        url: '/province',
        name: 'เจ้าหน้าที่ประจำเขตตรวจราชการ'
      },
      {
        ex_link: '0',
        icon: 'fa-long-arrow-alt-right',
        url: '#',
        name: 'หน่วยงานในส่วนภูมิภาค'
      },
      {
        ex_link: '0',
        icon: 'fa-long-arrow-alt-right',
        url: '/external/ggc-opm',
        name: 'คณะกรรมการธรรมมาภิบาลจังหวัด'
      },
      {
        ex_link: '0',
        icon: 'fa-long-arrow-alt-right',
        url: '#',
        name: 'เคลือข่ายคณะกรรมการธรรมมาภิบาลจังหวัด'
      },
      {
        ex_link: '1',
        icon: 'fa-long-arrow-alt-right',
        url: 'http://www.opm.go.th/opmportal/index.asp?pageid=1427&parent=1232&directory=14727&pagename=content1',
        name: 'ที่ปรึกษาผู้ตรวจราชการภาคประชาชน'
      },
    ]
  },
  {
    icon: 'fa-shekel-sign',
    url: "/training",
    name: "จัดอบรมหลักสูตร"
  },
  {
    icon: 'fa-chart',
    url: "/external/thaimap",
    name: "แผนที่"
  },
]
export const InspectorMinistry: NavBar[] = [ //ผุ้ตรวจกระทรวง
  {
    icon: 'fa-home',
    url: "/main",
    name: "หน้าหลัก"
  },
  // {
  //   icon: 'fa-calendar',
  //   url: "/inspectionplanevent",
  //   name: "ปฏิทินการตรวจราชการ"
  // },
  {
    icon: 'fa-archive',
    url: "/calendaruser",
    name: "ปฏิทินการตรวจราชการ"
  },
  {
    IDchildren: 'electronicbook',
    icon: 'fa-book',
    name: "สมุดตรวจอิเล็กทรอนิกส์",
    children: [
      {
        ex_link: '0',
        icon: 'fa-long-arrow-alt-right',
        url: '/electronicbook',
        name: 'สร้างเอง'
      },
      {
        ex_link: '0',
        icon: 'fa-long-arrow-alt-right',
        url: '/electronicbook/invited',
        name: 'ถูกเชิญ'
      },
    ]
  },
  {
    icon: 'fa-list-alt',
    url: "/supportgovernment",
    name: "ข้อมูลสนับสนุน"
  },
  {
    IDchildren: 'contactpersonnel',
    icon: 'fa-user-tie',
    name: "ข้อมูลการติดต่อบุคลากร",
    children: [
      {
        ex_link: '1',
        icon: 'fa-long-arrow-alt-right',
        url: 'http://www.opm.go.th/opmportal/index.asp?pageid=1558&parent=1232&directory=13214&pagename=content1',
        name: 'คณะรัฐมนตรี'
      },
      {
        ex_link: '0',
        icon: 'fa-long-arrow-alt-right',
        url: '/inspector',
        name: 'ผู้ตรวจราชการ'
      },
      {
        ex_link: '1',
        icon: 'fa-long-arrow-alt-right',
        url: '/province',
        name: 'เจ้าหน้าที่ประจำเขตตรวจราชการ'
      },
      {
        ex_link: '0',
        icon: 'fa-long-arrow-alt-right',
        url: '#',
        name: 'หน่วยงานในส่วนภูมิภาค'
      },
      {
        ex_link: '0',
        icon: 'fa-long-arrow-alt-right',
        url: '/external/ggc-opm',
        name: 'คณะกรรมการธรรมมาภิบาลจังหวัด'
      },
      {
        ex_link: '0',
        icon: 'fa-long-arrow-alt-right',
        url: '#',
        name: 'เคลือข่ายคณะกรรมการธรรมมาภิบาลจังหวัด'
      },
      {
        ex_link: '1',
        icon: 'fa-long-arrow-alt-right',
        url: 'http://www.opm.go.th/opmportal/index.asp?pageid=1427&parent=1232&directory=14727&pagename=content1',
        name: 'ที่ปรึกษาผู้ตรวจราชการภาคประชาชน'
      },
    ],

  },
  {
    icon: 'fa-chart',
    url: "/external/thaimap",
    name: "แผนที่"
  },

]
export const publicsector: NavBar[] = [ //ภาคประชาชน
  {
    icon: 'fa-home',
    url: "/main",
    name: "หน้าหลัก"
  },
  {
    icon: 'fa-book',
    url: "/answerpeople",
    name: "Userrole 7 แสดงความคิดเห็น"
  },
  {
    icon: 'fa-calendar',
    url: "/calendaruser",
    name: "ปฏิทินการตรวจราชการ" ,
    bridge:{name:'ปฏิทินการตรวจราชการ',status:true}
  },
  {
    icon: 'fa-list-alt',
    url: "/supportgovernment",
    name: "ข้อมูลสนับสนุน"
  },
  {
    IDchildren: 'contactpersonnel',
    icon: 'fa-user-tie',
    name: "ข้อมูลการติดต่อบุคลากร",
    children: [
      {
        ex_link: '1',
        icon: 'fa-long-arrow-alt-right',
        url: 'http://www.opm.go.th/opmportal/index.asp?pageid=1558&parent=1232&directory=13214&pagename=content1',
        name: 'คณะรัฐมนตรี'
      },
      {
        ex_link: '0',
        icon: 'fa-long-arrow-alt-right',
        url: '/inspector',
        name: 'ผู้ตรวจราชการ'
      },
      {
        ex_link: '1',
        icon: 'fa-long-arrow-alt-right',
        url: '/province',
        name: 'เจ้าหน้าที่ประจำเขตตรวจราชการ'
      },
      {
        ex_link: '0',
        icon: 'fa-long-arrow-alt-right',
        url: '#',
        name: 'หน่วยงานในส่วนภูมิภาค'
      },
      {
        ex_link: '0',
        icon: 'fa-long-arrow-alt-right',
        url: '/external/ggc-opm',
        name: 'คณะกรรมการธรรมมาภิบาลจังหวัด'
      },
      {
        ex_link: '0',
        icon: 'fa-long-arrow-alt-right',
        url: '#',
        name: 'เคลือข่ายคณะกรรมการธรรมมาภิบาลจังหวัด'
      },
      {
        ex_link: '1',
        icon: 'fa-long-arrow-alt-right',
        url: 'http://www.opm.go.th/opmportal/index.asp?pageid=1427&parent=1232&directory=14727&pagename=content1',
        name: 'ที่ปรึกษาผู้ตรวจราชการภาคประชาชน'
      },
    ]
  },
  {
    icon: 'fa-chart',
    url: "/external/thaimap",
    name: "แผนที่"
  },
]
export const president: NavBar[] = [ //ผู้บริหารหรือนายก
  {
    icon: 'fa-home',
    url: "/main",
    name: "หน้าหลัก"
  },
  {
    icon: 'fa-archive',
    url: "/centralpolicy",
    name: "แผนการตรวจประจำปี"
  },
  {
    icon: 'fa-hand-point-up',
    name: "ข้อสั่งการถึงผู้ตรวจราชการ",
    IDchildren: 'executiveorderdata',
    children: [
      {
        icon: 'fa-long-arrow-alt-right',
        url: '/executiveorder',
        name: 'ข้อสั่งการถึงผู้ตรวจราชการ',
        ex_link: '0',
      },
      {
        icon: 'fa-long-arrow-alt-right',
        url: '/executiveorderexport1',
        name: 'รายงานข้อสั่งการของผู้บริหาร',
        ex_link: '0',

      },
      {
        icon: 'fa-long-arrow-alt-right',
        url: '/executiveorderexport3',
        name: 'ทะเบียนข้อสั่งการของผู้บริหาร',
        ex_link: '0',

      }
    ]
  },
  {
    icon: 'fa-user-tie',
    url: "/commanderreport",
    name: "รายงานผลการตรวจราชการ"
  },
  {
    icon: 'fa-list-alt',
    url: "/supportgovernment",
    name: "ข้อมูลสนับสนุน"
  },
  {
    IDchildren: 'contactpersonnel',
    icon: 'fa-user-tie',
    name: "ข้อมูลการติดต่อบุคลากร",
    children: [
      {
        ex_link: '1',
        icon: 'fa-long-arrow-alt-right',
        url: 'http://www.opm.go.th/opmportal/index.asp?pageid=1558&parent=1232&directory=13214&pagename=content1',
        name: 'คณะรัฐมนตรี'
      },
      {
        ex_link: '0',
        icon: 'fa-long-arrow-alt-right',
        url: '/inspector',
        name: 'ผู้ตรวจราชการ'
      },
      {
        ex_link: '1',
        icon: 'fa-long-arrow-alt-right',
        url: '/province',
        name: 'เจ้าหน้าที่ประจำเขตตรวจราชการ'
      },
      {
        ex_link: '0',
        icon: 'fa-long-arrow-alt-right',
        url: '#',
        name: 'หน่วยงานในส่วนภูมิภาค'
      },
      {
        ex_link: '0',
        icon: 'fa-long-arrow-alt-right',
        url: '/external/ggc-opm',
        name: 'คณะกรรมการธรรมมาภิบาลจังหวัด'
      },
      {
        ex_link: '0',
        icon: 'fa-long-arrow-alt-right',
        url: '#',
        name: 'เคลือข่ายคณะกรรมการธรรมมาภิบาลจังหวัด'
      },
      {
        ex_link: '1',
        icon: 'fa-long-arrow-alt-right',
        url: 'http://www.opm.go.th/opmportal/index.asp?pageid=1427&parent=1232&directory=14727&pagename=content1',
        name: 'ที่ปรึกษาผู้ตรวจราชการภาคประชาชน'
      },
    ]
  },
  {
    icon: 'fa-chart',
    url: "/external/thaimap",
    name: "แผนที่"
  },
]
export const InspectorExamination: NavBar[] = [ //หน่วยรับตรวจ
  {
    icon: 'fa-home',
    url: "/main",
    name: "หน้าหลัก"
  },
  {
    icon: 'fa-archive',
    url: "/answersubject",
    name: "ประเด็นตรวจติดตาม"
  },
  {
    icon: 'fa-calendar',
    url: "/inspectionplanevent",
    name: "ปฏิทินการตรวจราชการ"
  },
  {
    icon: 'fa-book',
    url: "/electronicbookprovince",
    name: "สมุดตรวจอิเล็กทรอนิกส์"
  },
  {
    icon: 'fa-list-alt',
    url: "/supportgovernment",
    name: "ข้อมูลสนับสนุน"
  },
  {
    IDchildren: 'contactpersonnel',
    icon: 'fa-user-tie',
    name: "ข้อมูลการติดต่อบุคลากร",
    children: [
      {
        ex_link: '1',
        icon: 'fa-long-arrow-alt-right',
        url: 'http://www.opm.go.th/opmportal/index.asp?pageid=1558&parent=1232&directory=13214&pagename=content1',
        name: 'คณะรัฐมนตรี'
      },
      {
        ex_link: '0',
        icon: 'fa-long-arrow-alt-right',
        url: '/inspector',
        name: 'ผู้ตรวจราชการ'
      },
      {
        ex_link: '1',
        icon: 'fa-long-arrow-alt-right',
        url: '/province',
        name: 'เจ้าหน้าที่ประจำเขตตรวจราชการ'
      },
      {
        ex_link: '0',
        icon: 'fa-long-arrow-alt-right',
        url: '#',
        name: 'หน่วยงานในส่วนภูมิภาค'
      },
      {
        ex_link: '0',
        icon: 'fa-long-arrow-alt-right',
        url: '/external/ggc-opm',
        name: 'คณะกรรมการธรรมมาภิบาลจังหวัด'
      },
      {
        ex_link: '0',
        icon: 'fa-long-arrow-alt-right',
        url: '#',
        name: 'เคลือข่ายคณะกรรมการธรรมมาภิบาลจังหวัด'
      },
      {
        ex_link: '1',
        icon: 'fa-long-arrow-alt-right',
        url: 'http://www.opm.go.th/opmportal/index.asp?pageid=1427&parent=1232&directory=14727&pagename=content1',
        name: 'ที่ปรึกษาผู้ตรวจราชการภาคประชาชน'
      },
    ]
  },
  {
    icon: 'fa-chart',
    url: "/external/thaimap",
    name: "แผนที่"
  },

]
export const InspectorDepartment: NavBar[] = [ //ผู้ตรวจกรม/หน่วยงาน
  {
    icon: 'fa-home',
    url: "/main",
    name: "หน้าหลัก"
  },
  // {
  //   icon: 'fa-calendar',
  //   url: "/inspectionplanevent",
  //   name: "ปฏิทินการตรวจราชการ"
  // },
  {
    icon: 'fa-archive',
    url: "/calendaruser",
    name: "ปฏิทินการตรวจราชการ"
  },
  {
    IDchildren: 'electronicbook',
    icon: 'fa-book',
    name: "สมุดตรวจอิเล็กทรอนิกส์",
    children: [
      {
        ex_link: '0',
        icon: 'fa-long-arrow-alt-right',
        url: '/electronicbook',
        name: 'สร้างเอง'
      },
      {
        ex_link: '0',
        icon: 'fa-long-arrow-alt-right',
        url: '/electronicbook/invited',
        name: 'ถูกเชิญ'
      },
    ]
  },
  // {
  //   icon: 'fa-book',
  //   url: "/usercentralpolicy",
  //   name: "Accept"
  // },
  {
    icon: 'fa-list-alt',
    url: "/supportgovernment",
    name: "ข้อมูลสนับสนุน"
  },
  {
    IDchildren: 'contactpersonnel',
    icon: 'fa-user-tie',
    name: "ข้อมูลการติดต่อบุคลากร",
    children: [
      {
        ex_link: '1',
        icon: 'fa-long-arrow-alt-right',
        url: 'http://www.opm.go.th/opmportal/index.asp?pageid=1558&parent=1232&directory=13214&pagename=content1',
        name: 'คณะรัฐมนตรี'
      },
      {
        ex_link: '0',
        icon: 'fa-long-arrow-alt-right',
        url: '/inspector',
        name: 'ผู้ตรวจราชการ'
      },
      {
        ex_link: '1',
        icon: 'fa-long-arrow-alt-right',
        url: '/province',
        name: 'เจ้าหน้าที่ประจำเขตตรวจราชการ'
      },
      {
        ex_link: '0',
        icon: 'fa-long-arrow-alt-right',
        url: '#',
        name: 'หน่วยงานในส่วนภูมิภาค'
      },
      {
        ex_link: '0',
        icon: 'fa-long-arrow-alt-right',
        url: '/external/ggc-opm',
        name: 'คณะกรรมการธรรมมาภิบาลจังหวัด'
      },
      {
        ex_link: '0',
        icon: 'fa-long-arrow-alt-right',
        url: '#',
        name: 'เคลือข่ายคณะกรรมการธรรมมาภิบาลจังหวัด'
      },
      {
        ex_link: '1',
        icon: 'fa-long-arrow-alt-right',
        url: 'http://www.opm.go.th/opmportal/index.asp?pageid=1427&parent=1232&directory=14727&pagename=content1',
        name: 'ที่ปรึกษาผู้ตรวจราชการภาคประชาชน'
      },
    ],

  },
  {
    icon: 'fa-chart',
    url: "/external/thaimap",
    name: "แผนที่"
  },
]

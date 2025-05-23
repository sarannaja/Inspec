
export interface NavBar {
  icon?: string;
  name?: string;
  url?: string;
  menuname?: any;
  orderby? :any;
  ex_link?: string;
  children?: Array<Children>
  classtap?: string;
  IDchildren?: string;
  bridge?: { name: string, status: boolean }
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
    name: "หน้าหลัก",
    menuname: "m1",
    orderby: 1 ,
  },
  {
    icon: 'fa-archive',
    url: "/centralpolicy",
    name: "แผนการตรวจราชการ",
    menuname: "m2",
    orderby: 2 ,
  },
  {
    icon: 'fa-calendar',
    url: '/inspectionplanevent/all',
    name: "ปฏิทินการตรวจราชการ",
    menuname: "m3",
    orderby: 3 ,
  },
  {
    icon: 'fa-book',
    url: "/electronicbookall",
    name: "สมุดตรวจอิเล็กทรอนิกส์",
    menuname: "m24",
    orderby: 12 ,
  },
  {
    icon: 'fa-file-alt',
    name: "ทะเบียนรายงานผลการตรวจราชการ",
    url: '/allreport',
    menuname: "m5",
    orderby: 17 ,
  },
  {
    icon: 'fa-hand-point-up',
    name: "ข้อสั่งการถึงผู้ตรวจราชการ",
    IDchildren: 'executiveorderdata',
    menuname: "m6",
    orderby: 20 ,
    children: [
      {
        icon: 'fa-file',
        url: '/executiveorder',
        name: 'ข้อสั่งการถึงผู้ตรวจราชการ',
        ex_link: '0',
      },
      {
        icon: 'fa-file',
        url: '/executiveorderexport1',
        name: 'รายงานข้อสั่งการของผู้บริหาร',
        ex_link: '0',

      },
      {
        icon: 'fa-file',
        url: '/executiveorderexport3',
        name: 'ทะเบียนข้อสั่งการของผู้บริหาร',
        ex_link: '0',

      }
    ]
  },
  {
    icon: 'fa-paper-plane',
    name: "แจ้งข้อมูลถึงผู้ตรวจราชการ",
    IDchildren: 'requestorderdata',
    menuname: "m7",
    orderby: 21 ,
    children: [
      {
        icon: 'fa-file',
        url: '/requestorder',
        name: 'แจ้งข้อมูลถึงผู้ตรวจราชการ',
        ex_link: '0',
      },
      {
        icon: 'fa-file',
        url: '/exportrequestorderforadminprovince',
        name: 'รายงานแจ้งข้อมูลถึงผู้ตรวจราชการ',
        ex_link: '0',

      },
      {
        icon: 'fa-file',
        url: '/exportrequestorderforinspector',
        name: 'ทะเบียนแจ้งข้อมูลถึงผู้ตรวจราชการ',
        ex_link: '0',

      }
    ]
  },
  {
    icon: 'fa-database',
    name: "ข้อมูลพื้นฐาน",
    IDchildren: 'basicdata',
    menuname: "m8",
    orderby: 22 ,
    children: [
      {
        icon: 'fa-file',
        url: '/fiscalyearnew',
        name: 'ปีงบประมาณ',
        ex_link: '0'
      },
      {
        icon: 'fa-file',
        url: '/region',
        name: 'ชื่อเขตตรวจราชการ',
        ex_link: '0'
      },
      {
        icon: 'fa-file',
        url: '/typeexamibationplan',
        name: 'ประเภทแผนการตรวจ',
        ex_link: '0'
      },
      {
        icon: 'fa-file',
        url: '/fiscalyear',
        name: 'กำหนดเขตตรวจราชการ',
        ex_link: '0'
      },
      {
        icon: 'fa-file',
        url: '/sector',
        name: 'ภาค',
        ex_link: '0'
      },
      {
        icon: 'fa-file',
        url: '/provincesgroup',
        name: 'กลุ่มจังหวัด',
        ex_link: '0'
      },
      {
        icon: 'fa-file',
        url: '/province',
        name: 'จังหวัด',
        ex_link: '0'
      },
      {
        icon: 'fa-file',
        url: '/ministry',
        name: 'กระทรวง/กรม',
        ex_link: '0'
      },
      {
        icon: 'fa-file',
        url: '/side',
        name: 'ด้านของที่ปรึกษาภาคประชาชน',
        ex_link: '0'
      },
    ]
  },
  {
    icon: 'fa-list-alt',
    url: "/menu",
    name: "กำหนดสิทธิ์การใช้งาน",
    menuname: "m26",
    orderby: 23 ,
  },
  {
    icon: 'fa-user-friends',
    name: "จัดการผู้ใช้",
    IDchildren: 'userdata',
    menuname: "m9",
    orderby: 24 ,
    children: [
      {
        icon: 'fa-file',
        url: '/user/1',
        name: 'ผู้ดูแลระบบ',
        ex_link: 'user',
        id: '1'
      },
      {
        icon: 'fa-file',
        url: '/user/2',
        name: 'ผู้ดูแลแผนการตรวจราชการ',
        ex_link: 'user',
        id: '2'
      },
      {
        icon: 'fa-file',
        url: '/user/3',
        name: 'ผู้ตรวจราชการสำนักนายกรัฐมนตรี',
        ex_link: 'user',
        id: '3'
      },
      {
        icon: 'fa-file',
        url: '/user/6',
        name: 'ผู้ตรวจราชการกระทรวง',
        ex_link: 'user',
        id: '6'
      },
      {
        icon: 'fa-file',
        url: '/user/10',
        name: 'ผู้ตรวจราชการกรม',
        ex_link: 'user',
        id: '10'
      },
      {
        icon: 'fa-file',
        url: '/user/9',
        name: 'หน่วยรับตรวจ',
        ex_link: 'user',
        id: '9'
      },
      {
        icon: 'fa-file',
        url: '/user/4',
        name: 'ผู้ว่าราชการจังหวัด',
        ex_link: 'user',
        id: '4'
      },
      {
        icon: 'fa-file',
        url: '/user/5',
        name: 'หัวหน้าสำนักงานจังหวัด',
        ex_link: 'user',
        id: '5'
      },
      {
        icon: 'fa-file',
        url: '/user/7',
        name: 'ที่ปรึกษาผู้ตรวจราชการภาคประชาชน',
        ex_link: 'user',
        id: '7'
      },
      {
        icon: 'fa-file',
        url: '/user/8',
        name: 'ผู้บริหาร/นายก/รองนายก',
        ex_link: 'user',
        id: '8'
      },
      {
        icon: 'fa-file',
        url: '/user/11',
        name: 'ผู้ใช้ภายนอก',
        ex_link: 'user',
        id: '11'
      },
    ]
  },
  {
    icon: 'fa-list-alt',
    url: "/supportgovernment",
    name: "ข้อมูลสนับสนุน",
    menuname: "m10",
    orderby: 25 ,
  },
  {
    IDchildren: 'strategic',
    icon: 'fa-flag',
    name: "นโยบาย&แผนยุทธศาสตร์",
    menuname: "m11",
    orderby: 26 ,
    children: [
      {
        ex_link: '1',
        icon: 'fa-file',
        //url: '/nationalstrategy',
        url:'http://nscr.nesdc.go.th/ns/',
        name: 'ยุทธศาสตร์ชาติ'
      },
      {
        ex_link: '1',
        icon: 'fa-file',
        url: 'http://nscr.nesdc.go.th/cr/',
        name: 'แผนการปฏิรูปประเทศ'
      },
      {
        ex_link: '1',
        icon: 'fa-file',
        url: 'https://www.nesdc.go.th/main.php?filename=develop_issue',
        name: 'แผนพัฒนาเศรษฐกิจและสังคมแห่งชาติ'
      },
      {
        ex_link: '1',
        icon: 'fa-file',
        url: 'http://nscr.nesdc.go.th/master-plans/',
        name: 'แผนแม่บทภายใต้ยุทธศาสตร์ชาติ'
      },

      {
        ex_link: '1',
        icon: 'fa-file',
        url: 'https://www.soc.go.th/?p=22892',
        name: 'นโยบายรัฐบาล'
      },
    ]
  },
  {
    IDchildren: 'command',
    icon: 'fa-bolt',
    name: "คำสั่งต่าง ๆ",
    menuname: "m12",
    orderby: 27 ,
    children: [
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/instructionorder',
        name: 'คำสั่งรับผิดชอบเขตตรวจราชการ'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/inspectionorder',
        name: 'คำสั่งการตรวจราชการประจำปี'
      },
    ]
  },
  {
    IDchildren: 'contactpersonnel',
    icon: 'fa-user-tie',
    name: "ข้อมูลการติดต่อบุคลากร",
    menuname: "m13",
    orderby: 28 ,
    children: [
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/maincabinet',
        name: 'คณะรัฐมนตรี'
      },
      // {
      //   ex_link: '0',
      //   icon: 'fa-file',
      //   url: '/external/otps',
      //   name: 'คณะรัฐมนตรี(ระบบ otps)'
      // },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/inspector',
        name: 'ผู้ตรวจราชการ'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/officerinspection',
        name: 'เจ้าหน้าที่ประจำเขตตรวจราชการ'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/regionalagency',
        name: 'หน่วยงานในส่วนภูมิภาค'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/external/ggc-opm',
        name: 'คณะกรรมการธรรมมาภิบาลจังหวัด'
      },
      {
        ex_link: '1',
        icon: 'fa-file',
        url: 'https://www.ggc.opm.go.th/index.php?page=map',
        name: 'เครือข่ายคณะกรรมการธรรมมาภิบาลจังหวัด'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        // url: 'http://www.opm.go.th/opmportal/index.asp?pageid=1427&parent=1232&directory=14727&pagename=content1',
        url: '/advisercivilsector',
        name: 'ที่ปรึกษาผู้ตรวจราชการภาคประชาชน'
      },
    ]
  },
  {
    icon: 'fa-map-marker',
    url: "/external/thaimap",
    name: "แผนที่",
    menuname: "m16",
    orderby: 30 ,
  },
  {
    icon: 'fa-eye',
    url: "/log",
    name: "log",
    menuname: "m17",
    orderby: 29 ,
  },
  {
    icon: 'fa-external-link-alt',
    url: "/iframe",
    name: "iframe",
    menuname: "m27",
    orderby: 31 ,
  },
  {
    IDchildren: 'training',
    icon: 'fa-shekel-sign',
    name: "ข้อมูลหลักสูตรฝึกอบรม",
    menuname: "m14",
    orderby: 32 ,
    children: [
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/training',
        name: 'จัดการหลักสูตรฝึกอบรม'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/training/lecturer',
        name: 'วิทยากรอบรม'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/training/survey',
        name: 'จัดการแบบประเมิน'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/training/programtype',
        name: 'จัดการประเภทกิจกรรม'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/training/lecturertype',
        name: 'จัดการประเภทวิทยากร'
      },
    ]
  },
  {
    IDchildren: 'training_private',
    icon: 'fa-shekel-sign',
    name: "การฝึกอบรมหลักสูตร",
    menuname: "m15",
    orderby: 33 ,
    children: [
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/train',
        name: 'สมัครฝึกอบรม'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/training/report/history',
        name: 'ประวัติการฝึกอบรม'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/training/surveylecturer',
        name: 'แบบประเมินการอบรม'
      },
    ]


  },
  {
    icon: 'fa-file-alt',
    url: "/retrospective-report",
    name: "รายงานผลการตรวจราชการ",
    menuname: "m35",
    orderby: 34 ,
  },
  {
    icon: 'fa-file-alt',
    url: "/training-project-report",
    name: "รายงานโครงการฝึกอบรม",
    menuname: "m36",
    orderby: 35 ,
  },
]
export const Centraladmin: NavBar[] = [ //แอดมินส่วนกลาง
  {
    icon: 'fa-home',
    url: "/main",
    name: "หน้าหลัก",
    menuname: "m1",
    orderby: 1 ,
  },
  {
    icon: 'fa-archive',
    url: "/centralpolicy",
    name: "แผนการตรวจราชการ",
    menuname: "m2",
    orderby: 2 ,
  },
  {
    icon: 'fa-calendar',
    url: '/inspectionplanevent/all',
    name: "ปฏิทินการตรวจราชการ",
    menuname: "m3",
    orderby: 3 ,
  },
  {
    icon: 'fa-list-alt',
    url: "/supportgovernment",
    name: "ข้อมูลสนับสนุน",
    menuname: "m10",
    orderby: 25 ,
  },
  {
    IDchildren: 'strategic',
    icon: 'fa-flag',
    name: "นโยบาย&แผนยุทธศาสตร์",
    menuname: "m11",
    orderby: 26 ,
    children: [
      {
        ex_link: '1',
        icon: 'fa-file',
        //url: '/nationalstrategy',
        url:'http://nscr.nesdc.go.th/ns/',
        name: 'ยุทธศาสตร์ชาติ'
      },
      {
        ex_link: '1',
        icon: 'fa-file',
        url: 'http://nscr.nesdc.go.th/cr/',
        name: 'แผนการปฏิรูปประเทศ'
      },
      {
        ex_link: '1',
        icon: 'fa-file',
        url: 'https://www.nesdc.go.th/main.php?filename=develop_issue',
        name: 'แผนพัฒนาเศรษฐกิจและสังคมแห่งชาติ'
      },
      {
        ex_link: '1',
        icon: 'fa-file',
        url: 'http://nscr.nesdc.go.th/master-plans/',
        name: 'แผนแม่บทภายใต้ยุทธศาสตร์ชาติ'
      },

      {
        ex_link: '1',
        icon: 'fa-file',
        url: 'https://www.soc.go.th/?p=22892',
        name: 'นโยบายรัฐบาล'
      },
    ]
  },
  {
    IDchildren: 'command',
    icon: 'fa-bolt',
    name: "คำสั่งต่าง ๆ",
    menuname: "m12",
    orderby: 27 ,
    children: [
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/instructionorder',
        name: 'คำสั่งรับผิดชอบเขตตรวจราชการ'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/inspectionorder',
        name: 'คำสั่งการตรวจราชการประจำปี'
      },
    ]
  },
  {
    IDchildren: 'contactpersonnel',
    icon: 'fa-user-tie',
    name: "ข้อมูลการติดต่อบุคลากร",
    menuname: "m13",
    orderby: 28 ,
    children: [
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/maincabinet',
        name: 'คณะรัฐมนตรี'
      },
      // {
      //   ex_link: '0',
      //   icon: 'fa-file',
      //   url: '/external/otps',
      //   name: 'คณะรัฐมนตรี(ระบบ otps)'
      // },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/inspector',
        name: 'ผู้ตรวจราชการ'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/officerinspection',
        name: 'เจ้าหน้าที่ประจำเขตตรวจราชการ'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/regionalagency',
        name: 'หน่วยงานในส่วนภูมิภาค'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/external/ggc-opm',
        name: 'คณะกรรมการธรรมมาภิบาลจังหวัด'
      },
      {
        ex_link: '1',
        icon: 'fa-file',
        url: 'https://www.ggc.opm.go.th/index.php?page=map',
        name: 'เครือข่ายคณะกรรมการธรรมมาภิบาลจังหวัด'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        // url: 'http://www.opm.go.th/opmportal/index.asp?pageid=1427&parent=1232&directory=14727&pagename=content1',
        url: '/advisercivilsector',
        name: 'ที่ปรึกษาผู้ตรวจราชการภาคประชาชน'
      },
    ]
  },
  {
    icon: 'fa-map-marker',
    url: "/external/thaimap",
    name: "แผนที่",
    menuname: "m16",
    orderby: 30 ,
  },
]
export const Inspector: NavBar[] = [ //ผู้ตรวจ
  {
    icon: 'fa-home',
    url: "/main",
    name: "หน้าหลัก",
    menuname: "m1",
    orderby: 1 ,
  },
  {
    icon: 'fa-archive',
    url: "/centralpolicy",
    name: "แผนการตรวจราชการ",
    menuname: "m2",
    orderby: 2 ,
  },
  {
    IDchildren: 'schedule',
    icon: 'fa-calendar',
    name: "กำหนดการตรวจราชการ",
    menuname: "m18",
    orderby: 7 ,
    children: [
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/inspectionplanevent/all',
        name: 'ปฏิทินการตรวจราชการรวม'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/inspectionplanevent',
        name: 'ปฏิทินการตรวจราชการ'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/reportinspectionplanevent',
        name: 'รายงานกำหนดการตรวจราชการ'
      },
    ]
  },
  {
    icon: 'fa-file',
    url: "/subjectevent",
    name: "ประเด็นการตรวจติดตาม",
    menuname: "m19",
    orderby: 9 ,
  },
  {
    icon: 'fa-book',
    url: "/electronicbook",
    name: "สมุดตรวจอิเล็กทรอนิกส์(ผู้ตรวจราชการ)",
    menuname: "m4",
    orderby: 11 ,
  },
  {
    icon: 'fa-user-tie',
    name: "รายงานผลการตรวจราชการ",
    url: '/reportimport',
    menuname: "m20",
    orderby: 16 ,
  },
  {
    icon: 'fa-hand-point-up',
    name: "ข้อสั่งการถึงผู้ตรวจราชการ",
    IDchildren: 'executiveorderdata',
    menuname: "m6",
    orderby: 20 ,
    children: [
      {
        icon: 'fa-file',
        url: '/executiveorder',
        name: 'ข้อสั่งการถึงผู้ตรวจราชการ',
        ex_link: '0',
      },
      {
        icon: 'fa-file',
        url: '/executiveorderexport1',
        name: 'รายงานข้อสั่งการของผู้บริหาร',
        ex_link: '0',

      },
      {
        icon: 'fa-file',
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
    menuname: "m7",
    orderby: 21 ,
    children: [
      {
        icon: 'fa-file',
        url: '/requestorder',
        name: 'แจ้งข้อมูลถึงผู้ตรวจราชการ',
        ex_link: '0',
      },
      {
        icon: 'fa-file',
        url: '/exportrequestorderforadminprovince',
        name: 'รายงานคำร้องขอของหน่วยรับตรวจ',
        ex_link: '0',

      },
      {
        icon: 'fa-file',
        url: '/exportrequestorderforinspector',
        name: 'ทะเบียนคำร้องขอของหน่วยรับตรวจ',
        ex_link: '0',

      }
    ]
  },
  {
    icon: 'fa-list-alt',
    url: "/supportgovernment",
    name: "ข้อมูลสนับสนุน",
    menuname: "m10",
    orderby: 25 ,
  },
  {
    IDchildren: 'strategic',
    icon: 'fa-flag',
    name: "นโยบาย&แผนยุทธศาสตร์",
    menuname: "m11",
    orderby: 26 ,
    children: [
      {
        ex_link: '1',
        icon: 'fa-file',
        //url: '/nationalstrategy',
        url:'http://nscr.nesdc.go.th/ns/',
        name: 'ยุทธศาสตร์ชาติ'
      },
      {
        ex_link: '1',
        icon: 'fa-file',
        url: 'http://nscr.nesdc.go.th/cr/',
        name: 'แผนการปฏิรูปประเทศ'
      },
      {
        ex_link: '1',
        icon: 'fa-file',
        url: 'https://www.nesdc.go.th/main.php?filename=develop_issue',
        name: 'แผนพัฒนาเศรษฐกิจและสังคมแห่งชาติ'
      },
      {
        ex_link: '1',
        icon: 'fa-file',
        url: 'http://nscr.nesdc.go.th/master-plans/',
        name: 'แผนแม่บทภายใต้ยุทธศาสตร์ชาติ'
      },

      {
        ex_link: '1',
        icon: 'fa-file',
        url: 'https://www.soc.go.th/?p=22892',
        name: 'นโยบายรัฐบาล'
      },
    ]
  },
  {
    IDchildren: 'command',
    icon: 'fa-bolt',
    name: "คำสั่งต่าง ๆ",
    menuname: "m12",
    orderby: 27 ,
    children: [
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/instructionorder',
        name: 'คำสั่งรับผิดชอบเขตตรวจราชการ'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/inspectionorder',
        name: 'คำสั่งการตรวจราชการประจำปี'
      },
    ]
  },
  {
    IDchildren: 'contactpersonnel',
    icon: 'fa-user-tie',
    name: "ข้อมูลการติดต่อบุคลากร",
    menuname: "m13",
    orderby: 28 ,
    children: [
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/maincabinet',
        name: 'คณะรัฐมนตรี'
      },
      // {
      //   ex_link: '0',
      //   icon: 'fa-file',
      //   url: '/external/otps',
      //   name: 'คณะรัฐมนตรี(ระบบ otps)'
      // },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/inspector',
        name: 'ผู้ตรวจราชการ'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/officerinspection',
        name: 'เจ้าหน้าที่ประจำเขตตรวจราชการ'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/regionalagency',
        name: 'หน่วยงานในส่วนภูมิภาค'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/external/ggc-opm',
        name: 'คณะกรรมการธรรมมาภิบาลจังหวัด'
      },
      {
        ex_link: '1',
        icon: 'fa-file',
        url: 'https://www.ggc.opm.go.th/index.php?page=map',
        name: 'เครือข่ายคณะกรรมการธรรมมาภิบาลจังหวัด'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        // url: 'http://www.opm.go.th/opmportal/index.asp?pageid=1427&parent=1232&directory=14727&pagename=content1',
        url: '/advisercivilsector',
        name: 'ที่ปรึกษาผู้ตรวจราชการภาคประชาชน'
      },
    ]
  },
  {
    icon: 'fa-map-marker',
    url: "/external/thaimap",
    name: "แผนที่",
    menuname: "m16",
    orderby: 30 ,
  },

  {
    IDchildren: 'training_private',
    icon: 'fa-shekel-sign',
    name: "การฝึกอบรมหลักสูตร",
    menuname: "m15",
    orderby: 33 ,
    children: [
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/train',
        name: 'สมัครฝึกอบรม'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/training/report/history',
        name: 'ประวัติการฝึกอบรม'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/training/surveylecturer',
        name: 'แบบประเมินการอบรม'
      },
    ]
  },
]
export const Provincialgovernor: NavBar[] = [ //ผู้ว่าราชการจังหวัด
  {
    icon: 'fa-home',
    url: "/main",
    name: "หน้าหลัก",
    menuname: "m1",
    orderby: 1 ,
  },
  {
    icon: 'fa-book',
    url: "/electronicbookprovince",
    name: "สมุดตรวจอิเล็กทรอนิกส์(จังหวัด)",
    menuname: "m25",
    orderby: 13 ,
  },
  {
    icon: 'fa-list-alt',
    url: "/supportgovernment",
    name: "ข้อมูลสนับสนุน",
    menuname: "m10",
    orderby: 25 ,
  },
  {
    IDchildren: 'strategic',
    icon: 'fa-flag',
    name: "นโยบาย&แผนยุทธศาสตร์",
    menuname: "m11",
    orderby: 26 ,
    children: [
      {
        ex_link: '1',
        icon: 'fa-file',
        //url: '/nationalstrategy',
        url:'http://nscr.nesdc.go.th/ns/',
        name: 'ยุทธศาสตร์ชาติ'
      },
      {
        ex_link: '1',
        icon: 'fa-file',
        url: 'http://nscr.nesdc.go.th/cr/',
        name: 'แผนการปฏิรูปประเทศ'
      },
      {
        ex_link: '1',
        icon: 'fa-file',
        url: 'https://www.nesdc.go.th/main.php?filename=develop_issue',
        name: 'แผนพัฒนาเศรษฐกิจและสังคมแห่งชาติ'
      },
      {
        ex_link: '1',
        icon: 'fa-file',
        url: 'http://nscr.nesdc.go.th/master-plans/',
        name: 'แผนแม่บทภายใต้ยุทธศาสตร์ชาติ'
      },

      {
        ex_link: '1',
        icon: 'fa-file',
        url: 'https://www.soc.go.th/?p=22892',
        name: 'นโยบายรัฐบาล'
      },
    ]
  },
  {
    IDchildren: 'command',
    icon: 'fa-bolt',
    name: "คำสั่งต่าง ๆ",
    menuname: "m12",
    orderby: 27 ,
    children: [
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/instructionorder',
        name: 'คำสั่งรับผิดชอบเขตตรวจราชการ'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/inspectionorder',
        name: 'คำสั่งการตรวจราชการประจำปี'
      },
    ]
  },
  {
    IDchildren: 'contactpersonnel',
    icon: 'fa-user-tie',
    name: "ข้อมูลการติดต่อบุคลากร",
    menuname: "m13",
    orderby: 28 ,
    children: [
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/maincabinet',
        name: 'คณะรัฐมนตรี'
      },
      // {
      //   ex_link: '0',
      //   icon: 'fa-file',
      //   url: '/external/otps',
      //   name: 'คณะรัฐมนตรี(ระบบ otps)'
      // },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/inspector',
        name: 'ผู้ตรวจราชการ'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/officerinspection',
        name: 'เจ้าหน้าที่ประจำเขตตรวจราชการ'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/regionalagency',
        name: 'หน่วยงานในส่วนภูมิภาค'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/external/ggc-opm',
        name: 'คณะกรรมการธรรมมาภิบาลจังหวัด'
      },
      {
        ex_link: '1',
        icon: 'fa-file',
        url: 'https://www.ggc.opm.go.th/index.php?page=map',
        name: 'เครือข่ายคณะกรรมการธรรมมาภิบาลจังหวัด'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        // url: 'http://www.opm.go.th/opmportal/index.asp?pageid=1427&parent=1232&directory=14727&pagename=content1',
        url: '/advisercivilsector',
        name: 'ที่ปรึกษาผู้ตรวจราชการภาคประชาชน'
      },
    ]
  },
  {
    icon: 'fa-map-marker',
    url: "/external/thaimap",
    name: "แผนที่",
    menuname: "m16",
    orderby: 30 ,
  },
  {
    IDchildren: 'training_private',
    icon: 'fa-shekel-sign',
    name: "การฝึกอบรมหลักสูตร",
    menuname: "m15",
    orderby: 33 ,
    children: [
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/train',
        name: 'สมัครฝึกอบรม'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/training/report/history',
        name: 'ประวัติการฝึกอบรม'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/training/surveylecturer',
        name: 'แบบประเมินการอบรม'
      },
    ]
  },
]
export const Adminprovince: NavBar[] = [ //หัวหน้าสำนักงานจังหวัด
  {
    icon: 'fa-home',
    url: "/main",
    name: "หน้าหลัก",
    menuname: "m1",
    orderby: 1 ,
  },
  {
    icon: 'fa-archive',
    url: "/centralpolicy",
    name: "แผนการตรวจราชการ",
    menuname: "m2",
    orderby: 2 ,
  },
  {
    icon: 'fa-calendar',
    url: "/inspectionplanevent",
    name: "ปฏิทินการตรวจราชการ",
    menuname: "m29",
    orderby: 4 ,
  },
  {
    icon: 'fa-book',
    url: "/electronicbookprovince",
    name: "สมุดตรวจอิเล็กทรอนิกส์(จังหวัด)",
    menuname: "m25",
    orderby: 13 ,
  },
  {
    icon: 'fa-hands',
    name: "แจ้งข้อมูลถึงผู้ตรวจราชการ",
    IDchildren: 'requestorderdata',
    menuname: "m7",
    orderby: 21 ,
    children: [
      {
        icon: 'fa-file',
        url: '/requestorder',
        name: 'แจ้งข้อมูลถึงผู้ตรวจราชการ',
        ex_link: '0',
      },
      {
        icon: 'fa-file',
        url: '/exportrequestorderforadminprovince',
        name: 'รายงานคำร้องขอของหน่วยรับตรวจ',
        ex_link: '0',

      },
      {
        icon: 'fa-file',
        url: 'exportrequestorderforinspector',
        name: 'ทะเบียนคำร้องขอของหน่วยรับตรวจ',
        ex_link: '0',

      }
    ]
  },

  {
    icon: 'fa-list-alt',
    url: "/supportgovernment",
    name: "ข้อมูลสนับสนุน",
    menuname: "m10",
    orderby: 25 ,
  },
  {
    IDchildren: 'strategic',
    icon: 'fa-flag',
    name: "นโยบาย&แผนยุทธศาสตร์",
    menuname: "m11",
    orderby: 26 ,
    children: [
      {
        ex_link: '1',
        icon: 'fa-file',
        //url: '/nationalstrategy',
        url:'http://nscr.nesdc.go.th/ns/',
        name: 'ยุทธศาสตร์ชาติ'
      },
      {
        ex_link: '1',
        icon: 'fa-file',
        url: 'http://nscr.nesdc.go.th/cr/',
        name: 'แผนการปฏิรูปประเทศ'
      },
      {
        ex_link: '1',
        icon: 'fa-file',
        url: 'https://www.nesdc.go.th/main.php?filename=develop_issue',
        name: 'แผนพัฒนาเศรษฐกิจและสังคมแห่งชาติ'
      },
      {
        ex_link: '1',
        icon: 'fa-file',
        url: 'http://nscr.nesdc.go.th/master-plans/',
        name: 'แผนแม่บทภายใต้ยุทธศาสตร์ชาติ'
      },

      {
        ex_link: '1',
        icon: 'fa-file',
        url: 'https://www.soc.go.th/?p=22892',
        name: 'นโยบายรัฐบาล'
      },
    ]
  },
  {
    IDchildren: 'command',
    icon: 'fa-bolt',
    name: "คำสั่งต่าง ๆ",
    menuname: "m12",
    orderby: 27 ,
    children: [
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/instructionorder',
        name: 'คำสั่งรับผิดชอบเขตตรวจราชการ'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/inspectionorder',
        name: 'คำสั่งการตรวจราชการประจำปี'
      },
    ]
  },
  {
    IDchildren: 'contactpersonnel',
    icon: 'fa-user-tie',
    name: "ข้อมูลการติดต่อบุคลากร",
    menuname: "m13",
    orderby: 28 ,
    children: [
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/maincabinet',
        name: 'คณะรัฐมนตรี'
      },
      // {
      //   ex_link: '0',
      //   icon: 'fa-file',
      //   url: '/external/otps',
      //   name: 'คณะรัฐมนตรี(ระบบ otps)'
      // },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/inspector',
        name: 'ผู้ตรวจราชการ'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/officerinspection',
        name: 'เจ้าหน้าที่ประจำเขตตรวจราชการ'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/regionalagency',
        name: 'หน่วยงานในส่วนภูมิภาค'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/external/ggc-opm',
        name: 'คณะกรรมการธรรมมาภิบาลจังหวัด'
      },
      {
        ex_link: '1',
        icon: 'fa-file',
        url: 'https://www.ggc.opm.go.th/index.php?page=map',
        name: 'เครือข่ายคณะกรรมการธรรมมาภิบาลจังหวัด'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        // url: 'http://www.opm.go.th/opmportal/index.asp?pageid=1427&parent=1232&directory=14727&pagename=content1',
        url: '/advisercivilsector',
        name: 'ที่ปรึกษาผู้ตรวจราชการภาคประชาชน'
      },
    ]
  },
  {
    icon: 'fa-map-marker',
    url: "/external/thaimap",
    name: "แผนที่",
    menuname: "m16",
    orderby: 30 ,
  },
  {
    IDchildren: 'training_private',
    icon: 'fa-shekel-sign',
    name: "การฝึกอบรมหลักสูตร",
    menuname: "m15",
    orderby: 33 ,
    children: [
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/train',
        name: 'สมัครฝึกอบรม'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/training/report/history',
        name: 'ประวัติการฝึกอบรม'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/training/surveylecturer',
        name: 'แบบประเมินการอบรม'
      },
    ]
  },
]
export const InspectorMinistry: NavBar[] = [ //ผู้ตรวจกระทรวง
  {
    icon: 'fa-home',
    url: "/main",
    name: "หน้าหลัก",
    menuname: "m1",
    orderby: 1 ,
  },
  {
    IDchildren: 'calendarmenu',
    icon: 'fa-calendar',
    name: "ปฏิทินการตรวจราชการ",
    menuname: "m30",
    orderby: 5 ,
    children: [
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/inspectionplanevent',
        name: 'สร้างเอง'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/calendaruser',
        name: 'ถูกเชิญ'
      },
    ]
  },
  {
    icon: 'fa-file',
    url: "/answersubject",
    name: "ประเด็นการตรวจติดตาม",
    menuname: "m22",
    orderby: 10 ,
  },
  {
    IDchildren: 'electronicbook',
    icon: 'fa-book',
    name: "สมุดตรวจอิเล็กทรอนิกส์(ผู้ตรวจกระทรวง,กรม)",
    menuname: "m32",
    orderby: 14 ,
    children: [
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/electronicbook',
        name: 'ของฉัน'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/electronicbook/invited',
        name: 'อื่นๆ'
      },
    ]
  },
  {
    icon: 'fa-list-alt',
    url: "/supportgovernment",
    name: "ข้อมูลสนับสนุน",
    menuname: "m10",
    orderby: 25 ,
  },
  {
    IDchildren: 'strategic',
    icon: 'fa-flag',
    name: "นโยบาย&แผนยุทธศาสตร์",
    menuname: "m11",
    orderby: 26 ,
    children: [
      {
        ex_link: '1',
        icon: 'fa-file',
        //url: '/nationalstrategy',
        url:'http://nscr.nesdc.go.th/ns/',
        name: 'ยุทธศาสตร์ชาติ'
      },
      {
        ex_link: '1',
        icon: 'fa-file',
        url: 'http://nscr.nesdc.go.th/cr/',
        name: 'แผนการปฏิรูปประเทศ'
      },
      {
        ex_link: '1',
        icon: 'fa-file',
        url: 'https://www.nesdc.go.th/main.php?filename=develop_issue',
        name: 'แผนพัฒนาเศรษฐกิจและสังคมแห่งชาติ'
      },
      {
        ex_link: '1',
        icon: 'fa-file',
        url: 'http://nscr.nesdc.go.th/master-plans/',
        name: 'แผนแม่บทภายใต้ยุทธศาสตร์ชาติ'
      },

      {
        ex_link: '1',
        icon: 'fa-file',
        url: 'https://www.soc.go.th/?p=22892',
        name: 'นโยบายรัฐบาล'
      },
    ]
  },
  {
    IDchildren: 'command',
    icon: 'fa-bolt',
    name: "คำสั่งต่าง ๆ",
    menuname: "m12",
    orderby: 27 ,
    children: [
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/instructionorder',
        name: 'คำสั่งรับผิดชอบเขตตรวจราชการ'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/inspectionorder',
        name: 'คำสั่งการตรวจราชการประจำปี'
      },
    ]
  },
  {
    IDchildren: 'contactpersonnel',
    icon: 'fa-user-tie',
    name: "ข้อมูลการติดต่อบุคลากร",
    menuname: "m13",
    orderby: 28 ,
    children: [
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/maincabinet',
        name: 'คณะรัฐมนตรี'
      },
      // {
      //   ex_link: '0',
      //   icon: 'fa-file',
      //   url: '/external/otps',
      //   name: 'คณะรัฐมนตรี(ระบบ otps)'
      // },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/inspector',
        name: 'ผู้ตรวจราชการ'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/officerinspection',
        name: 'เจ้าหน้าที่ประจำเขตตรวจราชการ'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/regionalagency',
        name: 'หน่วยงานในส่วนภูมิภาค'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/external/ggc-opm',
        name: 'คณะกรรมการธรรมมาภิบาลจังหวัด'
      },
      {
        ex_link: '1',
        icon: 'fa-file',
        url: 'https://www.ggc.opm.go.th/index.php?page=map',
        name: 'เครือข่ายคณะกรรมการธรรมมาภิบาลจังหวัด'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        // url: 'http://www.opm.go.th/opmportal/index.asp?pageid=1427&parent=1232&directory=14727&pagename=content1',
        url: '/advisercivilsector',
        name: 'ที่ปรึกษาผู้ตรวจราชการภาคประชาชน'
      },
    ]
  },
  {
    icon: 'fa-map-marker',
    url: "/external/thaimap",
    name: "แผนที่",
    menuname: "m16",
    orderby: 30 ,
  },
  {
    IDchildren: 'training_private',
    icon: 'fa-shekel-sign',
    name: "การฝึกอบรมหลักสูตร",
    menuname: "m15",
    orderby: 33 ,
    children: [
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/train',
        name: 'สมัครฝึกอบรม'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/training/report/history',
        name: 'ประวัติการฝึกอบรม'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/training/surveylecturer',
        name: 'แบบประเมินการอบรม'
      },
    ]
  },

]
export const publicsector: NavBar[] = [ //ภาคประชาชน
  {
    icon: 'fa-home',
    url: "/main",
    name: "หน้าหลัก",
    menuname: "m1",
    orderby: 1 ,
  },
  {
    icon: 'fa-book',
    url: "/answerpeople",
    name: "ข้อซักถาม",
    menuname: "m21",
    orderby: 18 ,
  },
  {
    icon: 'fa-calendar',
    url: "/calendaruser",
    name: "ปฏิทินการตรวจราชการ",
    menuname: "m31",
    orderby: 6 ,
  },
  {
    icon: 'fa-list-alt',
    url: "/supportgovernment",
    name: "ข้อมูลสนับสนุน",
    menuname: "m10",
    orderby: 25 ,
  },
  {
    IDchildren: 'strategic',
    icon: 'fa-flag',
    name: "นโยบาย&แผนยุทธศาสตร์",
    menuname: "m11",
    orderby: 26 ,
    children: [
      {
        ex_link: '1',
        icon: 'fa-file',
        //url: '/nationalstrategy',
        url:'http://nscr.nesdc.go.th/ns/',
        name: 'ยุทธศาสตร์ชาติ'
      },
      {
        ex_link: '1',
        icon: 'fa-file',
        url: 'http://nscr.nesdc.go.th/cr/',
        name: 'แผนการปฏิรูปประเทศ'
      },
      {
        ex_link: '1',
        icon: 'fa-file',
        url: 'https://www.nesdc.go.th/main.php?filename=develop_issue',
        name: 'แผนพัฒนาเศรษฐกิจและสังคมแห่งชาติ'
      },
      {
        ex_link: '1',
        icon: 'fa-file',
        url: 'http://nscr.nesdc.go.th/master-plans/',
        name: 'แผนแม่บทภายใต้ยุทธศาสตร์ชาติ'
      },

      {
        ex_link: '1',
        icon: 'fa-file',
        url: 'https://www.soc.go.th/?p=22892',
        name: 'นโยบายรัฐบาล'
      },
    ]
  },
  {
    IDchildren: 'command',
    icon: 'fa-bolt',
    name: "คำสั่งต่าง ๆ",
    menuname: "m12",
    orderby: 27 ,
    children: [
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/instructionorder',
        name: 'คำสั่งรับผิดชอบเขตตรวจราชการ'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/inspectionorder',
        name: 'คำสั่งการตรวจราชการประจำปี'
      },
    ]
  },
  {
    IDchildren: 'contactpersonnel',
    icon: 'fa-user-tie',
    name: "ข้อมูลการติดต่อบุคลากร",
    menuname: "m13",
    orderby: 28 ,
    children: [
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/maincabinet',
        name: 'คณะรัฐมนตรี'
      },
      // {
      //   ex_link: '0',
      //   icon: 'fa-file',
      //   url: '/external/otps',
      //   name: 'คณะรัฐมนตรี(ระบบ otps)'
      // },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/inspector',
        name: 'ผู้ตรวจราชการ'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/officerinspection',
        name: 'เจ้าหน้าที่ประจำเขตตรวจราชการ'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/regionalagency',
        name: 'หน่วยงานในส่วนภูมิภาค'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/external/ggc-opm',
        name: 'คณะกรรมการธรรมมาภิบาลจังหวัด'
      },
      {
        ex_link: '1',
        icon: 'fa-file',
        url: 'https://www.ggc.opm.go.th/index.php?page=map',
        name: 'เครือข่ายคณะกรรมการธรรมมาภิบาลจังหวัด'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        // url: 'http://www.opm.go.th/opmportal/index.asp?pageid=1427&parent=1232&directory=14727&pagename=content1',
        url: '/advisercivilsector',
        name: 'ที่ปรึกษาผู้ตรวจราชการภาคประชาชน'
      },
    ]
  },
  {
    icon: 'fa-map-marker',
    url: "/external/thaimap",
    name: "แผนที่",
    menuname: "m16",
    orderby: 30 ,
  },
  {
    IDchildren: 'training_private',
    icon: 'fa-shekel-sign',
    name: "การฝึกอบรมหลักสูตร",
    menuname: "m15",
    orderby: 33 ,
    children: [
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/train',
        name: 'สมัครฝึกอบรม'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/training/report/history',
        name: 'ประวัติการฝึกอบรม'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/training/surveylecturer',
        name: 'แบบประเมินการอบรม'
      },
    ]
  },
]
export const president: NavBar[] = [ //ผู้บริหารหรือนายก
  {
    icon: 'fa-home',
    url: "/main",
    name: "หน้าหลัก",
    menuname: "m1",
    orderby: 1 ,
  },
  {
    icon: 'fa-archive',
    url: "/centralpolicy",
    name: "แผนการตรวจราชการ",
    menuname: "m2",
    orderby: 2 ,
  },
  {
    icon: 'fa-hand-point-up',
    name: "ข้อสั่งการถึงผู้ตรวจราชการ",
    IDchildren: 'executiveorderdata',
    menuname: "m6",
    orderby: 20 ,
    children: [
      {
        icon: 'fa-file',
        url: '/executiveorder',
        name: 'ข้อสั่งการถึงผู้ตรวจราชการ',
        ex_link: '0',
      },
      {
        icon: 'fa-file',
        url: '/executiveorderexport1',
        name: 'รายงานข้อสั่งการของผู้บริหาร',
        ex_link: '0',

      },
      {
        icon: 'fa-file',
        url: '/executiveorderexport3',
        name: 'ทะเบียนข้อสั่งการของผู้บริหาร',
        ex_link: '0',

      }
    ]
  },
  {
    icon: 'fa-user-tie',
    url: "/commanderreport",
    name: "รายงานผลการตรวจราชการ",
    menuname: "m34",
    orderby: 16 ,
  },
  {
    icon: 'fa-list-alt',
    url: "/supportgovernment",
    name: "ข้อมูลสนับสนุน",
    menuname: "m10",
    orderby: 25 ,
  },
  {
    IDchildren: 'strategic',
    icon: 'fa-flag',
    name: "นโยบาย&แผนยุทธศาสตร์",
    menuname: "m11",
    orderby: 26 ,
    children: [
      {
        ex_link: '1',
        icon: 'fa-file',
        //url: '/nationalstrategy',
        url:'http://nscr.nesdc.go.th/ns/',
        name: 'ยุทธศาสตร์ชาติ'
      },
      {
        ex_link: '1',
        icon: 'fa-file',
        url: 'http://nscr.nesdc.go.th/cr/',
        name: 'แผนการปฏิรูปประเทศ'
      },
      {
        ex_link: '1',
        icon: 'fa-file',
        url: 'https://www.nesdc.go.th/main.php?filename=develop_issue',
        name: 'แผนพัฒนาเศรษฐกิจและสังคมแห่งชาติ'
      },
      {
        ex_link: '1',
        icon: 'fa-file',
        url: 'http://nscr.nesdc.go.th/master-plans/',
        name: 'แผนแม่บทภายใต้ยุทธศาสตร์ชาติ'
      },

      {
        ex_link: '1',
        icon: 'fa-file',
        url: 'https://www.soc.go.th/?p=22892',
        name: 'นโยบายรัฐบาล'
      },
    ]
  },
  {
    IDchildren: 'command',
    icon: 'fa-bolt',
    name: "คำสั่งต่าง ๆ",
    menuname: "m12",
    orderby: 27 ,
    children: [
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/instructionorder',
        name: 'คำสั่งรับผิดชอบเขตตรวจราชการ'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/inspectionorder',
        name: 'คำสั่งการตรวจราชการประจำปี'
      },
    ]
  },
  {
    IDchildren: 'contactpersonnel',
    icon: 'fa-user-tie',
    name: "ข้อมูลการติดต่อบุคลากร",
    menuname: "m13",
    orderby: 28 ,
    children: [
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/maincabinet',
        name: 'คณะรัฐมนตรี'
      },
      // {
      //   ex_link: '0',
      //   icon: 'fa-file',
      //   url: '/external/otps',
      //   name: 'คณะรัฐมนตรี(ระบบ otps)'
      // },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/inspector',
        name: 'ผู้ตรวจราชการ'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/officerinspection',
        name: 'เจ้าหน้าที่ประจำเขตตรวจราชการ'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/regionalagency',
        name: 'หน่วยงานในส่วนภูมิภาค'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/external/ggc-opm',
        name: 'คณะกรรมการธรรมมาภิบาลจังหวัด'
      },
      {
        ex_link: '1',
        icon: 'fa-file',
        url: 'https://www.ggc.opm.go.th/index.php?page=map',
        name: 'เครือข่ายคณะกรรมการธรรมมาภิบาลจังหวัด'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        // url: 'http://www.opm.go.th/opmportal/index.asp?pageid=1427&parent=1232&directory=14727&pagename=content1',
        url: '/advisercivilsector',
        name: 'ที่ปรึกษาผู้ตรวจราชการภาคประชาชน'
      },
    ]
  },
  {
    icon: 'fa-map-marker',
    url: "/external/thaimap",
    name: "แผนที่",
    menuname: "m16",
    orderby: 30 ,
  },
  {
    IDchildren: 'training_private',
    icon: 'fa-shekel-sign',
    name: "การฝึกอบรมหลักสูตร",
    menuname: "m15",
    orderby: 33 ,
    children: [
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/train',
        name: 'สมัครฝึกอบรม'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/training/report/history',
        name: 'ประวัติการฝึกอบรม'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/training/surveylecturer',
        name: 'แบบประเมินการอบรม'
      },
    ]
  },
]
export const InspectorExamination: NavBar[] = [ //หน่วยรับตรวจ
  {
    icon: 'fa-home',
    url: "/main",
    name: "หน้าหลัก",
    menuname: "m1",
    orderby: 1 ,
  },

  {
    IDchildren: 'schedule',
    icon: 'fa-calendar',
    name: "กำหนดการตรวจราชการ",
    menuname: "m28",
    orderby: 8 ,
    children: [
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/inspectionplanevent',
        name: 'ปฏิทินการตรวจราชการ'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: "/calendaruser",
        name: "ถูกเชิญ"
      },
    ]
  },
  {
    icon: 'fa-archive',
    url: "/answersubject",
    name: "ประเด็นตรวจติดตาม",
    menuname: "m22",
    orderby: 10 ,
  },
  {
    icon: 'fa-file',
    url: "/answerrecommendationinspector",
    name: "ข้อเสนอแนะของผู้ตรวจราชการ",
    menuname: "m23",
    orderby: 19 ,
  },
  {
    IDchildren: 'electronicBook',
    icon: 'fa-book',
    name: "สมุดตรวจอิเล็กทรอนิกส์(หน่วยรับตรวจ)",
    menuname: "m33",
    orderby: 15 ,
    children: [
      {
        ex_link: '0',
        icon: 'fa-file',
        url: "/electronicbookdepartment",
        name: 'รอดำเนินการ'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/electronicbook/other',
        name: 'ถูกส่งต่อ'
      },
    ]
  },
  {
    icon: 'fa-list-alt',
    url: "/supportgovernment",
    name: "ข้อมูลสนับสนุน",
    menuname: "m10",
    orderby: 25 ,
  },
  {
    IDchildren: 'strategic',
    icon: 'fa-flag',
    name: "นโยบาย&แผนยุทธศาสตร์",
    menuname: "m11",
    orderby: 26 ,
    children: [
      {
        ex_link: '1',
        icon: 'fa-file',
        //url: '/nationalstrategy',
        url:'http://nscr.nesdc.go.th/ns/',
        name: 'ยุทธศาสตร์ชาติ'
      },
      {
        ex_link: '1',
        icon: 'fa-file',
        url: 'http://nscr.nesdc.go.th/cr/',
        name: 'แผนการปฏิรูปประเทศ'
      },
      {
        ex_link: '1',
        icon: 'fa-file',
        url: 'https://www.nesdc.go.th/main.php?filename=develop_issue',
        name: 'แผนพัฒนาเศรษฐกิจและสังคมแห่งชาติ'
      },
      {
        ex_link: '1',
        icon: 'fa-file',
        url: 'http://nscr.nesdc.go.th/master-plans/',
        name: 'แผนแม่บทภายใต้ยุทธศาสตร์ชาติ'
      },

      {
        ex_link: '1',
        icon: 'fa-file',
        url: 'https://www.soc.go.th/?p=22892',
        name: 'นโยบายรัฐบาล'
      },
    ]
  },
  {
    IDchildren: 'command',
    icon: 'fa-bolt',
    name: "คำสั่งต่าง ๆ",
    menuname: "m12",
    orderby: 27 ,
    children: [
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/instructionorder',
        name: 'คำสั่งรับผิดชอบเขตตรวจราชการ'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/inspectionorder',
        name: 'คำสั่งการตรวจราชการประจำปี'
      },
    ]
  },
  {
    IDchildren: 'contactpersonnel',
    icon: 'fa-user-tie',
    name: "ข้อมูลการติดต่อบุคลากร",
    menuname: "m13",
    orderby: 28 ,
    children: [
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/maincabinet',
        name: 'คณะรัฐมนตรี'
      },
      // {
      //   ex_link: '0',
      //   icon: 'fa-file',
      //   url: '/external/otps',
      //   name: 'คณะรัฐมนตรี(ระบบ otps)'
      // },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/inspector',
        name: 'ผู้ตรวจราชการ'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/officerinspection',
        name: 'เจ้าหน้าที่ประจำเขตตรวจราชการ'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/regionalagency',
        name: 'หน่วยงานในส่วนภูมิภาค'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/external/ggc-opm',
        name: 'คณะกรรมการธรรมมาภิบาลจังหวัด'
      },
      {
        ex_link: '1',
        icon: 'fa-file',
        url: 'https://www.ggc.opm.go.th/index.php?page=map',
        name: 'เครือข่ายคณะกรรมการธรรมมาภิบาลจังหวัด'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        // url: 'http://www.opm.go.th/opmportal/index.asp?pageid=1427&parent=1232&directory=14727&pagename=content1',
        url: '/advisercivilsector',
        name: 'ที่ปรึกษาผู้ตรวจราชการภาคประชาชน'
      },
    ]
  },
  {
    icon: 'fa-map-marker',
    url: "/external/thaimap",
    name: "แผนที่",
    menuname: "m16",
    orderby: 30 ,
  },
  {
    IDchildren: 'training_private',
    icon: 'fa-shekel-sign',
    name: "การฝึกอบรมหลักสูตร",
    menuname: "m15",
    orderby: 33 ,
    children: [
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/train',
        name: 'สมัครฝึกอบรม'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/training/report/history',
        name: 'ประวัติการฝึกอบรม'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/training/surveylecturer',
        name: 'แบบประเมินการอบรม'
      },
    ]
  },

]
export const InspectorDepartment: NavBar[] = [ //ผู้ตรวจกรม/หน่วยงาน
  {
    icon: 'fa-home',
    url: "/main",
    name: "หน้าหลัก",
    menuname: "m1",
    orderby: 1 ,
  },
  {
    IDchildren: 'calendarmenu',
    icon: 'fa-calendar',
    name: "ปฏิทินการตรวจราชการ",
    menuname: "m30",
    orderby: 5 ,
    children: [
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/inspectionplanevent',
        name: 'สร้างเอง'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/calendaruser',
        name: 'ถูกเชิญ'
      },
    ]
  },
  {
    icon: 'fa-file',
    url: "/answersubject",
    name: "ประเด็นการตรวจติดตาม",
    menuname: "m22",
    orderby: 10 ,
  },
  {
    IDchildren: 'electronicbook',
    icon: 'fa-book',
    name: "สมุดตรวจอิเล็กทรอนิกส์(ผู้ตรวจกระทรวง,กรม)",
    url: '/electronicbook/invited',
    menuname: "m32",
    orderby: 14 ,
    children: [
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/electronicbook',
        name: 'ของฉัน'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/electronicbook/invited',
        name: 'อื่นๆ'
      },
    ]
  },
  {
    icon: 'fa-list-alt',
    url: "/supportgovernment",
    name: "ข้อมูลสนับสนุน",
    menuname: "m10",
    orderby: 25 ,
  },
  {
    IDchildren: 'strategic',
    icon: 'fa-flag',
    name: "นโยบาย&แผนยุทธศาสตร์",
    menuname: "m11",
    orderby: 26 ,
    children: [
      {
        ex_link: '1',
        icon: 'fa-file',
        //url: '/nationalstrategy',
        url:'http://nscr.nesdc.go.th/ns/',
        name: 'ยุทธศาสตร์ชาติ'
      },
      {
        ex_link: '1',
        icon: 'fa-file',
        url: 'http://nscr.nesdc.go.th/cr/',
        name: 'แผนการปฏิรูปประเทศ'
      },
      {
        ex_link: '1',
        icon: 'fa-file',
        url: 'https://www.nesdc.go.th/main.php?filename=develop_issue',
        name: 'แผนพัฒนาเศรษฐกิจและสังคมแห่งชาติ'
      },
      {
        ex_link: '1',
        icon: 'fa-file',
        url: 'http://nscr.nesdc.go.th/master-plans/',
        name: 'แผนแม่บทภายใต้ยุทธศาสตร์ชาติ'
      },

      {
        ex_link: '1',
        icon: 'fa-file',
        url: 'https://www.soc.go.th/?p=22892',
        name: 'นโยบายรัฐบาล'
      },
    ]
  },
  {
    IDchildren: 'command',
    icon: 'fa-bolt',
    name: "คำสั่งต่าง ๆ",
    menuname: "m12",
    orderby: 27 ,
    children: [
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/instructionorder',
        name: 'คำสั่งรับผิดชอบเขตตรวจราชการ'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/inspectionorder',
        name: 'คำสั่งการตรวจราชการประจำปี'
      },
    ]
  },
  {
    IDchildren: 'contactpersonnel',
    icon: 'fa-user-tie',
    name: "ข้อมูลการติดต่อบุคลากร",
    menuname: "m13",
    orderby: 28 ,
    children: [
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/maincabinet',
        name: 'คณะรัฐมนตรี'
      },
      // {
      //   ex_link: '0',
      //   icon: 'fa-file',
      //   url: '/external/otps',
      //   name: 'คณะรัฐมนตรี(ระบบ otps)'
      // },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/inspector',
        name: 'ผู้ตรวจราชการ'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/officerinspection',
        name: 'เจ้าหน้าที่ประจำเขตตรวจราชการ'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/regionalagency',
        name: 'หน่วยงานในส่วนภูมิภาค'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/external/ggc-opm',
        name: 'คณะกรรมการธรรมมาภิบาลจังหวัด'
      },
      {
        ex_link: '1',
        icon: 'fa-file',
        url: 'https://www.ggc.opm.go.th/index.php?page=map',
        name: 'เครือข่ายคณะกรรมการธรรมมาภิบาลจังหวัด'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        // url: 'http://www.opm.go.th/opmportal/index.asp?pageid=1427&parent=1232&directory=14727&pagename=content1',
        url: '/advisercivilsector',
        name: 'ที่ปรึกษาผู้ตรวจราชการภาคประชาชน'
      },
    ]
  },
  {
    icon: 'fa-map-marker',
    url: "/external/thaimap",
    name: "แผนที่",
    orderby: 30 ,
    menuname: "m16",
  },
  {
    IDchildren: 'training_private',
    icon: 'fa-shekel-sign',
    name: "การฝึกอบรมหลักสูตร",
    menuname: "m15",
    orderby: 33 ,
    children: [
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/train',
        name: 'สมัครฝึกอบรม'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/training/report/history',
        name: 'ประวัติการฝึกอบรม'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/training/surveylecturer',
        name: 'แบบประเมินการอบรม'
      },
    ]
  },
]
export const External: NavBar[] = [ //บุคคลภายนอก
  {
    icon: 'fa-home',
    url: "/main",
    name: "หน้าหลัก",
    menuname: "m1",
    orderby: 1 ,
  },
  {
    IDchildren: 'training_private',
    icon: 'fa-shekel-sign',
    name: "การฝึกอบรมหลักสูตร",
    menuname: "m15",
    orderby: 33 ,
    children: [
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/train',
        name: 'สมัครฝึกอบรม'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/training/report/history',
        name: 'ประวัติการฝึกอบรม'
      },
      {
        ex_link: '0',
        icon: 'fa-file',
        url: '/training/surveylecturer',
        name: 'แบบประเมินการอบรม'
      },
    ]
  },
]

export const allNav: any[] = [
  superAdmin,
  Centraladmin,
  Inspector,
  Provincialgovernor,
  Adminprovince,
  InspectorMinistry,
  publicsector,
  president,
  InspectorExamination,
  InspectorDepartment,
  External,
]

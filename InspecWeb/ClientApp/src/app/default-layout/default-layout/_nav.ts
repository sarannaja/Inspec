
export interface NavBar {
  icon?: string;
  name?: string;
  url?: string;
  ex_link?: string;
  children?: Array<Children>
  classtap?: string;
  IDchildren?: string;
}

export interface Children {
  icon?: string;
  name?: string;
  url?: string;
  ex_link?: string;
  id?: string;
}

export const superAdmin:NavBar[] = [ // ซุปเปอร์แอดมิน
    {
        icon:'fa-home',
        url:"/main",
        name:"หน้าหลัก"
    },
    {
        icon:'fa-archive',
        url:"/centralpolicy",
        name:"แผนการตรวจประจำปี"
    },
    {
        icon:'fa-calendar',
        url:"/inspectionplanevent",
        name:"ปฏิทินการตรวจราชการ"
    },
    {
      icon:'fa-book',
      url:"/electronicbook",
      name:"สมุดตรวจอิเล็กทรอนิกส์"
  },
    {
        icon:'fa-hand-point-up',
        url:"/executiveorder",
        name:"ข้อสั่งการผู้บริหาร"
    },
    {
        icon:'fa-hands',
        url:"#3",
        name:"แจ้งคำร้องขอ"
    },
    // {
    //     classtap:'sidebar-header',
    //     url:"#",
    //     name:"______________________"
    // },
    {
        icon:'fa-database',
        name:"ข้อมูลพื้นฐาน",
        IDchildren:'basicdata',
        children:[
            {
                icon:'fa-long-arrow-alt-right',
                url:'/fiscalyear',
                name:'ปีงบประมาณ'
            },
            {
                icon:'fa-long-arrow-alt-right',
                url:'/region',
                name:'เขตตรวจราชการ'
            },
            {
                icon:'fa-long-arrow-alt-right',
                url:'/province',
                name:'จังหวัด'
            },
            {
                icon:'fa-long-arrow-alt-right',
                url:'/ministry',
                name:'กระทรวง/กรม'
            },
        ]
    } ,
    // {
    //     icon:'fa-user-friends',
    //     url:"/user",
    //     name:"จัดการผู้ใช้"
    // },
    {
    icon:'fa-user-friends',
    name:"จัดการผู้ใช้",
    IDchildren:'userdata',
    children:[
        {
            icon:'fa-long-arrow-alt-right',
            url:'/user/1',
            name:'ผู้ดูแลระบบ',
            ex_link:'user',
            id:'1'
        },
        {
            icon:'fa-long-arrow-alt-right',
            url:'/user/2',
            name:'ผู้ดูแลแผนการตรวจราชการประจำปี',
            ex_link:'user',
            id:'2'
        },
        {
            icon:'fa-long-arrow-alt-right',
            url:'/user/3',
            name:'ผู้ตรวจราชการ',
            ex_link:'user',
            id:'3'
        },
        {
            icon:'fa-long-arrow-alt-right',
            url:'/user/4',
            name:'ผู้ว่าราชการจังหวัด',
            ex_link:'user',
            id:'4'
        },
        {
            icon:'fa-long-arrow-alt-right',
            url:'/user/5',
            name:'ผู้ตรวจจังหวัด',
            ex_link:'user',
            id:'5'
        },
        {
            icon:'fa-long-arrow-alt-right',
            url:'/user/6',
            name:'ผู้ตรวจกระทรวง',
            ex_link:'user',
            id:'6'
        },
        {
            icon:'fa-long-arrow-alt-right',
            url:'/user/9',
            name:'ผู้ตรวจกรม',
            ex_link:'user',
            id:'9'
        },
        {
            icon:'fa-long-arrow-alt-right',
            url:'/user/7',
            name:'ผู้ตรวจภาคประชาชน',
            ex_link:'user',
            id:'7'
        },
        {
            icon: 'fa-long-arrow-alt-right',
            url: '/user/8',
            name: 'นายก/รองนายก',
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
    IDchildren: 'contactpersonnel',
    icon: 'fa-user-tie',
    name: "ข้อมูลการติดต่อบุคลากร",
    children: [
      {
        ex_link: '0',
        icon: 'fa-long-arrow-alt-right',
        url: '/cabinet',
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
        ex_link: '1',
        icon: 'fa-long-arrow-alt-right',
        url: 'http://www.ggc.opm.go.th/index.php?page=index&language=th',
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
        ex_link: '1',
        icon: 'fa-long-arrow-alt-right',
        url: 'http://www.ggc.opm.go.th/index.php?page=index&language=th',
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
]
export const Inspector: NavBar[] = [ //ผู้ตรวจ
  {
    icon: 'fa-home',
    url: "/main",
    name: "หน้าหลัก"
  },
  {
    icon: 'fa-calendar',
    url: "/inspectionplanevent",
    name: "ปฏิทินการตรวจราชการ"
  },
  {
    icon: 'fa-book',
    url: "#1",
    name: "สมุดตรวจอิเล็กทรอนิกส์"
  },
  {
    icon: 'fa-hand-point-up',
    url: "#",
    name: "ข้อสั่งการผู้บริหาร"
  },
  {
    icon: 'fa-hands',
    url: "#",
    name: "แจ้งคำร้องขอ"
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
        ex_link: '1',
        icon: 'fa-long-arrow-alt-right',
        url: 'http://www.ggc.opm.go.th/index.php?page=index&language=th',
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


]
export const Provincialgovernor: NavBar[] = [ //ผู้ว่าราชการจังหวัด
  {
    icon: 'fa-home',
    url: "/main",
    name: "หน้าหลัก"
  },
  {
    icon: 'fa-book',
    url: "#1",
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
        ex_link: '1',
        icon: 'fa-long-arrow-alt-right',
        url: 'http://www.ggc.opm.go.th/index.php?page=index&language=th',
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
]
export const Adminprovince: NavBar[] = [ //แอดมินจังหวัด
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
    url: "#1",
    name: "สมุดตรวจอิเล็กทรอนิกส์"
  },
  {
    icon: 'fa-hands',
    url: "#",
    name: "แจ้งคำร้องขอ"
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
        ex_link: '1',
        icon: 'fa-long-arrow-alt-right',
        url: 'http://www.ggc.opm.go.th/index.php?page=index&language=th',
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
]
export const InspectorMinistry: NavBar[] = [ //ผุ้ตรวจกระทรวง
  {
    icon: 'fa-home',
    url: "/main",
    name: "หน้าหลัก"
  },
  {
    icon: 'fa-book',
    url: "/usercentralpolicy",
    name: "สมุดตรวจอิเล็กทรอนิกส์"
  },
  {
    icon: 'fa-calendar',
    url: "/inspectionplanevent",
    name: "ปฏิทินการตรวจราชการ"
  },
  // {
  //   icon: 'fa-book',
  //   url: "#1",
  //   name: "สมุดตรวจอิเล็กทรอนิกส์"
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
        ex_link: '1',
        icon: 'fa-long-arrow-alt-right',
        url: 'http://www.ggc.opm.go.th/index.php?page=index&language=th',
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

]
export const publicsector: NavBar[] = [ //User ภาคประชาชน
  {
    icon: 'fa-home',
    url: "/main",
    name: "หน้าหลัก"
  },
  {
    icon: 'fa-book',
    url: "/usercentralpolicy",
    name: "สมุดตรวจอิเล็กทรอนิกส์"
  },
  {
    icon: 'fa-calendar',
    url: "/inspectionplanevent",
    name: "ปฏิทินการตรวจราชการ"
  },
  // {
  //   icon: 'fa-book',
  //   url: "#1",
  //   name: "สมุดตรวจอิเล็กทรอนิกส์"
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
        ex_link: '1',
        icon: 'fa-long-arrow-alt-right',
        url: 'http://www.ggc.opm.go.th/index.php?page=index&language=th',
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
]
export const president: NavBar[] = [ //User นายก
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
    url: "/executiveorder",
    name: "ข้อสั่งการผู้บริหาร"
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
        ex_link: '1',
        icon: 'fa-long-arrow-alt-right',
        url: 'http://www.ggc.opm.go.th/index.php?page=index&language=th',
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
]
export const InspectorDepartment:NavBar[] = [ //ผู้ตรวจกรม/หน่วยงาน
    {
        icon:'fa-home',
        url:"/main",
        name:"หน้าหลัก"
    },
    {
        icon:'fa-archive',
        url:"/usercentralpolicy",
        name:"แผนการตรวจประจำปี"
    },
    {
        icon:'fa-calendar',
        url:"/inspectionplanevent",
        name:"ปฏิทินการตรวจราชการ"
    },
    {
      icon:'fa-book',
      url:"#1",
      name:"สมุดตรวจอิเล็กทรอนิกส์"
  },
    {
        icon:'fa-list-alt',
        url:"/supportgovernment",
        name:"ข้อมูลสนับสนุน"
    },
    {
        IDchildren:'contactpersonnel',
        icon:'fa-user-tie',
        name:"ข้อมูลการติดต่อบุคลากร",
        children:[
            {
                ex_link: '1',
                icon:'fa-long-arrow-alt-right',
                url:'http://www.opm.go.th/opmportal/index.asp?pageid=1558&parent=1232&directory=13214&pagename=content1',
                name:'คณะรัฐมนตรี'
            },
            {
                ex_link: '0',
                icon:'fa-long-arrow-alt-right',
                url:'/inspector',
                name:'ผู้ตรวจราชการ'
            },
            {
                ex_link: '1',
                icon:'fa-long-arrow-alt-right',
                url:'/province',
                name:'เจ้าหน้าที่ประจำเขตตรวจราชการ'
            },
            {
                ex_link: '0',
                icon:'fa-long-arrow-alt-right',
                url:'#',
                name:'หน่วยงานในส่วนภูมิภาค'
            },
            {
                ex_link: '1',
                icon:'fa-long-arrow-alt-right',
                url:'http://www.ggc.opm.go.th/index.php?page=index&language=th',
                name:'คณะกรรมการธรรมมาภิบาลจังหวัด'
            },
            {
                ex_link: '0',
                icon:'fa-long-arrow-alt-right',
                url:'#',
                name:'เคลือข่ายคณะกรรมการธรรมมาภิบาลจังหวัด'
            },
            {
                ex_link: '1',
                icon:'fa-long-arrow-alt-right',
                url:'http://www.opm.go.th/opmportal/index.asp?pageid=1427&parent=1232&directory=14727&pagename=content1',
                name:'ที่ปรึกษาผู้ตรวจราชการภาคประชาชน'
            },
        ]
    } ,

]

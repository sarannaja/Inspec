
export interface NavBar{
    icon?:string;
    name?:string;
    url?:string;
    ex_link?:string;
    children?:Array<Children>
    classtap?:string;
    IDchildren?:string;
}

export interface Children{
    icon?:string;
    name?:string;
    url?:string;
    ex_link?:string;
}



export const superAdmin:NavBar[] = [
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
        url:"#1",
        name:"สมุดตรวจราชการ"
    },
    {
        icon:'fa-hand-point-up',
        url:"#2",
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
                name:'ผู้ดูแลระบบ'
            },
            {
                icon:'fa-long-arrow-alt-right',
                url:'/user/2',
                name:'ผู้ดูแลแผนการตรวจราชการประจำปี'
            },
            {
                icon:'fa-long-arrow-alt-right',
                url:'/user/3',
                name:'ผู้ตรวจราชการ'
            },
            {
                icon:'fa-long-arrow-alt-right',
                url:'/user/4',
                name:'ผู้ว่าราชการจังหวัด'
            },
            {
                icon:'fa-long-arrow-alt-right',
                url:'/user/5',
                name:'ผู้ตรวจจังหวัด'
            },
            {
                icon:'fa-long-arrow-alt-right',
                url:'/user/6',
                name:'ผู้ตรวจกระทรวง/กรม'
            },
            {
                icon:'fa-long-arrow-alt-right',
                url:'/user/7',
                name:'ผู้ตรวจภาคประชาชน'
            },
            {
                icon:'fa-long-arrow-alt-right',
                url:'/user/8',
                name:'นายก/รองนายก'
            },
        ]
    } ,
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
    {
        icon:'fa-shekel-sign',
        url:"/training",
        name:"จัดอบรมหลักสูตร"
    },

]


export const Centraladmin:NavBar[] = [ //แอดมินส่วนกลาง
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
export const Inspector:NavBar[] = [ //ผู้ตรวจ
    {
        icon:'fa-home',
        url:"/main",
        name:"หน้าหลัก"
    },
    {
        icon:'fa-calendar',
        url:"/inspectionplanevent",
        name:"ปฏิทินการตรวจราชการ"
    },
    {
        icon:'fa-book',
        url:"#",
        name:"สมุดตรวจราชการ"
    },
    {
        icon:'fa-hand-point-up',
        url:"#",
        name:"ข้อสั่งการผู้บริหาร"
    },
    {
        icon:'fa-hands',
        url:"#",
        name:"แจ้งคำร้องขอ"
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

export const Provincialgovernor:NavBar[] = [ //ผู้ว่าราชการจังหวัด
    {
        icon:'fa-home',
        url:"/main",
        name:"หน้าหลัก"
    },
    {
        icon:'fa-book',
        url:"#",
        name:"สมุดตรวจราชการ"
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

export const Adminprovince:NavBar[] = [ //แอดมินจังหวัด
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
        url:"#",
        name:"สมุดตรวจราชการ"
    },
    {
        icon:'fa-hand-point-up',
        url:"/executiveorder",
        name:"ข้อสั่งการผู้บริหาร"
    },
    {
        icon:'fa-hands',
        url:"#",
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
    {
        icon:'fa-user-friends',
        url:"/user",
        name:"จัดการผู้ใช้"
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
    {
        icon:'fa-shekel-sign',
        url:"/training",
        name:"จัดอบรมหลักสูตร"
    },

]
// import { children } from './_nav';
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

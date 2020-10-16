import { MenuService } from './../services/menu.service';
import { Component, OnInit } from '@angular/core';


@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})

export class MenuComponent implements OnInit {
  loading = false;
  result: any[] = [];
  dtOptions: DataTables.Settings = {};
  constructor(private menuService: MenuService,) { }

  ngOnInit() {
    this.getlogdata();
  }

  getlogdata(){
    this.menuService.getmenulistdata()
    .subscribe(result => {
      console.log('momomo',result);
      this.result = result;
      this.loading = true
    })
  }

  // openeditModal(template: TemplateRef<any>, id, fiscalYearId, userRegion, UserProvince, ministryId: number, departmentId: number, provincialDepartmentId, SideId,
  //   commandnumber, commandnumberdate, email, prefix, fname, lname, position, phoneNumber, startdate, enddate, img, Autocreateuser,signature,userName) {
    
  //   this.addForm.reset()
  //   this.Autocreateuser = Autocreateuser; // สร้าง UerName เองหรือป่าว
  //   this.id = id;
  //   this.img = img;
  //   this.regionService.getregiondataforuser(fiscalYearId).subscribe(res => {
  //     var uniqueRegion: any = [];
  //     uniqueRegion = res.importFiscalYearRelations.filter(
  //       (thing, i, arr) => arr.findIndex(t => t.regionId === thing.regionId) === i
  //     );
  //     this.selectdataregion = uniqueRegion.map((item, index) => {
  //       return {
  //         value: item.region.id,
  //         label: item.region.name
  //       }
  //     })
  //   })
  //   if (enddate == null) {
  //     this.ed = enddate;
  //   } else {
  //     this.ed = this.time(enddate);
  //   }

  //   if (commandnumberdate == null) {
  //     this.cd = commandnumberdate;
  //   } else {
  //     this.cd = this.time(commandnumberdate);
  //   }

  //   this.addForm.patchValue({

  //     Role_id: this.roleId,
  //     Prefix: prefix,
  //     FName: fname,
  //     LName: lname,
  //     Position: position,
  //     PhoneNumber: phoneNumber,
  //     Email: email,
  //     MinistryId: ministryId,
  //     DepartmentId: departmentId,
  //     FiscalYear: fiscalYearId,
  //     ProvincialDepartmentId: provincialDepartmentId,
  //     UserRegion: userRegion.map(result => {
  //       return result.regionId
  //     }),
  //     UserProvince: this.roleId == 9 ? UserProvince.map(result => {
  //       // console.log("gg", result);

  //       return result.provinceId
  //     }) : UserProvince,

  //     // UserProvince: UserProvince,
  //     files: new FormControl(null, [Validators.required]),
  //     Startdate: this.time(startdate),
  //     Enddate: this.ed,
  //     Commandnumber: commandnumber,
  //     SideId: SideId,
  //     Commandnumberdate: this.cd,
  //     Formprofile: 0,
  //     Img: img,
  //     Signature:signature,
  //     Autocreateuser: Autocreateuser, //แพตข้อมูลว่าสร้าง UerName เองหรือป่าว
  //     UserName: userName
  //   })
  //   this.DepartmentId = departmentId
  //   // console.log(' value: departmentId', departmentId);

  //   this.getDataDepartments({ value: ministryId })
  //   this.getProvincialDepartments({ value: departmentId })
  //   this.MinistryId = ministryId

  //   this.modalRef = this.modalService.show(template);
  // }

}

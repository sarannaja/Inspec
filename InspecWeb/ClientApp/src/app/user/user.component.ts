import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { IOption } from 'ng-select';
import { Router, ActivatedRoute } from '@angular/router';
import { NotificationService } from '../services/Pipe/alert.service';
import { ProvinceService } from '../services/province.service';
import { RegionService } from '../services/region.service';
import { MinistryService } from '../services/ministry.service';
import { UserService } from '../services/user.service'; // ผู้ใช้
import { NgxSpinnerService } from "ngx-spinner";

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {

  modalRef: BsModalRef;
  selectdatarole: Array<IOption>
  selectdataministry: Array<IOption>
  selectdatadeparment: Array<IOption>
  selectdataprovince: Array<IOption>
  selectdataregion: Array<IOption>
  loading = false;
  dtOptions: DataTables.Settings = {};
  roleId:any;
  modelname:any;
  resultuser: any[] = [];
  datarole: any = [
    {
      id: 1,
      name: 'Super Admin'
    },
    {
      id: 2,
      name: 'Admin แผนการตรวจราชการประจำปี'
    },
    {
      id: 3,
      name: 'ผู้ตรวจราชการ'
    },
    {
      id: 4,
      name: 'ผู้ว่าราชการจังหวัด'
    },
    {
      id: 5,
      name: 'ผู้ตรวจจังหวัด'
    },
    {
      id: 6,
      name: 'ผู้ตรวจกระทรวง/ผู้ตรวจกรม/หน่วยงาน'
    },
    {
      id: 7,
      name: 'ผู้ตรวจภาคประชาชน'
    },
    {
      id: 8,
      name: 'นายก/รองนายก'
    },
    {
      id: 9,
      name: 'User Trianning'
    },

  ]
  datadeparment: any = [
    {
      id: 1,
      name: 'กองทัพไทย'
    },
    {
      id: 2,
      name: 'สำนักงานรัฐมนตรีกระทรวงการคลัง'
    },
  ]

  constructor(
    private modalService: BsModalService,
    private router: Router,
    private provinceService: ProvinceService,
    private regionService: RegionService,
    private ministryService: MinistryService,
    private route: ActivatedRoute,
    private userService: UserService,
    private spinner: NgxSpinnerService
  ) {
  this.roleId =  this.route.snapshot.paramMap.get('id') //เลขที่ส่งมาจาก url 
  }

  ngOnInit() {
    console.log(this.roleId);
    
    this.spinner.show();
    this.dtOptions = {
      pagingType: 'full_numbers',
     
    };
    this.getUser()
    this.getDataProvinces()
    this.getDataRegions()
    this.getDataMinistries()
    this.selectdatarole = this.datarole.map((item, index) => {
      return { value: item.id, label: item.name }
    })
    this.selectdatadeparment = this.datadeparment.map((item, index) => {
      return { value: item.id, label: item.name }
    })

  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }

  getUser() {
    this.userService.getuserdata(this.roleId)
      .subscribe(result => {
        //alert(this.roleId);
        this.resultuser = result;
        this.loading = true
        this.spinner.hide();
        console.log(this.resultuser);
      })
  }

  getDataProvinces() {
    this.provinceService.getprovincedata()
      .subscribe(result => {
        this.selectdataprovince = result.map((item, index) => {
          return { value: item.id, label: item.name }
        })

      })
  }

  getDataRegions() {
    this.regionService.getregiondata()
      .subscribe(result => {
        this.selectdataregion = result.map((item, index) => {
          return { value: item.id, label: item.name }
        })

      })
  }

  getDataMinistries() {
    this.ministryService.getministry()
      .subscribe(result => {
        this.selectdataministry = result.map((item, index) => {
          return { value: item.id, label: item.name }
        })
      })
  }
}

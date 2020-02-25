import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { IOption } from 'ng-select';
import { Router } from '@angular/router';
import { NotificationService } from '../services/Pipe/alert.service';
import { ProvinceService } from '../services/province.service';
import { RegionService } from '../services/region.service';
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

  datarole:any = [
    {
      id: 1,
      name: 'Super Admin'
    },
    {
      id: 2,
      name: 'Admin แผนการตรวจราชการประจำปี/นโยบายกลาง'
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
      name: 'ผู้ตรวจกระทรวง'
    },
    {
      id: 7,
      name: 'ผู้ตรวจกรม/หน่วยงาน'
    },
    {
      id: 8,
      name: 'ผู้ตรวจภาคประชาชน'
    },
    {
      id: 9,
      name: 'นายก/รองนายก'
    },
    {
      id: 10,
      name: 'User Trianning'
    },
    
  ]
  dataministry:any = [
    {
      id: 1,
      name: 'กองทัพไทย'
    },
  ]
  datadeparment:any = [
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
    private modalService: BsModalService ,
    private router: Router,
    private provinceService:ProvinceService,
    private regionService : RegionService,
    ) { }

  ngOnInit() {
    this.getDataProvinces()
    this.getDataRegions()
    this.selectdatarole = this.datarole.map((item,index)=>{
      return { value:item.id , label:item.name }
    })
    this.selectdataministry = this.dataministry.map((item,index)=>{
      return { value:item.id , label:item.name }
    })
    this.selectdatadeparment = this.datadeparment.map((item,index)=>{
      return { value:item.id , label:item.name }
    })
    
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }

  getDataProvinces(){
    this.provinceService.getprovincedata()
    .subscribe(result=>{
      //console.log(result);

      this.selectdataprovince = result.map((item,index)=>{
        return { value:item.id , label:item.name }
      })
      
    })
  }

  getDataRegions(){
    this.regionService.getregiondata()
    .subscribe(result=>{
      this.selectdataregion = result.map((item,index)=>{
        return { value:item.id , label:item.name }
      })
      
    })
  }

}

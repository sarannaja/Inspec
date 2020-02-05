import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { IOption } from 'ng-select';

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
  datarole:any = [
    {
      id: 1,
      name: 'Super Admin'
    },
    {
      id: 2,
      name: 'Admin'
    },
  ]
  dataministry:any = [
    {
      id: 1,
      name: 'กระทรวงการคลัง'
    },
    {
      id: 2,
      name: 'กระทรวงกลาโหม'
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
  dataprovince:any = [
    {
      id: 1,
      name: 'กรุงเทพ'
    },
    {
      id: 2,
      name: 'ปทุมธานี'
    },
  ]
  constructor(private modalService: BsModalService) { }

  ngOnInit() {
    this.selectdatarole = this.datarole.map((item,index)=>{
      return { value:item.id , label:item.name }
    })
    this.selectdataministry = this.dataministry.map((item,index)=>{
      return { value:item.id , label:item.name }
    })
    this.selectdatadeparment = this.datadeparment.map((item,index)=>{
      return { value:item.id , label:item.name }
    })
    this.selectdataprovince = this.dataprovince.map((item,index)=>{
      return { value:item.id , label:item.name }
    })
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }
}

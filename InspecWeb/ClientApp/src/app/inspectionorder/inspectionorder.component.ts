import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { InspectionorderService } from '../services/inspectionorder.service';

@Component({
  selector: 'app-inspectionorder',
  templateUrl: './inspectionorder.component.html',
  styleUrls: ['./inspectionorder.component.css']
})
export class InspectionorderComponent implements OnInit {

  resultInspectionorder: any = []
  delid: any
  year: any
  order: any
  name: any
  createBy: any
  modalRef: BsModalRef;
  Form: FormGroup
  
  constructor(private modalService: BsModalService, private fb: FormBuilder, private inspectionorderservice: InspectionorderService,
    public share: InspectionorderService) { }

    ngOnInit() {
      console.log(this.modalRef);
      this.inspectionorderservice.getinspectionorderdata().subscribe(result=>{
      this.resultInspectionorder = result
    })
    
    this.Form = this.fb.group({
      "year": new FormControl(null, [Validators.required]),
      "order": new FormControl(null, [Validators.required]),
      "name": new FormControl(null, [Validators.required]),
      "createBy": new FormControl(null, [Validators.required]),
    })
  }
  openModal(template: TemplateRef<any>, modalType:string = 'edit') {
    modalType != 'edit' ? this.Form.reset() : null;
    this.modalRef = this.modalService.show(template);
    
  }

  onEdit(modaleditInspectionorder,item){
    this.openModal(modaleditInspectionorder)
    this.delid = item.id;
    this.year =item. year;
    this.order =item. order
    this.name =item. name
    this.createBy =item. createBy
    console.log(this.delid);
    console.log(this.year);
    console.log(this.order);
    console.log(this.name);
    console.log(this.createBy);
    this.Form.patchValue(item)

  }
  storeInspectionorder(value) {
    // alert(JSON.stringify(value));
    this.inspectionorderservice.addInspectionorder(value).subscribe(response => {
      console.log(value);
      this.Form.reset()
      this.modalRef.hide()
      this.inspectionorderservice.getinspectionorderdata().subscribe(result => {
        this.resultInspectionorder = result
        console.log(this.resultInspectionorder);
      })
    })
  }
  deleteInspectionorder(value) {
    this.inspectionorderservice.deleteInspectionorder(value).subscribe(response => {
      console.log(value);
      this.modalRef.hide()
      this.inspectionorderservice.getinspectionorderdata().subscribe(result => {
        this.resultInspectionorder = result
        console.log(this.resultInspectionorder);
      })
    })
  }
  editInspectionorder(value,delid) {
    console.clear();
    console.log(value);
    this.inspectionorderservice.editInspectionorder(value,delid).subscribe(response => {
      this.Form.reset()
      this.modalRef.hide()
      this.inspectionorderservice.getinspectionorderdata().subscribe(result => {
        this.resultInspectionorder = result
       
      })
    })
  }

}

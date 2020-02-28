import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { InspectorService } from '../services/inspector.service';

@Component({
  selector: 'app-inspector',
  templateUrl: './inspector.component.html',
  styleUrls: ['./inspector.component.css']
})
export class InspectorComponent implements OnInit {

  resultInspector: any = []
  delid: any
  name: any
  phonenumber: any
  regionId: any
  createBy: any
  modalRef: BsModalRef;
  Form: FormGroup
  
  constructor(private modalService: BsModalService, private fb: FormBuilder, private inspectorservice: InspectorService,
    public share: InspectorService) { }

    ngOnInit() {
      console.log(this.modalRef);
      this.inspectorservice.getinspectordata().subscribe(result=>{
      this.resultInspector = result
    })
    
    this.Form = this.fb.group({
      "name": new FormControl(null, [Validators.required]),
      "phonenumber": new FormControl(null, [Validators.required]),
      "regionId": new FormControl(null, [Validators.required]),
      "createBy": new FormControl(null, [Validators.required]),
    })
  }
  openModal(template: TemplateRef<any>, modalType:string = 'edit') {
    modalType != 'edit' ? this.Form.reset() : null;
    this.modalRef = this.modalService.show(template);
    
  }

  onEdit(modaleditInspector,item){
    this.openModal(modaleditInspector)
    this.delid = item.id;
    this.name =item.name;
    this.phonenumber =item.phonenumber
    this.regionId =item.regionId
    this.createBy =item.createBy
    console.log(this.delid);
    console.log(this.name);
    console.log(this.phonenumber);
    console.log(this.regionId);
    console.log(this.createBy);
    this.Form.patchValue(item)

  }
  storeInspector(value) {
    // alert(JSON.stringify(value));
    this.inspectorservice.addInspector(value).subscribe(response => {
      console.log(value);
      this.Form.reset()
      this.modalRef.hide()
      this.inspectorservice.getinspectordata().subscribe(result => {
        this.resultInspector = result
        console.log(this.resultInspector);
      })
    })
  }
  deleteInspecor(value) {
    this.inspectorservice.deleteInspector(value).subscribe(response => {
      console.log(value);
      this.modalRef.hide()
      this.inspectorservice.getinspectordata().subscribe(result => {
        this.resultInspector = result
        console.log(this.resultInspector);
      })
    })
  }
  editInspector(value,delid) {
    console.clear();
    console.log(value);
    this.inspectorservice.editInspector(value,delid).subscribe(response => {
      this.Form.reset()
      this.modalRef.hide()
      this.inspectorservice.getinspectordata().subscribe(result => {
        this.resultInspector = result
       
      })
    })
  }

}

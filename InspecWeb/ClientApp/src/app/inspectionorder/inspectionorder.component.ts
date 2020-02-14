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
  name: any
  modalRef: BsModalRef;
  Form: FormGroup
  forbiddenUsernames = ['admin', 'test', 'xxxx'];
  constructor(private modalService: BsModalService, private fb: FormBuilder, private inspectionorderservice: InspectionorderService,
    public share: InspectionorderService) { }

  ngOnInit() {
    this.Form = this.fb.group({
      "provincename": new FormControl(null, [Validators.required]),
      // "test" : new FormControl(null,[Validators.required,this.forbiddenNames.bind(this)])
    })

    //แก้ไข
    this.Form.patchValue({
      // test: "testest"
    })

    this.inspectionorderservice.getinspectionorderdata().subscribe(result => {
      this.resultInspectionorder = result
      console.log(this.resultInspectionorder);
    })
  }
  openModal(template: TemplateRef<any>, id, name) {
    this.delid = id;
    this.name = name;
    console.log(this.delid);
    console.log(this.name);

    this.modalRef = this.modalService.show(template);
  }
  forbiddenNames(control: FormControl): { [s: string]: boolean } {
    if (this.forbiddenUsernames.indexOf(control.value) !== -1) {
      return { 'forbiddenNames': true };
    }
    return null;
  }

  storeProvince(value) {
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
  deleteProvince(value) {
    this.inspectionorderservice.deleteInspectionorder(value).subscribe(response => {
      console.log(value);
      this.modalRef.hide()
      this.inspectionorderservice.getinspectionorderdata().subscribe(result => {
        this.resultInspectionorder = result
        console.log(this.resultInspectionorder);
      })
    })
  }
  editProvince(value,delid) {
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

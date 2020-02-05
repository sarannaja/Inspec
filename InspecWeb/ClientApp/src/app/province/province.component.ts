import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-province',
  templateUrl: './province.component.html',
  styleUrls: ['./province.component.css']
})
export class ProvinceComponent implements OnInit {

  modalRef: BsModalRef;
  Form: FormGroup
  forbiddenUsernames = ['admin', 'test', 'xxxx'];
  constructor(private modalService: BsModalService, private fb: FormBuilder) { }

  ngOnInit() {
    this.Form = this.fb.group({
      "provincename" : new FormControl(null,[Validators.required]),
      "test" : new FormControl(null,[Validators.required,this.forbiddenNames.bind(this)])
    })
    
    //แก้ไข
    this.Form.patchValue({
      test: "testest"
    })
  }
  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }
  forbiddenNames(control: FormControl): { [s: string]: boolean } {
    if (this.forbiddenUsernames.indexOf(control.value) !== -1) {
      return { 'forbiddenNames': true };
    }
    return null;
  }
  storeProvince(value){
    console.log(value);
    this.Form.reset()
    this.modalRef.hide()
  }
}

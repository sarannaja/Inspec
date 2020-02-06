import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { ProvinceService } from '../services/province.service';

@Component({
  selector: 'app-province',
  templateUrl: './province.component.html',
  styleUrls: ['./province.component.css']
})
export class ProvinceComponent implements OnInit {

  resultprovince: any = []
  delid: any
  modalRef: BsModalRef;
  Form: FormGroup
  forbiddenUsernames = ['admin', 'test', 'xxxx'];
  constructor(private modalService: BsModalService, private fb: FormBuilder, private provinceservice: ProvinceService,
    public share: ProvinceService) { }

  ngOnInit() {
    this.Form = this.fb.group({
      "provincename": new FormControl(null, [Validators.required]),
      // "test" : new FormControl(null,[Validators.required,this.forbiddenNames.bind(this)])
    })

    //แก้ไข
    this.Form.patchValue({
      // test: "testest"
    })

    this.provinceservice.getprovincedata().subscribe(result => {
      this.resultprovince = result
      console.log(this.resultprovince);

    })
  }
  openModal(template: TemplateRef<any>, id) {
    this.delid = id;
    console.log(id);
    
    this.modalRef = this.modalService.show(template);
  }
  forbiddenNames(control: FormControl): { [s: string]: boolean } {
    if (this.forbiddenUsernames.indexOf(control.value) !== -1) {
      return { 'forbiddenNames': true };
    }
    return null;
  }

  storeProvince(value) {
    this.provinceservice.addProvince(value).subscribe(response => {
      console.log(value);
      this.Form.reset()
      this.modalRef.hide()
      this.provinceservice.getprovincedata().subscribe(result => {
        this.resultprovince = result
        console.log(this.resultprovince);
      })
    })
  }
  deleteProvince(value) {
    this.provinceservice.deleteProvince(value).subscribe(response => {
      console.log(value);
      this.modalRef.hide()
      this.provinceservice.getprovincedata().subscribe(result => {
        this.resultprovince = result
        console.log(this.resultprovince);
      })
    })
  }

}

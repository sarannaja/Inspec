import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { ProvinceService } from '../services/province.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-province',
  templateUrl: './province.component.html',
  styleUrls: ['./province.component.css']

})
export class ProvinceComponent implements OnInit {

  resultprovince: any[] = []
  delid: any
  name: any
  link: any
  modalRef: BsModalRef;
  Form: FormGroup;
  EditForm: FormGroup;
  dtOptions: DataTables.Settings = {};
  forbiddenUsernames = ['admin', 'test', 'xxxx'];
  constructor(private modalService: BsModalService, private fb: FormBuilder, private provinceservice: ProvinceService,
    public share: ProvinceService, private router: Router) { }

  ngOnInit() {
    this.dtOptions = {
      pagingType: 'full_numbers',
      columnDefs: [
        {
          targets: [4],
          orderable: false
        }
      ]
     
    };

    this.Form = this.fb.group({
      "provincename": new FormControl(null, [Validators.required]),
      "provincelink": new FormControl(null, [Validators.required])
      // "test" : new FormControl(null,[Validators.required,this.forbiddenNames.bind(this)])
    })

    //แก้ไข


    this.provinceservice.getprovincedata()
      .subscribe(result => {
        this.resultprovince = result
        console.log(this.resultprovince);
      })
    this.Form.patchValue({
      // test: "testest"
    })
  }

  District(id) {
    this.router.navigate(['/district', id])
  }

  openModal(template: TemplateRef<any>, id, name, link) {
    this.delid = id;
    this.name = name;
    this.link = link
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
  editModal(template: TemplateRef<any>, id, name, link) {
    this.delid = id;
    this.name = name;
    this.link = link
    console.log(this.delid);
    console.log(this.name);

    this.modalRef = this.modalService.show(template);
    this.EditForm = this.fb.group({
      "provincename": new FormControl(null, [Validators.required]),
      "provincelink": new FormControl(null, [Validators.required])
      // "test" : new FormControl(null,[Validators.required,this.forbiddenNames.bind(this)])
    })
    this.EditForm.patchValue({
      "provincename": name,
      "provincelink": link
    })
  }
  editProvince(value, delid) {
    console.clear();
    console.log("kkkk" + JSON.stringify(value));
    this.provinceservice.editProvince(value, delid).subscribe(response => {
      this.Form.reset()
      this.modalRef.hide()
      this.provinceservice.getprovincedata().subscribe(result => {
        this.resultprovince = result
      })
    })
  }
}

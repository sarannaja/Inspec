import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { InformationoperationService } from '../services/informationoperation.service';

@Component({
  selector: 'app-informationoperation',
  templateUrl: './informationoperation.component.html',
  styleUrls: ['./informationoperation.component.css']
})
export class InformationoperationComponent implements OnInit {
  resultinformationoperation: any = []
  delid:any
  location:any
  name: any
  detail: any
  tel: any
  province: any
  district: any
  modalRef:BsModalRef;
  Form : FormGroup
  files: string[] = []
  loading = false;
  dtOptions: DataTables.Settings = {};

  constructor(private modalService: BsModalService, 
    private fb: FormBuilder, 
    private informationoperationservice: InformationoperationService,
    public share: InformationoperationService) { }

  ngOnInit() {
    this.dtOptions = {
      pagingType: 'full_numbers',
      columnDefs: [
        {
          targets: [5],
          orderable: false
        }
      ]

    };
    this.informationoperationservice.getinformationoperation().subscribe(result=>{
    this.resultinformationoperation = result
    this.loading = true
    })
    this.Form = this.fb.group({
      location: new FormControl(null, [Validators.required]),
      name: new FormControl(null, [Validators.required]),
      detail: new FormControl(null, [Validators.required]),
      tel: new FormControl(null, [Validators.required]),
      province: new FormControl(null, [Validators.required]),
      district: new FormControl(null, [Validators.required]),
      files : new FormControl(null, [Validators.required])
    })
  }
  openModal(template: TemplateRef<any>=null, id=null, location=null, name=null, detail=null, tel=null, province=null, district=null) {
    this.delid = id;
    this.location = location;
    this.name = name;
    this.detail = detail;
    this.tel = tel;
    this.province = province;
    this.district = district;
    this.modalRef = this.modalService.show(template);
  }
  uploadFile(event) {
    const file = (event.target as HTMLInputElement).files;
    this.Form.patchValue({
      files: file
    });
    this.Form.get('files').updateValueAndValidity()

  }

  storeInformationoperation(value) {
    // alert(JSON.stringify(value));
    this.informationoperationservice.addInformationoperation(value, this.Form.value.files).subscribe(response => {
      console.log(value);
      this.Form.reset()
      this.modalRef.hide()
      this.informationoperationservice.getinformationoperation().subscribe(result => {
        this.resultinformationoperation = result
        console.log(this.resultinformationoperation);
      })
    })
  }
  deleteInformationoperation(value) {
    this.informationoperationservice.deleteInformationoperation(value).subscribe(response => {
      console.log(value);
      this.modalRef.hide()
      this.informationoperationservice.getinformationoperation().subscribe(result => {
        this.resultinformationoperation = result
        console.log(this.resultinformationoperation);
      })
    })
  }
  editInformationoperation(value,delid) {
    console.clear();
    console.log(value);
    this.informationoperationservice.editInformationoperation(value,delid).subscribe(response => {
      this.Form.reset()
      this.modalRef.hide()
      this.informationoperationservice.getinformationoperation().subscribe(result => {
        this.resultinformationoperation = result
       
      })
    })
  }

}

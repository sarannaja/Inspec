import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { SubdistrictService } from '../services/subdistrict.service';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-subdistrict',
  templateUrl: './subdistrict.component.html',
  styleUrls: ['./subdistrict.component.css']
})
export class SubdistrictComponent implements OnInit {

  resultsubdistrict: any = []
  id;
  titleprovince: [];
  titledistrict: [];
  Form: FormGroup;
  modalRef: BsModalRef;
  subdistrict_id:any;
  loading = false;
  dtOptions: DataTables.Settings = {};
  province_id:any;


  constructor(
    private modalService: BsModalService,
    private fb: FormBuilder,
    private subdistrictservice: SubdistrictService,
    private activatedRoute: ActivatedRoute,
    private spinner: NgxSpinnerService,
    private router:Router,
    public share: SubdistrictService) {
    this.id = activatedRoute.snapshot.paramMap.get('id')
  }

  ngOnInit() {
    this.form();
    this.getsubdistrict();
  }
  getsubdistrict(){
    this.spinner.show();
    this.subdistrictservice.getsubdistrictdata(this.id).subscribe(result => {
      this.resultsubdistrict = result
      this.titleprovince = result[0].district.province.name
      this.province_id = result[0].district.province.id
      this.titledistrict = result[0].district.name
      this.loading = true;
      this.spinner.hide();
    
    })
  }

  openModal(template: TemplateRef<any>, id,name) {
    this.Form.reset()
    this.subdistrict_id = id;//ID สำหรับลบ
    this.Form.patchValue({
      Name: name,
    })
    this.modalRef = this.modalService.show(template);
  }
  storeSubdistrict(value){
    this.subdistrictservice.addsubdistrict(value,this.id).subscribe(response => {
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false
      this.getsubdistrict()
    })
  }

  updateSubdistrict(value,id){
    this.subdistrictservice.editsupdistrict(value,id).subscribe(response => {
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false
      this.getsubdistrict()
    })
  }

  delete(value) {
    this.subdistrictservice.deletesupdistrict(value).subscribe(response => {
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false
      this.getsubdistrict()
    })
  }

  form() {
    this.Form = this.fb.group({
      Name: new FormControl(null, [Validators.required]),
    })
  }

}

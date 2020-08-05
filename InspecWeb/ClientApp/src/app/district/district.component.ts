import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { DistrictService } from '../services/district.service';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from "ngx-spinner";
@Component({
  selector: 'app-district',
  templateUrl: './district.component.html',
  styleUrls: ['./district.component.css']
})
export class DistrictComponent implements OnInit {

  resultdistrict: any = []
  id
  name: any
  titleprovince:[]
  modalRef: BsModalRef;
  Form: FormGroup;
  loading = false;
  district_id : any;
  dtOptions: DataTables.Settings = {};

  constructor(
    private modalService: BsModalService,
    private fb: FormBuilder,
    private districtservice: DistrictService,
    private activatedRoute : ActivatedRoute,
    private router:Router,
    private spinner: NgxSpinnerService,
    public share: DistrictService) {
    this.id = activatedRoute.snapshot.paramMap.get('id')
    this.name = activatedRoute.snapshot.paramMap.get('name')
  }

  ngOnInit() {
    this.getdistrict();
    this.form();
  }

  getdistrict(){
    this.spinner.show();
    this.districtservice.getdistrictdata(this.id).subscribe(result => {
      this.resultdistrict = result
      this.titleprovince = result[0].province.name
      this.loading = true;
      this.spinner.hide();
      //console.log(this.resultdistrict);
    })
  }

  Subdistrict(id){
    this.router.navigate(['/subdistrict',id])
  }

  openModal(template: TemplateRef<any>, id,name) {
    this.Form.reset()
    this.district_id = id;//ID สำหรับลบ
    this.Form.patchValue({
      Name: name,
    })
    this.modalRef = this.modalService.show(template);
  }
  storeDistrict(value){
    this.districtservice.adddistrict(value,this.id).subscribe(response => {
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false
      this.getdistrict()
    })
  }

  updateDistrict(value,id){
    this.districtservice.editdistrict(value,id).subscribe(response => {
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false
      this.getdistrict()
    })
  }

  delete(value) {
    this.districtservice.deletedistrict(value).subscribe(response => {
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false
      this.getdistrict()
    })
  }

  form() {
    this.Form = this.fb.group({
      Name: new FormControl(null, [Validators.required]),
    })
  }

}

import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
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
  modalRef:BsModalRef;
  Form : FormGroup
  files: string[] = []
  loading = false;
  dtOptions: DataTables.Settings = {};

  constructor(private modalService: BsModalService, private fb: FormBuilder, private inspectionorderservice: InspectionorderService,
    public share: InspectionorderService) { }

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
    this.inspectionorderservice.getinspectionorder().subscribe(result=>{
    this.resultInspectionorder = result
    this.loading = true
    })
    this.Form = this.fb.group({
      name: new FormControl(null, [Validators.required]),
      year: new FormControl(null, [Validators.required]),
      order: new FormControl(null, [Validators.required]),
      createBy: new FormControl(null, [Validators.required]),
      files : new FormControl(null, [Validators.required])
    })
  }
  openModal(template: TemplateRef<any>, id, year,order,name,createBy) {
    this.delid = id;
    this.name = name;
    this.year = year;
    this.order = order;
    this.createBy = createBy;
    console.log(this.delid);
    console.log(this.name);
    console.log(this.year);
    console.log(this.order);
    console.log(this.createBy);
    
    this.modalRef = this.modalService.show(template);
  }
  uploadFile(event) {
    const file = (event.target as HTMLInputElement).files;
    this.Form.patchValue({
      files: file
    });
    this.Form.get('files').updateValueAndValidity()

  }

  storeInspectionorder(value) {
    // alert(JSON.stringify(value));
    this.inspectionorderservice.addInspectionorder(value, this.Form.value.files).subscribe(response => {
      console.log(value);
      this.Form.reset()
      this.modalRef.hide()
      this.inspectionorderservice.getinspectionorder().subscribe(result => {
        this.resultInspectionorder = result
        console.log(this.resultInspectionorder);
      })
    })
  }
  deleteInspectionorder(value) {
    this.inspectionorderservice.deleteInspectionorder(value).subscribe(response => {
      console.log(value);
      this.modalRef.hide()
      this.inspectionorderservice.getinspectionorder().subscribe(result => {
        this.resultInspectionorder = result
        console.log(this.resultInspectionorder);
      })
    })
  }
  editInspectionorder(value,delid) {
    console.log(value);
    this.inspectionorderservice.editInspectionorder(value,delid).subscribe(response => {
      this.Form.reset()
      this.modalRef.hide()
      this.inspectionorderservice.getinspectionorder().subscribe(result => {
        this.resultInspectionorder = result
       
      })
    })
  }

}


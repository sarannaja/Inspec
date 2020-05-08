import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { GorvermentinspectionplanService } from '../services/governmentinspectionplan.service';

@Component({
  selector: 'app-governmentinspectionplan',
  templateUrl: './governmentinspectionplan.component.html',
  styleUrls: ['./governmentinspectionplan.component.css']
})
export class GovernmentinspectionplanComponent implements OnInit {
  resultgovernmentinspectionplan: any = []
  delid:any
  year:any
  title:any
  modalRef:BsModalRef;
  Form : FormGroup
  files: string[] = []
  loading = false;
  dtOptions: DataTables.Settings = {};

  constructor(private modalService: BsModalService, private fb: FormBuilder, private governmentinspectionplanservice: GorvermentinspectionplanService,
    public share: GorvermentinspectionplanService) { }

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
    this.governmentinspectionplanservice.getgovernmentinspectionplan().subscribe(result=>{
    this.resultgovernmentinspectionplan = result
    this.loading = true
    })
    this.Form = this.fb.group({
      year: new FormControl(null, [Validators.required]),
      title: new FormControl(null, [Validators.required]),
      files : new FormControl(null, [Validators.required])
    })
  }
  openModal(template: TemplateRef<any>, id, year,title) {
    this.delid = id;
    this.year = year;
    this.title = title
    console.log(this.delid);
    console.log(this.year);
    console.log(this.title);
    
    this.modalRef = this.modalService.show(template);
  }
  uploadFile(event) {
    const file = (event.target as HTMLInputElement).files;
    this.Form.patchValue({
      files: file
    });
    this.Form.get('files').updateValueAndValidity()

  }

  storeGovernmentinspectionplan(value) {
    // alert(JSON.stringify(value));
    this.governmentinspectionplanservice.addGovernmentinspectionplan(value, this.Form.value.files).subscribe(response => {
      console.log(value);
      this.Form.reset()
      this.modalRef.hide()
      this.governmentinspectionplanservice.getgovernmentinspectionplan().subscribe(result => {
        this.resultgovernmentinspectionplan = result
        console.log(this.resultgovernmentinspectionplan);
      })
    })
  }
  deleteGovernmentinspectionplan(value) {
    this.governmentinspectionplanservice.deleteGovernmentinspectionplan(value).subscribe(response => {
      console.log(value);
      this.modalRef.hide()
      this.governmentinspectionplanservice.getgovernmentinspectionplan().subscribe(result => {
        this.resultgovernmentinspectionplan = result
        console.log(this.resultgovernmentinspectionplan);
      })
    })
  }
  editGovernmentinspectionplan(value,delid) {
    console.clear();
    console.log(value);
    this.governmentinspectionplanservice.editGovernmentinspectionplan(value,delid).subscribe(response => {
      this.Form.reset()
      this.modalRef.hide()
      this.governmentinspectionplanservice.getgovernmentinspectionplan().subscribe(result => {
        this.resultgovernmentinspectionplan = result
       
      })
    })
  }

}

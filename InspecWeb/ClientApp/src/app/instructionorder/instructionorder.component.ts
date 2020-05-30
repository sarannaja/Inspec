import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { InstructionorderService } from '../services/instructionorder.service';

@Component({
  selector: 'app-instructionorder',
  templateUrl: './instructionorder.component.html',
  styleUrls: ['./instructionorder.component.css']
})
export class InstructionorderComponent implements OnInit {
  resultInstructionorder: any = []
  delid: any
  year: any
  order: any
  name: any
  createBy: any
  detail: any
  modalRef:BsModalRef;
  Form : FormGroup
  files: string[] = []
  loading = false;
  dtOptions: DataTables.Settings = {};

  constructor(private modalService: BsModalService, private fb: FormBuilder, private instructionorderservice: InstructionorderService,
    public share: InstructionorderService) { }

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
    this.instructionorderservice.getinstructionorder().subscribe(result=>{
    this.resultInstructionorder = result
    this.loading = true
    })
    this.Form = this.fb.group({
      name: new FormControl(null, [Validators.required]),
      year: new FormControl(null, [Validators.required]),
      order: new FormControl(null, [Validators.required]),
      createBy: new FormControl(null, [Validators.required]),
      detail: new FormControl(null, [Validators.required]),
      files : new FormControl(null, [Validators.required])
    })
  }
  openModal(template: TemplateRef<any>, id, year,order,name,createBy,detail) {
    this.delid = id;
    this.name = name;
    this.year = year;
    this.order = order;
    this.createBy = createBy;
    this.detail = detail;
    console.log(this.delid);
    console.log(this.name);
    console.log(this.year);
    console.log(this.order);
    console.log(this.createBy);
    console.log(this.detail);
    
    this.modalRef = this.modalService.show(template);
  }
  uploadFile(event) {
    const file = (event.target as HTMLInputElement).files;
    this.Form.patchValue({
      files: file
    });
    this.Form.get('files').updateValueAndValidity()

  }

  storeInstructionorder(value) {
    // alert(JSON.stringify(value));
    this.instructionorderservice.addInstructionorder(value, this.Form.value.files).subscribe(response => {
      console.log(value);
      this.Form.reset()
      this.modalRef.hide()
      this.instructionorderservice.getinstructionorder().subscribe(result => {
        this.resultInstructionorder = result
        console.log(this.resultInstructionorder);
      })
    })
  }
  deleteInstructionorder(value) {
    this.instructionorderservice.deleteInstructionorder(value).subscribe(response => {
      console.log(value);
      this.modalRef.hide()
      this.instructionorderservice.getinstructionorder().subscribe(result => {
        this.resultInstructionorder = result
        console.log(this.resultInstructionorder);
      })
    })
  }
  editInstructionorder(value,delid) {
    console.log(value);
    this.instructionorderservice.editInstructionorder(value,delid).subscribe(response => {
      this.Form.reset()
      this.modalRef.hide()
      this.instructionorderservice.getinstructionorder().subscribe(result => {
        this.resultInstructionorder = result
       
      })
    })
  }

}


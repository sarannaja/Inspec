import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { InstructionorderService } from '../services/instructionorder.service';
import { NotofyService } from '../services/notofy.service';
import { NgxSpinnerService } from 'ngx-spinner';

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
  filename :any;
  submitted = false;

  constructor(private modalService: BsModalService, 
    private fb: FormBuilder, 
    private instructionorderservice: InstructionorderService,
    public share: InstructionorderService,
    private _NotofyService: NotofyService,
    private spinner: NgxSpinnerService,
    ) { }

  ngOnInit() {
    this.getdata();
    this.dtOptions = {
      pagingType: 'full_numbers',
      "language": {
        "lengthMenu": "แสดง  _MENU_  รายการ",
        "search": "ค้นหา:",
        "info": "แสดง _START_ ถึง _END_ จาก _TOTAL_ แถว",
        "infoEmpty": "แสดง 0 ของ 0 รายการ",
        "zeroRecords": "ไม่พบข้อมูล",
        "paginate": {
          "first": "หน้าแรก",
          "last": "หน้าสุดท้าย",
          "next": "ต่อไป",
          "previous": "ย้อนกลับ"
        },
      }

    };
    this.Form = this.fb.group({
      name: new FormControl(null, [Validators.required]),
      year: new FormControl(null, [Validators.required]),
      order: new FormControl(null, [Validators.required]),
      createBy: new FormControl(null, [Validators.required]),
      detail: new FormControl(null, [Validators.required]),
      files : new FormControl(null)
    })
  }
  getdata(){
    this.instructionorderservice.getinstructionorder().subscribe(result=>{
      this.resultInstructionorder = result
      this.loading = true
      })
  }
  openModal(template: TemplateRef<any>, id, year,order,name,createBy,detail,filename) {
    this.Form.reset()
    this.submitted = false;
    this.delid = id;
    this.filename = filename;
    this.Form.patchValue({
    name : name,
    year : year,
    order : order,
    detail : detail,
    createBy : createBy
    });
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

    this.submitted = true;
    if (this.Form.invalid) {
        return;
    }
    
    this.instructionorderservice.addInstructionorder(value, this.Form.value.files).subscribe(response => {
      this.Form.reset()
      this.loading = false;
      this.getdata();
      this.modalRef.hide()
      this._NotofyService.onSuccess("เพิ่มข้อมูล")
    })
  }
  deleteInstructionorder(value) {
    this.instructionorderservice.deleteInstructionorder(value).subscribe(response => {
      this.loading = false;
      this.getdata();
      this.modalRef.hide()
      this._NotofyService.onSuccess("ลบข้อมูล")
    })
  }
  editInstructionorder(value,delid) {
    this.submitted = true;
    if (this.Form.invalid) {
        return;
    }
    this.instructionorderservice.editInstructionorder(value,this.Form.value.files,delid,this.filename).subscribe(response => {
      this.Form.reset()
      this.loading = false;
      this.getdata();
      this.modalRef.hide()
      this._NotofyService.onSuccess("แก้ไขข้อมูล")
    })
  }
  get f() { return this.Form.controls; }
}


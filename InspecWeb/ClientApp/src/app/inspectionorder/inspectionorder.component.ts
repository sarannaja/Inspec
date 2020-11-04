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
      files : new FormControl(null)
    })
  }

  getdata(){
    this.inspectionorderservice.getinspectionorder().subscribe(result=>{
      this.resultInspectionorder = result
      this.loading = true
      })
  }
  openModal(template: TemplateRef<any>, id, year,order,name,createBy,filename) {

    this.delid = id;
    this.Form.patchValue({
    name : name,
    year : year,
    order : order,
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

  storeInspectionorder(value) {
    this.inspectionorderservice.addInspectionorder(value, this.Form.value.files).subscribe(response => {
      this.Form.reset()
      this.getdata();
      this.modalRef.hide()
    })
  }
  deleteInspectionorder(value) {
    this.inspectionorderservice.deleteInspectionorder(value).subscribe(response => {
      this.getdata();
      this.modalRef.hide()
    })
  }
  editInspectionorder(value,delid) {  
    alert(1)
    this.inspectionorderservice.editInspectionorder(value,this.Form.value.files,delid).subscribe(response => {
      alert(3)
      this.Form.reset()
      this.getdata();
      this.modalRef.hide()
    })
  }

}


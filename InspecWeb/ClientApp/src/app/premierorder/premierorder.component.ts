import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { PremierorderService } from '../services/premierorder.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { UserService } from '../services/user.service';
import { LogService } from '../services/log.service';

@Component({
  selector: 'app-premierorder',
  templateUrl: './premierorder.component.html',
  styleUrls: ['./premierorder.component.css']
})
export class PremierorderComponent implements OnInit {
  resultpremierorder: any = []
  delid: any
  year: any
  title: any
  modalRef:BsModalRef;
  Form : FormGroup
  files: string[] = []
  loading = false;
  submitted = false;
  dtOptions: DataTables.Settings = {};
  userid :any;
  role_id :any

  constructor(
    private modalService: BsModalService,
    private fb: FormBuilder,
    private premierorderservice: PremierorderService,
    public share: PremierorderService,
    private spinner: NgxSpinnerService,
    private authorize: AuthorizeService,
    private userService: UserService,
    private logService: LogService,
    ) { }

  ngOnInit() {
    this.spinner.show();
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
      },
    };
    this.getdata()
    this.Form = this.fb.group({
      title: new FormControl(null, [Validators.required]),
      year: new FormControl(null, [Validators.required]),
      files : new FormControl(null, [Validators.required])
    })
  }

  getdata(){
    this.authorize.getUser()
    .subscribe(result => {
      this.userid = result.sub
      this.userService.getuserfirstdata(this.userid)
        .subscribe(result => {
        this.role_id = result[0].role_id
      })
    })
    this.premierorderservice.getpremierorder().subscribe(result=>{
      this.resultpremierorder = result
      this.loading = true
      this.spinner.hide();
      
      })
  }
  openModal(template: TemplateRef<any>=null, id=null, year=null,title=null) {
    this.delid = id;
    this.title = title;
    this.year = year;    
    this.modalRef = this.modalService.show(template);
  }
  uploadFile(event) {
    const file = (event.target as HTMLInputElement).files;
    this.Form.patchValue({
      files: file
    });
    this.Form.get('files').updateValueAndValidity()

  }

  storePremierorder(value) {
    this.premierorderservice.addPremierorder(value, this.Form.value.files).subscribe(response => {
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false
      this.getdata()
    })
  }
  deletePremierorder(value) {
    this.premierorderservice.deletePremierorder(value).subscribe(response => {
      this.modalRef.hide()
      this.loading = false
      this.getdata()
    })
  }
  editPremierorder(value,delid) {
    this.premierorderservice.editPremierorder(value,delid).subscribe(response => {
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false
      this.getdata()
    })
  }

}


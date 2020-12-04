import { Component, OnInit, TemplateRef, Inject } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, FormControl, Validators} from '@angular/forms';
import { FiscalyearService } from '../services/fiscalyear.service';
import { CircularLetterService } from '../services/circular-letter.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { AuthorizeService } from 'src/api-authorization-new/authorize.service';
import { UserService } from '../services/user.service';
import { LogService } from '../services/log.service';
import { NotofyService } from '../services/notofy.service';


@Component({
  selector: 'app-circularletter',
  templateUrl: './circularletter.component.html',
  styleUrls: ['./circularletter.component.css']
})
export class CircularletterComponent implements OnInit {
  files: string[] = []
  data: any = []
  id:any;
  role_id:any;
  modalRef:BsModalRef;
  Form : FormGroup;
  loading = false;
  dtOptions: any = {};
  fileUrl: any;
  filename:any;
  submitted = false;
  title:any;
  userid:any;

  constructor(
    private authorize: AuthorizeService,
    private modalService: BsModalService, 
    private fb: FormBuilder, 
    private fiscalyearService: FiscalyearService,
    private circularletterservice: CircularLetterService,
    private userService: UserService,
    private spinner: NgxSpinnerService,
    private logService: LogService,
    private _NotofyService: NotofyService,
    @Inject('BASE_URL') baseUrl: string,
    ) { 
      this.fileUrl = baseUrl + '/circularletters';
    }

  ngOnInit() {
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
    this.getdata();
    this.form();
  }
  getdata(){
    this.spinner.show();
    //<!-- เช็ค role  -->
    this.authorize.getUser()
    .subscribe(result => {
      this.userid = result.sub
      this.userService.getuserfirstdata(result.sub)
          .subscribe(result => {
            this.role_id = result[0].role_id
          })
    })
    //<!-- END เช็ค role -->

    this.circularletterservice.getdata()
    .subscribe(result => {
      this.data = result;
      this.loading = true
      this.spinner.hide();
      console.log("data", this.data);
    })
  }

  form(){
    this.Form = this.fb.group({ 
      Title: new FormControl(null, [Validators.required]),
      files: new FormControl(null),
    })
  }

  openModal(template: TemplateRef<any>,id,title,filename) {
    this.Form.reset()
    this.submitted = false;
    this.id = id;
    this.title = title;
    this.filename = filename;
    this.modalRef = this.modalService.show(template);
    this.Form.patchValue({
      Title: title
    })
  }
  uploadFile(event) {
    const file = (event.target as HTMLInputElement).files;
    this.Form.patchValue({
      files: file
    });
    this.Form.get('files').updateValueAndValidity()

  }

  store(value){
    this.submitted = true;
    if (this.Form.invalid) {
        return;
    }
    this.circularletterservice.store(value, this.Form.value.files)
    .subscribe(result => {
      this.logService.addLog(this.userid,'Circularletters','เพิ่ม',result.title,result.id).subscribe();
      this.Form.reset();
      this.loading = false;
      this.getdata();
      this._NotofyService.onSuccess("เพิ่มข้อมูล")
      this.modalRef.hide();
    })
  }

  update(value,id){
    this.submitted = true;
    if (this.Form.invalid) {
        return;
    }
    this.circularletterservice.update(value, this.Form.value.files,id)
    .subscribe(result => {
      this.logService.addLog(this.userid,'Circularletters','แก้ไข',result.title,result.id).subscribe();
      this.Form.reset();
      this.loading = false;
      this.getdata();
      this._NotofyService.onSuccess("แก้ไขข้อมูล")
      this.modalRef.hide();
    })
  }

  destroy(id){
    this.circularletterservice.destroy(id)
    .subscribe(result => {
      this.logService.addLog(this.userid,'Circularletters','ลบ',this.title,this.id).subscribe();
      this.Form.reset();
      this.loading = false;
      this.getdata();
      this._NotofyService.onSuccess("ลบข้อมูล")
      this.modalRef.hide();
    })
  }
  get f() { return this.Form.controls; }
}

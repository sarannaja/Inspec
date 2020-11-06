import { Component, OnInit, TemplateRef, Inject } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { FiscalyearService } from '../services/fiscalyear.service';
import { Router } from '@angular/router';
import { IMyOptions } from 'mydatepicker-th';
import { NgxSpinnerService } from "ngx-spinner";
import { NotofyService } from '../services/notofy.service';
import { LogService } from '../services/log.service';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-governmentinspectionarea',
  templateUrl: './governmentinspectionarea.component.html',
  styleUrls: ['./governmentinspectionarea.component.css']
})
export class GovernmentinspectionareaComponent implements OnInit {

  resultfiscalyear: any[] = []
  fileset:any[] = []
  delid: any
  year: any
  startdate: any
  enddate: any
  orderdate: any
  modalRef: BsModalRef;
  Form: FormGroup
  loading = false;
  ed: any;
  sd: any;
  od:any;
  fileUrl :any;
  submitted = false;
  userid:any;
  role_id:any;
  dtOptions: DataTables.Settings = {};
  public myDatePickerOptions: IMyOptions = {
    // other options...
    dateFormat: 'dd/mm/yyyy',
    showTodayBtn: true
  };
  private myDatePickerOptionsyear: IMyOptions = {
    // other options...
    dateFormat: 'YYYY',
    
  };

  constructor(
    private modalService: BsModalService, 
    private fb: FormBuilder, 
    private fiscalyearservice: FiscalyearService,
    public share: FiscalyearService, 
    private router: Router,
    private spinner: NgxSpinnerService,
    private logService: LogService,
    private authorize: AuthorizeService,
    private userService: UserService,
    @Inject('BASE_URL') baseUrl: string,
    private _NotofyService: NotofyService,
    ) { 
      this.fileUrl = baseUrl + '/Setinspectionareafile';
    }

  ngOnInit() {
    this.spinner.show();
    this.getdata();
    this.Formx();
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
    this.fiscalyearservice.getfiscalyeardata().subscribe(result => {
      this.resultfiscalyear = result
      this.loading = true;
      this.spinner.hide();
    })
  }
  openModal(template: TemplateRef<any>=null, id=null, year=null,orderdate=null,startdate=null,enddate=null,setinspectionareaFiles=null) {
    this.Form.reset();
    this.submitted = false;
    
    this.delid = id;
    this.year = year;
    this.fileset = setinspectionareaFiles;
    if (orderdate == null) {
      this.od = orderdate;
    } else {
      this.od = this.time(orderdate);
    }

    if (startdate == null) {
      this.sd = startdate;
    } else {
      this.sd = this.time(startdate);
    }

    if (enddate == null) {
      this.ed = enddate;
    } else {
      this.ed = this.time(enddate);
    }

    this.Form.patchValue({
      year : year=null,
      orderdate: this.od,
      startdate : this.sd,
      enddate : this.ed,
    })
    this.modalRef = this.modalService.show(template);
  }

  uploadFile(event) {
    const file = (event.target as HTMLInputElement).files;
    this.Form.patchValue({
      files: file
    });
    this.Form.get('files').updateValueAndValidity()

  }

  storeFiscalyear(value) {

    this.submitted = true;
    
    if (this.Form.invalid) {
        return;
    }
    this.fiscalyearservice.addFiscalyear(value,this.Form.value.files).subscribe(response => {
      this.logService.addLog(this.userid,'ข้อสั่งการถึงผู้ตรวจราชการ','เพิ่ม',response.year,response.id).subscribe();
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false;
      this._NotofyService.onSuccess("เพื่มข้อมูล")
      this.getdata();
    })
  }
  deleteFiscalyear(value) {
    this.fiscalyearservice.deleteFiscalyear(value).subscribe(response => {
      this.logService.addLog(this.userid,'ข้อสั่งการถึงผู้ตรวจราชการ','ลบ',this.year,this.delid).subscribe();
      this.modalRef.hide()
      this.loading = false;
      this._NotofyService.onSuccess("ลบข้อมูล")
      this.getdata();
    })
  }
 
  editFiscalyear(value, delid) {
    this.fiscalyearservice.editFiscalyear(value,this.Form.value.files, delid).subscribe(response => {
      this.logService.addLog(this.userid,'ข้อสั่งการถึงผู้ตรวจราชการ','แก้ไข',response.year,response.id).subscribe();
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false;
      this._NotofyService.onSuccess("แก้ไขข้อมูล")
      this.getdata();
    })
  }

  activeFiscalyear(delid){
    this.fiscalyearservice.activeFiscalyear(delid).subscribe(response => {
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false;
      this._NotofyService.onSuccess("Active")
      this.getdata();
    })
  }

  Formx(){
    this.Form = this.fb.group({
      year: new FormControl(null, [Validators.required]),
      startdate: new FormControl(null, [Validators.required]),
      enddate: new FormControl(null, [Validators.required]),
      orderdate: new FormControl(null, [Validators.required]),
      files: new FormControl(null, [Validators.required]),
    })
  }
  get f() { return this.Form.controls; }

  time(date) {
    var ssss = new Date(date)
    var new_date = { date: { year: ssss.getFullYear(), month: ssss.getMonth() + 1, day: ssss.getDate() } }
    return new_date
  }

  DetailFiscalYear(id: any) {
    this.router.navigate(['/fiscalyear/detailfiscalyear', id])
  }
 
}

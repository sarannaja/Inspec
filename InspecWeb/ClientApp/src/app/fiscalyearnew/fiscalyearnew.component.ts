import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NotofyService } from '../services/notofy.service';
import { FiscalyearnewService } from '../services/fiscalyearnew.service';
import { IMyOptions } from 'mydatepicker-th';
import { LogService } from '../services/log.service';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-fiscalyearnew',
  templateUrl: './fiscalyearnew.component.html',
  styleUrls: ['./fiscalyearnew.component.css']
})
export class FiscalyearnewComponent implements OnInit {
  public myDatePickerOptions: IMyOptions = {
    // other options...
    dateFormat: 'dd/mm/yyyy',
  };
  data: any[] = []
  delid: any
  name: any
  modalRef: BsModalRef;
  Form: FormGroup
  loading = false;
  submitted = false;
  dtOptions: any = {};
  userid:any;
  role_id:any;
  year : any;
  date: any = { date: { year: (new Date()).getFullYear(), month: (new Date()).getMonth() + 1, day: (new Date()).getDate() } };
  constructor(private modalService: BsModalService, private fb: FormBuilder,
    private fiscalyearnewservice: FiscalyearnewService,
    private router: Router,
    private _NotofyService: NotofyService,
    private logService: LogService, 
    private authorize: AuthorizeService,
    private userService: UserService,
    ) { }
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

    this.Form = this.fb.group({
      Year: new FormControl(null, [Validators.required]),
      Startdate: new FormControl(null, [Validators.required]),
      Enddate: new FormControl(null, [Validators.required]),
    })
  }

  getdata() {
    this.authorize.getUser()
      .subscribe(result => {
        this.userid = result.sub
        this.userService.getuserfirstdata(this.userid)
          .subscribe(result => {
            this.role_id = result[0].role_id
        })
    })
    this.fiscalyearnewservice.getdata().subscribe(result => {
      this.data = result
      this.loading = true;
    })
  }
  openModal(template: TemplateRef<any>=null, id=null, Year=null, Startdate=null, Enddate=null) {
    this.Form.reset()
    this.submitted = false;
    this.delid = id;
    this.year = Year;
    this.Form.patchValue({
      Year: Year,
      Startdate: this.time(Startdate),
      Enddate: Enddate ? this.time(Enddate) : null,
    })
    this.modalRef = this.modalService.show(template);
  }
  store(value) {
    this.submitted = true;
    if (this.Form.invalid) {
        return;
    }
    this.fiscalyearnewservice.store(value).subscribe(response => {

      this.logService.addLog(this.userid,'FiscalYearNew','เพิ่ม',response.year,value.id).subscribe();

      this.Form.reset()
      this.modalRef.hide()
      this.loading = false;
      this.getdata()
      this._NotofyService.onSuccess("เพื่มข้อมูล")
    })
  }
  delete(value) {
    this.fiscalyearnewservice.delete(value).subscribe(response => {
      this.logService.addLog(this.userid,'FiscalYearNew','ลบ',this.year,this.delid).subscribe();
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false;
      this.getdata()
      this._NotofyService.onSuccess("ลบข้อมูล")
    })
  }

  edit(value, delid) {
    this.submitted = true;
    if (this.Form.invalid) {
        return;
    }
    this.fiscalyearnewservice.update(value, delid).subscribe(response => {
      this.logService.addLog(this.userid,'FiscalYearNew','แก้ไข',response.year,this.delid).subscribe();
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false;
      this.getdata()
      this._NotofyService.onSuccess("แก้ไขข้อมูล")
    })
  }

  time(date) {
    var ssss = new Date(date)
    var new_date = { date: { year: ssss.getFullYear(), month: ssss.getMonth() + 1, day: ssss.getDate() } }
    return new_date
  }
  get f() { return this.Form.controls; }
}

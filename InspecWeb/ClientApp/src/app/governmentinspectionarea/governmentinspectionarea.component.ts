import { Component, OnInit, TemplateRef, Inject } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { FiscalyearService } from '../services/fiscalyear.service';
import { Router } from '@angular/router';
import { IMyOptions } from 'mydatepicker-th';
import { NgxSpinnerService } from "ngx-spinner";
import { NotofyService } from '../services/notofy.service';
import { LogService } from '../services/log.service';
import { AuthorizeService } from 'src/api-authorization-new/authorize.service';
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
  dtOptions: any = {};
  public myDatePickerOptions: IMyOptions = {
    // other options...
    dateFormat: 'dd/mm/yyyy',
    showTodayBtn: true
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
  Detail(id: any) {
    if (this.userid == null) {
      this.router.navigate(['/supportgovernment/governmentinspectionareamain/detail', id])
    } else {
      this.router.navigate(['/supportgovernment/governmentinspectionarea/detail', id])
    }

  }
  excel(id){
    window.location.href = '/api/fiscalyear/excelfiscalyear/'+ id;
  }
}

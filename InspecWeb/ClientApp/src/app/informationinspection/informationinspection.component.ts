import { Component, OnInit, TemplateRef, Inject } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, FormControl, Validators} from '@angular/forms';
import { FiscalyearService } from '../services/fiscalyear.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { UserService } from '../services/user.service';
import { InformationinspectionService } from '../services/informationinspection.service';


@Component({
  selector: 'app-informationinspection',
  templateUrl: './informationinspection.component.html',
  styleUrls: ['./informationinspection.component.css']
})
export class InformationinspectionComponent implements OnInit {
  files: string[] = []
  data: any = []
  id:any;
  role_id:any;
  modalRef:BsModalRef;
  Form : FormGroup;
  loading = false;
  dtOptions: DataTables.Settings = {};
  fileUrl: any;
  filename:any;

  constructor(
    private authorize: AuthorizeService,
    private modalService: BsModalService, 
    private fb: FormBuilder, 
    private fiscalyearService: FiscalyearService,
    private informationinspectionservice: InformationinspectionService,
    private userService: UserService,
    private spinner: NgxSpinnerService,
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
      this.userService.getuserfirstdata(result.sub)
          .subscribe(result => {
            this.role_id = result[0].role_id
          })
    })
    //<!-- END เช็ค role -->

    this.informationinspectionservice.getdata()
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
      files: new FormControl(null, [Validators.required]),
    })
  }

  openModal(template: TemplateRef<any>,id,title,filename) {
    this.Form.reset()
    this.id = id;
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
    this.informationinspectionservice.store(value, this.Form.value.files)
    .subscribe(result => {
      this.Form.reset();
      this.loading = false;
      this.getdata();
      this.modalRef.hide();
    })
  }

  update(value,id){
    this.informationinspectionservice.update(value, this.Form.value.files,id)
    .subscribe(result => {
      this.Form.reset();
      this.loading = false;
      this.getdata();
      this.modalRef.hide();
    })
  }

  destroy(id){
    this.informationinspectionservice.destroy(id)
    .subscribe(result => {
      this.Form.reset();
      this.loading = false;
      this.getdata();
      this.modalRef.hide();
    })
  }
}

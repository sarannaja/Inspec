import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { CabineService } from '../services/cabine.service';
import { MinistryService } from '../services/ministry.service';
import { NotofyService } from '../services/notofy.service';
import { LogService } from '../services/log.service';
import { UserService } from '../services/user.service';
import { AuthorizeService } from 'src/api-authorization/authorize.service';

@Component({
  selector: 'app-cabinet',
  templateUrl: './cabinet.component.html',
  styleUrls: ['./cabinet.component.css']
})
export class CabinetComponent implements OnInit {
  files: string[] = []
  resultcabine: any = []
  selectministry:any=[];
  delid:any
  name:any
  position:any
  image:string[] = []
  selectdataministry:any;
  prefix:any
  detail:any
  modalRef:BsModalRef;
  Form : FormGroup;
  loading = false;
  submitted = false;
  dtOptions: any = {};
  userid:any;
  role_id:any;
  filename:any;
  ministry :any;

  constructor(
    private modalService: BsModalService, 
    private fb: FormBuilder, 
    private cabineservice: CabineService,
    private ministryService: MinistryService,
    private authorize: AuthorizeService,
    private userService: UserService,
    private logService: LogService,
    private _NotofyService: NotofyService,
    ) { }

  ngOnInit() {
    //this.getdata();
    this.getDataMinistries();
    this.getDataMinistriesfirst();
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
      dom: 'Bfrtip',
      buttons: [
        { extend: 'excel', text: 'Excel', className: 'btn btn-success glyphicon glyphicon-list-alt' },
        { extend: 'pdf', text: 'Pdf', className: 'btn btn-primary glyphicon glyphicon-file' },
        { extend: 'print', text: 'Print', className: 'btn btn-primary glyphicon glyphicon-print' },
      ]
    };

    this.Form = this.fb.group({
      name : new FormControl(null, [Validators.required]),
      position: new FormControl(null, [Validators.required]),
      prefix : new FormControl(null, [Validators.required]),
      detail : new FormControl(null, [Validators.required]),
      files : new FormControl(null),
      Commandnumber : new FormControl(null, [Validators.required]),
      cabinet : new FormControl(null, [Validators.required]),
      tel : new FormControl(null, [Validators.required]),
      MinistryId : new FormControl(null, [Validators.required]),
    })
  }

  getdata(id){
    this.authorize.getUser()
    .subscribe(result => {
      this.userid = result.sub
      this.userService.getuserfirstdata(this.userid)
        .subscribe(result => {
        this.role_id = result[0].role_id
      })
    })
    this.cabineservice.getcabineministry(id).subscribe(result=>{
      this.resultcabine = result
      //console.log("xxx",result);
      this.loading = true
      })
  }

   getDataMinistriesfirst() {
    
     this.ministryService.getministry()
      .subscribe(result => {
        this.selectministry = result;
        this.ministry = 0;
        //console.log("momox",result[0].id)
        this.getdata(this.ministry);
        //alert(this.ministry)
      });
  }

  Changeministry(event){
    // alert(JSON.stringify(event.target));
    // console.log("momox",event.target)
    // alert(event.target.value);
    this.ministry = event.target.value;
    this.loading = false;
    this.getdata(event.target.value);
  }

  getDataMinistries() {
    var test: any = [];
    this.ministryService.getministry()
      .subscribe(result => {
        this.selectdataministry = result.map((item, index) => {
          return { value: item.id, label: item.name }
        })


      });
  }

  openModal(template: TemplateRef<any>, id, name, position, prefix, detail, Commandnumber, cabinet,tel,filename,MinistryId) {
    this.Form.reset()
    this.submitted = false;
    this.delid = id;
    this.name = name;
    this.filename = filename;
    this.Form.patchValue({
      name : name,
      position: position,
      prefix : prefix,
      detail : detail,
      Commandnumber : Commandnumber,
      cabinet : cabinet,
      tel : tel,
     MinistryId : MinistryId,
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
  storeCabine(value) {
    this.submitted = true;
    if (this.Form.invalid) {
      return;
    }
    this.cabineservice.addCabine(value, this.Form.value.files).
    subscribe(response => {
      this.logService.addLog(this.userid,'Cabines','เพิ่ม',response.name,response.id).subscribe();
      this.Form.reset()
      this.loading = false;
      this.modalRef.hide()
      this._NotofyService.onSuccess("เพิ่มข้อมูล")
      this.getdata(this.ministry);
    })
  }
  deleteCabine(value) {
    this.cabineservice.deleteCabine(value).subscribe(response => {
      this.logService.addLog(this.userid,'Cabines','ลบ',this.name,this.delid).subscribe();
      this.loading = false;
      this.modalRef.hide()
      this._NotofyService.onSuccess("ลบข้อมูล")
      this.getdata(this.ministry);
    })
  }
  editCabine(value,delid) {

    this.submitted = true;
    if (this.Form.invalid) {
      return;
    }

    this.cabineservice.editCabine(value,this.Form.value.files,this.filename,delid).subscribe(response => {
      this.logService.addLog(this.userid,'Cabines','แก้ไข',response.name,response.id).subscribe();
      this.Form.reset()
      this.loading = false;
      this.modalRef.hide()
      this._NotofyService.onSuccess("แก้ไขข้อมูล")
      this.getdata(this.ministry);
    })
  }
  get f() { return this.Form.controls; }
}

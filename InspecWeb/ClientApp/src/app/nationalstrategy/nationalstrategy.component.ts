import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { NationalstrategyService } from '../services/nationalstrategy.service';
import { UserService } from '../services/user.service';
import { AuthorizeService } from 'src/api-authorization-new/authorize.service';

@Component({
  selector: 'app-nationalstrategy',
  templateUrl: './nationalstrategy.component.html',
  styleUrls: ['./nationalstrategy.component.css']
})
export class NationalstrategyComponent implements OnInit {
  resultnationalstrategy: any = []
  delid: any
  title: any
  link: any
  modalRef:BsModalRef;
  Form : FormGroup
  files: string[] = []
  loading = false;
  role_id:any;
  namefile :any;
  dtOptions: any = {};

  constructor(private modalService: BsModalService, 
    private fb: FormBuilder, 
    private nationalstrategyservice: NationalstrategyService,
    public share: NationalstrategyService,
    private userService: UserService,
    private authorize: AuthorizeService,
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

    this.form();
    this.getdata();
  }

  getdata(){
    //<!-- เช็ค role  -->
    this.authorize.getUser()
    .subscribe(result => {
      this.userService.getuserfirstdata(result.sub)
          .subscribe(result => {
            this.role_id = result[0].role_id
          })
    })
    //<!-- END เช็ค role -->

   this.nationalstrategyservice.getnationalstrategy().subscribe(result=>{
   this.resultnationalstrategy = result
   this.loading = true
   })
  }

  form(){
    this.Form = this.fb.group({
      title: new FormControl(null, [Validators.required]),
      files : new FormControl(null, [Validators.required])
    })
  }
  openModal(template: TemplateRef<any>, id, title,namefile) {
    this.delid = id;
    this.title = title;
    this.namefile = namefile;
    this.Form.patchValue({
      title: title
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

  storeNationalstrategy(value) {
    this.loading = false
    this.nationalstrategyservice.addNationalstrategy(value, this.Form.value.files).subscribe(response => {
      this.Form.reset()
      this.modalRef.hide()
      this.nationalstrategyservice.getnationalstrategy().subscribe(result => {
        this.resultnationalstrategy = result
        this.loading = true
      })
    })
  }

  updateNationalstrategy(value,id){
    this.loading = false
    this.nationalstrategyservice.editNationalstrategy(value, this.Form.value.files,id,this.namefile).subscribe(response => {
      this.Form.reset()
      this.modalRef.hide()
      this.nationalstrategyservice.getnationalstrategy().subscribe(result => {
        this.resultnationalstrategy = result
        this.loading = true
      })
    })
  }
  delete(id){
    this.loading = false
    this.nationalstrategyservice.delete(id).subscribe(response => {
      this.Form.reset()
      this.modalRef.hide()
      this.nationalstrategyservice.getnationalstrategy().subscribe(result => {
        this.resultnationalstrategy = result
        this.loading = true
      })
    })
  }


}


import { Component, OnInit, TemplateRef, Inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TrainingService } from '../../services/training.service';
import { Router } from '@angular/router';
import { FormBuilder, FormControl, FormGroup, Validators, FormArray } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from "ngx-spinner";
import { AuthorizeService } from 'src/api-authorization-new/authorize.service';
import { NotofyService } from 'src/app/services/notofy.service';
import { LogService } from 'src/app/services/log.service';

@Component({
  selector: 'app-list-training-document',
  templateUrl: './list-training-document.component.html',
  styleUrls: ['./list-training-document.component.css']
})
export class ListTrainingDocumentComponent implements OnInit {

  trainingid: string
  resulttraining: any[] = []
  modalRef: BsModalRef;
  delid: any
  name: any
  link: any
  loading = false;
  dtOptions: any = {};
  Form: FormGroup;
  EditForm: FormGroup;
  form: FormGroup;
  files: string[] = []
  downloadUrl: string;
  userid: string;
  namefile: any;

  constructor(private modalService: BsModalService,
    private authorize: AuthorizeService,
    private _NotofyService: NotofyService,
    private spinner: NgxSpinnerService,
    private logService: LogService,
    private fb: FormBuilder,
    private trainingservice: TrainingService,
    public share: TrainingService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    @Inject('BASE_URL') baseUrl: string) {
    this.trainingid = activatedRoute.snapshot.paramMap.get('id')
    this.downloadUrl = baseUrl + '/Uploads'
  }

  ngOnInit() {
    this.spinner.show();
    this.getuserinfo();
    this.dtOptions = {
      columnDefs: [
        {
          targets: [1, 2, 3, 4],
          orderable: false
        }
      ],
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
      detail: new FormControl(null, [Validators.required]),

    })
    this.form = this.fb.group({
      files: [null]
    })

    this.trainingservice.getlisttrainingdocumentdata(this.trainingid)
      .subscribe(result => {
        this.resulttraining = result;
        this.loading = true;
        this.spinner.hide();
        //console.log(this.resulttraining);
      })
  }

  //start getuser
  getuserinfo() {
    this.authorize.getUser()
      .subscribe(result => {
        this.userid = result.sub
      })
  }

  openModal(template: TemplateRef<any>, id: any = null) {
    this.delid = id;
    //console.log(this.delid);
    this.modalRef = this.modalService.show(template);
  }

  opendeleteModal(template: TemplateRef<any>, id: any = null, name) {
    this.delid = id;
    this.namefile = name;
    //console.log(this.delid);
    this.modalRef = this.modalService.show(template);
  }


  storeTraining(value) {
    //alert(JSON.stringify(value))
    //alert(this.form.value.files)
    this.spinner.show();
    this.trainingservice.addTrainingDocument(value, this.form.value.files, this.trainingid).subscribe(response => {
      console.log("addTrainingDocument =>", response);
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false;
      this.logService.addLog(this.userid,'TrainingDocument','เพิ่ม',response.detail,response.id).subscribe();
      this.trainingservice.sendmaildocument(this.trainingid).subscribe(resultmail => {
        this.trainingservice.getlisttrainingdocumentdata(this.trainingid).subscribe(result => {
          this.resulttraining = result;
          this.loading = true;
          this.spinner.hide();
          this._NotofyService.onSuccess("เพิ่มข้อมูลและทำการส่ง Email ผู้มีสิทธิ์เข้าอบรม");
          //console.log(this.resulttraining);
        });
      });
      
    })
  }

  uploadFile(event) {
    const file = (event.target as HTMLInputElement).files;
    this.form.patchValue({
      files: file
    });
    this.form.get('files').updateValueAndValidity()
  }

  addFile(event) {
    this.files = event.target.files
  }
  get f() { return this.Form.controls }
  get d() { return this.f.inputdate as FormArray }

  deleteTraining(value) {
    this.trainingservice.deleteTrainingDocument(value).subscribe(response => {
      //console.log(value);
      this.modalRef.hide()
      this.loading = false;
      this.logService.addLog(this.userid,'TrainingDocument','ลบ',this.namefile,this.delid).subscribe();
      this.trainingservice.getlisttrainingdocumentdata(this.trainingid).subscribe(result => {
        this.resulttraining = result
        this.loading = true;
        this._NotofyService.onSuccess("ลบข้อมูล")
        //console.log(this.resulttraining);
      })
    })
  }

  gotoBack() {
    window.history.back();
  }

  gotoMain(){
    this.router.navigate(['/main'])
  }

  gotoMainTraining(){
    this.router.navigate(['/training'])
  }

  gotoTrainingManage(){
    this.router.navigate(['/training/manage/', this.trainingid])
  }

}

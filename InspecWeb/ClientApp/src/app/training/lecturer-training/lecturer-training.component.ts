import { Component, OnInit, TemplateRef, Inject } from '@angular/core';
import { TrainingService } from '../../services/training.service';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

import { NotofyService } from '../../services/notofy.service';
import { LogService } from '../../services/log.service';
import { AuthorizeService } from 'src/api-authorization-new/authorize.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { ɵNullViewportScroller } from '@angular/common';

@Component({
  selector: 'app-lecturer-training',
  templateUrl: './lecturer-training.component.html',
  styleUrls: ['./lecturer-training.component.css']
})
export class LecturerTrainingComponent implements OnInit {

  resulttraining: any[] = []
  modalRef: BsModalRef;
  delid: any
  loading = false;
  dtOptions: any = {};
  downloadUrl: any;
  mainUrl: string;
  Form: FormGroup;
  EditForm: FormGroup;
  submitted = false;
  ViewForm: FormGroup;
  selectdatalecturer: Array<any>;
  resultdatalecturer: any[];
  userid: string;
  resulttraininglecturerById: any[];
  ImgProfile: any;
  lecturerName: any;


  constructor(private modalService: BsModalService,
    private authorize: AuthorizeService,
    private _NotofyService: NotofyService,
    private spinner: NgxSpinnerService,
    private logService: LogService,
    private fb: FormBuilder,
    private trainingservice: TrainingService,
    public share: TrainingService,
    private router: Router,
    @Inject('BASE_URL') baseUrl: string) {
      this.downloadUrl = baseUrl + 'Uploads/'
      this.mainUrl = baseUrl
    }

  ngOnInit() {
    this.getuserinfo();
    this.dtOptions = {
      pagingType: 'full_numbers',
      columnDefs: [
        {
          targets: [1, 2, 3, 4],
          orderable: false
        }
      ],
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
      "LecturerType": new FormControl(null, [Validators.required]),
      lecturername: new FormControl(null, [Validators.required]),
      lecturerphone: new FormControl(null, [Validators.required]),
      lectureremail: new FormControl(null, [Validators.required]),
      education: new FormControl(null, [Validators.required]),
      workhistory: new FormControl(null, [Validators.required]),
      experience: new FormControl(null, [Validators.required]),
      detailplus: new FormControl(null, [Validators.required]),
      picFiles: [null],

    })

    this.trainingservice.gettraininglecturer()
    .subscribe(result => {
      this.resulttraining = result
      this.loading = true;
      console.log("gettraininglecturer =>", this.resulttraining);
    })

    this.trainingservice.getTrainingLecturerType()
    .subscribe(result => {

      this.resultdatalecturer = result
      if (this.resultdatalecturer.length > 0){
        this.selectdatalecturer = this.resultdatalecturer.map((item, index) => {
          return { value: item.id, label: item.name }
        })
      }
      this.spinner.hide();
    })
  }
  get f() { return this.Form.controls }

  //start getuser
  getuserinfo() {
    this.spinner.show();
    this.authorize.getUser()
      .subscribe(result => {
        this.userid = result.sub
      })
  }

  

  CreateTraining(){
    this.router.navigate(['/training/createtraining'])
  }
  openModal(template: TemplateRef<any>, id: any = null) {
    this.delid = id;
    console.log(this.delid);

    this.submitted = false;
    this.modalRef = this.modalService.show(template);
  }

  deleteModal(template: TemplateRef<any>, id, lecturerName, template2: TemplateRef<any>) {
    this.submitted = false;
    this.delid = id;
    this.lecturerName = lecturerName;
    console.log(this.delid);

    this.trainingservice.getUsetraininglecturer(id)
      .subscribe(result => {
        console.log("getUsetraininglecturer =>", result);
        console.log("getUsetraininglecturer result.length  =>", result.length );
        
        if(result.length > 0){
          this.modalRef = this.modalService.show(template2);
        }
        else{
          this.modalRef = this.modalService.show(template);
        }

      })
    
    
    
    
  }


  storeTraining(value) {
    console.log("storeTraining =>", value);
    
    if (this.Form.invalid) {
      console.log("in1");
      this.submitted = true;
      return;
    } else {


      //alert(JSON.stringify(value))
      this.trainingservice.addTraininglecturer(value, this.Form.value.picFiles).subscribe(response => {
        //alert(JSON.stringify(response))
        console.log("addTraininglecturer =>", response);
        this.modalRef.hide()
        this.Form.reset()
        this.logService.addLog(this.userid,'TrainingLecturer','เพิ่ม', response.lecturerName, response.id).subscribe();
        this.trainingservice.gettraininglecturer()
        .subscribe(result => {
          this.resulttraining = result
          this.loading = true;
          console.log(this.resulttraining);
          this._NotofyService.onSuccess("เพิ่มข้อมูล")
        })

      })
    }
  }

  ViewModal(template: TemplateRef<any>, id, lecturerType, lecturerName, phone, email, education, workHistory, experience, detailplus, imgProfile) {
    this.delid = id;
    this.ImgProfile = imgProfile;
    console.log(detailplus);

    this.modalRef = this.modalService.show(template);
    this.ViewForm = this.fb.group({
      "vlecturertype": new FormControl(null, [Validators.required]),
      "lecturername": new FormControl(null, [Validators.required]),
      "lecturerphone": new FormControl(null, [Validators.required]),
      "lectureremail": new FormControl(null, [Validators.required]),
      "education": new FormControl(null, [Validators.required]),
      "workhistory": new FormControl(null, [Validators.required]),
      "experience": new FormControl(null, [Validators.required]),
      "detailplus": new FormControl(null, [Validators.required]),
      picFiles: [null],
    })


    this.ViewForm.patchValue({
      "vlecturertype": lecturerType,
      "lecturername": lecturerName,
      "lecturerphone": phone,
      "lectureremail": email,
      "education": education,
      "workhistory": workHistory,
      "experience": experience,
      "detailplus": detailplus,

    })
  }

  editModal(template: TemplateRef<any>, id, lecturerType, lecturerName, phone, email, education, workHistory, experience, detailplus, imgProfile) {
    this.delid = id;
    this.ImgProfile = imgProfile;
    console.log("ImgProfile =>", this.ImgProfile);
    console.log("lecturerType =>", lecturerType);

    //console.log(this.delid);

    this.modalRef = this.modalService.show(template);
    this.EditForm = this.fb.group({
      "LecturerType": new FormControl(null, [Validators.required]),
      "lecturername": new FormControl(null, [Validators.required]),
      "lecturerphone": new FormControl(null, [Validators.required]),
      "lectureremail": new FormControl(null, [Validators.required]),
      "education": new FormControl(null, [Validators.required]),
      "workhistory": new FormControl(null, [Validators.required]),
      "experience": new FormControl(null, [Validators.required]),
      "detailplus": new FormControl(null, [Validators.required]),
      picFiles: [null],
    })


    this.EditForm.patchValue({
      "LecturerType": lecturerType,
      "lecturername": lecturerName,
      "lecturerphone": phone,
      "lectureremail": email,
      "education": education,
      "workhistory": workHistory,
      "experience": experience,
      "detailplus": detailplus,
    })

    // this.trainingservice.gettraininglecturerById(this.delid)
    // .subscribe(result => {
    //   this.resulttraininglecturerById = result
    //   this.loading = true;
    //   console.log(this.resulttraining);
    // })




  }

  editTraining(value) {
    console.log("editTraining =>", value);

    // alert(JSON.stringify(value));
    // console.clear();
    // console.log("kkkk" + JSON.stringify(value));
    this.trainingservice.editTraininglecturer(value, this.delid, this.EditForm.value.picFiles).subscribe(response => {
      this.EditForm.reset()
      this.modalRef.hide()
      this.loading = false
      this.logService.addLog(this.userid,'TrainingLecturer','แก้ไข', response.lecturerName, response.id).subscribe();
      this.trainingservice.gettraininglecturer()
      .subscribe(result => {
        this.resulttraining = result
        this.loading = true;
        console.log(this.resulttraining);
        this._NotofyService.onSuccess("แก้ไขข้อมูล")
      })
    })
  }

  deleteTraining(value) {
    console.log(value);
    this.trainingservice.deleteTrainingLecturer(value).subscribe(response => {
      this.modalRef.hide()
      this.loading = false;
      this.logService.addLog(this.userid,'TrainingLecturer','ลบ', this.lecturerName, this.delid).subscribe();
      this.trainingservice.gettraininglecturer()
      .subscribe(result => {
        this.resulttraining = result
        this.loading = true;
        console.log(this.resulttraining);
        this._NotofyService.onSuccess("ลบข้อมูล")
      })
    })
  }

  gotoMain(){
    this.router.navigate(['/main'])
  }

  gotoProgramTraining(trainingid){
    this.router.navigate(['/training/program/', trainingid])
  }

  uploadFile(event) {
    const file = (event.target as HTMLInputElement).files;
    this.Form.patchValue({
      picFiles: file
    });
    //this.Form.get('files').updateValueAndValidity()
  }

  uploadFileEdit(event) {
    const file = (event.target as HTMLInputElement).files;
    this.EditForm.patchValue({
      picFiles: file
    });
    //this.EditForm.get('files').updateValueAndValidity()
  }
}

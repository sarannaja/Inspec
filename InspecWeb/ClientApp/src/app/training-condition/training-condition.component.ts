import { Component, OnInit, TemplateRef, Inject} from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TrainingService } from '../services/training.service';
import { Router } from '@angular/router';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from "ngx-spinner";
import { AuthorizeService } from 'src/api-authorization-new/authorize.service';
import { NotofyService } from '../services/notofy.service';
import { LogService } from '../services/log.service';

@Component({
  selector: 'app-training-condition',
  templateUrl: './training-condition.component.html',
  styleUrls: ['./training-condition.component.css']
})
export class TrainingConditionComponent implements OnInit {

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
  isNameSelected: boolean;
  startyear: any;
  endyear: any;
  conditiontype: any;
  userid: string;
  delname: any;

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
    }



  ngOnInit() {
    this.spinner.show();
    this.getuserinfo();
    this.dtOptions = {
      //pagingType: 'full_numbers',
      columnDefs: [
        {
          targets: [1,4],
          orderable: false
        }
      ]

    };
    this.Form = this.fb.group({
      name: new FormControl(null, [Validators.required]),
      startyear: new FormControl(null, [Validators.required]),
      endyear: new FormControl(null, [Validators.required]),
      conditiontype: new FormControl(null, [Validators.required]),

    })

    this.Form.patchValue({
      conditiontype: "1"
    })

    this.trainingservice.getTrainingCondition(this.trainingid)
    .subscribe(result => {
      this.resulttraining = result
      this.loading = true
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


  selectInput(event) {
    let selected = event.target.value;
    if (selected == "2") {
      this.isNameSelected = true;
      this.Form.patchValue({
        name: "เกณฑ์รับสมัครต้องมีอายุอยู่ระหว่าง"
      })
    } else {
      this.isNameSelected = false;
      this.Form.patchValue({
        name: ""
      })
    }
  }

  openModal(template: TemplateRef<any>, id: any = null) {
     this.delid = id;
    // console.log(this.delid);

    this.modalRef = this.modalService.show(template);
  }

  opendeleteModal(template: TemplateRef<any>, id: any = null, name) {
    this.delid = id;
    this.delname = name;
   // console.log(this.delid);

   this.modalRef = this.modalService.show(template);
 }

  storeTraining(value) {
    //alert(JSON.stringify(value))
    this.trainingservice.addTrainingCondition(value, this.trainingid).subscribe(response => {
      console.log("addTrainingCondition =>", response);
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false;
      this.logService.addLog(this.userid,'TrainingCondition','เพิ่ม',response.name,response.id).subscribe();
      //this.router.navigate(['/training/surveylist/',trainingid])
      //this.router.navigate(['training'])
      this.trainingservice.getTrainingCondition(this.trainingid)
      .subscribe(result => {
        this.resulttraining = result
        this.loading = true
        this._NotofyService.onSuccess("เพิ่มข้อมูล");
        //console.log(this.resulttraining);
      })

    })
  }

  deleteTrainingCondition(value) {
    this.trainingservice.deleteTrainingCondition(value).subscribe(response => {
      console.log(value);
      this.modalRef.hide()
      this.loading = false;
      this.logService.addLog(this.userid,'TrainingCondition','ลบ', this.delname, this.delid).subscribe();
      this.trainingservice.getTrainingCondition(this.trainingid).subscribe(result => {
        this.resulttraining = result
        this.loading = true;
        this._NotofyService.onSuccess("ลบข้อมูล");
        console.log(this.resulttraining);
      })
    })
  }

  editModal(template: TemplateRef<any>, id, name, startyear, endyear, conditiontype) {
    this.delid = id;
    this.name = name;
    // this.startyear = name;
    // this.endyear = name;
    // this.conditiontype = name;

    //console.log(this.delid);
    //console.log(this.name);

    this.modalRef = this.modalService.show(template);
    this.EditForm = this.fb.group({
      name: new FormControl(null, [Validators.required]),
      startyear: new FormControl(null, [Validators.required]),
      endyear: new FormControl(null, [Validators.required]),
      conditiontype: new FormControl(null, [Validators.required]),
    })
    this.EditForm.patchValue({
      "name": name,
      "startyear": startyear,
      "endyear": endyear,
      "conditiontype" : conditiontype
    })
  }

  editTrainingCondition(value, delid) {
    // alert(JSON.stringify(value));
    // console.clear();
    // console.log("kkkk" + JSON.stringify(value));
    this.trainingservice.editTrainingCondition(value, delid).subscribe(response => {
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false;
      this.logService.addLog(this.userid,'TrainingCondition','แก้ไข',response.name,response.id).subscribe();
      this.trainingservice.getTrainingCondition(this.trainingid)
      .subscribe(result => {
        this.resulttraining = result;
        this.loading = true;
        this._NotofyService.onSuccess("แก้ไขข้อมูล");
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

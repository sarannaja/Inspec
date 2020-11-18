import { Component, OnInit, TemplateRef, Inject } from '@angular/core';
import { TrainingService } from '../../services/training.service';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ExportReportService } from 'src/app/services/export-report.service';

@Component({
  selector: 'app-ratelogin-training-report',
  templateUrl: './ratelogin-training-report.component.html',
  styleUrls: ['./ratelogin-training-report.component.css']
})
export class RateloginTrainingReportComponent implements OnInit {

  resulttraining: any = [];
  resultsurveytraining: any[] = []
  trainingid: string;
  modalRef: BsModalRef;
  delid: any
  loading = false;
  dtOptions: any = {};
  downloadUrl: any;
  mainUrl: string;
  Form: FormGroup;
  EditForm: FormGroup;
  selectdatalecturer: any[] = []
  selectdatasurvey: Array<any>
  lecturerid: any
  trainingname: any;
  gen: any;
  trainingyear: any;
  relateLoginData: any = [];
  url: any;

  constructor(private modalService: BsModalService,
    private fb: FormBuilder,
    private trainingservice: TrainingService,
    public share: TrainingService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private exportService: ExportReportService,
    @Inject('BASE_URL') baseUrl: string) {
      this.mainUrl = baseUrl
      this.trainingid = activatedRoute.snapshot.paramMap.get('id')
      this.url = baseUrl;
    }

  ngOnInit() {

    this.trainingservice.getTrainingLoginRate(this.trainingid)
    .subscribe(result => {
      this.resulttraining = result;
      this.loading = true;
      console.log(this.resulttraining);
    })


    this.trainingservice.getdetailtraining(this.trainingid)
      .subscribe(result => {
        console.log("res test: ", result);
        this.relateLoginData = result;
        if (result.length != 0) {
          this.trainingname = result[0].name
          this.gen = result[0].generation
          this.trainingyear = result[0].year
        }
        this.loading = true;

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

  printReport() {
    this.exportService.reportRateLogin(this.resulttraining, this.trainingname, this.gen, this.trainingyear).subscribe(res => {
      window.open(this.url + "Uploads/" + res.data);
      console.log(res);
    })
  }

}

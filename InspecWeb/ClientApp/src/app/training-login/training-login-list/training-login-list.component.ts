import { Component, Inject, OnInit, TemplateRef } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Router } from '@angular/router';
import { BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { AuthorizeService } from 'src/api-authorization-new/authorize.service';
import { InspectionplanService } from 'src/app/services/inspectionplan.service';
import { NotofyService } from 'src/app/services/notofy.service';
import { TrainingLoginService } from 'src/app/services/training-login.service';
import { TrainingService } from 'src/app/services/training.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-training-login-list',
  templateUrl: './training-login-list.component.html',
  styleUrls: ['./training-login-list.component.css']
})
export class TrainingLoginListComponent implements OnInit {
  userid: string;
  role_id: any;
  dtOptions: any = {};
  trainingProgramData: any = [];
  trainingId: any;
  loading = false;
  datalist: any[];
  downloadUrl: string;
  mainUrl: string;

  constructor(
    private router: Router,
    private trainingService: TrainingLoginService,
    private authorize: AuthorizeService,
    private modalService: BsModalService,
    private inspectionplanservice: InspectionplanService,
    private spinner: NgxSpinnerService,
    private userService: UserService,
    private _NotofyService: NotofyService,
    private activatedRoute: ActivatedRoute,
    @Inject('BASE_URL') baseUrl: string) {
      this.downloadUrl = baseUrl + '/Uploads'
      this.mainUrl = baseUrl
      this.trainingId = activatedRoute.snapshot.paramMap.get('trainingid')
    }
  // ) {
  //   this.trainingId = activatedRoute.snapshot.paramMap.get('trainingid')
  // }

  ngOnInit() {
    this.authorize.getUser()
      .subscribe(result => {
        this.userid = result.sub
        console.log(result);
        // alert(this.userid)
        this.userService.getuserfirstdata(this.userid)
          .subscribe(result => {
            // this.resultuser = result;
            //console.log("test" , this.resultuser);
            this.role_id = result[0].role_id
            // alert(this.role_id)
          })
      })
    this.dtOptions = {
      pagingType: 'full_numbers',
      columnDefs: [
        {
          targets: [3],
          orderable: false
        }
      ],
      "language": {
        "lengthMenu": "แสดง  _MENU_  รายการ",
        "search": "ค้นหา:",
        // "info": "แสดง _PAGE_ ของ _PAGES_ รายการ",
        // "info": "แสดง _PAGE_ ของ _PAGES_ รายการ จาก _TOTAL_ แถว",
        "info": "แสดง _START_ ถึง _END_ จาก _TOTAL_ แถว",
        // "info": "แสดง _START_ ถึง _END_ จาก _TOTAL_ แถว",
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

    this.getTrainingProgramDate();
  }

  openModal() {
    //console.log(this.delid)
    
  }


  getTrainingProgramDate() {
    this.trainingService.getTrainingProgramDate2(this.trainingId).subscribe(res => {

      this.trainingProgramData = res;

      // this.trainingProgramData = res.filter(
      //   (thing, i, arr) => arr.findIndex(t => t.programDate === thing.programDate) === i
      // );

      //this.datalist = _.orderBy(this.trainingProgramData, [''], ['asc']);
      
      console.log("training programdate: ", this.trainingProgramData);
      this.loading = true;
    })
  }

  gotoList(programid, programType, programDate) {
    console.log("id: ", programid);
    console.log("programType: ", programType);

    this.router.navigate(['training/login/list/detail/' + programid, {programType: programType , trainingId: this.trainingId, programDate: programDate}]);
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
    this.router.navigate(['/training/manage/', this.trainingId])
  }
}

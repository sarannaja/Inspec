import { Component, OnInit, TemplateRef, Inject} from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TrainingService } from '../../services/training.service';
import { UserService } from 'src/app/services/user.service';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { Router } from '@angular/router';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';


@Component({
  selector: 'app-history-training-report',
  templateUrl: './history-training-report.component.html',
  styleUrls: ['./history-training-report.component.css']
})
export class HistoryTrainingReportComponent implements OnInit {

  trainingid: string
  resulttraining: any[] = []
  modalRef: BsModalRef;
  delid: any
  loading = false;
  dtOptions: DataTables.Settings = {};
  Form: any;
  userid: string;
  resultuser: any;
  role_id: any;
  Prefix: any;
  Name: any;
  Position: any;
  PhoneNumber: any;
  Email: any;
  Img: any;
  CardId: any;
  Username: any;
  
  constructor(private modalService: BsModalService, 
    private authorize: AuthorizeService,
    private UserService: UserService,
    private fb: FormBuilder, 
    private trainingservice: TrainingService,
    public share: TrainingService, 
    private router: Router,
    private activatedRoute: ActivatedRoute,
    @Inject('BASE_URL') baseUrl: string) {
      //this.trainingid = activatedRoute.snapshot.paramMap.get('id')
    }
    
    

  ngOnInit() {

    this.getuserinfo()

    

  }

  

  //start getuser
  getuserinfo(){
    this.authorize.getUser()
    .subscribe(result => {
      this.userid = result.sub  
      //alert(this.userid)
      this.UserService.getuserfirstdata(this.userid)      
      .subscribe(result => { 
        this.resultuser = result;
        //console.log("test" , this.resultuser);
        this.role_id = result[0].role_id
      
        this.Prefix = result[0].prefix
        this.Username = result[0].userName
        this.Name = result[0].name
        this.Position = result[0].position
        this.PhoneNumber = result[0].phoneNumber
        this.Email = result[0].email
        this.Img = result[0].img
        console.log("Username=>",this.Username);
    //     this.trainingservice.gethistorytraining(this.Name)
    // .subscribe(result => {
    //   this.resulttraining = result
    //   this.loading = true
    //   console.log(this.resulttraining);
    // })

    this.trainingservice.getTraininghistoryreport(this.Username)
    .subscribe(result => {
      this.resulttraining = result
      this.loading = true;
      console.log("resulttraining=>",this.resulttraining);
    })

        // this.Form.patchValue({
        //   Prefix: this.Prefix,
        //   name: this.Name,
        //   cardid: this.CardId,
        //   position: this.Position,
        //   phone: this.PhoneNumber,
        //   email: this.Email,
        //   Formprofile:1,
        //   //files: this.files,
        // });

      })
    })

  }
  //End getuser

  gotoBack() {
    window.history.back();
  }

}

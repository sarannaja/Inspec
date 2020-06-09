import { Component, OnInit, TemplateRef, Inject} from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TrainingService } from '../../../services/training.service';
import { Router } from '@angular/router';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';


@Component({
  selector: 'app-list-register-training-report',
  templateUrl: './list-register-training-report.component.html',
  styleUrls: ['./list-register-training-report.component.css']
})
export class ListRegisterTrainingReportComponent implements OnInit {

  trainingid: string
  resulttraining: any[] = []
  resulttrainingtopic: any[] = []
  modalRef: BsModalRef;
  delid: any
  loading = false;
  dtOptions: DataTables.Settings = {};
  Form: any;
  trainingname: any;
  
  constructor(private modalService: BsModalService, 
    private fb: FormBuilder, 
    private trainingservice: TrainingService,
    public share: TrainingService, 
    private router: Router,
    private activatedRoute: ActivatedRoute,
    @Inject('BASE_URL') baseUrl: string) {
      this.trainingid = activatedRoute.snapshot.paramMap.get('id')
    }
    
    

  ngOnInit() {

    this.trainingservice.getregistertrainingdata(this.trainingid)
    .subscribe(result => {
      this.resulttraining = result
      this.loading = true
      //console.log(this.resulttraining);
    })


    this.trainingservice.getdetailtraining(this.trainingid)
    .subscribe(result => {
      if (result.length != 0){
        this.trainingname = result[0].name
      }
      this.resulttrainingtopic = result
      this.loading = true;
      //console.log(this.resulttraining);
    })
    
  }

  openModal(template: TemplateRef<any>, id) {
    this.delid = id;
    console.log(this.delid);

    this.modalRef = this.modalService.show(template);
  }

  editRegisterList(value, delid) {
    // alert(JSON.stringify(value));
    // console.clear();
    // console.log("kkkk" + JSON.stringify(value));
    this.trainingservice.editRegisterList(value, delid).subscribe(response => {
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false
      this.trainingservice.getregistertrainingdata(this.trainingid).subscribe(result => {
        this.resulttraining = result
        this.loading = true
      })
    })
  }

  //start getuser
// getuserinfo(){
//   this.authorize.getUser()
//   .subscribe(result => {
//     this.userid = result.sub  
//     //alert(this.userid)
//     this.UserService.getuserfirstdata(this.userid)      
//     .subscribe(result => { 
//       this.resultuser = result;
//       //console.log("test" , this.resultuser);
//       this.role_id = result[0].role_id
     
//       this.Prefix = result[0].prefix
//       this.Name = result[0].name
//       this.Position = result[0].position
//       this.PhoneNumber = result[0].phoneNumber
//       this.Email = result[0].email
//       this.Img = result[0].img
     
//       this.Form.patchValue({
//         Prefix: this.Prefix,
//         name: this.Name,
//         cardid: this.CardId,
//         position: this.Position,
//         phone: this.PhoneNumber,
//         email: this.Email,
//         Formprofile:1,
//         //files: this.files,
//       });

//     })
//   })

// }
//End getuser

}

import { Component, OnInit, TemplateRef, Inject} from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TrainingService } from '../../services/training.service';
import { Router } from '@angular/router';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';


@Component({
  selector: 'app-list-training-register',
  templateUrl: './list-training-register.component.html',
  styleUrls: ['./list-training-register.component.css']
})
export class ListTrainingRegisterComponent implements OnInit {

  trainingid: string
  resulttraining: any[] = []
  modalRef: BsModalRef;
  delid: any
  loading = false;
  dtOptions: DataTables.Settings = {};
  Form: any;
  
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

    this.dtOptions = {
      pagingType: 'full_numbers',
      columnDefs: [
        {
          targets: [3,4],
          orderable: false
        }
      ]

    };

    this.Form = this.fb.group({
      "approve": new FormControl(null, [Validators.required]),
      
    })

    this.trainingservice.getregistertrainingdata(this.trainingid)
    .subscribe(result => {
      this.resulttraining = result
      this.loading = true
      //console.log(this.resulttraining);
    })
    

    // this.trainingservice.gettrainingdata2(id)
    // .subscribe(result => {
    //   this.resulttraining = result
    //   this.loading = true;
    //   console.log(this.resulttraining);
    // })

    //this.gettrainingdata2()
  }

  // gettrainingdata2() {
  //   this.trainingservice.gettrainingdata2(1)
  //   .subscribe(result => {
  //     this.resulttraining = result
  //     this.loading = true
  //     //console.log(this.resulttraining);
  //   })
  // }

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
        this.modalRef.hide()
      })
    })
  }

}

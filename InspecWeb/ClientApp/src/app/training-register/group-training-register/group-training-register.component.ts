import { Component, OnInit, TemplateRef, Inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TrainingService } from '../../services/training.service';
import { Router } from '@angular/router';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';


@Component({
  selector: 'app-group-training-register',
  templateUrl: './group-training-register.component.html',
  styleUrls: ['./group-training-register.component.css']
})
export class GroupTrainingRegisterComponent implements OnInit {

  trainingid: string
  resulttraining: any[] = []
  modalRef: BsModalRef;
  delid: any
  loading = false;
  dtOptions: DataTables.Settings = {};
  Form: any;
  resulttrainingPhase: any;
  resulttrainingPhasetable: any[] = []
  people: any[] = []
  approve: any[] = []
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
          targets: [3, 4],
          orderable: false
        }
      ]
    };

    this.Form = this.fb.group({
      "approve1": new FormControl(null),
      "approve2": new FormControl(null),
      "approve3": new FormControl(null),
      "approve4": new FormControl(null),
      "approve5": new FormControl(null),
      "approve6": new FormControl(null),
      "approve7": new FormControl(null),
      "approve8": new FormControl(null),
      "approve9": new FormControl(null),
      "approve10": new FormControl(null),
    })

    this.trainingservice.getregistertrainingdata(this.trainingid)
      .subscribe(res => {
        this.resulttraining = res.filter(result => {
          return result.status == 1
        })
        this.loading = true
        console.log("resulttraining", this.resulttraining);

      })

    this.trainingservice.getTrainingPhaseCount(this.trainingid)
      .subscribe(result => {
        this.resulttrainingPhase = result
        console.log(this.resulttrainingPhase);

      })

    this.trainingservice.getTrainingPhase(this.trainingid)
      .subscribe(result => {
        this.resulttrainingPhasetable = result
        this.loading = true
        console.log("resulttrainingPhasetable", this.resulttrainingPhasetable);
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
    // alert(id)
    this.delid = id;
    console.log(this.delid);
    this.modalRef = this.modalService.show(template);
  }

  editRegisterList(value, delid) {
    // alert(JSON.stringify(value));
    // console.clear();
    // console.log("kkkk" + JSON.stringify(value));
    this.trainingservice.editRegisterGroup(this.approve, delid).subscribe(response => {
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false

      this.trainingservice.getregistertrainingdata(this.trainingid).subscribe(res => {
        this.resulttraining = res.filter(result => {
          return result.status == 1
        })
        this.loading = true
        this.modalRef.hide()
      })

    })
  }

  editRegisterList2(value) {
    // alert(JSON.stringify(value));
    // console.clear();
    // console.log("kkkk" + JSON.stringify(value));
    this.trainingservice.editRegisterGroup2(this.approve, this.people).subscribe(response => {
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false

      this.trainingservice.getregistertrainingdata(this.trainingid).subscribe(res => {
        this.resulttraining = res.filter(result => {
          return result.status == 1
        })
        this.loading = true
        this.modalRef.hide()
      })
      this.people = []
    })
  }

  addsPeople2(value) {
    // //console.log('item.id');
    // var subject = value.vaule
    this.people = this.addPeople(this.people, value)
    //console.log(this.people);
  }

  addPeople(array: any[], value) {
    var distinctThings: any[] = array.filter(
      (thing, i, arr) => arr.findIndex(t => t === value) === i
    );
    // //console.log('distinctThings', distinctThings);
    if (distinctThings.length != 0) {
      var s = new Set(array);
      s.delete(value);
      return Array.from(s);
    } else {
      var s = new Set(array);
      s.add(value);
      return Array.from(s);
    }
  }

  gotoBack() {
    window.history.back();
  }

  selectgroup(value, phase, array_number) {
    let p = { value: value, phaseNo: phase }
    let index = this.approve.findIndex(f => f.phaseNo == phase)
    this.approve.length > 0 ?

      (this.approve.find(result => result.phaseNo == p.phaseNo) ?
        this.approve[index].value = value :
        this.approve.push(p))
      :
      this.approve.push(p)

    console.log("value", this.approve)
    // console.log("phase", phase)
  }
}

import { Component, OnInit, TemplateRef } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CentralpolicyService } from '../services/centralpolicy.service';
import { InspectionplanService } from '../services/inspectionplan.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { IOption } from 'ng-select';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-inspection-plan',
  templateUrl: './inspection-plan.component.html',
  styleUrls: ['./inspection-plan.component.css']
})
export class InspectionPlanComponent implements OnInit {

  resultinspectionplan: any = []
  resultcentralpolicy: any = []
  inspectionplan: Array<any> = []
  id
  name: any
  modalRef: BsModalRef;
  selectdatacentralpolicy: IOption[] = []
  Form: FormGroup
  CentralpolicyId: any

  constructor(private modalService: BsModalService, private router: Router, private fb: FormBuilder, private centralpolicyservice: CentralpolicyService, private inspectionplanservice: InspectionplanService, private activatedRoute: ActivatedRoute, ) {
    this.id = activatedRoute.snapshot.paramMap.get('id')
    this.name = activatedRoute.snapshot.paramMap.get('name')
  }

  ngOnInit() {

    this.Form = this.fb.group({
      CentralpolicyId: new FormControl(null, [Validators.required])
    })

    this.getinspectionplanservice()

  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }

  Subject(id) {
    this.router.navigate(['/subject', id])
  }

  CraateInspectionPlan(id) {
    this.router.navigate(['/inspectionplan/createinspectionplan', id])
  }
  EditInspectionPlan(id: any) {
    this.router.navigate(['/inspectionplan/editinspectionplan', id])
  }
  DetailCentralPolicy(id: any) {
    this.router.navigate(['/centralpolicy/detailcentralpolicy', id])
  }
  storeCentralPolicyEventRelation(value) {
    // alert(JSON.stringify(value))
    this.inspectionplanservice.addCentralPolicyEvent(value, this.id).subscribe(response => {
      this.Form.reset()
      this.modalRef.hide()
      // this.loading = false
      // this.inspectionplanservice.getinspectionplandata(this.id).subscribe(result => {
      //   this.resultinspectionplan = result[0].centralPolicyEvents //Chose
      //   console.log(this.resultinspectionplan[0].centralPolicyEvents.centralPolicy);
      // })
      this.getinspectionplanservice()
    })
  }
  getinspectionplanservice() {
    this.inspectionplanservice.getinspectionplandata(this.id).subscribe(result => {
      this.resultinspectionplan = result[0].centralPolicyEvents //Chose

      this.centralpolicyservice.getcentralpolicydata()
        .subscribe(result => {
          this.resultcentralpolicy = result //All
          this.getRecycled()
        })
    })
  }
  getRecycled() {
    this.selectdatacentralpolicy = []
    this.inspectionplan = this.resultinspectionplan

    if (this.inspectionplan.length == 0) {
      for (var i = 0; i < this.resultcentralpolicy.length; i++) {
        this.selectdatacentralpolicy.push({ value: this.resultcentralpolicy[i].id, label: this.resultcentralpolicy[i].title })
      }
    }
    else {
      for (var i = 0; i < this.resultcentralpolicy.length; i++) {
        var n = 0;
        for (var ii = 0; ii < this.inspectionplan.length; ii++) {
          if (this.resultcentralpolicy[i].id == this.inspectionplan[ii].centralPolicyId) {
            n++;
          }
        }
        if(n == 0) {
          this.selectdatacentralpolicy.push({ value: this.resultcentralpolicy[i].id, label: this.resultcentralpolicy[i].title })
        }
      }
    }
  }
}

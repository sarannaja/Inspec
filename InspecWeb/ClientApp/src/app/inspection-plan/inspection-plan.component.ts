import { Component, OnInit, TemplateRef } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CentralpolicyService } from '../services/centralpolicy.service';
import { InspectionplanService } from '../services/inspectionplan.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { IOption } from 'ng-select';
import { FormGroup, FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-inspection-plan',
  templateUrl: './inspection-plan.component.html',
  styleUrls: ['./inspection-plan.component.css']
})
export class InspectionPlanComponent implements OnInit {

  resultinspectionplan: any = []
  resultcentralpolicy: any = []
  id
  name: any
  modalRef: BsModalRef;
  selectdatacentralpolicy: Array<IOption>
  Form: FormGroup

  constructor(private modalService: BsModalService, private router:Router, private fb: FormBuilder, private centralpolicyservice: CentralpolicyService, private inspectionplanservice: InspectionplanService, private activatedRoute : ActivatedRoute,) {
    this.id = activatedRoute.snapshot.paramMap.get('id')
    this.name = activatedRoute.snapshot.paramMap.get('name')
   }

  ngOnInit() {

    this.Form = this.fb.group({

    })

    this.inspectionplanservice.getinspectionplandata(this.id).subscribe(result => {
      this.resultinspectionplan = result[0].centralPolicyEvents
      console.log(this.resultinspectionplan[0].centralPolicyEvents);
    })

    this.centralpolicyservice.getcentralpolicydata()
    .subscribe(result => {
      this.resultcentralpolicy = result

      this.selectdatacentralpolicy = this.resultcentralpolicy.map((item,index)=>{
        return { value:item.id , label:item.title }
      })

      console.log(this.resultcentralpolicy);
    })

  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }

  CraateInspectionPlan(){
    this.router.navigate(['/inspectionplan/createinspectionplan'])
  }
  EditInspectionPlan(id:any){
    this.router.navigate(['/inspectionplan/editinspectionplan',id])
  }
  DetailCentralPolicy(id:any){
    this.router.navigate(['/centralpolicy/detailcentralpolicy',id])
  }
  storeCentralPolicyEventRelation(value){

  }
}

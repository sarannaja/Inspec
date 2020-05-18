import { Component, OnInit, TemplateRef } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CentralpolicyService } from '../services/centralpolicy.service';
import { InspectionplanService } from '../services/inspectionplan.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { IOption } from 'ng-select';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { AuthorizeService } from 'src/api-authorization/authorize.service';

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
  provinceid
  name: any
  modalRef: BsModalRef;
  selectdatacentralpolicy: IOption[] = []
  Form: FormGroup
  CentralpolicyId: any
  loading = false;
  data: any = [];
  userid: string
  dtOptions: DataTables.Settings = {};
  centralpolicyprovinceid: any
  constructor(private modalService: BsModalService, private router: Router, private fb: FormBuilder, private centralpolicyservice: CentralpolicyService, private inspectionplanservice: InspectionplanService, private activatedRoute: ActivatedRoute, private authorize: AuthorizeService, ) {
    this.id = activatedRoute.snapshot.paramMap.get('id')
    this.provinceid = activatedRoute.snapshot.paramMap.get('provinceid')
    this.name = activatedRoute.snapshot.paramMap.get('name')
  }

  ngOnInit() {
    // alert(this.provinceid)

    this.authorize.getUser()
      .subscribe(result => {
        this.userid = result.sub
        console.log(result);
        // alert(this.userid)
      })

      this.dtOptions = {
        pagingType: 'full_numbers',
        columnDefs: [
          {
            targets: [5],
            orderable: false
          }
        ]};

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
    this.inspectionplanservice.getcentralpolicyprovinceid(id, this.provinceid).subscribe(result => {
      console.log("result123", result);
      this.centralpolicyprovinceid = result
      // this.resultinspectionplan = result[0].centralPolicyEvents //Chose
      this.router.navigate(['/centralpolicy/detailcentralpolicyprovince', result])
    })
    // var id = this.centralpolicyprovinceid
    // this.router.navigate(['/centralpolicy/detailcentralpolicyprovince', id])
    // this.router.navigate(['/centralpolicy/detailcentralpolicy', id])
  }
  AcceptCentralPolicy(id: any) {
    this.router.navigate(['/acceptcentralpolicy', id])
  }
  storeCentralPolicyEventRelation(value) {
    // alert(JSON.stringify(value))
    this.inspectionplanservice.addCentralPolicyEvent(value, this.id, this.userid,this.provinceid).subscribe(response => {
      this.Form.reset()
      this.modalRef.hide()
      // this.loading = false
      // this.inspectionplanservice.getinspectionplandata(this.id).subscribe(result => {
      //   this.resultinspectionplan = result[0].centralPolicyEvents //Chose
      //   console.log(this.resultinspectionplan[0].centralPolicyEvents.centralPolicy);
      // })
      this.loading = false;
      this.data = [];
      this.getinspectionplanservice()
    })
  }

  getinspectionplanservice() {
    this.inspectionplanservice.getinspectionplandata(this.id, this.provinceid).subscribe(result => {
      console.log("result", result);

      this.resultinspectionplan = result //Chose
      let test1 = result.test;
      let test2 = result.inspectionplandata;

      let data1: any = [];
      let data2: any = [];

      test1.forEach(element => {
          data1.push(element)
      });
      console.log("Data 1: ", data1);

      test2.forEach(element => {
        element.centralPolicyEvents.forEach(element2 => {
          data2.push(element2)
        });
      });
      console.log("Data 2: ", data2);

      data1.forEach(element => {
        data2.forEach(element2 => {
          if (element2.centralPolicyId == element.centralPolicyId) {
            this.data.push(element)
          }
        });
      });
      // this.loading = true;
      console.log("RESULTS: ", this.data);
      this.inspectionplanservice.getcentralpolicydata(this.provinceid)
        .subscribe(result => {
          this.resultcentralpolicy = result //All
          this.getRecycled()
          // alert(JSON.stringify(this.resultcentralpolicy))
        })
    })
  }

  getRecycled() {
    this.selectdatacentralpolicy = []
    this.inspectionplan = this.resultinspectionplan.test

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
        if (n == 0) {
          this.selectdatacentralpolicy.push({ value: this.resultcentralpolicy[i].id, label: this.resultcentralpolicy[i].title })
        }
      }
    }
    this.loading = true;
  }
}


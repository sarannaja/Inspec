import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { DetailexecutiveorderService } from 'src/app/services/detailexecutiveorder.service';
import { ProvinceService } from 'src/app/services/province.service';
import { IOption } from 'ng-select';

@Component({
  selector: 'app-detail-executive-order',
  templateUrl: './detail-executive-order.component.html',
  styleUrls: ['./detail-executive-order.component.css']
})
export class DetailExecutiveOrderComponent implements OnInit {

  modalRef: BsModalRef;
  id: any
  text: any
  centralpolicyid: any
  provinceid: any
  resultexecutiveorder: any = []  //
  resultdetailexecutiveorder: any = []
  resultprovince: any = []
  Form: FormGroup
  provinceId: any;
  selectdataprovince:Array<IOption>
  EditForm: FormGroup;
  loading = false;
  dtOptions: DataTables.Settings = {};


  constructor(
    private modalService: BsModalService,
    private detailexecutiveorderService: DetailexecutiveorderService,
    private activatedRoute: ActivatedRoute,
    private provinceservice: ProvinceService,
    private fb: FormBuilder
  ) {
    this.id = activatedRoute.snapshot.paramMap.get('id')
  }

  ngOnInit() {

    this.Form = this.fb.group({
      "name": new FormControl(null, [Validators.required]),
      provinceId: new FormControl(null,[Validators.required])
    })

    this.getDetailExecutiveOrder()
    this.getProvine()
    
  }
  openModal(template: TemplateRef<any>) {

    this.modalRef = this.modalService.show(template);
  }

  getDetailExecutiveOrder() {
    this.detailexecutiveorderService.getdetailexecutiveorderdata(this.id)
      .subscribe(result => {
        this.resultdetailexecutiveorder = result
        console.log(this.resultdetailexecutiveorder);
        this.loading = true
      })
  }

  getProvine(){
    this.detailexecutiveorderService.getprovince(this.id)
      .subscribe(result => {
        this.resultprovince = result
        console.log("in1", this.resultprovince);
        
        this.selectdataprovince = this.resultprovince.map((item, index) => {
          return { value: item.province.id, label: item.province.name }
        })
        console.log("in2",this.selectdataprovince); 
      })
  }
  addModal(template: TemplateRef<any>, id, name) {
    this.id = id;
    this.text = name;
    this.centralpolicyid = id;
    this.provinceid = id;
    console.log(this.id);
    console.log(this.text);
    console.log(this.id);
    console.log(this.id);
    this.modalRef = this.modalService.show(template);
    this.EditForm = this.fb.group({
      "detailexecutiveordername": new FormControl(null, [Validators.required]),
      // "test" : new FormControl(null,[Validators.required,this.forbiddenNames.bind(this)])
    })
    this.EditForm.patchValue({
      "detailexecutiveordername": name
    })
    
  }
  
  storedetailexecutiveorder(value) {
    // alert(value)
    // alert(JSON.stringify(value))

    this.detailexecutiveorderService.adddetailexecutiveorder(value, this.id)
      .subscribe(result => {
        console.log("RES: ", result);

      })
    // console.log("test: ", value);
    // alert(JSON.stringify(value))
    this.modalRef.hide();
    this.Form.reset();

  }
  answerModal(template: TemplateRef<any>, id, name) {
    this.id = id;
    this.text = name;
    console.log(this.id);
    console.log(this.text);

    this.modalRef = this.modalService.show(template);
    this.EditForm = this.fb.group({
      "detailexecutiveordername": new FormControl(null, [Validators.required]),
      // "test" : new FormControl(null,[Validators.required,this.forbiddenNames.bind(this)])
    })
    this.EditForm.patchValue({
      "detailexecutiveordername": name
    })
  }
  answerdetailexecutiveorder(value) {
    this.detailexecutiveorderService.answerdetailexecutiveorder(value).subscribe(result => {
      console.log("RES: ", result);

    })
    // console.log("test: ", value);
    // alert(JSON.stringify(value))
    this.modalRef.hide();
  }
}
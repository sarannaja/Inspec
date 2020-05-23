import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { DetailexecutiveorderService } from 'src/app/services/detailexecutiveorder.service';
import { ProvinceService } from 'src/app/services/province.service';
import { IOption } from 'ng-select';
import { RequestorderService } from 'src/app/services/requestorder.service';

@Component({
  selector: 'app-detail-request-order',
  templateUrl: './detail-request-order.component.html',
  styleUrls: ['./detail-request-order.component.css']
})
export class DetailRequestOrderComponent implements OnInit {

  modalRef: BsModalRef;
  id: any
  text: any
  answerdetail: any
  answerproblem: any
  answercounsel: any
  centralpolicyid: any
  provinceid: any
  resultrequestorder: any = []  //
  resultdetailrequestorder: any = []
  resultprovince: any = []
  file: string[] = []
  Form: FormGroup
  provinceId: any;
  selectdataprovince: Array<IOption>
  EditForm: FormGroup;
  loading = false;
  dtOptions: DataTables.Settings = {};
  idAnswer: Number;

  constructor(
    private modalService: BsModalService,
    private requestorderService: RequestorderService,
    private activatedRoute: ActivatedRoute,
    private provinceservice: ProvinceService,
    private fb: FormBuilder
  ) {
    this.id = activatedRoute.snapshot.paramMap.get('id')
    this.Form = this.fb.group({
      name: [''],
      files: [null]
    })
   }

  ngOnInit() {
    this.Form = this.fb.group({
      "name": new FormControl(null, [Validators.required]),
      "AnswerDetail": new FormControl(null, [Validators.required]),
      "AnswerProblem": new FormControl(null, [Validators.required]),
      "AnswerCounsel": new FormControl(null, [Validators.required]),
      provinceId: new FormControl(null, [Validators.required]),
      files: new FormControl(null, [Validators.required]),
      files2: new FormControl(null, [Validators.required]),
    })

    this.getDetailRequestOrder()
    this.getProvine()
  }
  openModal(template: TemplateRef<any> ) {
    this.modalRef = this.modalService.show(template);
  }
  editModal(template: TemplateRef<any> , id){
    this.idAnswer = id;
    this.modalRef = this.modalService.show(template);
  }
  viewModal(template: TemplateRef<any>,id){
}
getDetailRequestOrder() {
  this.requestorderService.getdetailrequestorderdata(this.id)
    .subscribe(result => {
      this.resultdetailrequestorder = result
      console.log(this.resultdetailrequestorder);
      this.loading = true
    })
}
getProvine() {
  this.requestorderService.getprovince(this.id)
    .subscribe(result => {
      this.resultprovince = result
      console.log("in1", this.resultprovince);

      this.selectdataprovince = this.resultprovince.map((item, index) => {
        return { value: item.province.id, label: item.province.name }
      })
      console.log("in2", this.selectdataprovince);
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
    "detailrequestordername": new FormControl(null, [Validators.required]),
    // "test" : new FormControl(null,[Validators.required,this.forbiddenNames.bind(this)])
  })
  this.EditForm.patchValue({
    "detailrequestordername": name
  })
}
uploadFile(event) {
  console.log("event", event);

  const file = (event.target as HTMLInputElement).files;
  this.Form.patchValue({
    files: file
  });
  this.Form.get('files').updateValueAndValidity()
}
storedetailrequestorder(value) {
  //alert(JSON.stringify(value))
 console.log("value", this.Form.value.files)
 this.requestorderService.adddetailrequestorder(value, this.Form.value.files, this.id)
   .subscribe(result => {
     console.log("RES: ", result);

   })
 // console.log("test: ", value);
 // alert(JSON.stringify(value))
 this.modalRef.hide();
 this.Form.reset();
}
answerModal(template: TemplateRef<any>, name,id) {
  this.id = id;
  this.answerdetail = name;
  this.answerproblem = name;
  this.answercounsel = name;
  console.log(this.answerdetail);
  console.log(this.answerproblem);
  console.log(this.answercounsel);
  this.modalRef = this.modalService.show(template);
  this.EditForm = this.fb.group({
    "detailrequestordername": new FormControl(null, [Validators.required]),
    // "test" : new FormControl(null,[Validators.required,this.forbiddenNames.bind(this)])
  })
  this.EditForm.patchValue({
    "detailrequestordername": name
  })
}
uploadFile2(event) {
  console.log("event", event);

  const file = (event.target as HTMLInputElement).files;
  this.Form.patchValue({
    files: file
  });
  this.Form.get('files2').updateValueAndValidity()
}
storeanswerrequestorder(value) {
  //alert(this.idAnswer)
  // alert(JSON.stringify(value))
  console.log("value", this.Form.value.files)

  this.requestorderService.answerrequestorder(value, this.Form.value.files , this.idAnswer)
    .subscribe(result => {
      console.log("RES: ", result);

    })
  // console.log("test: ", value);
  // alert(JSON.stringify(value))
  this.modalRef.hide();
  this.Form.reset();
}

}
import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { SubquestionService } from '../services/subquestion.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-subquestion',
  templateUrl: './subquestion.component.html',
  styleUrls: ['./subquestion.component.css']
})
export class SubquestionComponent implements OnInit {

  resultsubquestion: any = []
  id
  delid: any
  name: any
  modalRef: BsModalRef;
  // router: any
  Form: FormGroup;

  constructor(
    private modalService: BsModalService,
    private fb: FormBuilder,
    private subquestionservice: SubquestionService,
    private activatedRoute : ActivatedRoute,
    private router:Router,
    public share: SubquestionService) {
    this.id = activatedRoute.snapshot.paramMap.get('id')
    this.name = activatedRoute.snapshot.paramMap.get('name')
  }

  ngOnInit() {
    this.Form = this.fb.group({
      "name": new FormControl(null, [Validators.required]),
    })

    // alert(this.name)

    this.subquestionservice.getsubquestiondata(this.id).subscribe(result => {
      this.resultsubquestion = result
      console.log(this.resultsubquestion);
    })
  }

  openModal(template: TemplateRef<any>, id, name) {
    this.delid = id;
    this.name = name;
    console.log(this.delid);
    console.log(this.name);

    this.modalRef = this.modalService.show(template);
  }

  storeSubquestion(value) {
    this.subquestionservice.addSubquestion(value, this.id).subscribe(response => {
      console.log(value);
      this.Form.reset()
      this.modalRef.hide()
      this.subquestionservice.getsubquestiondata(this.id).subscribe(result => {
        this.resultsubquestion = result
        console.log(this.resultsubquestion);
      })
    })
  }
  deleteSubquestion(value) {
    this.subquestionservice.deleteSubquestion(value).subscribe(response => {
      console.log(value);
      this.modalRef.hide()
      this.subquestionservice.getsubquestiondata(this.id).subscribe(result => {
        this.resultsubquestion = result
        console.log(this.resultsubquestion);
      })
    })
  }
}

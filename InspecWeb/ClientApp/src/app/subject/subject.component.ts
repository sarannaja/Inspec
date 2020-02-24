import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { SubjectService } from '../services/subject.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-subject',
  templateUrl: './subject.component.html',
  styleUrls: ['./subject.component.css']
})
export class SubjectComponent implements OnInit {

  resultsubject: any = []
  id
  delid: any
  name: any
  modalRef: BsModalRef;
  // router: any
  Form: FormGroup;

  constructor(
    private modalService: BsModalService,
    private fb: FormBuilder,
    private subjectservice: SubjectService,
    private activatedRoute : ActivatedRoute,
    private router:Router,
    public share: SubjectService) {
    this.id = activatedRoute.snapshot.paramMap.get('id')
    this.name = activatedRoute.snapshot.paramMap.get('name')
  }

  ngOnInit() {
    this.Form = this.fb.group({
      "name": new FormControl(null, [Validators.required]),
    })

    // alert(this.name)

    this.subjectservice.getsubjectdata(this.id).subscribe(result => {
      this.resultsubject = result
      console.log(this.resultsubject);
    })
  }
  Subquestion(id){
    this.router.navigate(['/subquestion',id])
  }

  openModal(template: TemplateRef<any>, id, name) {
    this.delid = id;
    this.name = name;
    console.log(this.delid);
    console.log(this.name);

    this.modalRef = this.modalService.show(template);
  }

  storeSubject(value) {
    this.subjectservice.addSubject(value, this.id).subscribe(response => {
      console.log(value);
      this.Form.reset()
      this.modalRef.hide()
      this.subjectservice.getsubjectdata(this.id).subscribe(result => {
        this.resultsubject = result
        console.log(this.resultsubject);
      })
    })
  }
  deleteSubject(value) {
    this.subjectservice.deleteSubject(value).subscribe(response => {
      console.log(value);
      this.modalRef.hide()
      this.subjectservice.getsubjectdata(this.id).subscribe(result => {
        this.resultsubject = result
        console.log(this.resultsubject);
      })
    })
  }
}

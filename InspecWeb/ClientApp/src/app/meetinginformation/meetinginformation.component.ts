import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { MeetinginformationService } from '../services/meetinginformation.service';

@Component({
  selector: 'app-meetinginformation',
  templateUrl: './meetinginformation.component.html',
  styleUrls: ['./meetinginformation.component.css']
})
export class MeetinginformationComponent implements OnInit {
  resultmeetinginformation: any = []
  delid: any
  year: any
  title: any
  modalRef:BsModalRef;
  Form : FormGroup
  files: string[] = []
  loading = false;
  dtOptions: DataTables.Settings = {};

  constructor(private modalService: BsModalService, private fb: FormBuilder, private meetinginformationservice: MeetinginformationService,
    public share: MeetinginformationService) { }

  ngOnInit() {
    this.dtOptions = {
      pagingType: 'full_numbers',
      

    };
    this.meetinginformationservice.getmeetinginformation().subscribe(result=>{
    this.resultmeetinginformation = result
    this.loading = true
    })
    this.Form = this.fb.group({
      title: new FormControl(null, [Validators.required]),
      year: new FormControl(null, [Validators.required]),
      files : new FormControl(null, [Validators.required])
    })
  }
  openModal(template: TemplateRef<any>, id, year,title) {
    this.delid = id;
    this.title = title;
    this.year = year;
    console.log(this.delid);
    console.log(this.title);
    console.log(this.year);
    
    this.modalRef = this.modalService.show(template);
  }
  uploadFile(event) {
    const file = (event.target as HTMLInputElement).files;
    this.Form.patchValue({
      files: file
    });
    this.Form.get('files').updateValueAndValidity()

  }

  storeMeetinginformation(value) {
    // alert(JSON.stringify(value));
    this.meetinginformationservice.addMeetinginformation(value, this.Form.value.files).subscribe(response => {
      console.log(value);
      this.Form.reset()
      this.modalRef.hide()
      this.meetinginformationservice.getmeetinginformation().subscribe(result => {
        this.resultmeetinginformation = result
        console.log(this.resultmeetinginformation);
      })
    })
  }
  deleteMeetinginformation(value) {
    this.meetinginformationservice.deleteMeetinginformation(value).subscribe(response => {
      console.log(value);
      this.modalRef.hide()
      this.meetinginformationservice.getmeetinginformation().subscribe(result => {
        this.resultmeetinginformation = result
        console.log(this.resultmeetinginformation);
      })
    })
  }
  editMeetinginformation(value,delid) {
    console.log(value);
    this.meetinginformationservice.editMeetinginformation(value,delid).subscribe(response => {
      this.Form.reset()
      this.modalRef.hide()
      this.meetinginformationservice.getmeetinginformation().subscribe(result => {
        this.resultmeetinginformation = result
       
      })
    })
  }

}


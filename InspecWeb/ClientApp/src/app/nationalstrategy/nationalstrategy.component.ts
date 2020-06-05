import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { NationalstrategyService } from '../services/nationalstrategy.service';

@Component({
  selector: 'app-nationalstrategy',
  templateUrl: './nationalstrategy.component.html',
  styleUrls: ['./nationalstrategy.component.css']
})
export class NationalstrategyComponent implements OnInit {
  resultnationalstrategy: any = []
  delid: any
  title: any
  link: any
  modalRef:BsModalRef;
  Form : FormGroup
  files: string[] = []
  loading = false;
  dtOptions: DataTables.Settings = {};

  constructor(private modalService: BsModalService, private fb: FormBuilder, private nationalstrategyservice: NationalstrategyService,
    public share: NationalstrategyService) { }

  ngOnInit() {
    this.dtOptions = {
      pagingType: 'full_numbers',
      columnDefs: [
        {
          targets: [5],
          orderable: false
        }
      ]

    };
    this.nationalstrategyservice.getnationalstrategy().subscribe(result=>{
    this.resultnationalstrategy = result
    this.loading = true
    })
    this.Form = this.fb.group({
      title: new FormControl(null, [Validators.required]),
      link: new FormControl(null, [Validators.required]),
      files : new FormControl(null, [Validators.required])
    })
  }
  openModal(template: TemplateRef<any>, id, title, link) {
    this.delid = id;
    this.title = title;
    this.link = link;
    console.log(this.delid);
    console.log(this.title);
    console.log(this.link);
    
    this.modalRef = this.modalService.show(template);
  }
  uploadFile(event) {
    const file = (event.target as HTMLInputElement).files;
    this.Form.patchValue({
      files: file
    });
    this.Form.get('files').updateValueAndValidity()

  }

  storeNationalstrategy(value) {
    // alert(JSON.stringify(value));
    this.nationalstrategyservice.addNationalstrategy(value, this.Form.value.files).subscribe(response => {
      console.log(value);
      this.Form.reset()
      this.modalRef.hide()
      this.nationalstrategyservice.getnationalstrategy().subscribe(result => {
        this.resultnationalstrategy = result
        console.log(this.resultnationalstrategy);
      })
    })
  }
  //  deleteNationalstrategy(value) {
  //    this.nationalstrategyservice.deleteNationalstrategy(value).subscribe(response => {
  //      console.log(value);
  //      this.modalRef.hide()
  //      this.nationalstrategyservice.getnationalstrategy().subscribe(result => {
  //        this.resultnationalstrategy = result
  //        console.log(this.resultnationalstrategy);
  //      })
  //    })
  //  }
  // editNationalstrategy(value,delid) {
  //   console.log(value);
  //   this.nationalstrategyservice.editNationalstrategy(value,delid).subscribe(response => {
  //     this.Form.reset()
  //     this.modalRef.hide()
  //     this.nationalstrategyservice.getnationalstrategy().subscribe(result => {
  //       this.resultnationalstrategy = result
       
  //     })
  //   })
  // }

}


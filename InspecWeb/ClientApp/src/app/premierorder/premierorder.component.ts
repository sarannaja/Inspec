import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { PremierorderService } from '../services/premierorder.service';

@Component({
  selector: 'app-premierorder',
  templateUrl: './premierorder.component.html',
  styleUrls: ['./premierorder.component.css']
})
export class PremierorderComponent implements OnInit {
  resultpremierorder: any = []
  delid: any
  year: any
  title: any
  modalRef:BsModalRef;
  Form : FormGroup
  files: string[] = []
  loading = false;
  dtOptions: DataTables.Settings = {};

  constructor(private modalService: BsModalService, private fb: FormBuilder, private premierorderservice: PremierorderService,
    public share: PremierorderService) { }

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
    this.premierorderservice.getpremierorder().subscribe(result=>{
    this.resultpremierorder = result
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

  storePremierorder(value) {
    // alert(JSON.stringify(value));
    this.premierorderservice.addPremierorder(value, this.Form.value.files).subscribe(response => {
      console.log(value);
      this.Form.reset()
      this.modalRef.hide()
      this.premierorderservice.getpremierorder().subscribe(result => {
        this.resultpremierorder = result
        console.log(this.resultpremierorder);
      })
    })
  }
  deletePremierorder(value) {
    this.premierorderservice.deletePremierorder(value).subscribe(response => {
      console.log(value);
      this.modalRef.hide()
      this.premierorderservice.getpremierorder().subscribe(result => {
        this.resultpremierorder = result
        console.log(this.resultpremierorder);
      })
    })
  }
  editPremierorder(value,delid) {
    console.log(value);
    this.premierorderservice.editPremierorder(value,delid).subscribe(response => {
      this.Form.reset()
      this.modalRef.hide()
      this.premierorderservice.getpremierorder().subscribe(result => {
        this.resultpremierorder = result
       
      })
    })
  }

}

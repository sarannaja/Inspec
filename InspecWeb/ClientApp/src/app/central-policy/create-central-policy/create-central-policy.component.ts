import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators, FormArray } from '@angular/forms';
import { CentralpolicyService } from 'src/app/services/centralpolicy.service';
import { IMyOptions } from 'mydatepicker-th';

interface addInput {
  id: number;
  Name: string;
}

@Component({
  selector: 'app-create-central-policy',
  templateUrl: './create-central-policy.component.html',
  styleUrls: ['./create-central-policy.component.css']
})
export class CreateCentralPolicyComponent implements OnInit {
  @ViewChild("fileUpload", { static: true }) fileUpload: ElementRef
  private myDatePickerOptions: IMyOptions = {
    // other options...
    dateFormat: 'dd/mm/yyyy',
  };

  // Initialized to specific date (09.10.2018).
  // private model: Object = { date: { year: 2018, month: 10, day: 9 } };
  files: string[] = []
  resultcentralpolicy: any = []
  id: any = 1
  Form: FormGroup
  start_date: Date
  input: Array<addInput> = [{ id: 1, Name: '' }]
  name: string = ''
  constructor(private fb: FormBuilder, private centralpolicyservice: CentralpolicyService,
    public share: CentralpolicyService) { }
  // files:FileList
  ngOnInit() {
    this.Form = this.fb.group({
      title: new FormControl(null, [Validators.required]),
      start_date: new FormControl(null, [Validators.required]),
      end_date: new FormControl(null, [Validators.required]),
      subjects: this.fb.array([]),
      files: new FormControl(null, [Validators.required]),
    })

    this.Form.patchValue({
      // กรณีจะแก้ไข
    })
    // this.addInput()
  }

  storeCentralpolicy() {
    // alert(JSON.stringify(value))
    // this.centralpolicyservice.addCentralpolicy(value).subscribe(response => {
    //   console.log(value);
    //   this.Form.reset()
    //   this.centralpolicyservice.getcentralpolicydata().subscribe(result => {
    //     this.resultcentralpolicy = result
    //     console.log(this.resultcentralpolicy);
    //   })
    // })

    const creds = this.Form.controls.subjects as FormArray;
    this.input.forEach((item, index) => {
      creds.push(this.fb.group(item))
    })

    const formData = new FormData();

    formData.append('title', this.Form.value.title);
    formData.append('start_date', this.Form.value.start_date.date.year + '-' + this.Form.value.start_date.date.month + '-' + this.Form.value.start_date.date.day);
    formData.append('end_date', this.Form.value.end_date.date.year + '-' + this.Form.value.end_date.date.month + '-' + this.Form.value.end_date.date.day);
    // formData.append('subjects', this.Form.value.subjects);

    // for(let i = 0; i< this.files.length ; i++){
    //   formData.append('files',this.files[i] );
    // }
    console.log(this.Form.value);
    // alert(JSON.stringify(this.Form.value))
    this.centralpolicyservice.addCentralpolicy(formData)
      .subscribe(result => {
        console.log(result);

      })
  }


  append() {
    let id = this.id + 1
    this.id = id
    this.input.push({
      id: id,
      Name: ''
    });

  }
  addInput() {
    // this.Form.reset('subjects')
    const creds = this.Form.controls.subjects as FormArray;
    this.input.forEach((item, index) => {
      creds.push(this.fb.group(item))
    })
    console.log(this.Form.value);
    alert(JSON.stringify(this.Form.value))

    // for (let iii = 0; iii <= this.input.length; iii++) {
    //   creds.push(this.fb.group({ id: this.input[iii].id, name: this.input[iii].name }));
    // }
    // console.log(this.Form.value);



  }
  toInput(event, index) {
    this.input[index].Name = event.target.value
    console.log(this.input);
    // var obj = this.input.filter((item, index) => {
    //   if (item.id == id) {
    //     item.name = event.target.value
    //   }
    //   return item
    // })
    // this.input = obj
  }
  addFile(event) {
    this.files = event.target.files
    // console.log(event.target.files);

  }
  // onClick(){
  //   const fileUpload = this.fileUpload.nativeElement

  //   fileUpload.onchange = () => {
  //     for(let i =0; i<=fileUpload.files.length; i++){
  //       var reader = new FileReader();
  //       const file = fileUpload.files[i]
  //       reader.onload =(event)=>{
  //         this.files.push({data:file,inProgress:false,progress:0,url:event.target.result})
  //       }
  //       reader.readAsDataURL(file)
  //     }
  //   }
  //   fileUpload.click()
  // }
}

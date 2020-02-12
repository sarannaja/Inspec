import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { CentralpolicyService } from 'src/app/services/centralpolicy.service';
import { IMyOptions } from 'mydatepicker-th';

@Component({
  selector: 'app-create-central-policy',
  templateUrl: './create-central-policy.component.html',
  styleUrls: ['./create-central-policy.component.css']
})
export class CreateCentralPolicyComponent implements OnInit {

  private myDatePickerOptions: IMyOptions = {
    // other options...
    dateFormat: 'dd/mm/yyyy',
};

  // Initialized to specific date (09.10.2018).
  // private model: Object = { date: { year: 2018, month: 10, day: 9 } };

  resultcentralpolicy: any = []
  id: any
  Form: FormGroup
  start_date:Date
  input:Array<any> = [
    {
      id:1
    }
  ]
  constructor(private fb: FormBuilder, private centralpolicyservice: CentralpolicyService,
    public share: CentralpolicyService) { }

  ngOnInit() {
    this.Form = this.fb.group({
      "title": new FormControl(null, [Validators.required]),
      "start_date": new FormControl(null, [Validators.required]),
      "end_date": new FormControl(null, [Validators.required]),
      "subjects": new FormControl(null, [Validators.required]),
      "files": new FormControl(null, [Validators.required]),
    })

    this.Form.patchValue({
      // กรณีจะแก้ไข
    })
  }
  Start_date(event){
    console.log(event);

  }

  storeCentralpolicy(value) {
    alert(JSON.stringify(value))
    // this.centralpolicyservice.addCentralpolicy(value).subscribe(response => {
    //   console.log(value);
    //   this.Form.reset()
    //   this.centralpolicyservice.getcentralpolicydata().subscribe(result => {
    //     this.resultcentralpolicy = result
    //     console.log(this.resultcentralpolicy);
    //   })
    // })
  }

  append()  {
    this.input.push({
      id:2
    });
  }
}

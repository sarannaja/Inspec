import { Component, OnInit } from '@angular/core';
import { IMyOptions } from 'mydatepicker-th';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { InspectionplaneventService } from 'src/app/services/inspectionplanevent.service';

@Component({
  selector: 'app-create-inspection-plan-event',
  templateUrl: './create-inspection-plan-event.component.html',
  styleUrls: ['./create-inspection-plan-event.component.css']
})
export class CreateInspectionPlanEventComponent implements OnInit {

  private myDatePickerOptions: IMyOptions = {
    // other options...
    dateFormat: 'dd/mm/yyyy',
  };

  files: string[] = []
  resultinspectionplan: any = []
  delid: any
  title: any
  start_date: any
  end_date: any
  Form: FormGroup;

  constructor(private fb: FormBuilder, private router: Router, private inspectionplanservice: InspectionplaneventService,) { }

  ngOnInit() {
    this.Form = this.fb.group({
      title: new FormControl(null, [Validators.required]),
      start_date: new FormControl(null, [Validators.required]),
      end_date: new FormControl(null, [Validators.required]),
    })
  }

  storeInspectionPlanEvent(value) {
    this.inspectionplanservice.addInspectionplanevent(value).subscribe(response => {
      console.log(value);
      this.Form.reset()
      this.router.navigate(['inspectionplanevent'])
    })
  }
}

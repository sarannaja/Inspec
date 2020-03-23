import { Component, OnInit } from '@angular/core';
import { IMyOptions } from 'mydatepicker-th';
import { FormGroup, FormBuilder, FormControl, Validators, FormArray } from '@angular/forms';
import { Router } from '@angular/router';
import { InspectionplaneventService } from 'src/app/services/inspectionplanevent.service';
import { ProvinceService } from 'src/app/services/province.service';
import { IOption } from 'ng-select';

interface addInput {
  id: number;
  name: string;
}

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
  resultprovince: any = []
  delid: any
  title: any
  start_date: any
  end_date: any
  Form: FormGroup;
  selectdataprovince: Array<IOption>
  id: any = 1
  input: any = [{ date: '', province: '' }]


  constructor(private fb: FormBuilder, private router: Router, private inspectionplaneventservice: InspectionplaneventService, private provinceservice: ProvinceService) { }

  ngOnInit() {
    this.Form = this.fb.group({
      title: new FormControl(null, [Validators.required]),
      input: new FormArray([])
      // start_date: new FormControl(null, [Validators.required]),
      // end_date: new FormControl(null, [Validators.required]),
    });
    this.t.push(this.fb.group({
      date: '',
      provinces: []
    }))
    this.provinceservice.getprovincedata().subscribe(result => {
      this.resultprovince = result

      this.selectdataprovince = this.resultprovince.map((item, index) => {
        return { value: item.id, label: item.name }
      })

      console.log(this.resultprovince);
    })

  }

  get f() { return this.Form.controls }
  get t() { return this.f.input as FormArray }

  // get y(){return this.t.controls}
  // get p(){return this.y.provinces as FormArray}


  storeInspectionPlanEvent(value) {
    // alert(JSON.stringify(value))
    this.inspectionplaneventservice.addInspectionplanevent(value).subscribe(response => {
      console.log(value);
      this.Form.reset()
      this.router.navigate(['inspectionplanevent'])
    })
  }

  append() {
    this.t.push(this.fb.group({
      date: '',
      provinces: []
    }))
    // this.input.push({
    //   date: '',
    //   province: ''
    // });
  }
}

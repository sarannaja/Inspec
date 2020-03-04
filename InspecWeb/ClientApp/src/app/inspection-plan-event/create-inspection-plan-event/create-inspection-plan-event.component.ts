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
  input: any =  [{ date: '', name: '' }]


  constructor(private fb: FormBuilder, private router: Router, private inspectionplanservice: InspectionplaneventService, private provinceservice: ProvinceService) { }

  ngOnInit() {
    this.Form = this.fb.group({
      title: new FormControl(null, [Validators.required]),
      input: new FormArray([])
      // start_date: new FormControl(null, [Validators.required]),
      // end_date: new FormControl(null, [Validators.required]),
    });



    this.provinceservice.getprovincedata().subscribe(result => {
      this.resultprovince = result

      this.selectdataprovince = this.resultprovince.map((item,index)=>{
        return { value:item.id , label:item.name }
      })

      console.log(this.resultprovince);
    })

  }

  get f() {return this.Form.controls}
  get t() {return this.f.input as FormArray}

  addvalues(){



        this.t.push(this.fb.group({
          //  date: this.input[i].date,
          //  name: this.input[i].name
        }))

  }

  storeInspectionPlanEvent(value) {
    alert(JSON.stringify(value))
    this.inspectionplanservice.addInspectionplanevent(value).subscribe(response => {
      console.log(value);
      this.Form.reset()
      this.router.navigate(['inspectionplanevent'])
    })
  }

    append() {

    this.input.push({
      date: '',
      name: ''
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
}

import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators, FormArray } from '@angular/forms';
import { CentralpolicyService } from 'src/app/services/centralpolicy.service';
import { IMyOptions } from 'mydatepicker-th';
import { Router } from '@angular/router';
import { FiscalyearService } from 'src/app/services/fiscalyear.service';
import { ProvinceService } from 'src/app/services/province.service';
import { IOption } from 'ng-select';

interface addInput {
  id: number;
  name: string;
}

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
  files: string[] = []
  resultcentralpolicy: any = []
  resultfiscalyear: any = []
  resultprovince: any = []
  delid: any
  title: any
  start_date: any
  end_date: any
  Form: FormGroup;
  selectdataprovince: Array<IOption>

  constructor(private fb: FormBuilder, private centralpolicyservice: CentralpolicyService,
    public share: CentralpolicyService, private router: Router, private fiscalyearservice: FiscalyearService,
    private provinceservice: ProvinceService) { }

  ngOnInit() {
    this.Form = this.fb.group({
      title: new FormControl(null, [Validators.required]),
      start_date: new FormControl(null, [Validators.required]),
      end_date: new FormControl(null, [Validators.required]),
      files: new FormControl(null, [Validators.required]),
      // "test" : new FormControl(null,[Validators.required,this.forbiddenNames.bind(this)])
    })

    //แก้ไข

    this.provinceservice.getprovincedata().subscribe(result => {
      this.resultprovince = result

      this.selectdataprovince = this.resultprovince.map((item,index)=>{
        return { value:item.id , label:item.name }
      })

      console.log(this.resultprovince);
    })

    this.fiscalyearservice.getfiscalyeardata().subscribe(result => {
      this.resultfiscalyear = result
      console.log(this.resultcentralpolicy);
    })


    this.Form.patchValue({
      // กรณีจะแก้ไข
    })
    // this.addInput()
  }

  storeCentralpolicy(value) {
    // alert(JSON.stringify(value))
    this.centralpolicyservice.addCentralpolicy(value).subscribe(response => {
      console.log(value);
      this.Form.reset()
      this.router.navigate(['centralpolicy'])
      // this.centralpolicyservice.getcentralpolicydata().subscribe(result => {
      //   this.centralpolicyservice = result
      //   console.log(this.resultcentralpolicy);
      // })
    })
  }

  addFile(event) {
    this.files = event.target.files
  }


  // append() {
  //   let id = this.id + 1
  //   this.id = id
  //   this.input.push({
  //     id: id,
  //     name: ''
  //   });

  // }
  // addInput() {
  //   // this.Form.reset('subjects')
  //   const creds = this.Form.controls.subjects as FormArray;
  //   this.input.forEach((item, index) => {
  //     creds.push(this.fb.group(item))
  //   })
  //   console.log(this.Form.value);
  //   alert(JSON.stringify(this.Form.value))

  //   // for (let iii = 0; iii <= this.input.length; iii++) {
  //   //   creds.push(this.fb.group({ id: this.input[iii].id, name: this.input[iii].name }));
  //   // }
  //   // console.log(this.Form.value);



  // }
  // toInput(event, index) {
  //   this.input[index].name = event.target.value
  //   console.log(this.input);
  //   // var obj = this.input.filter((item, index) => {
  //   //   if (item.id == id) {
  //   //     item.name = event.target.value
  //   //   }
  //   //   return item
  //   // })
  //   // this.input = obj
  // }
}

import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, Validators, FormGroup } from '@angular/forms';
import { TrainingService } from 'src/app/services/training.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create-training',
  templateUrl: './create-training.component.html',
  styleUrls: ['./create-training.component.css']
})
export class CreateTrainingComponent implements OnInit {

  name: any
  detail: any
  start_date: any
  end_date: any
  lecturer_name: any
  regis_start_date: any
  regis_end_date:any
  Form: FormGroup;
  files: string[] = []

  constructor(private fb: FormBuilder, private trainingservice: TrainingService,
    public share: TrainingService, private router: Router) { }

  ngOnInit() {
    this.Form = this.fb.group({
      name: new FormControl(null, [Validators.required]),
      detail: new FormControl(null, [Validators.required]),
      start_date: new FormControl(null, [Validators.required]),
      end_date: new FormControl(null, [Validators.required]),
      lecturer_name: new FormControl(null, [Validators.required]),
      regis_start_date: new FormControl(null, [Validators.required]),
      regis_end_date: new FormControl(null, [Validators.required]),
      files: new FormControl(null, [Validators.required]),
      // "test" : new FormControl(null,[Validators.required,this.forbiddenNames.bind(this)])
    })
  }
  storeTraining(value) {
    // alert(JSON.stringify(value))
    this.trainingservice.addTraining(value).subscribe(response => {
      console.log(value);
      this.Form.reset()
      this.router.navigate(['training'])
      // this.centralpolicyservice.getcentralpolicydata().subscribe(result => {
      //   this.centralpolicyservice = result
      //   console.log(this.resultcentralpolicy);
      // })
    })
  }

  addFile(event) {
    this.files = event.target.files
  }
}

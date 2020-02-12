import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-create-inspection-plan',
  templateUrl: './create-inspection-plan.component.html',
  styleUrls: ['./create-inspection-plan.component.css']
})
export class CreateInspectionPlanComponent implements OnInit {
  input:Array<any> = [
    {
      id:1
    }
  ]

  input2:Array<any> = [
    {
      id:1
    }
  ]

  constructor() { }

  ngOnInit() {
  }

  append()  {
    this.input.push({
      id:2
    });
  }
  append2()  {
    this.input2.push({
      id:2
    });
  }

}

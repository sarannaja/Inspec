import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-create-central-policy',
  templateUrl: './create-central-policy.component.html',
  styleUrls: ['./create-central-policy.component.css']
})
export class CreateCentralPolicyComponent implements OnInit {

  input:Array<any> = [
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
}

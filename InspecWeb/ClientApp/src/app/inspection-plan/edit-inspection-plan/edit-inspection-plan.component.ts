import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-edit-inspection-plan',
  templateUrl: './edit-inspection-plan.component.html',
  styleUrls: ['./edit-inspection-plan.component.css']
})
export class EditInspectionPlanComponent implements OnInit {

  id:any
  constructor(private route:ActivatedRoute) { }

  ngOnInit() {
    this.id = this.route.snapshot.paramMap.get('id')
  }

}

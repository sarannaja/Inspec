import { Component, OnInit } from '@angular/core';
import { Ministers } from '../../../models/otps';

@Component({
  selector: 'app-minister-modal',
  templateUrl: './minister-modal.component.html',
  styleUrls: ['./minister-modal.component.css']
})
export class MinisterModalComponent implements OnInit {
  title;
  minister:Ministers
  constructor() { }

  ngOnInit() {
    console.log(this.minister);
    
  }

}

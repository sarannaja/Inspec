import { Component, OnInit, Input } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-opmdetail-title-modal',
  templateUrl: './title-modal.component.html',
  styleUrls: ['./title-modal.component.css']
})
export class OpmDetailTitleModalComponent implements OnInit {
  @Input() title:string;
  constructor(public modalRef: BsModalRef) { }

  ngOnInit() {
  }

}

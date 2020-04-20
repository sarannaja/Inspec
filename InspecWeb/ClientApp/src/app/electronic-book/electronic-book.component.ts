import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-electronic-book',
  templateUrl: './electronic-book.component.html',
  styleUrls: ['./electronic-book.component.css']
})
export class ElectronicBookComponent implements OnInit {

  constructor(
    private router: Router,
  ) { }

  ngOnInit() {
  }

  createElectronicBook() {
    this.router.navigate(['/electronicbook/create'])
  }

}

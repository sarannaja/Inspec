import { Component, OnInit } from '@angular/core';
import { ExampleService } from '../services/example.service';

@Component({
  selector: 'app-example',
  templateUrl: './example.component.html',
  styleUrls: ['./example.component.css']
})
export class ExampleComponent implements OnInit {
  data: any = {
   
  }
  constructor(private example: ExampleService) { }

  ngOnInit() {
    alert("example")
    this.postData({ name: 'example',
    array: [{ name: 'palm', subjects: 'a' }, { name: 'yo', subjects: 'a' }],
    obj: { name: 'palm', subjects: 'a' }})

  }

  postData(data) {
    console.log(data);
    
    this.example.sendExample(data)
      .subscribe(result => {
        console.log(result);

      })
  }
}





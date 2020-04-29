import { Component, OnInit } from '@angular/core';
import { AuthorizeService } from 'src/api-authorization/authorize.service';

@Component({
  selector: 'app-answer-subject',
  templateUrl: './answer-subject.component.html',
  styleUrls: ['./answer-subject.component.css']
})
export class AnswerSubjectComponent implements OnInit {

  userid: string

  constructor(
    private authorize: AuthorizeService,
  ) { }

  ngOnInit() {
    this.authorize.getUser()
    .subscribe(result => {
      this.userid = result.sub
      console.log(result);
      alert(this.userid)
    })
  }

}

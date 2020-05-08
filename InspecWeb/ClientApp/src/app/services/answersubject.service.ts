import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AnswersubjectService {

  url = "";

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) 
  {   
    this.url = baseUrl + 'api/answersubject/';
  }
  getuseredata(id):Observable<any[]> {
    return this.http.get<any[]>(this.url + id)
  }
}

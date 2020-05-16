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
    return this.http.get<any[]>(this.url+"nik/" + id)
  }
  getsubjectlistdata(id):Observable<any[]>{
    return this.http.get<any[]>(this.url+"subjectlist/" + id)
  }
  getsubjectdetaildata(id):Observable<any[]>{
    return this.http.get<any[]>(this.url+"subjectdetail/" + id)
  }
}

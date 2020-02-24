import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class SubquestionService {

  url = "";

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.url = baseUrl + 'api/subquestion/';
  }
  getsubquestiondata(id) {
    console.log(id);
    return this.http.get(this.url+id)
  }
  addSubquestion(subquestionData, subjectid) {
    const formData = new FormData();

    formData.append('name', subquestionData.name);
    formData.append('subjectid', subjectid);

    console.log('FORMDATA: ' + formData.get("name"));
    return this.http.post(this.url, formData);
  }

  deleteSubquestion(id) {
    return this.http.delete(this.url + id);
  }
}

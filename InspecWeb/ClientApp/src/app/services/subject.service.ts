import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class SubjectService {

  url = "";

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.url = baseUrl + 'api/subject/';
  }
  getsubjectdata(id) {
    console.log(id);
    return this.http.get(this.url+id)
  }
  addSubject(subjectData, centralpolicyid) {
    const formData = new FormData();

    formData.append('name', subjectData.name);
    formData.append('start_date', subjectData.start_date.date.year + '-' + subjectData.start_date.date.month + '-' + subjectData.start_date.date.day);
    formData.append('end_date', subjectData.end_date.date.year + '-' + subjectData.end_date.date.month + '-' + subjectData.end_date.date.day);
    formData.append('centralpolicyid', centralpolicyid);

    console.log('FORMDATA: ' + formData.get("name"));
    return this.http.post(this.url, formData);
  }

  deleteSubject(id) {
    return this.http.delete(this.url + id);
  }
}

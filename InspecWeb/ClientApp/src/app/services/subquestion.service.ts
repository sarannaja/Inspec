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
    return this.http.get(this.url + id)
  }
  addSubquestionopen(subquestionData) {
    const formData = new FormData();
    formData.append('SubjectCentralPolicyProvinceId', subquestionData.subjectId);
    formData.append('Name', subquestionData.name);
    formData.append('Box', subquestionData.box);
    for (var i = 0; i < subquestionData.ProvincialDepartmentId.length; i++) {
      formData.append('departmentId', subquestionData.ProvincialDepartmentId[i].ProvincialDepartmentId);
    }
    // console.log('FORMDATA: ' + formData.get("name"));
    return this.http.post(this.url + "addquestionopen", formData);
  }
  addSubquestionclose(subquestionData) {
    console.log("subquestionData", subquestionData);

    const formData = new FormData();
    formData.append('SubjectCentralPolicyProvinceId', subquestionData.subjectId);
    formData.append('Name', subquestionData.name);
    formData.append('Box', subquestionData.box);
    for (var i = 0; i < subquestionData.ProvincialDepartmentId.length; i++) {
      formData.append('departmentId', subquestionData.ProvincialDepartmentId[i].ProvincialDepartmentId);
    }
    for (var ii = 0; ii < subquestionData.inputanswerclose.length; ii++) {
      formData.append('answerclose', subquestionData.inputanswerclose[ii].answerclose);
    }
    // console.log('FORMDATA: ' + formData.get("name"));
    return this.http.post(this.url + "addquestionclose", formData);
  }
  addSubquestioncloseevent(subquestionData) {
    console.log("subquestionData", subquestionData);

    const formData = new FormData();
    formData.append('SubjectCentralPolicyProvinceId', subquestionData.subjectId);
    formData.append('Name', subquestionData.name);
    formData.append('Box', subquestionData.box);
    // for (var i = 0; i < subquestionData.ProvincialDepartmentId.length; i++) {
    //   formData.append('departmentId', subquestionData.ProvincialDepartmentId[i].ProvincialDepartmentId);
    // }
    for (var ii = 0; ii < subquestionData.inputanswerclose.length; ii++) {
      formData.append('answerclose', subquestionData.inputanswerclose[ii].answerclose);
    }
    // console.log('FORMDATA: ' + formData.get("name"));
    return this.http.post(this.url + "addquestioncloseevent", formData);
  }
  deleteSubquestion(id) {
    return this.http.delete(this.url + id);
  }
}

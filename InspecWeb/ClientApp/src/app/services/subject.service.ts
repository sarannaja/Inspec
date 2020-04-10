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
    return this.http.get(this.url + id)
  }
  addSubject(subjectData, centralpolicyid) {

    console.log("ARRAY: ", subjectData.centralpolicydateid);
    const formData = {
      Name: subjectData.name,
      Answer: subjectData.name,
      CentralPolicyId: parseInt(centralpolicyid),
      CentralPolicyDateId: subjectData.centralpolicydateid,
      inputquestionopen: subjectData.inputquestionopen,
      inputquestionclose: subjectData.inputquestionclose,
      inputanswerclose: subjectData.inputanswerclose,
    }
    //     formData.append('Name', subjectData.name);
    //     formData.append('Answer', subjectData.name);
    //     formData.append('CentralPolicyId', centralpolicyid);
    //     formData.append('CentralPolicyDateId', subjectData.centralpolicydateid);
    // ``
    console.log('FORMDATA: ', formData);
    return this.http.post(this.url, formData);
  }

  deleteSubject(id) {
    return this.http.delete(this.url + id);
  }

  storesubjectprovince(centralpolicyid, provincevalue) {
    // alert(JSON.stringify(provincevalue))
    const formData = new FormData();
    formData.append('centralpolicyid', centralpolicyid);
    formData.append('provincevalue', provincevalue)
    return this.http.post<any>(this.url + "subjectprovince/", formData)
  }
}


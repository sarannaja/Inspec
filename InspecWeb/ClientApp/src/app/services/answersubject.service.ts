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
    return this.http.get<any[]>(this.url+"user/" + id)
  }
  getsubjectlistdata(id):Observable<any[]>{
    return this.http.get<any[]>(this.url+"subjectlist/" + id)
  }
  getsubjectdetaildata(id):Observable<any[]>{
    return this.http.get<any[]>(this.url+"subjectdetail/" + id)
  }
  addAnswer(answersubjectdata) {
    console.log('answersubjectdata: ', answersubjectdata);
    const formData = {
      inputanswer: answersubjectdata,
     
    }
    console.log('FORMDATA: ', formData);
    return this.http.post<any>(this.url, formData);
  }
  addAnsweroutsider(answersubjectdata) {
    console.log('answersubjectdata: ', answersubjectdata);
    const formData = {
      inputansweroutsider: answersubjectdata,
     
    }
    console.log('FORMDATA: ', formData);
    return this.http.post<any>(this.url+"outsider", formData);
  }
  addFiles(subjectid, file: FileList) {
    // alert(subjectid)
    // alert(JSON.stringify(file))
    // console.log("subjectid",subjectid);
    console.log("file", file);
    console.log("subjectid", subjectid);

    const formData = new FormData();
    // for (var i = 0; i < subjectid.length; i++) {
      formData.append('SubjectCentralPolicyProvinceId', subjectid);
    // }
    for (var i = 0; i < file.length; i++) {
      formData.append("files", file[i]);
    }
    return this.http.post(this.url + "addfiles", formData);
  }
}

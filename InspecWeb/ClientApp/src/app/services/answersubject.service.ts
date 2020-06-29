import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AnswersubjectService {

  url = "";
  // file: FileList

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.url = baseUrl + 'api/answersubject/';
  }
  getuseredata(id): Observable<any[]> {
    return this.http.get<any[]>(this.url + "user/" + id)
  }
  getuserpeopleedata(id): Observable<any[]> {
    return this.http.get<any[]>(this.url + "userpeople/" + id)
  }
  getsubjectlistdata(id, userid): Observable<any[]> {
    return this.http.get<any[]>(this.url + "subjectlist/" + id + "/" + userid)
  }
  getansweruserdata(userid): Observable<any[]> {
    return this.http.get<any[]>(this.url + "answeruser/" + userid)
  }
  getsubjectdetaildata(id): Observable<any[]> {
    return this.http.get<any[]>(this.url + "subjectdetail/" + id)
  }
  getcentralpolicyprovinc(id): Observable<any> {
    return this.http.get<any>(this.url + "centralpolicyprovinc/" + id)
  }
  getAnsweruser(userid) {
    return this.http.get<any>(this.url + "answeruser/" + userid)
  }
  getAnsweruserdetail(id, userid) {
    return this.http.get<any>(this.url + "answeruserdetail/" + id + "/" + userid)
  }
  getAnsweruserlist(id, userid) {
    return this.http.get<any>(this.url + "answeruserlist/" + id + "/" + userid)
  }
  getAnswerstatus(id, userid) {
    return this.http.get<any>(this.url + "answerstatus/" + id + "/" + userid)
  }
  getAnswerfile(id, userid) {
    return this.http.get<any>(this.url + "answerfile/" + id + "/" + userid)
  }
  addAnswer(answersubjectdata) {
    console.log('answersubjectdata: ', answersubjectdata);
    const formData = {
      inputanswer: answersubjectdata,
    }
    console.log('FORMDATA: ', formData);
    return this.http.post<any>(this.url, formData);
  }
  addAnswercentralpolicyprovince(answerdata, centralpolicyprovinceId, userid) {
    console.log("answerdata", answerdata);
    // const formData = {
    //   CentralPolicyProvinceId: parseInt(centralpolicyprovinceId),
    //   UserId: userid,
    //   Answer: answerdata.AnswerPeople
    // }
    const formData = new FormData();
    formData.append('CentralPolicyProvinceId', centralpolicyprovinceId);
    formData.append('UserId', userid);
    formData.append('Answer', answerdata.AnswerPeople);

    console.log('FORMDATA: ', formData);
    return this.http.post<any>(this.url + "answercentralpolicyprovince", formData);
  }
  addAnsweroutsider(answersubjectdata) {
    console.log('answersubjectdata: ', answersubjectdata);
    const formData = {
      inputansweroutsider: answersubjectdata,

    }
    console.log('FORMDATA: ', formData);
    return this.http.post<any>(this.url + "outsider", formData);
  }
  addFiles(subjectid, filedata, userid) {
    var file: FileList
    console.log("filedata", filedata);
    console.log("subjectid", subjectid);

    file = filedata.files
    var Type = filedata.Type
    console.log("file", file);
    console.log("type", Type);

    const formData = new FormData();
    formData.append('SubjectCentralPolicyProvinceId', subjectid);
    formData.append('UserId', userid);
    formData.append('Type', Type);
    for (var i = 0; i < file.length; i++) {
      formData.append("files", file[i]);
    }
    return this.http.post(this.url + "addfiles", formData);
  }
  addStatus(StatusData, SubjectCentralPolicyProvinceId, UserId) {
    const formData = new FormData();
    console.log("Suggestion", StatusData);
    console.log("SubjectCentralPolicyProvinceId", SubjectCentralPolicyProvinceId);
    console.log("UserId", UserId);

    formData.append('SubjectCentralPolicyProvinceId', SubjectCentralPolicyProvinceId);
    formData.append('UserId', UserId);
    formData.append('Status', StatusData.Status);

    return this.http.post(this.url + "addstatus", formData);
  }
  editAnswer(Answerdata, id) {
    console.log(Answerdata[0].Description);
    const formData = new FormData();
    formData.append('answer', Answerdata[0].Answer);
    formData.append('description', Answerdata[0].Description);
    return this.http.put(this.url + id, formData);
  }
  editStatus(Statusdata, id) {
    const formData = new FormData();
    formData.append('status', Statusdata.Status);
    return this.http.put(this.url + "editstatus/" + id, formData);
  }
  deleteFile(id) {
    return this.http.delete(this.url + "deleteanswerfile/" + id);
  }
}

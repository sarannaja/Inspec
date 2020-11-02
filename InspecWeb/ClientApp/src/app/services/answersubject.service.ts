import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Answerrole7, GetQuestionPeople } from './nikmodel/answarrole7';
import { CentralPolicyProvince, Answerrole7List } from './nikmodel/answerrole7list';

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
  getcentralpolicyprovince(id, inspectionPlanEventId): Observable<any[]> {
    return this.http.get<any[]>(this.url + "centralpolicyprovince/" + id + "/" + inspectionPlanEventId)
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
  getansweruserrole7data(userid): Observable<any[]> {
    return this.http.get<any[]>(this.url + "answeruserrole7/" + userid)
  }
  getAnsweruserlistrole7(id, InspectionPlanEventId, userid) {
    return this.http.get<Answerrole7List[]>(this.url + "answeruserlistrold7/" + id + "/" + InspectionPlanEventId + "/" + userid)
  }
  getAnswerstatusrole7(id, userid) {
    return this.http.get<Answerrole7>(this.url + "answerstatusrole7/" + id + "/" + userid)
  }
  getAnsweroutsider(id, userid) {
    return this.http.get<any>(this.url + "answersoutsider/" + id + "/" + userid)
  }
  getRecommendationinspector(userid) {
    return this.http.get<any>(this.url + "recommendationinspector/" + userid)
  }
  getRecommendationinspectordetail(id) {
    return this.http.get<any>(this.url + "recommendationinspectordetail/" + id)
  }
  getRecommendationinspectoruser(userid) {
    return this.http.get<any>(this.url + "recommendationinspectoruser/" + userid)
  }
  getAnswerRecommendationinspectoruser(userid, id) {
    return this.http.get<any>(this.url + "answerrecommendationinspectoruser/" + id + "/" + userid)
  }
  getSubjectEventFiles(subjectgroupid): Observable<any[]>{
    return this.http.get<any[]>(this.url + "subjecteventfiles/" + subjectgroupid)
  }
  addAnswer(answersubjectdata) {
    console.log('answersubjectdata: ', answersubjectdata);
    const formData = {
      inputanswer: answersubjectdata,
    }
    console.log('FORMDATA: ', formData);
    return this.http.post<any>(this.url, formData);
  }
  addAnswercentralpolicyprovince(answercentralpolicyprovincedata) {
    console.log("answerdata", answercentralpolicyprovincedata);
    const formData = {
      inputanswercentralpolicyprovince: answercentralpolicyprovincedata,
    }
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
    // formData.append('Type', Type);
    // for (var i = 0; i < file.length; i++) {
    //   formData.append("files", file[i]);
    // }
    function getFileExtension2(filename) {
      return filename.split('.').pop();
    }
    if (filedata.fileData != null) {
      for (var iii = 0; iii < filedata.fileData.length; iii++) {
        var filename: string = filedata.fileData[iii].AnswerSubjectFile.name
        formData.append("files", filedata.fileData[iii].AnswerSubjectFile, `${filedata.fileData[iii].fileDescription}.${getFileExtension2(filename)}`);
        // formData.append("fileDescription", value.fileData[iii].fileDescription);
      }
    }
    return this.http.post(this.url + "addfiles", formData);
  }
  addStatus(StatusData, SubjectCentralPolicyProvinceId, UserId, subjectGroupId) {
    const formData = new FormData();
    console.log("Suggestion", StatusData);
    console.log("SubjectCentralPolicyProvinceId", SubjectCentralPolicyProvinceId);
    console.log("UserId", UserId);

    formData.append('SubjectCentralPolicyProvinceId', SubjectCentralPolicyProvinceId);
    formData.append('subjectGroupId', subjectGroupId);
    formData.append('UserId', UserId);
    formData.append('Status', StatusData.Status);

    return this.http.post<any>(this.url + "addstatus", formData);
  }
  addStatusrole7(StatusData, CentralPolicyEventId, UserId) {
    const formData = new FormData();
    formData.append('CentralPolicyEventId', CentralPolicyEventId);
    formData.append('UserId', UserId);
    formData.append('Status', StatusData.Status);

    return this.http.post<any>(this.url + "addstatusrole7", formData);
  }
  addRecommendationinspector(RecommendationinspectorData, UserId) {
    const formData = new FormData();
    formData.append('SubjectGroupId', RecommendationinspectorData.SubjectGroupId);
    formData.append('UserId', UserId);
    formData.append('Answer', RecommendationinspectorData.Answer)
    formData.append('Status', RecommendationinspectorData.Status);
    return this.http.post<any>(this.url + "addrecommendationinspector", formData);
  }
  editAnswer(Answerdata, id) {
    console.log(Answerdata[0].Description);
    const formData = new FormData();
    formData.append('answer', Answerdata[0].Answer);
    formData.append('description', Answerdata[0].Description);
    return this.http.put(this.url + id, formData);
  }
  editStatus(Statusdata, id, subjectGroupId) {
    const formData = new FormData();
    formData.append('status', Statusdata.Status);
    formData.append('subjectGroupId', subjectGroupId);
    return this.http.put(this.url + "editstatus/" + id, formData);
  }
  editStatusrole7(Statusdata, id) {
    const formData = new FormData();
    formData.append('status', Statusdata.Status);
    return this.http.put(this.url + "editstatusrole7/" + id, formData);
  }
  editAnswerrole7(editanswerrole7) {
    console.log("editanswerrole7", editanswerrole7);
    const formData = {
      editanswerrole7: editanswerrole7,
    }
    console.log('FORMDATA: ', formData);
    return this.http.put<any>(this.url + "editanswerrole7", formData);
  }
  deleteFile(id) {
    return this.http.delete(this.url + "deleteanswerfile/" + id);
  }
  editAnswerRecommendationinspector(AnswerRecommendationinspector, id) {
    const formData = new FormData();
    formData.append('Answer', AnswerRecommendationinspector.Answer);
    formData.append('Status', AnswerRecommendationinspector.Status);
    return this.http.put(this.url + "editanswerrecommendationinspector/" + id, formData);
  }
}

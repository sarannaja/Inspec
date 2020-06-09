import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class DetailexecutiveorderService {
  url = "";
  files: FileList

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    this.url = baseUrl + 'api/ExecutiveOrder/';
  }

  getexecutiveorderdata(): Observable<any[]> {
    return this.http.get<any[]>(this.url)
  }
  getdetailexecutiveorderdata(id): Observable<any> {
    return this.http.get<any>(this.url + "detail/" + id)
  }
  getviewexecutiveorderdata(id): Observable<any> {
    return this.http.get<any>(this.url + "view/" + id)
  }
  getdetailexecutiveorderdatarole3(id, userid): Observable<any> {
    return this.http.get<any>(this.url + "detailrole3/" + id + "/" + userid)
  }
  getCentralpolicydata(id): Observable<any> {
    return this.http.get<any>(this.url + "ex/" + id)
  }
  getprovince(id): Observable<any> {
    return this.http.get<any>(this.url + "province/" + id)
  }
  adddetailexecutiveorder(detailexecutiveorderData, file: FileList, centralpolicyid) {
    const formData = new FormData();
    formData.append('Name', detailexecutiveorderData.name);
    formData.append('CentralpolicyId', centralpolicyid);
    formData.append('ProvinceId', detailexecutiveorderData.provinceId);
    formData.append('Userid', detailexecutiveorderData.byuserid);
    for (var iii = 0; iii < file.length; iii++) {
      formData.append("files", file[iii]);
    }
    //console.log('IDuser: ' + formData.get("byuserid"));
    // console.log('Name: ' + formData.get("Name"));
    // console.log('CentralpolicyId: ' + formData.get("CentralpolicyId"));
    // console.log('ProvinceId: ' + formData.get("ProvinceId"));
    // console.log('files: ' , formData.get("files"));
    return this.http.post<any>(this.url, formData);
  }
  answerexecutiveorder(detailexecutiveorderData, file: FileList, id) {

    // console.log(detailexecutiveorderData)
    const formData = new FormData();
    formData.append('id', id);
    formData.append('AnswerDetail', detailexecutiveorderData.AnswerDetail);
    formData.append('AnswerProblem', detailexecutiveorderData.AnswerProblem);
    formData.append('AnswerCounsel', detailexecutiveorderData.AnswerCounsel);
    formData.append('AnswerUserId', detailexecutiveorderData.byuserid);
    for (var iii = 0; iii < file.length; iii++) {
      formData.append("files", file[iii]);
    }
    //console.log('IDuser: ' + formData.get("AnswerUserId"));

    // console.log('FORMDATA: ' , formData);
    // console.log('AnswerDetail: ' + formData.get("AnswerDetail"));
    // console.log('AnswerProblem: ' + formData.get("AnswerProblem"));
    // console.log('AnswerCounsel: ' + formData.get("AnswerCounsel"));
    // console.log('files: ' , formData.get("files"));
    return this.http.put<any>(this.url, formData);
  }
  getexcutive1(userId) {
    return this.http.get(this.url + "export1/" + userId)


  }

  exportExecutive(userId) {
    console.log("in service: ", userId);

    const formData = {
      userId: userId,
      typeId: "1",
    }

    return this.http.post<any>(this.url + "/exportexecutive", formData)
  }
}


import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class RequestorderService {
  url = "";
  files: FileList

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    this.url = baseUrl + 'api/RequestOrder/';
}
getrequestorderdata(): Observable<any[]> {
  return this.http.get<any[]>(this.url)
}
getdetailrequestorderdata(id): Observable<any> {
  return this.http.get<any>(this.url +"detail/"+ id)
}
getprovince(id): Observable<any> {
  return this.http.get<any>(this.url +"province/" + id)
}
adddetailrequestorder(detailrequestorderData, file: FileList, centralpolicyid) {
  const formData = new FormData();
  formData.append('Name', detailrequestorderData.name);
  formData.append('CentralpolicyId', centralpolicyid);
  formData.append('ProvinceId', detailrequestorderData.provinceId);
  for (var iii = 0;  iii < file.length; iii++) {
    formData.append("files", file[iii]);
  }
    console.log('Name: ' + formData.get("Name"));
    console.log('CentralpolicyId: ' + formData.get("CentralpolicyId"));
    console.log('ProvinceId: ' + formData.get("ProvinceId"));
    console.log('files: ' , formData.get("files"));
    return this.http.post( this.url, formData);
}
answerrequestorder(detailrequestorderData, file: FileList , id) {
     
  console.log(detailrequestorderData)
  const formData = new FormData();
  formData.append('id', id);
  formData.append('AnswerDetail', detailrequestorderData.AnswerDetail);
  formData.append('AnswerProblem', detailrequestorderData.AnswerProblem);
  formData.append('AnswerCounsel', detailrequestorderData.AnswerCounsel);
  for (var iii = 0;  iii < file.length; iii++) {
    formData.append("files", file[iii]);
  }
  console.log('FORMDATA: ' , formData);
  console.log('AnswerDetail: ' + formData.get("AnswerDetail"));
  console.log('AnswerProblem: ' + formData.get("AnswerProblem"));
  console.log('AnswerCounsel: ' + formData.get("AnswerCounsel"));
  console.log('files: ' , formData.get("files"));
  return this.http.put( this.url, formData);
}

}


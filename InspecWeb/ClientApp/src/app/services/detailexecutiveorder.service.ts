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
    return this.http.get<any>(this.url +"detail/"+ id)
  }
  getprovince(id): Observable<any> {
    return this.http.get<any>(this.url +"province/" + id)
  }
  adddetailexecutiveorder(detailexecutiveorderData, centralpolicyid) {
    //alert(JSON.stringify(detailexecutiveorderData))
    
    const formData = new FormData();
    formData.append('name', detailexecutiveorderData.name);
    formData.append('centralpolicyid', centralpolicyid);
    formData.append('provinceid', detailexecutiveorderData.provinceId);
    console.log('FORMDATA: ' + formData.get("name"));
    console.log('FORMDATA: ' + formData.get("centralpolicyid"));
    console.log('FORMDATA: ' + formData.get("provinceid"));
    return this.http.post( this.baseUrl + 'api/ExecutiveOrder', formData);
  }
  
  answerdetailexecutiveorder(detailexecutiveorderData) {
    
    const formData = new FormData();
    formData.append('name', detailexecutiveorderData.name);
    console.log('FORMDATA: ' + formData.get("name"));
    return this.http.post(this.url, formData);
  }
}


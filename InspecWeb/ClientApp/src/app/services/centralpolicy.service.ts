import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CentralpolicyService {

  url = "";

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.url = baseUrl + 'api/centralpolicy/';
  }

  getcentralpolicydata(): Observable<any[]> {
    return this.http.get<any[]>(this.url)
  }
  getdetailcentralpolicydata(id): Observable<any[]> {
    return this.http.get<any[]>(this.url + id)
  }
  detailcentralpolicydata(id) {
    console.log(id);
    return this.http.get(this.url + id)
  }
  addCentralpolicy(centralpolicyData) {

    // alert(JSON.stringify(centralpolicyData))
    // const formData = new FormData();

    // formData.append('title', centralpolicyData.title);
    // formData.append('start_date', centralpolicyData.start_date.date.year + '-' + centralpolicyData.start_date.date.month + '-' + centralpolicyData.start_date.date.day);
    // formData.append('end_date', centralpolicyData.end_date.date.year + '-' + centralpolicyData.end_date.date.month + '-' + centralpolicyData.end_date.date.day);
    // formData.append('subjects', centralpolicyData.subjects);
    // formData.append('files', "filetest.pdf");

    const formData = {
      Title: centralpolicyData.title,
      StartDate: centralpolicyData.start_date.date.year + '-' + centralpolicyData.start_date.date.month + '-' + centralpolicyData.start_date.date.day,
      EndDate: centralpolicyData.end_date.date.year + '-' + centralpolicyData.end_date.date.month + '-' + centralpolicyData.end_date.date.day,
      Type: centralpolicyData.type,
      ProvinceId : centralpolicyData.ProvinceId,
      FiscalYearId: centralpolicyData.year,
      files: "CENTRALPOLICY.pdf",
    }

    console.log('FORMDATA: ' + formData);
    return this.http.post(this.url, formData);
  }

  deleteCentralPolicy(id) {
    return this.http.delete(this.url + id);
  }

}

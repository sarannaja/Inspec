import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CentralpolicyService {

  url = "";
  files: FileList

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
  addCentralpolicy(centralpolicyData,file: FileList) {

    var inputdate = centralpolicyData.inputdate.map((item , index) => {
      return {
        StartDate:item.start_date.date.year + '-' + item.start_date.date.month + '-' + item.start_date.date.day,
        EndDate:item.end_date.date.year + '-' + item.end_date.date.month + '-' + item.end_date.date.day,
      }
    })
    // for (var i = 0; i < file.length; i++) {
    //   // formData.append("files", file[i]);
    // }
    const formData = {
      Title: centralpolicyData.title,
      // StartDate: centralpolicyData.start_date.date.year + '-' + centralpolicyData.start_date.date.month + '-' + centralpolicyData.start_date.date.day,
      // EndDate: centralpolicyData.end_date.date.year + '-' + centralpolicyData.end_date.date.month + '-' + centralpolicyData.end_date.date.day,
      Type: centralpolicyData.type,
      ProvinceId : centralpolicyData.ProvinceId,
      FiscalYearId: centralpolicyData.year,
      Status: centralpolicyData.status,
      files: file,
      inputdate: inputdate,
    }

    console.log('FORMDATA: ' , formData);
    return this.http.post(this.url, formData);
  }

  deleteCentralPolicy(id) {
    return this.http.delete(this.url + id);
  }

  addCentralpolicyUser(data,id) {
    const formData = {
      CentralPolicyId: id,
      UserId: data.UserPeopleId,
    }
    console.log('FORMDATA: ' + formData);
    return this.http.post(this.url + "users" , formData);
  }

  getcentralpolicyuserdata(id): Observable<any[]> {
    return this.http.get<any[]>(this.url + "users/" + id)
  }
}

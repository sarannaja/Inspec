import { Injectable, Inject, Input } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Inslpectionplan } from './modelaof/reportInspectionplan';

@Injectable({
  providedIn: 'root'
})
export class InspectionplaneventService {

  url = "";

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    this.url = baseUrl + 'api/inspectionplanevent/';
  }

  getinspectionplaneventdata(id): Observable<any[]> {
    let path = this.url + 'inspectionplan/' + id;
    console.log("URL: ", path);
    return this.http.get<any[]>(path)
  }

  getinspectionplaneventalldata(): Observable<any[]> {
    let path = this.url + 'inspectionplanall';
    console.log("URL: ", path);
    return this.http.get<any[]>(path)
  }

  getinspectionplaneventuserdata(id): Observable<any> {
    //  alert(id)
    let path = this.url + 'inspectionplanuser/' + id;
    console.log("URL: ", path);

    return this.http.get<any>(path)
  }
  getinspectionplaneventuserprovincedata(proid): Observable<any> {
    //  alert(id)
    let path = this.url + 'inspectionplanprovince/' + proid;
    console.log("URL: ", path);

    return this.http.get<any>(path)
  }

  addInspectionplanevent(inspectionplaneventData, userid) {
    // alert(JSON.stringify(inspectionplaneventData.input))
    var input = inspectionplaneventData.input.map((item, index) => {
      return {
        StartPlanDate: item.start_date_plan.date.year + '-' + item.start_date_plan.date.month + '-' + item.start_date_plan.date.day,
        EndPlanDate: item.end_date_plan.date.year + '-' + item.end_date_plan.date.month + '-' + item.end_date_plan.date.day,
        ProvinceId: item.provinces,
        // CentralPolicyId: item.centralpolicies,
      }
    })
    // alert(JSON.stringify(input))
    const formData = {
      Name: inspectionplaneventData.title,
      input: input,
      CreatedBy: userid,
    }
    console.log('FORMDATA: ' + formData);
    return this.http.post(this.url, formData);
  }

  deleteInspectionplanevent(id) {
    return this.http.delete(this.url + id);
  }

  getuserregion(userid): Observable<any[]> {
    return this.http.get<any[]>(this.url + "userregion/" + userid)
  }

  getscheduledata(userid): Observable<any[]> {
    return this.http.get<any[]>(this.url + "inspectionplanexportindex/" + userid)
  }

  getregion(userid): Observable<any[]> {
    return this.http.get<any[]>(this.url + "userallregion/" + userid)
  }

  getpeople(): Observable<any[]> {
    return this.http.get<any[]>(this.url + "getpeople")
  }

  getprovincialdepartment(): Observable<any[]> {
    return this.http.get<any[]>(this.url + "getprovincialdepartment")
  }

  getexportprovince(value): Observable<Inslpectionplan> {
    return this.http.get<Inslpectionplan>(this.url + "exportexcelcalendarprovince/" + value)
  }
  getexportregion(value): Observable<Inslpectionplan> {
    return this.http.get<Inslpectionplan>(this.url + "exportexcelcalendarregion/" + value)
  }
  getexportpeople(value): Observable<Inslpectionplan> {
    return this.http.get<Inslpectionplan>(this.url + "exportexcelcalendarpeople/" + value)
  }
  getexportdate(): Observable<Inslpectionplan> {
    return this.http.get<Inslpectionplan>(this.url + "exportexcelcalendardate")
  }
  exportexcelcalendardepartment(value): Observable<Inslpectionplan> {
    return this.http.get<Inslpectionplan>(this.url + "exportexcelcalendardepartment/" + value)
  }
  getUserOwner(id) {

    const apiURL = this.baseUrl + 'api/get_role/' + id
    return this.http.get<any>(apiURL)
    // .toPromise()

  }
}
class Owner {
  constructor(
    public name: number,
    public phoneNumber: number,

  ) { }
}

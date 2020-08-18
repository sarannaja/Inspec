import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Report1 } from '../models/reportnik';

@Injectable({
  providedIn: 'root'
})
export class ReportService {

  url = "";


  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    this.url = baseUrl + 'api/report/';
  }

  getreportsubject(id): Observable<Report1> {
    return this.http.get<Report1>(this.url + id)
  }
  getprovince(): Observable<any> {
    return this.http.get<any>(this.url + "province")
  }
  getcentralpolicy(id): Observable<any> {
    return this.http.get<any>(this.url + "centralpolicyprovinces/" + id)
  }
  getsubjectdetaildata(id): Observable<any> {
    return this.http.get<any>(this.url + "subjectdetail/" + id)
  }
  async role(id) {
    return await this.http.get<any>(this.baseUrl + 'api/get_role/' + id)
  }
  createReportSubject(resultdetailcentralpolicy, resultdetailcentralpolicyprovince) {
    var test = []
    var test2 = []
    // console.log("1", resultdetailcentralpolicy);
    console.log("2", resultdetailcentralpolicyprovince);
    test = resultdetailcentralpolicyprovince.map((item, index) => {
      return {
        name: item.name,
        explanation: item.explanation,
        namesubquestion: item.subquestionCentralPolicyProvinces.map((item2, index) => {
          return item2.name;
        }),
        provincialDepartment: item.subquestionCentralPolicyProvinces[0].subjectCentralPolicyProvinceGroups.map((item2, index) => {
          return item2.provincialDepartment.name
        }),
      }
    })
    console.log("test", test);
    const formData = {
      type: resultdetailcentralpolicy.type,
      fiscalyear: resultdetailcentralpolicy.fiscalYear.year,
      title: resultdetailcentralpolicy.title,
      Subject: test,
    }
    return this.http.post<any>(this.url + "reportsubject", formData)
  }
  createReporttype1(reportdata) {
    const formData = new FormData();
    formData.append('provincialDepartmentId', reportdata.provincialDepartmentId);
    formData.append('reporttype', reportdata.type);
    return this.http.post<any>(this.url + "reportperformance", formData)
  }
  createReporttype2(reportdata, provinceid, CentralPolicyProvinceId, SubjectGroupId) {
    const formData = new FormData();
    formData.append('CentralPolicyProvinceId', CentralPolicyProvinceId);
    formData.append('SubjectGroupId', SubjectGroupId);
    formData.append('provinceid', provinceid);
    formData.append('reporttype', reportdata.type);
    return this.http.post<any>(this.url + "reportperformance", formData)
  }
  createReport2() {
    // const formData = new FormData();
    // formData.append('provinceid', reportdata.provinceid);
    // formData.append('centralpolicyid', reportdata.centralpolicyid);
    // formData.append('subjectid', reportdata.subjectid);
    // formData.append('reporttype', reportdata.reporttype);
    return this.http.get<any>(this.url + "reportsuggestions")
  }
  createReport3() {
    // const formData = new FormData();
    // formData.append('provinceid', reportdata.provinceid);
    // formData.append('centralpolicyid', reportdata.centralpolicyid);
    // formData.append('subjectid', reportdata.subjectid);
    // formData.append('reporttype', reportdata.reporttype);
    return this.http.get<any>(this.url + "reportsuggestionsresult")
  }
  createReport4() {
    // const formData = new FormData();
    // formData.append('provinceid', reportdata.provinceid);
    // formData.append('centralpolicyid', reportdata.centralpolicyid);
    // formData.append('subjectid', reportdata.subjectid);
    // formData.append('reporttype', reportdata.reporttype);
    return this.http.get<any>(this.url + "reportquestionnaire")
  }
  createReport5() {
    // const formData = new FormData();
    // formData.append('provinceid', reportdata.provinceid);
    // formData.append('centralpolicyid', reportdata.centralpolicyid);
    // formData.append('subjectid', reportdata.subjectid);
    // formData.append('reporttype', reportdata.reporttype);
    return this.http.get<any>(this.url + "reportcomment")
  }
}

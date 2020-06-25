import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class InspectionplanService {

  url = "";

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.url = baseUrl + 'api/inspectionplan/';
  }

  getinspectionplandata(id, provinceid) {
    console.log(id);
    return this.http.get<any>(this.url + id + '/' + provinceid)
  }

  getTimeline(id) {
    return this.http.get<any>(this.url + "getTimeline/" + id);
  }

  getScheduleData(id, provinceId) {
    return this.http.get<any>(this.url + "getScheduleData/" + id + "/" + provinceId);
  }

  getcentralpolicyprovinceid(centralpolicyid, provinceid) {
    return this.http.get(this.url + 'getcentralpolicyprovinceid/' + centralpolicyid + '/' + provinceid)
  }

  addCentralPolicyEvent(CentralPolicyEventData, Id, userid, proid) {
    // alert(JSON.stringify(CentralPolicyEventData))
    // alert(JSON.stringify(Id))
    const formData = {
      InspectionPlanEventId: parseInt(Id),
      CentralPolicyId: CentralPolicyEventData.CentralpolicyId,
      ProvinceId: proid,
      CreatedBy: userid,
      StartDate: CentralPolicyEventData.startdate.date.year + '-' + CentralPolicyEventData.startdate.date.month + '-' + CentralPolicyEventData.startdate.date.day,
      EndDate: CentralPolicyEventData.enddate.date.year + '-' + CentralPolicyEventData.enddate.date.month + '-' + CentralPolicyEventData.enddate.date.day,
      NotificationDate: CentralPolicyEventData.notificationdate.date.year + '-' + CentralPolicyEventData.notificationdate.date.month + '-' + CentralPolicyEventData.notificationdate.date.day,
      DeadlineDate: CentralPolicyEventData.deadlinedate.date.year + '-' + CentralPolicyEventData.deadlinedate.date.month + '-' + CentralPolicyEventData.deadlinedate.date.day,
    }
    // alert(JSON.stringify(formData));
    console.log('FORMDATA: ', formData);
    return this.http.post(this.url + "AddCentralPolicyEvents", formData);
  }

  addInspectionPlan(InspectionPlanData, userid, inspectionplaneventid, yearId) {
    // alert(JSON.stringify(InspectionPlanData))
    // alert(yearId);
    const formData = {
      InspectionPlanEventId: parseInt(inspectionplaneventid),
      Title: InspectionPlanData.title,
      StartDate: InspectionPlanData.start_date,
      EndDate: InspectionPlanData.end_date,
      Type: InspectionPlanData.type,
      ProvinceId: InspectionPlanData.ProvinceId,
      // FiscalYearId: InspectionPlanData.year,
      FiscalYearId: yearId,
      // files: "INSPECTIONPLAN.pdf",
      UserID: userid,
      Status: "ใช้งานจริง"
    }
    console.log('FORMDATA POST: ', formData);
    return this.http.post(this.url, formData);
  }

  inspectionplansprovince(provinceid, userid, start_date_plan_i, end_date_plan_i) {

    // alert(start_date_plan_i)
    // alert(end_date_plan_i)

    const formData = new FormData();
    formData.append('provinceid', provinceid);
    formData.append('userid', userid);
    formData.append('start_date_plan', start_date_plan_i);
    formData.append('end_date_plan', end_date_plan_i);
    return this.http.post(this.url + "inspectionprovince", formData);

  }

  getcentralpolicydata(provinceid): Observable<any[]> {
    // return this.http.get<any[]>(this.url)
    return this.http.get<any[]>(this.url + 'getcentralpolicydata/' + provinceid)
  }

  changeplanstatus(planid) {
    const formData = new FormData();
    formData.append('planid', planid);

    return this.http.post(this.url + "changeplanstatus", formData);
  }
}

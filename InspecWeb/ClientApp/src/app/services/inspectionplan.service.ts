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

  addCentralPolicyEvent(CentralPolicyEventData, Id, userid, proid, startDate, endDate) {
    // alert(JSON.stringify(CentralPolicyEventData))
    // alert(JSON.stringify(Id))
    console.log("startDate: ", startDate);
    console.log("endDate: ", endDate);

    const formData = {
      InspectionPlanEventId: parseInt(Id),
      CentralPolicyId: CentralPolicyEventData.CentralpolicyId,
      ProvinceId: proid,
      CreatedBy: userid,
      // StartDate: CentralPolicyEventData.startdate.date.year + '-' + CentralPolicyEventData.startdate.date.month + '-' + CentralPolicyEventData.startdate.date.day,
      // EndDate: CentralPolicyEventData.enddate.date.year + '-' + CentralPolicyEventData.enddate.date.month + '-' + CentralPolicyEventData.enddate.date.day,
      StartDate: startDate.year + '-' + startDate.month + '-' + startDate.day,
      EndDate: endDate.year + '-' + endDate.month + '-' + endDate.day,
      // NotificationDate: CentralPolicyEventData.notificationdate.date.year + '-' + CentralPolicyEventData.notificationdate.date.month + '-' + CentralPolicyEventData.notificationdate.date.day,
      // DeadlineDate: CentralPolicyEventData.deadlinedate.date.year + '-' + CentralPolicyEventData.deadlinedate.date.month + '-' + CentralPolicyEventData.deadlinedate.date.day,
    }
    // alert(JSON.stringify(formData));
    console.log('FORMDATA: ', formData);
    return this.http.post(this.url + "AddCentralPolicyEvents", formData);
  }

  addInspectionPlan(InspectionPlanData, userid, inspectionplaneventid, yearId, startDate, endDate, year) {
    // alert(JSON.stringify(InspectionPlanData))
    // alert(year.year);
    console.log("inspectData : ", InspectionPlanData);
    console.log("sDate: ", startDate);
    console.log("eDate: ", endDate);

    const formData = {
      InspectionPlanEventId: parseInt(inspectionplaneventid),
      Title: InspectionPlanData.title,
      // StartDate: InspectionPlanData.start_date,
      // EndDate: InspectionPlanData.end_date,
      StartDate: startDate.year + '-' + startDate.month + '-' + startDate.day,
      EndDate: endDate.year + '-' + endDate.month + '-' + endDate.day,
      Type: InspectionPlanData.type,
      ProvinceId: InspectionPlanData.ProvinceId,
      FiscalYearId: year.year + 543,
      // FiscalYearId: yearId,
      // files: "INSPECTIONPLAN.pdf",
      UserID: userid,
      Status: "ใช้งานจริง"
    }

    // const formData = new FormData();
    // formData.append('InspectionPlanEventId', inspectionplaneventid);
    // formData.append('Title', InspectionPlanData.title);
    // formData.append('StartDate', startDate);
    // formData.append('EndDate', endDate);
    // formData.append('Type', InspectionPlanData.type);
    // formData.append('ProvinceId', InspectionPlanData.ProvinceId);
    // formData.append('FiscalYearId', InspectionPlanData.year);
    // formData.append('UserID', userid);
    // formData.append('Status', "ใช้งานจริง");


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

  getcentralpolicydata(provinceid, year): Observable<any[]> {
    var year = year.year + 543;
    // alert(year.year)
    // return this.http.get<any[]>(this.url)
    return this.http.get<any[]>(this.url + 'getcentralpolicydata/' + provinceid + '/' + year)
  }

  changeplanstatus(planid) {
    const formData = new FormData();
    formData.append('planid', planid);

    return this.http.post(this.url + "changeplanstatus", formData);
  }

  editplandate(planid, startdate, enddate) {
    const formData = new FormData();
    formData.append('planid', planid);
    formData.append('startdate', startdate.year + '-' + startdate.month + '-' + startdate.day);
    formData.append('enddate', enddate.year + '-' + enddate.month + '-' + enddate.day);
    return this.http.post(this.url + "editplandate", formData);
  }
  deleteplandate(planid) {
    return this.http.delete(this.url + 'deleteplandate/' + planid);
  }

  deleteCentralPolicyEvent(id) {
    return this.http.delete(this.url + 'deletecentralpolicyevent/' + id);
  }

  deleteCentralPolicy(id) {
    return this.http.delete(this.url + 'deletecentralpolicy/' + id);
  }

  getcentralpolicyeventdata(id): Observable<any[]> {
    // return this.http.get<any[]>(this.url)
    return this.http.get<any[]>(this.url + 'getcentralpolicyeventdata/' + id)
  }

  editcentralpolicy(ceneventid, startdate, enddate, value) {
    const formData = new FormData();
    formData.append('ceneventid', ceneventid);
    formData.append('startdate', startdate.year + '-' + startdate.month + '-' + startdate.day);
    formData.append('enddate', enddate.year + '-' + enddate.month + '-' + enddate.day);
    formData.append('year', value.year);
    formData.append('title', value.title);
    return this.http.post(this.url + "editcentralpolicy", formData);
  }

}

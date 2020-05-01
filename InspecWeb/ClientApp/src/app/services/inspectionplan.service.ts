import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class InspectionplanService {

  url = "";

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.url = baseUrl + 'api/inspectionplan/';
  }

  getinspectionplandata(id) {
    console.log(id);
    return this.http.get(this.url + id)
  }
  addCentralPolicyEvent(CentralPolicyEventData, Id) {
    // alert(JSON.stringify(CentralPolicyEventData))
    // alert(JSON.stringify(Id))
    const formData = {
      InspectionPlanEventId: parseInt(Id),
      CentralPolicyId: CentralPolicyEventData.CentralpolicyId
    }
    // alert(JSON.stringify(formData));
    console.log('FORMDATA: ', formData);
    return this.http.post(this.url + "AddCentralPolicyEvents", formData);
  }

  addInspectionPlan(InspectionPlanData, userid) {
    const formData = {
      // InspectionPlanEventId: parseInt(Id),
      Title: InspectionPlanData.title,
      StartDate: InspectionPlanData.start_date.date.year + '-' + InspectionPlanData.start_date.date.month + '-' + InspectionPlanData.start_date.date.day,
      EndDate: InspectionPlanData.end_date.date.year + '-' + InspectionPlanData.end_date.date.month + '-' + InspectionPlanData.end_date.date.day,
      Type: InspectionPlanData.type,
      ProvinceId: InspectionPlanData.ProvinceId,
      FiscalYearId: InspectionPlanData.year,
      files: "INSPECTIONPLAN.pdf",
      UserID: userid,
      Status: InspectionPlanData.status
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
}

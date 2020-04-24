import { Injectable, Inject, Input } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class InspectionplaneventService {

  url = "";

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string)
   {
    this.url = baseUrl + 'api/inspectionplanevent/';
   }

   getinspectionplaneventdata(id):Observable<any[]> {
    //  alert(id)
     let path = this.url + 'inspectionplan/' + id;
     console.log("URL: ", path);

    return this.http.get<any[]>(path)
  }

  addInspectionplanevent(inspectionplaneventData, userid) {
    // alert(JSON.stringify(inspectionplaneventData.input))
    var input = inspectionplaneventData.input.map((item , index) => {
      return {
        StartPlanDate:item.start_date_plan.date.year + '-' + item.start_date_plan.date.month + '-' + item.start_date_plan.date.day,
        EndPlanDate:item.end_date_plan.date.year + '-' + item.end_date_plan.date.month + '-' + item.end_date_plan.date.day,
        ProvinceId:item.provinces,
        CentralPolicyId:item.centralpolicies,
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

}

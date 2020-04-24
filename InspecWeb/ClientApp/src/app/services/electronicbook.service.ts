import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ElectronicbookService {

  url = "";

  constructor(
    private http: HttpClient, @Inject('BASE_URL') baseUrl: string
  ) {
    this.url = baseUrl + 'api/electronicbook/';
  }

  addElectronicBook(value) {
    // alert(JSON.stringify(inspectionplaneventData.input))
    var input = value.input.map((item , index) => {
      return {
        // StartPlanDate:item.start_date_plan.date.year + '-' + item.start_date_plan.date.month + '-' + item.start_date_plan.date.day,
        // EndPlanDate:item.end_date_plan.date.year + '-' + item.end_date_plan.date.month + '-' + item.end_date_plan.date.day,
        ProvinceId:item.provinces,
        CentralPolicyId:item.centralpolicies,
      }
    })

    var userMinistry = value.map((item , index) => {
      return {
        // StartPlanDate:item.start_date_plan.date.year + '-' + item.start_date_plan.date.month + '-' + item.start_date_plan.date.day,
        // EndPlanDate:item.end_date_plan.date.year + '-' + item.end_date_plan.date.month + '-' + item.end_date_plan.date.day,
        Id:item.UserMinistryId,
      }
    })

    var userPeople = value.map((item , index) => {
      return {
        // StartPlanDate:item.start_date_plan.date.year + '-' + item.start_date_plan.date.month + '-' + item.start_date_plan.date.day,
        // EndPlanDate:item.end_date_plan.date.year + '-' + item.end_date_plan.date.month + '-' + item.end_date_plan.date.day,
        Id:item.UserPeopleId,
      }
    })
    // alert(JSON.stringify(input))
    const formData = {
      Detail: value.checkDetail,
      Inputelectronicbook: input,
      UserMinistryId: value.userMinistry,
      UserPeopleId: value.userPeople
    }
    console.log('FORMDATA: ' + formData);
    return this.http.post(this.url, formData);
  }
}

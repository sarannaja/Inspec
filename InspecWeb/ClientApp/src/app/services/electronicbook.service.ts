import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

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

  getElectronicBook(userId) {
    return this.http.get(this.url + userId)
  }

  addElectronicBook(value, id) {
    // alert(JSON.stringify(inspectionplaneventData.input))
    var input = value.input.map((item , index) => {
      return {
        // StartPlanDate:item.start_date_plan.date.year + '-' + item.start_date_plan.date.month + '-' + item.start_date_plan.date.day,
        // EndPlanDate:item.end_date_plan.date.year + '-' + item.end_date_plan.date.month + '-' + item.end_date_plan.date.day,
        ProvinceId:item.provinces,
        CentralPolicyId:item.centralpolicies,
      }
    })
    var userMinistry = value.UserMinistryId.map((item , index) => {
      return {
        Id:item
      }
    })
    var userPeople = value.UserPeopleId.map((item , index) => {
      return {
        Id:item
      }
    })

    const formData = {
      Detail: value.checkDetail,
      Inputelectronicbook: input,
      UserMinistryId: userMinistry,
      UserPeopleId: userPeople,
      id: id
    }
    console.log('FORMDATA: ', formData);
    return this.http.post(this.url, formData);
  }

  deleteElectronicBook(id) {
    return this.http.delete(this.url + id)
  }

  getElectronicBookDetail(centralPolicyUserId): Observable <any> {
    return this.http.get<any>(this.url + 'getElectronicBookById/' + centralPolicyUserId);
  }
}

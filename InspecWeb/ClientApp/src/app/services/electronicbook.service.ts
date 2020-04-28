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

  addElectronicBook(value, id, file: FileList) {
    // alert(JSON.stringify(inspectionplaneventData.input))
    const formData = new FormData();
    formData.append('Detail', value.checkDetail);
    formData.append('id', id);
    formData.append('Status', value.Status);


    var input: Array<any> = value.input.map((item , index) => {
      return {
        // StartPlanDate:item.start_date_plan.date.year + '-' + item.start_date_plan.date.month + '-' + item.start_date_plan.date.day,
        // EndPlanDate:item.end_date_plan.date.year + '-' + item.end_date_plan.date.month + '-' + item.end_date_plan.date.day,
        ProvinceId:item.provinces,
        CentralPolicyId:item.centralpolicies,
      }
    })

    for (var i = 0; i < input.length; i++) {
      console.log("input: ", input[i]);
      // console.log("inputdateii: ", inputdate[ii].StartDate);
      // formData.append('Inputelectronicbook', input[i]);
    }

    formData.append('CentralPolicyId', input[0].CentralPolicyId);
    formData.append('ProvinceId', input[0].ProvinceId);

    var userMinistry: Array<any> = value.UserMinistryId.map((item , index) => {
      return {
        Id:item
      }
    })

    for (var i = 0; i < userMinistry.length; i++) {
      console.log("i: ", i);
      // console.log("inputdateii: ", inputdate[ii].StartDate);
      formData.append('UserMinistryId', userMinistry[i].Id);
    }

    var userPeople: Array<any> = value.UserPeopleId.map((item , index) => {
      return {
        Id:item
      }
    })

    for (var i = 0; i < userPeople.length; i++) {
      console.log("i: ", i);
      // console.log("inputdateii: ", inputdate[ii].StartDate);
      formData.append('UserPeopleId', userPeople[i].Id);
    }

    for (var iii = 0; iii < file.length; iii++) {
      formData.append("files", file[iii]);
    }

    // const formData = {
    //   Detail: value.checkDetail,
    //   Inputelectronicbook: input,
    //   UserMinistryId: userMinistry,
    //   UserPeopleId: userPeople,
    //   id: id,
    //   Status: value.Status,
    //   files: file
    // }

    console.log("UserPeopleId", (formData.getAll("UserPeopleId")));

    console.log('FORMDATA: ', formData);
    return this.http.post(this.url, formData);
  }

  deleteElectronicBook(id) {
    return this.http.delete(this.url + id)
  }

  getElectronicBookDetail(centralPolicyUserId): Observable <any> {
    return this.http.get<any>(this.url + 'getElectronicBookById/' + centralPolicyUserId);
  }

  editElectronicBookDetail(value, electID, file: FileList,) {
    console.log("EDIT VALUE: ", value);
    console.log("EDIT FILE: ", file);
    // const formData = {
    //   Detail: value.eBookDetail,
    //   Status: value.Status
    // }

    const formData = new FormData();
    formData.append('Detail', value.eBookDetail);
    formData.append('Status', value.Status);

    for (var i = 0; i < file.length; i++) {
      formData.append("files", file[i]);
    }

    return this.http.put(this.url + 'editElectronicBookDetail/' + electID, formData)
  }

  getNotSelectedInspectionPlan(id) {
    console.log(id);
    return this.http.get(this.url + "getcentralpolicyfromprovince/" + id)
  }

  deleteFile(id) {
    return this.http.delete(this.url + 'deletefile/' + id);
  }
}

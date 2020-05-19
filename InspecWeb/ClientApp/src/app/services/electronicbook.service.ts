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

  addElectronicBook(value, id, file: FileList, CentralPolicyId) {
    // alert(JSON.stringify(inspectionplaneventData.input))
    const formData = new FormData();
    formData.append('Detail', value.checkDetail);
    formData.append('Problem', value.Problem);
    formData.append('Suggestion', value.Suggestion);
    formData.append('id', id);
    formData.append('Status', value.Status);
    formData.append('CentralPolicyId', CentralPolicyId);

    console.log("detail", formData.getAll("Detail"));
    console.log("Problem", formData.getAll("Problem"));
    console.log("Suggestion", formData.getAll("Suggestion"));
    console.log("id", formData.getAll("id"));
    console.log("Status", formData.getAll("Status"));

    for (var iii = 0; iii < file.length; iii++) {
      formData.append("files", file[iii]);
    }

    console.log('FORMDATA: ', formData);
    return this.http.post(this.url, formData);
  }


  addElectronicBookFileFromCalendar(value, file: FileList, electronicbookid) {
    const formData = new FormData();
    formData.append('ElectronicBookId', electronicbookid);


    for (var iii = 0; iii < file.length; iii++) {
      formData.append("files", file[iii]);
    }

    console.log('FORMDATA: ', formData);
    return this.http.post(this.url + "calendarfile", formData);
  }

  deleteElectronicBook(id) {
    return this.http.delete(this.url + id)
  }

  getElectronicBookDetail(electID): Observable<any> {
    return this.http.get<any>(this.url + 'getElectronicBookById/' + electID);
  }

  editElectronicBookDetail(value, electID, file: FileList, ) {
    console.log("EDIT VALUE: ", value);
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

  getCalendarFile(electID) {
    console.log("SERVICE EID: ", electID);

    return this.http.get(this.url + "getCalendarFile/" + electID)
  }

  getElectronicbookFile(electID) {
    console.log("SERVICE EID: ", electID);

    return this.http.get(this.url + "getElectronicbookFile/" + electID)
  }

  getElectronicBookProvince(userId) {
    return this.http.get(this.url + "province/" + userId)
  }
}

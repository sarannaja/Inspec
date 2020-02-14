import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class InspectionorderService {

  count = 0
  url = "";


  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string)
  {
    this.url = baseUrl + 'api/inspectionorder';
  }
  getinspectionorderdata() {
    return this.http.get(this.url)
  }
  addInspectionorder(inspectionorderData) {
    const formData = new FormData();
    formData.append('name', inspectionorderData.inspectionordername);
    console.log('FORMDATA: ' + formData);
    return this.http.post(this.url, formData);
  }
  deleteInspectionorder(id) {
    return this.http.delete(this.url + id);
  }
  editInspectionorder(inspectionorderData,id) {
    console.log(inspectionorderData);

    const formData = new FormData();
    formData.append('name', inspectionorderData.inspectionordername);
    console.log('FORMDATA: ' + JSON.stringify(formData));
    return this.http.put(this.url+id, formData);
  }
}

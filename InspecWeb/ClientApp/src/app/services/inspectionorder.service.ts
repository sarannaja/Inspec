import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class InspectionorderService {
  url = "https://localhost:5001/api/inspectionorder/";

  constructor(private http: HttpClient) {}
  getinspectionorderdata() {
    return this.http.get(this.url)
  }
  addInspectionorder(inspectionorderData) {
    alert(JSON.stringify(inspectionorderData))
    const formData = new FormData();
    formData.append('year',inspectionorderData.year);
    formData.append('order',inspectionorderData.order);
    formData.append('name',inspectionorderData.name);
    formData.append('createBy',inspectionorderData.createBy);
    return this.http.post(this.url, formData);
  }
  deleteInspectionorder(id) {
    return this.http.delete(this.url + id);
  }
  editInspectionorder(inspectionorderData,id) {
    console.log(inspectionorderData);

    const formData = new FormData();
    formData.append('year',inspectionorderData.year);
    formData.append('order',inspectionorderData.order);
    formData.append('name',inspectionorderData.name);
    formData.append('createBy',inspectionorderData.createBy);
    console.log('FORMDATA: ' + JSON.stringify(formData));
    return this.http.put(this.url+id, formData);
  }
}

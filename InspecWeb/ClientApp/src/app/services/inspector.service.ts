import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class InspectorService {
  url = "https://localhost:5001/api/inspector/";

  constructor(private http: HttpClient) {}
  getinspectordata() {
    return this.http.get(this.url)
  }
  addInspector(inspectorData) {
    alert(JSON.stringify(inspectorData))
    const formData = new FormData();
    formData.append('name',inspectorData.name);
    formData.append('phonenumber',inspectorData.phonenumber);
    formData.append('regionId',inspectorData.regionId);
    return this.http.post(this.url, formData);
  }
  deleteInspector(id) {
    return this.http.delete(this.url + id);
  }
  editInspector(inspectorData,id) {
    console.log(inspectorData);

    const formData = new FormData();
    formData.append('name',inspectorData.name);
    formData.append('phonenumber',inspectorData.phonenumber);
    formData.append('regionId',inspectorData.regionId);
    console.log('FORMDATA: ' + JSON.stringify(formData));
    return this.http.put(this.url+id, formData);
  }
}

import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class MinistermonitoringService {
  url = "https://localhost:5001/api/ministermonitoring/";

  constructor(private http: HttpClient) {}
  getministermonitoringdata() {
    return this.http.get(this.url)
  }
  addMinistermonitoring(ministermonitoringData) {
    alert(JSON.stringify(ministermonitoringData))
    const formData = new FormData();
    formData.append('name',ministermonitoringData.name);
    formData.append('position',ministermonitoringData.position);
    formData.append('image',ministermonitoringData.image);
    formData.append('createAt',ministermonitoringData.createAt);
    return this.http.post(this.url, formData);
  }
  deleteMinistermonitoring(id) {
    return this.http.delete(this.url + id);
  }
  editMinistermonitoring(ministermonitoringData,id) {
    console.log(ministermonitoringData);

    const formData = new FormData();
    formData.append('name',ministermonitoringData.name);
    formData.append('position',ministermonitoringData.position);
    formData.append('iamge',ministermonitoringData.image);
    formData.append('createAt',ministermonitoringData.createAt);
    return this.http.put(this.url+id, formData);
  }
}

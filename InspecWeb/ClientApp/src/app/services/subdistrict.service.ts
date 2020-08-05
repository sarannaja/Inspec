import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})
export class SubdistrictService {

  url = "";

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) { 
    this.url = baseUrl + 'api/subdistrict/';
  }
  getsubdistrictdata(id) {
    console.log(id);
    
    return this.http.get(this.url+id)
  }
  addsubdistrict(supdistrictData,provinceId) {
    const formData = new FormData();
    formData.append('DistrictId', provinceId);
    formData.append('Name', supdistrictData.Name);
    return this.http.post(this.url, formData);
  }
  deletesupdistrict(id) {
    return this.http.delete(this.url + id);
  }
  editsupdistrict(supdistrictData,id) {
    const formData = new FormData();
    formData.append('Name', supdistrictData.Name);
   
    return this.http.put(this.url+id, formData);
  }
}

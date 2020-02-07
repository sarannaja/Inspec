import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ProvinceService {

  count = 0
  url = "https://localhost:5001/api/province/";


  constructor(private http: HttpClient) { }
  getprovincedata() {
    return this.http.get(this.url)
  }
  addProvince(provinceData) {
    const formData = new FormData();
    formData.append('name', provinceData.provincename);
    console.log('FORMDATA: ' + formData);
    return this.http.post(this.url, formData);
  }
  deleteProvince(id) {
    return this.http.delete(this.url + id);
  }
  editProvince(provinceData,id) {
    console.log(provinceData);
    
    const formData = new FormData();
    formData.append('name', provinceData.provincename);
    console.log('FORMDATA: ' + JSON.stringify(formData));
    return this.http.put(this.url+id, formData);
  }
}

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ProvinceService {

  count = 0
  constructor( private http : HttpClient) { }
  getprovincedata (){
    return this.http.get("https://localhost:5001/api/province")
  }

  addProvince(provinceData) {
    const formData = new FormData();
    formData.append('name', provinceData.provincename);

    console.log('FORMDATA: ' + formData);
    return this.http.post('https://localhost:5001/api/province', formData);
  }
  deleteProvince(id){
    return this.http.delete('https://localhost:5001/api/province/' + id);
  }
}

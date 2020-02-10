import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class RegionService {

  count = 0
  url = "https://localhost:5001/api/region/";


  constructor(private http: HttpClient) { }
  getregiondata() {
    return this.http.get(this.url)
  }
  addRegion(regionData) {
    const formData = new FormData();
    formData.append('name', regionData.regionname);
    console.log('FORMDATA: ' + formData);
    return this.http.post(this.url, formData);
  }
  deleteRegion(id) {
    return this.http.delete(this.url + id);
  }
  editRegion(regionData,id) {
    console.log(regionData);

    const formData = new FormData();
    formData.append('name', regionData.regionname);
    console.log('FORMDATA: ' + JSON.stringify(formData));
    return this.http.put(this.url+id, formData);
  }
}

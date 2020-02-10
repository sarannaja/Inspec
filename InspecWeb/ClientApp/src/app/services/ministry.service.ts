import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class MinistryService {
  url = "https://localhost:5001/api/ministry/";

  constructor(private http:HttpClient) { }
  getministry(){
    return this.http.get(this.url)
  }
  addMinistry(ministryData){
    const formData = new FormData();
    formData.append('name',ministryData.ministryname);
    return this.http.post(this.url, formData);
  }
  deleteMinistry(id) {
    return this.http.delete(this.url + id);
  }
  editMinistry(ministryData,id) {
    console.log(ministryData);

    const formData = new FormData();
    formData.append('name', ministryData.ministryname);
    console.log('FORMDATA: ' + JSON.stringify(formData));
    return this.http.put(this.url+id, formData);
  }
}

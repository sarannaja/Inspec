import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class MinistryService {
  url = "";

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) 
  {
    this.url = baseUrl + 'api/ministry/';
  }
  getministry() {
    return this.http.get(this.url)
  }
  addMinistry(ministryData) {
    const formData = new FormData();
    formData.append('name', ministryData.ministryname);
    return this.http.post(this.url, formData);
  }
  deleteMinistry(id) {
    return this.http.delete(this.url + id);
  }
  editMinistry(ministryData, id) {
    console.log(ministryData);

    const formData = new FormData();
    formData.append('name', ministryData.ministryname);
    console.log('FORMDATA: ' + JSON.stringify(formData));
    return this.http.put(this.url + id, formData);
  }
}

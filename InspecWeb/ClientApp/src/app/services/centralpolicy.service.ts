import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class CentralpolicyService {

  url = "https://localhost:5001/api/centralpolicy";

  constructor(private http: HttpClient) { }

  addCentralpolicy(formData) {
    // alert(JSON.stringify(centralpolicyData))
    // files



    // console.log('FORMDATA: ' + formData);

    return this.http.post(this.url, formData);
  }

}

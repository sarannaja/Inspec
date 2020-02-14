import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class CentralpolicyService {


  url = "";

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) 
   {
    this.url = baseUrl + 'api/centralpolicy/';
   }

  addCentralpolicy(formData) {
    // alert(JSON.stringify(centralpolicyData))
    // files



    // console.log('FORMDATA: ' + formData);

    return this.http.post(this.url, formData);
  }

}

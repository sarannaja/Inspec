import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class CentralpolicyService {

  url = "https://localhost:5001/api/centralpolicy/";

  constructor(private http: HttpClient) { }

  addCentralpolicy(centralpolicyData) {
    alert(JSON.stringify(centralpolicyData))

    const formData = new FormData();

    formData.append('title', centralpolicyData.title);
    formData.append('start_date', centralpolicyData.start_date.date.year + '-' + centralpolicyData.start_date.date.month + '-' + centralpolicyData.start_date.date.day);
    formData.append('end_date', centralpolicyData.end_date.date.year + '-' + centralpolicyData.end_date.date.month + '-' + centralpolicyData.end_date.date.day);
    formData.append('subjects', centralpolicyData.subjects);
    formData.append('files', "filetest.pdf");

    console.log('FORMDATA: ' + formData);

    return this.http.post(this.url, formData);
  }

}

import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class InspectionplaneventService {

  url = "";

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string)
   {
    this.url = baseUrl + 'api/inspectionplanevent/';
   }

   getinspectionplaneventdata():Observable<any[]> {
    return this.http.get<any[]>(this.url)
  }

  addInspectionplanevent(inspectionplaneventData) {

    const formData = new FormData();

    formData.append('title', inspectionplaneventData.title);
    formData.append('start_date', inspectionplaneventData.start_date.date.year + '-' + inspectionplaneventData.start_date.date.month + '-' + inspectionplaneventData.start_date.date.day);
    formData.append('end_date', inspectionplaneventData.end_date.date.year + '-' + inspectionplaneventData.end_date.date.month + '-' + inspectionplaneventData.end_date.date.day);

    console.log('FORMDATA: ' + formData);
    return this.http.post(this.url, formData);
  }

  deleteInspectionplanevent(id) {
    return this.http.delete(this.url + id);
  }

}

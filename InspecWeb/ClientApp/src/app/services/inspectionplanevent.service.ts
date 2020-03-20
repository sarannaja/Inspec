import { Injectable, Inject, Input } from '@angular/core';
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
    // alert(JSON.stringify(inspectionplaneventData.input))
    var input = inspectionplaneventData.input.map((item , index) => {
      return {
        PlanDate:item.date.date.year + '-' + item.date.date.month + '-' + item.date.date.day,
        ProvinceId:item.provinces,
      }
    })
    // alert(JSON.stringify(input))
    const formData = {
      Name: inspectionplaneventData.title,
      input: input,
    }
    console.log('FORMDATA: ' + formData);
    return this.http.post(this.url, formData);
  }

  deleteInspectionplanevent(id) {
    return this.http.delete(this.url + id);
  }

}

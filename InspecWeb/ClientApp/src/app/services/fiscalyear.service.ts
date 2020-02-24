import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FiscalyearService {

  count = 0
  url = "";

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.url = baseUrl + 'api/fiscalyear/';
  }

  getfiscalyeardata(): Observable<any[]> {
    return this.http.get<any[]>(this.url)
  }
  addFiscalyear(fiscalyearData) {
    const formData = new FormData();
    formData.append('year', fiscalyearData.fiscalyear);
    console.log('FORMDATA: ' + formData);
    return this.http.post(this.url, formData);
  }
  deleteFiscalyear(id) {
    return this.http.delete(this.url + id);
  }
  editFiscalyear(fiscalyearData, id) {
    console.log(fiscalyearData);
    const formData = new FormData();
    formData.append('year', fiscalyearData.fiscalyear);
    console.log('FORMDATA: ' + JSON.stringify(formData));
    return this.http.put(this.url + id, formData);
  }
}

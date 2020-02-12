import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class FiscalyearService {

  count = 0
  url = "https://localhost:5001/api/fiscalyear/";

  constructor(private http: HttpClient) { }

  getfiscalyeardata() {
    return this.http.get(this.url)
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

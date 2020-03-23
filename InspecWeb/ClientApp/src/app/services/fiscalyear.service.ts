import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FiscalyearService {

  count = 0
  url = "";

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    this.url = baseUrl + 'api/fiscalyear/';
  }

  getfiscalyeardata(): Observable<any[]> {
    return this.http.get<any[]>(this.url)
  }
  addFiscalyear(fiscalyearData) {
    const formData = new FormData();
    formData.append('year', fiscalyearData.fiscalyear);
    
    formData.append('startdate', fiscalyearData.startdate.date.year + '-' + fiscalyearData.startdate.date.month + '-' + fiscalyearData.startdate.date.day);
    formData.append('enddate', fiscalyearData.enddate.date.year + '-' + fiscalyearData.enddate.date.month + '-' + fiscalyearData.enddate.date.day);
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

  getDetailFiscalyear(id): Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl = "api/fiscalyear/" + id)
  }
  getProvinceRecycled(id): Observable<any[]> {
    return this.http.get<any[]>(this.url + "getProvinceRecycled/" + id)
  }
  addDetailFiscalyear(detailfiscalyearData, Id) {
    // console.log("ARRAY: ", detailfiscalyearData.ProvinceId);

    const formData = {

      FiscalYearId: parseInt(Id),
      RegionId: detailfiscalyearData.RegionId,
      ProvinceId: detailfiscalyearData.ProvinceId
    }

    console.log('FORMDATA: ', formData);
    return this.http.post(this.url + "AddRelation", formData);
  }
  deleteDetailFiscalyear(id, fiscalyearid) {
    // console.log("PPP: " + id);

    return this.http.delete(this.url + "DeleteRelation/" + id + "/" + fiscalyearid);
  }
}

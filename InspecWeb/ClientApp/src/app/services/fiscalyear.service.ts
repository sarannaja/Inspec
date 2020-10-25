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
    return this.http.get<any>(this.url)
  }
  getcurrentyeardata(year): Observable<any> {
    // alert(year)
    return this.http.get<any>(this.url + "getCurrentFiscalYear/" + year)
  }
  getreportfiscalyearrelations(fiscalYearId, userId) {
    return this.http.get<any>(this.url + "getReportFiscalYearRelations/" + fiscalYearId + "/" + userId)
  }
  addFiscalyear(data, file: FileList) {
    const formData = new FormData();
    formData.append('Year', data.year);
    formData.append('StartDate', data.startdate.date.year + '-' + data.startdate.date.month + '-' + data.startdate.date.day);
    formData.append('EndDate', data.enddate.date.year + '-' + data.enddate.date.month + '-' + data.enddate.date.day);
    formData.append('Orderdate', data.orderdate.date.year + '-' + data.orderdate.date.month + '-' + data.orderdate.date.day);
    console.log('FORMDATA: ' + formData);

    if (file != null) {
      for (var iii = 0; iii < file.length; iii++) {
        formData.append("files", file[iii]);
      }
    } else {
      formData.append("files", null);
    }
    return this.http.post(this.url, formData);
  }
  deleteFiscalyear(id) {
    return this.http.delete(this.url + id);
  }
  editFiscalyear(data, file: FileList, id) {
    console.log(data);
    const formData = new FormData();
    formData.append('Year', data.year);
    formData.append('StartDate', data.startdate.date.year + '-' + data.startdate.date.month + '-' + data.startdate.date.day);
    formData.append('EndDate', data.enddate.date.year + '-' + data.enddate.date.month + '-' + data.enddate.date.day);
    formData.append('Orderdate', data.orderdate.date.year + '-' + data.orderdate.date.month + '-' + data.orderdate.date.day);
    console.log('FORMDATA: ' + formData);

    if (file != null) {
      for (var iii = 0; iii < file.length; iii++) {
        formData.append("files", file[iii]);
      }
    } else {
      formData.append("files", null);
    }
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
  editDetailFiscalyear(detailfiscalyearData, Idfiscalyear, id) {

    const formData = {

      FiscalYearId: parseInt(Idfiscalyear),
      RegionId: detailfiscalyearData.RegionId,
      ProvinceId: detailfiscalyearData.ProvinceId
    }

    console.log('FORMDATA: ', formData);
    return this.http.post(this.url + "EditRelation/" + id, formData);
  }
  deleteDetailFiscalyear(id, fiscalyearid) {
    // console.log("PPP: " + id);

    return this.http.delete(this.url + "DeleteRelation/" + id + "/" + fiscalyearid);
  }



  activeFiscalyear(id) {
    const formData = new FormData();
    formData.append('Id', id);
    return this.http.put<any>(`${this.url}activefiscalyear`, formData);
  }
}

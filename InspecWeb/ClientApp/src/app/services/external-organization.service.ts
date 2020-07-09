import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Regions, Ministers, Cabinets, Province, ProvinceFiscalYears, ProvinceFiscalYear } from '../external-organization/models/otps' ;
import { NewRegion } from '../external-organization/models/Region';
import { OtpsProvineFiscalYear } from '../models/otpsprovince';
import { ProvinceOtps } from '../external-organization/models/province-otps';
import { OpmCase } from '../external-organization/models/opmcase';
// import { NewRegion, Province } from '../external-organization/models/Region';
// [assembly: RegisterModule(typeof(SSLRequestModule))]                                                                                                                                                                                                                                                                                                                                             

@Injectable({
  providedIn: 'root'
})
export class ExternalOrganizationService {
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json; charset=utf-8'
    })
  };
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }
  //api ระบบพี่ตัง
  getRegions(): Observable<Regions[]> {
    return this.http.get<Regions[]>('http://203.113.14.20:3000/testservice/otps/regions', this.httpOptions)
  }
  getRegionById(id): Observable<NewRegion> {
    return this.http.get<NewRegion>(`${this.baseUrl}api/ExternalOrganization/otps/region/${id}`, this.httpOptions)
  }

  getProvinces():Observable<Province[]>{
    return this.http.get<Province[]>(this.baseUrl + 'api/ExternalOrganization/otps/provinces', this.httpOptions)
  }
  getProvince(id:any):Observable<ProvinceFiscalYear>{
    return this.http.get<ProvinceFiscalYear>(this.baseUrl + `api/ExternalOrganization/otps/provinces/${id}`, this.httpOptions)
  }
  
  getMinisters(): Observable<Ministers[]> {
    return this.http.get<Ministers[]>(this.baseUrl + 'api/ExternalOrganization/otps/ministers', this.httpOptions)
  }

  getCabinets(): Observable<Cabinets[]> {
    return this.http.get<Cabinets[]>(this.baseUrl + 'api/ExternalOrganization/otps/cabinets', this.httpOptions)
  }

  getGccopm(provinceId:any,representId:any): Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl + `api/ExternalOrganization/gcc-opm/${provinceId}/${representId}`, this.httpOptions)
  }

  getGccProvice(): Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl + 'api/ExternalOrganization/gcc/provinces', this.httpOptions)
  }

  getGccwara(): Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl + 'api/ExternalOrganization/gcc/wara', this.httpOptions)
  }

  getGcc1111(): Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl + 'api/ExternalOrganization/opm-1111', this.httpOptions)
  }
  getGcc1111CaseDetail(id): Observable<OpmCase> {
    return this.http.get<OpmCase>(this.baseUrl + `api/ExternalOrganization/opm/case/${id}`, this.httpOptions)
  }
  getGcc1111UserProvince(userId): Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl + `api/ExternalOrganization/opm/userprovince/${userId}`, this.httpOptions)
  }
  getOtpsProviceById(id): Observable<OtpsProvineFiscalYear> {
    return this.http.get<OtpsProvineFiscalYear>(this.baseUrl + `api/ExternalOrganization/otps/provinces2/${id}`, this.httpOptions)
  }
  getOtpsProviceOtps(): Observable<ProvinceOtps[]> {
    return this.http.get<ProvinceOtps[]>(this.baseUrl + `api/ExternalOrganization/otps/provinces2`)
  }
}

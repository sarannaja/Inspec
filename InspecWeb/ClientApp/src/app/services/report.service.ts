import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Report1 } from '../models/reportnik';

@Injectable({
  providedIn: 'root'
})
export class ReportService {

  url = "";


  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.url = baseUrl + 'api/report/';
  }

  getreportsubject(id): Observable<Report1> {
    return this.http.get<Report1>(this.url + id)
  }

}

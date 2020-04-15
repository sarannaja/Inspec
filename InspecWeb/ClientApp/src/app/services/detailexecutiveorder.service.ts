import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class DetailexecutiveorderService {
  url = "";
  files: FileList

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.url = baseUrl + 'api/detailexecutiveorder/';
}

  getExecutiveorderdata(): Observable<any[]> {
    return this.http.get<any[]>(this.url)
  }

  getdetailExecutiveorderdata(id): Observable<any> {
    return this.http.get<any>(this.url + id)
  }

}


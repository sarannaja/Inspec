import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ExportReportService {
  url = "";

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') baseUrl: string
  ) {
    this.url = baseUrl + 'api/export';
  }

  exportReport(userId) {
    console.log("in service: ", userId);

    const formData = {
      userId: userId,
      typeId : "1",
    }

    return this.http.post<any>(this.url + "/report", formData)
  }
}

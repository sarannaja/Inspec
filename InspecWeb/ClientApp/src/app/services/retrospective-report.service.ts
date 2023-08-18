import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class RetrospectiveReportService {
  url = "";

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') baseUrl: string
  ) {
    this.url = baseUrl + 'api/OldReport/'
  }

  getOldReports() {
    return this.http.get<any>(this.url + "getOldReports");
  }

  getOldReportById(oldReportId) {
    return this.http.get<any>(this.url + "getOldReport/" + oldReportId);
  }

  importOldReport(value, userId, file: FileList) {
    console.log('value => ', value);
    console.log('userId => ', userId);

    const formData = new FormData();
    formData.append('Year', value.year.toString());
    formData.append('CentralPolicyType', value.centralPolicyType);
    formData.append('Name', value.name);
    formData.append('ReportType', value.reportType);
    formData.append('userId', userId);
    formData.append('Round', value.round);

    if (file != null) {
      for (var i = 0; i < file.length; i++) {
        formData.append("files", file[i]);
      }
    }
    console.log('name', formData.getAll("Name"));
    return this.http.post<any>(this.url + "importOldReport", formData);
  }

  editOldReport(value, file: FileList, oldReportId) {
    console.log('edit value => ', value);
    console.log('edit oldReportId => ', oldReportId);

    const formData = new FormData();
    formData.append('Year', value.year.toString());
    formData.append('CentralPolicyType', value.centralPolicyType);
    formData.append('Name', value.name);
    formData.append('ReportType', value.reportType);
    formData.append('Round', value.round);

    if (file != null) {
      for (var i = 0; i < file.length; i++) {
        formData.append("files", file[i]);
      }
    }
    return this.http.put<any>(this.url + "editOldReport/" + oldReportId, formData);
  }

  deleteOldReport(oldReportId) {
    return this.http.delete<any>(this.url + "deleteOldReport/" + oldReportId)
  }
}

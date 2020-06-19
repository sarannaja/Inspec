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

  exportReport(userId, electId, cenProId) {
    console.log("in service: ", userId);

    const formData = {
      userId: userId,
      typeId: "1",
      centralPolicyProvinceId: cenProId
    }

    return this.http.post<any>(this.url + "/report", formData)
  }

  createReport(reportData, cenProId) {

    var exportData: any = [];

    exportData = reportData.subjectcentralpolicyprovincedata.map((item, index) => {
      //  item.electronicBookSuggestGroups.forEach(element => {
      //   test = element.detail + ", " + test;
      // });
      var detail: any = [];
      var suggestion: any = [];
      var problem: any = [];
      var opinion: any = [];

      for (let index = 0; index < item.electronicBookSuggestGroups.length; index++) {
        const element = item.electronicBookSuggestGroups[index];
        if (element.detail == null || element.detail == "null" || element.detail == "") {
          detail = "-"
        } else {
          detail = detail + element.detail + ", ";
        }

      }

      for (let index = 0; index < item.electronicBookSuggestGroups.length; index++) {
        const element = item.electronicBookSuggestGroups[index];
        // alert(element.suggestion);
        if (element.suggestion == null || element.suggestion == "null" || element.suggestion == "") {
          suggestion = "-"
        } else {
          suggestion = suggestion + element.suggestion + ", ";
        }
      }

      for (let index = 0; index < item.electronicBookSuggestGroups.length; index++) {
        const element = item.electronicBookSuggestGroups[index];
        if (element.problem == null || element.problem == "null" || element.problem == "") {
          problem = "-"
        } else {
          problem = problem + element.problem + ", ";
        }
      }

      for (let index = 0; index < item.suggestionSubjects.length; index++) {
        const element = item.suggestionSubjects[index];
        opinion = opinion + element.suggestion + ", ";
      }

      return {
        subject: item.name,
        department: item.subquestionCentralPolicyProvinces[0].subjectCentralPolicyProvinceGroups[0].provincialDepartment.name,
        detail: detail,
        suggestion: suggestion,
        problem: problem,
        opinionPeople: opinion
      }
    })

    console.log("CHECK: ", exportData);

    const formData = {
      reportData: exportData,
      centralPolicyProvinceId: cenProId
    }

    return this.http.post<any>(this.url + "/createReport", formData)
  }

  getSubjectReport() {
    return this.http.get<any>(this.url + "/subjectImport");
  }

  getImportedReport(userId) {
    return this.http.get<any>(this.url + "/getImportedReport/" + userId);
  }

  getCommanderReport() {
    return this.http.get<any>(this.url + "/getCommanderReport");
  }

  getCommanderReportById(reportId) {
    return this.http.get<any>(this.url + "/getCommanderReport/" + reportId);
  }

  postImportedReport(value, file: FileList, fileExcel: FileList, userId) {
    console.log("Value: ", value);
    console.log("Word: ", file);
    console.log("Excel: ", fileExcel);

    const formData = new FormData();
    formData.append('SubjectId', value.Subject);
    formData.append('TypeReport', value.TypeReport);
    formData.append('TypeExport', value.TypeExport);
    formData.append('CreateBy', userId);

    for (var i = 0; i < file.length; i++) {
      formData.append("fileWord", file[i]);
    }

    for (var i = 0; i < fileExcel.length; i++) {
      formData.append("fileExcel", fileExcel[i]);
    }

    return this.http.post<any>(this.url + "/addImportReport", formData);
  }

  deleteReport(deleteId) {
    return this.http.delete<any>(this.url + "/deleteImportedReport/" + deleteId);
  }

  sendCommand(value, reportId, commanderId) {
    console.log("ReportId: ", reportId);
    console.log("CommanderId: ", commanderId);
    console.log("Command Value: ", value.command);

    const formData = {
      Command: value.command,
      Commander: commanderId,
      ReportId: reportId
    }

    // const formData = new FormData();
    // formData.append('Command', value.command);
    // formData.append('Commander', commanderId);

    return this.http.put<any>(this.url + "/sendCommand", formData);
  }
}

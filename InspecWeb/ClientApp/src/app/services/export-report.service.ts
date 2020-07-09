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

  createReport2(res, reportId) {
    var exportData: any = [];
    exportData = res.importData.importReportGroups.map((item, index) => {
      var subjectData: any = [];
      var subjectMaster: any = [];
      var departmentData: any = [];
      var departmentStr: any = [];
      var departmentAll: any = [];
      var subquestion: any = [];

      item.centralPolicyEvent.centralPolicy.centralPolicyProvinces.forEach(element => {
        subjectData = element.subjectCentralPolicyProvinces.map(element2 => {
          if (element2.type == "Master") {
            return {
              subject: element2.name
            }
          }
        });
      });
      subjectData.forEach(elementS => {
        if (elementS != undefined) {
          subjectMaster.push(elementS)
        }
      });
      console.log("subjectMaster: ", subjectMaster);

      // item.centralPolicyEvent.centralPolicy.centralPolicyProvinces.forEach(element => {
      //   departmentData = element.subjectCentralPolicyProvinces.map(element2 => {
      //     if (element2.type == "Master") {
      //       element2.subquestionCentralPolicyProvinces.forEach(element3 => {
      //         console.log("ele3: ", element3);
      //         subquestion = subquestion + element3.name;
      //         element3.subjectCentralPolicyProvinceGroups.forEach(element4 => {
      //           console.log("ele4: ", element4);
      //           departmentStr = departmentStr + element4.provincialDepartment.name;
      //         });
      //       });
      //       return {
      //         department: departmentStr,
      //         subquestion: subquestion
      //       }
      //     }
      //   });
      // });
      // console.log("department: ", departmentData);
      // console.log("subjectDataJa: ", subjectData);
      // departmentData.forEach(elementD => {
      //   if (elementD != undefined) {
      //     departmentAll.push(elementD)
      //   }
      // });
      // console.log("departmentStr: ", departmentAll);

      return {
        centralPolicy: item.centralPolicyEvent.centralPolicy.title,
        department: res.importData.user.departments.name,
        tableData: subjectMaster,
        // centralPolicyType: res.importData.centralPolicyType,
        // command: res.importData.command,
        // detailReport: res.importData.detailReport,
        fiscalYear: res.importData.fiscalYear.year,
        // inspectionRound: res.importData.inspectionRound,
        // monitoringTopics: res.importData.monitoringTopics,
        province: res.importData.province.name,
        region: res.importData.region.name,
        // reportType: res.importData.reportType,
        // suggestion: res.importData.suggestion
      }
    })
    console.log("Data: ", exportData);

    const formData = {
      reportData2: exportData,
      reportId: reportId
    }
    return this.http.post<any>(this.url + "/createReport2X", formData)
  }

  getSubjectReport() {
    return this.http.get<any>(this.url + "/subjectImport");
  }

  getImportedReport(userId) {
    return this.http.get<any>(this.url + "/getImportedReport/" + userId);
  }

  getImportedReportById(reportId) {
    console.log("reportId: ", reportId);

    return this.http.get<any>(this.url + "/getImportedReportById/" + reportId);
  }

  getCommanderReport() {
    return this.http.get<any>(this.url + "/getCommanderReport");
  }

  getCommanderReportById(provinceId) {
    console.log("provinceId: ", provinceId);

    return this.http.get<any>(this.url + "/getCommanderReport/" + provinceId);
  }

  postImportReport(value, userId, file: FileList) {
    const formData = new FormData();

    for (var i = 0; i < value.centralPolicyEvent.length; i++) {
      formData.append("centralPolicyEventId", value.centralPolicyEvent[i]);
    }

    formData.append('centralPolicyType', value.centralPolicyType);
    formData.append('reportType', value.reportType);
    formData.append('inspectionRound', value.inspectionRound);
    formData.append('fiscalYearId', value.fiscalYear);
    formData.append('regionId', value.region);
    formData.append('provinceId', value.province);
    formData.append('monitoringTopics', value.monitoringTopics);
    formData.append('detailReport', value.detailReport);
    formData.append('suggestion', value.suggestion);
    formData.append('command', value.command);
    formData.append('UserId', userId);

    for (var i = 0; i < file.length; i++) {
      formData.append("files", file[i]);
    }

    return this.http.post<any>(this.url + "/addImportReport", formData);
  }

  deleteReport(deleteId) {
    return this.http.delete<any>(this.url + "/deleteImportedReport/" + deleteId);
  }

  getImportReportFiscalYears() {
    return this.http.get<any>(this.url + "/getImportReportFiscalYears/");
  }

  getImportReportFiscalYearRelations(fiscalYearId) {
    console.log("fiscalYearId: ", fiscalYearId);

    return this.http.get<any>(this.url + "/getImportReportFiscalYearRelations/" + fiscalYearId);
  }

  sendToCommander(reportID) {
    console.log("reportIddd: ", reportID);
    const formData = new FormData();
    formData.append('reportId', reportID);
    return this.http.put<any>(this.url + "/sendReportToCommander", formData);
  }

  sendCommand(reportID, value, userId) {
    console.log("reportIddd: ", reportID);
    console.log("value: ", value);
    console.log("userIDDD: ", userId);

    const formData = new FormData();
    formData.append('reportId', reportID);
    formData.append('command', value.command);
    formData.append('UserId', userId);
    return this.http.post<any>(this.url + "/sendCommand", formData);
  }

  editImportReport(value, reportId) {
    console.log("EditValue: ", value);
    console.log("Edit ID: ", reportId);

    const formData = new FormData();
    for (var i = 0; i < value.centralPolicyEvent.length; i++) {
      formData.append("centralPolicyEventId", value.centralPolicyEvent[i]);
    }
    formData.append('centralPolicyType', value.centralPolicyType);
    formData.append('reportType', value.reportType);
    formData.append('inspectionRound', value.inspectionRound);
    formData.append('fiscalYearId', value.fiscalYear);
    formData.append('regionId', value.region);
    formData.append('provinceId', value.province);
    formData.append('monitoringTopics', value.monitoringTopics);
    formData.append('detailReport', value.detailReport);
    formData.append('suggestion', value.suggestion);
    formData.append('command', value.command);
    formData.append('reportId', reportId);

    return this.http.post<any>(this.url + "/editImportReport", formData);
  }
}

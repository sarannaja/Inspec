import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Calendar } from 'src/app/services/modelaof/reportInspectionplan';
import { NgIf } from '@angular/common';
import { data } from 'jquery';
import { Observable } from 'rxjs';
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
    console.log("REPORT RES: ", res);
    var subjectData: any = [];
    var subjectMaster: any = [];
    var departmentData: any = [];
    var departmentStr: any = [];
    var departmentAll: any = [];
    var subquestion: any = [];

    exportData = res.importData.importReportGroups.map((item, index) => {
      item.centralPolicyEvent.centralPolicy.centralPolicyProvinces.forEach(element => {
        element.subjectCentralPolicyProvinces.map(element2 => {
          console.log("TTEST: ", element2.name);

          if (element2.type == "Master") {
            subjectData.push({
              subject: element2.name
            })
            // return {
            //   subject: element2.name
            // }
          }
        });
      });
      console.log("subJECTDATA: ", subjectData);

      // var uniqueSubject = subjectData.filter(
      //   (thing, i, arr) => arr.findIndex(t => t.regionId === thing.regionId) === i
      // );
      // console.log("uniqueSubjectDATA: ", uniqueSubject);
      subjectData.forEach(elementS => {
        if (elementS != undefined) {
          subjectMaster.push(elementS)
        }
      });
      console.log("subjectMaster: ", subjectMaster);
      var uniqueSubjectMaster = subjectMaster.filter(
        (thing, i, arr) => arr.findIndex(t => t.subject === thing.subject) === i
      );
      console.log("uniqueSubjectDATA: ", uniqueSubjectMaster);

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
      subjectData = [];
      subjectMaster = [];

      return {
        centralPolicy: item.centralPolicyEvent.centralPolicy.title,
        department: res.importData.user.departments.name,
        tableData: uniqueSubjectMaster,
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
    return this.http.post<any>(this.url + "/createReport2", formData)
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

  getCommanderReportById(provinceId, userID) {
    console.log("provinceId: ", provinceId);

    return this.http.get<any>(this.url + "/getCommanderReport/" + provinceId + "/" + userID);
  }

  getCommanderReportDetailById(reportId) {
    console.log("reportId: ", reportId);

    return this.http.get<any>(this.url + "/getCommanderReportDetailById/" + reportId);
  }

  postImportReport(value, userId, file: FileList, departmentId) {
    const formData = new FormData();
    console.log("DepartmentReport: ", departmentId);

    for (var i = 0; i < value.centralPolicyEvent.length; i++) {
      formData.append("centralPolicyEventId", value.centralPolicyEvent[i]);
    }

    formData.append('centralPolicyTypeId', value.centralPolicyType);
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
    formData.append('DepartmentId', departmentId);

    if (file != null) {
      for (var i = 0; i < file.length; i++) {
        formData.append("files", file[i]);
      }
    }

    return this.http.post<any>(this.url + "/addImportReport", formData);
  }

  deleteReport(deleteId) {
    return this.http.delete<any>(this.url + "/deleteImportedReport/" + deleteId);
  }

  getImportReportFiscalYears() {
    return this.http.get<any>(this.url + "/getImportReportFiscalYears/");
  }

  getImportReportFiscalYears2() {
    return this.http.get<any>(this.url + "/getImportReportFiscalYears2/");
  }

  getImportReportFiscalYearRelations() {
    return this.http.get<any>(this.url + "/getImportReportFiscalYearRelations");
  }
  getImportReportprovinceFiscalYearRelations(fiscalYearId, regionid) {
    console.log("fiscalYearId: ", fiscalYearId);

    return this.http.get<any>(this.url + "/getImportReportprovinceFiscalYearRelations/" + fiscalYearId + "/" + regionid);
  }

  getImportReportdepartmentFiscalYearRelations(provinceid) {
    console.log("fiscalYearId: ", provinceid);

    return this.http.get<any>(this.url + "/getImportReportdepartmentFiscalYearRelations/" + provinceid);
  }

  getImportReportpeopleFiscalYearRelations(departmentid, provinceid) {
    console.log("fiscalYearId: ", departmentid);

    return this.http.get<any>(this.url + "/getImportReportpeopleFiscalYearRelations/" + departmentid + "/" + provinceid);
  }

  sendToCommander(reportID, value) {
    console.log("reportIddd: ", reportID);
    console.log("VALUEJA: ", value.commander);

    const formData = new FormData();
    formData.append('reportId', reportID);
    // formData.append('Commander', value.commander);
    for (var i = 0; i < value.commander.length; i++) {
      formData.append("CommanderAr", value.commander[i]);
    }
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
    return this.http.put<any>(this.url + "/sendCommand", formData);
  }

  editImportReport(value, reportId) {
    console.log("EditValue: ", value);
    console.log("Edit ID: ", reportId);

    const formData = new FormData();
    // for (var i = 0; i < value.centralPolicyEvent.length; i++) {
    //   formData.append("centralPolicyEventId", value.centralPolicyEvent[i]);
    // }
    formData.append('centralPolicyTypeId', value.centralPolicyType);
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

  getCommander() {
    return this.http.get<any>(this.url + "/getCommander");
  }

  getAllImportedReport() {
    return this.http.get<any>(this.url + "/getAllImportedReport");
  }

  getDepartments() {
    return this.http.get<any>(this.url + "/getDepartments");
  }

  getRegions() {
    return this.http.get<any>(this.url + "/getRegions");
  }

  getZones() {
    return this.http.get<any>(this.url + "/getZones");
  }

  getPresident() {
    return this.http.get<any>(this.url + "/getPresident");
  }

  getProvinces() {
    return this.http.get<any>(this.url + "/getProvinces");
  }

  getAllReportByDepartment(value) {
    console.log("Department: ", value);
    var departmentId = value.department;

    return this.http.get<any>(this.url + "/getAllReportByDepartment/" + departmentId);
  }

  reportDepartment(res, reportType) {
    var exportData: any = [];
    console.log("REPORT RES: ", res);
    console.log("Report Type: ", reportType);

    var subjectData: any = [];
    var department: any;
    var command: any;
    var commandDate: any;
    department = res.reports[0].department.name;
    exportData = res.reports.map((item, index) => {
      subjectData = [];
      command = [];
      item.importReportGroups.forEach(element => {
        element.centralPolicyEvent.centralPolicy.centralPolicyProvinces.forEach(element2 => {
          element2.subjectCentralPolicyProvinces.forEach(element3 => {
            if (element3.type == "Master") {
              if (element3.name == null || element3.name == "null" || element3.name == "") {
                subjectData = "-"
              } else {
                subjectData = subjectData + element3.name + "\n";
              }
            }
          });
        });
      });
      console.log("subJAAA: ", subjectData);

      for (let index = 0; index < item.reportCommanders.length; index++) {
        const element = item.reportCommanders[index];
        if (element.command == null || element.command == "null" || element.command == "") {
          command = "ไม่มี";
          commandDate = null
        } else {
          command = command + element.command + "\n"
          // commandDate = commandDate + element.commandDate
        }
      }

      return {
        dateReport: item.createAt,
        subject: subjectData,
        createBy: item.user.prefix + item.user.name,
        status: item.status,
        command: command,
        // dateCommand: commandDate,
      }
    });
    console.log("ExportDATA: ", exportData);
    const formData = {
      allReport: exportData,
      reportType: reportType,
      reportDepartment: department,
    }
    return this.http.post<any>(this.url + "/exportAllDepartmentReport", formData)
  }

  // getAllReportByPresidentId(value) {
  //   console.log("President: ", value);
  //   var presidentId = value.president;

  //   return this.http.get<any>(this.url + "/getAllReportByPresident/" + presidentId);
  // }

  getAllReportByPresidentId(value) {
    console.log("Region: ", value);
    var regionId = value;

    return this.http.get<any>(this.url + "/getAllReportByRegion/" + regionId);
  }

  getAllReportByRegionId(value) {
    console.log("Region: ", value);
    var regionId = value.region;

    return this.http.get<any>(this.url + "/getAllReportByRegion/" + regionId);
  }

  getAllReportByZoneId(value) {
    console.log("Zone: ", value);
    var zoneId = value.zone;

    return this.http.get<any>(this.url + "/getAllReportByZone/" + zoneId);
  }

  getAllReportProvinceId(value) {
    console.log("provinceId: ", value);
    var provinceId = value.province;

    return this.http.get<any>(this.url + "/getAllReportByProvince/" + provinceId);
  }

  getAllReportDay(value) {

    var dateReport: Array<any> = value.inputdate.map((item, index) => {
      return {
        startDate: item.start_date.date.year + '-' + item.start_date.date.month + '-' + item.start_date.date.day,
      }
    })
    console.log("DATENAJA: ", dateReport);

    const formData = new FormData();
    formData.append('startDate', dateReport[0].startDate);

    return this.http.post<any>(this.url + "/getAllReportByDay", formData);
  }

  reportRegion(res, reportType) {
    var exportData: any = [];
    console.log("REPORT RES: ", res);
    console.log("Report Type: ", reportType);

    var subjectData: any = [];
    var provinceData: any = [];
    var region: any;
    var regionId: any;
    var command: any = [];
    var commandDate: any = [];
    region = res.reports[0].region.name;
    regionId = res.reports[0].region.id;

    exportData = res.reports.map((item, index) => {
      subjectData = [];
      command = [];
      commandDate = [];
      item.importReportGroups.forEach(element => {
        element.centralPolicyEvent.centralPolicy.centralPolicyProvinces.forEach(element2 => {
          element2.subjectCentralPolicyProvinces.forEach(element3 => {
            if (element3.type == "Master") {
              if (element3.name == null || element3.name == "null" || element3.name == "") {
                subjectData = "-"
              } else {
                subjectData = subjectData + element3.name + "\n";
              }
            }
          });
        });
      });
      console.log("subJAAA: ", subjectData);

      for (let index = 0; index < item.reportCommanders.length; index++) {
        const element = item.reportCommanders[index];
        if (element.command == null || element.command == "null" || element.command == "") {
          command = "ไม่มี";
          commandDate = null
        } else {
          command = command + element.command + "\n"
          // commandDate = commandDate + element.commandDate
        }
      }
      // command = item.reportCommanders[0].command;
      // commandDate = item.reportCommanders[0].createAt;


      return {
        dateReport: item.createAt,
        subject: subjectData,
        createBy: item.user.prefix + item.user.name,
        status: item.status,
        command: command,
        // dateCommand: commandDate,
        provinceReport: item.province.name,
      }
    });
    console.log("ExportDATA: ", exportData);
    const formData = {
      allReport: exportData,
      reportType: reportType,
      reportRegion: region,
      // reportRegionId: regionId,
    }
    console.log("formData: ", formData);
    return this.http.post<any>(this.url + "/exportAllRegionReport", formData)
  }

  reportProvince(res, reportType) {
    var exportData: any = [];
    console.log("REPORT RES: ", res);
    console.log("Report Type: ", reportType);

    var subjectData: any = [];
    var provinceData: any = [];
    var province: any;
    var command: any;
    var commandDate: any;
    province = res.reports[0].province.name;

    exportData = res.reports.map((item, index) => {
      subjectData = [];
      command = [];
      item.importReportGroups.forEach(element => {
        element.centralPolicyEvent.centralPolicy.centralPolicyProvinces.forEach(element2 => {
          element2.subjectCentralPolicyProvinces.forEach(element3 => {
            if (element3.type == "Master") {
              if (element3.name == null || element3.name == "null" || element3.name == "") {
                subjectData = "-"
              } else {
                subjectData = subjectData + element3.name + "\n";
              }
            }
          });
        });
      });
      console.log("subJAAA: ", subjectData);

      for (let index = 0; index < item.reportCommanders.length; index++) {
        const element = item.reportCommanders[index];
        if (element.command == null || element.command == "null" || element.command == "") {
          command = "ไม่มี";
          commandDate = null
        } else {
          command = command + element.command + "\n"
          // commandDate = commandDate + element.commandDate
        }
      }

      return {
        dateReport: item.createAt,
        subject: subjectData,
        createBy: item.user.prefix + item.user.name,
        status: item.status,
        command: command,
        // dateCommand: commandDate,
        provinceReport: item.province.name,
      }
    });
    console.log("ExportDATA: ", exportData);
    const formData = {
      allReport: exportData,
      reportType: reportType,
      reportProvince: province,
    }
    return this.http.post<any>(this.url + "/exportAllProvinceReport", formData)
  }

  reportDay(res, reportType) {
    var exportData: any = [];
    console.log("REPORT RES: ", res);
    console.log("Report Type: ", reportType);

    var subjectData: any = [];
    var provinceData: any = [];
    var date: any;
    var command: any;
    var commandDate: any;
    date = res.reports[0].createAt;

    exportData = res.reports.map((item, index) => {
      subjectData = [];
      command = [];
      item.importReportGroups.forEach(element => {
        element.centralPolicyEvent.centralPolicy.centralPolicyProvinces.forEach(element2 => {
          element2.subjectCentralPolicyProvinces.forEach(element3 => {
            if (element3.type == "Master") {
              if (element3.name == null || element3.name == "null" || element3.name == "") {
                subjectData = "-"
              } else {
                subjectData = subjectData + element3.name + "\n";
              }
            }
          });
        });
      });
      console.log("subJAAA: ", subjectData);

      for (let index = 0; index < item.reportCommanders.length; index++) {
        const element = item.reportCommanders[index];
        if (element.command == null || element.command == "null" || element.command == "") {
          command = "ไม่มี";
          commandDate = null
        } else {
          command = command + element.command + "\n"
          // commandDate = commandDate + element.commandDate
        }
      }

      return {
        dateReport: item.createAt,
        subject: subjectData,
        createBy: item.user.prefix + item.user.name,
        status: item.status,
        command: command,
        // dateCommand: commandDate,
        provinceReport: item.province.name,
      }
    });
    console.log("ExportDATA: ", exportData);
    const formData = {
      allReport: exportData,
      reportType: reportType,
      reportDate: date,
    }
    return this.http.post<any>(this.url + "/exportAllDayReport", formData)
  }

  reportZone(res, reportType) {
    var exportData: any = [];
    console.log("REPORT RES: ", res);
    console.log("Report Type: ", reportType);

    var subjectData: any = [];
    var provinceData: any = [];
    var zone: any;
    var command: any;
    var commandDate: any;
    zone = res.reports[0].zone.name;

    exportData = res.reports.map((item, index) => {
      subjectData = [];
      command = [];
      item.importReportGroups.forEach(element => {
        element.centralPolicyEvent.centralPolicy.centralPolicyProvinces.forEach(element2 => {
          element2.subjectCentralPolicyProvinces.forEach(element3 => {
            if (element3.type == "Master") {
              if (element3.name == null || element3.name == "null" || element3.name == "") {
                subjectData = "-"
              } else {
                subjectData = subjectData + element3.name + "\n";
              }
            }
          });
        });
      });
      console.log("subJAAA: ", subjectData);

      for (let index = 0; index < item.reportCommanders.length; index++) {
        const element = item.reportCommanders[index];
        if (element.command == null || element.command == "null" || element.command == "") {
          command = "ไม่มี";
          commandDate = null
        } else {
          command = command + element.command + "\n"
          // commandDate = commandDate + element.commandDate
        }
      }

      return {
        dateReport: item.createAt,
        subject: subjectData,
        createBy: item.user.prefix + item.user.name,
        status: item.status,
        command: command,
        // dateCommand: commandDate,
        provinceReport: item.province.name,
      }
    });
    console.log("ExportDATA: ", exportData);
    const formData = {
      allReport: exportData,
      reportType: reportType,
      reportZone: zone,
    }
    return this.http.post<any>(this.url + "/exportAllZoneReport", formData)

  }
  CreateReportCalendar(res, regionId, provinceId, departmentId, peopleId, date) {

    var inputdate;
    if (date && date.date != null) {
      inputdate = date.date.year + '-' + date.date.month + '-' + date.date.day;
    } else {
      inputdate = null;
    }

    console.log("resresres", res);
    var monthNamesThai = ["มกราคม", "กุมภาพันธ์", "มีนาคม", "เมษายน", "พฤษภาคม", "มิถุนายน",
      "กรกฎาคม", "สิงหาคม", "กันยายน", "ตุลาคม", "พฤษจิกายน", "ธันวาคม"];
    var calendar: Array<any> = res
    var centralPolicyUser: Array<any> = []
    var maptest: Array<any> =
      calendar.map((item, index) => {
        var ddd = new Date(item.startDate)
        var startDate = ddd.getDate() + " " + monthNamesThai[ddd.getMonth()] + " " + (ddd.getFullYear() + 543)
        return {
          index: index + 1,
          startDate,
          title: item.title,
          status: item.status,
          province: item.province,
          namecreatedby: item.namecreatedby,
          phonenumbercreatedby: item.phonenumbercreatedby,
          ///////////
          nameinvited: item.nameinvited.filter(
            (thing, i, arr) => { return arr.findIndex(t => t.userId === thing.userId) === i }
          ).filter(result => {
            return result.centralPolicyId == item.centralPolicyId
          }).map(result2 => {
            return result2.user.prefix + " " + result2.user.name + " " + result2.user.phoneNumber + " " + result2.status + "\n"
          }).toString()
        }
      })

    console.log("maptestmaptest", maptest);

    var provinceId2 = provinceId
    var departmentId2 = departmentId
    var peopleId2 = peopleId
    var regionId2 = regionId
    if (regionId == null) {
      regionId2 = 0;
    }
    if (provinceId == null) {
      provinceId2 = 0;
    }
    if (departmentId == null) {
      departmentId2 = 0;
    }
    if (peopleId == null) {
      peopleId2 = "0";
    }
    const formData = {
      reportCalendarData: maptest,
      regionId: regionId2,
      provinceId: provinceId2,
      departmentId: departmentId2,
      peopleId: peopleId2,
      date: inputdate
    }
    return this.http.post<any>(this.url + "/CreateReportCalendar", formData)
  }

  getCelendarReportById(regionId, provinceId, departmentId, peopleId, date) {
    // alert(provinceId)
    var inputdate;
    if (date && date.date != null) {
      inputdate = date.date.year + '-' + date.date.month + '-' + date.date.day;
    } else {
      inputdate = null;
    }
    // alert(inputdate)

    var provinceId2 = provinceId
    var departmentId2 = departmentId
    var peopleId2 = peopleId
    var regionId2 = regionId
    if (regionId == null) {
      regionId2 = 0;
    }
    if (provinceId == null) {
      provinceId2 = 0;
    }
    if (departmentId == null) {
      departmentId2 = 0;
    }
    if (peopleId == null) {
      peopleId2 = "0";
    }

    const formData = {
      regionId: regionId2,
      provinceId: provinceId2,
      departmentId: departmentId2,
      peopleId: peopleId2,
      date: inputdate
    }
    console.log(formData)
    return this.http.post<any>(this.url + "/getCelendarReportById", formData);
  }


  CreateReportTrainingRegister(trainingid) {

    // alert(trainingid)

    const formData = {
      TrainingId: trainingid,
    }

    return this.http.post<any>(this.url + "/CreateReportTrainingRegister", formData)
  }

  changeActive(reportId) {
    const formData = new FormData();
    formData.append('reportId', reportId);
    return this.http.put<any>(this.url + "/changeActive", formData);
  }

  getAllImportedReportActive() {
    return this.http.get<any>(this.url + "/getAllActiveImportedReport");
  }

  reportRateLogin(data, trainingNameJa, gen, trainingYearJa) {
    console.log('rateData: ', data);
    console.log('trainingName: ', trainingNameJa);
    console.log("gen: ", gen);
    console.log("trainingYear", trainingYearJa);
    var newrateCoure: any;
    var exportData: any = [];

    console.log("newrateCoure=>", newrateCoure);

    exportData = data.map((item, index) => {
      if (item.rateCourse == "" || item.rateCourse == null || item.rateCourse == "null") {
        newrateCoure = 0;
      }
      else {
        newrateCoure = item.rateCourse;
      }
      return {
        name: item.name,
        position: item.registerdata.position,
        department: item.registerdata.department,
        phone: item.registerdata.phone,
        count: item.count,
        countCourse: item.countCourse,
        rateCourse: newrateCoure,
      }
    })
    console.log("exportRelate: ", exportData);

    const formData = {
      allReportRateLogin: exportData,
      trainingName: trainingNameJa,
      trainingGen: gen,
      trainingYear: trainingYearJa,
    }
    console.log("formRelate: ", formData);
    return this.http.post<any>(this.url + "/reportRateLogin", formData);
  }

  reportTrainingList(data) {
    console.log('rateData: ', data);

    var exportData: any = [];

    exportData = data.map((item, index) => {
      return {
        generation: item.trainingdata.generation,
        year: item.trainingdata.year,
        name: item.name,
        detail: item.trainingdata.detail,
        start: item.start,
        end: item.end,
        location: item.trainingdata.location,
        count: item.count,
        approveCount: item.approveCount
      }
    })
    console.log("exportRelate: ", exportData);

    const formData = {
      allReportTrainingRegister: exportData,
    }
    console.log("formRelate: ", formData);
    return this.http.post<any>(this.url + "/reportTrainingRegister", formData);
  }

  sortDate(userId) {
    return this.http.get(this.url + "/sortDate/" + userId)
  }

  sortDateDESC(userId) {
    return this.http.get(this.url + "/sortDateDESC/" + userId)
  }
}

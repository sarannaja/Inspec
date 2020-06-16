import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ElectronicbookService {

  url = "";

  constructor(
    private http: HttpClient, @Inject('BASE_URL') baseUrl: string
  ) {
    this.url = baseUrl + 'api/electronicbook/';
  }

  getElectronicBook(userId) {
    return this.http.get(this.url + userId)
  }

  getElectronicBookInvited(userId) {
    return this.http.get(this.url + "invited/" + userId)
  }

  addElectronicBook(value, id, file: FileList, CentralPolicyId, provinceId) {
    // alert(value.description);
    // alert( value.fileType)
    console.log("Add EBook: ", value);

    const formData = new FormData();
    formData.append('Detail', value.checkDetail);
    formData.append('Problem', value.Problem);
    formData.append('Suggestion', value.Suggestion);
    formData.append('id', id);
    formData.append('Status', value.Status);
    formData.append('CentralPolicyId', CentralPolicyId);
    formData.append('Description', value.description);
    formData.append('Type', value.fileType);
    formData.append('ProvinceId', provinceId);

    for (var i = 0; i < value.UserMinistryId.length; i++) {
      formData.append('UserMinistryId', value.UserMinistryId[i]);
    }
    for (var i = 0; i < value.UserPeopleId.length; i++) {
      formData.append('UserPeopleId', value.UserPeopleId[i]);
    }

    console.log("detail", formData.getAll("Detail"));
    console.log("Problem", formData.getAll("Problem"));
    console.log("Suggestion", formData.getAll("Suggestion"));
    console.log("id", formData.getAll("id"));
    console.log("Status", formData.getAll("Status"));

    for (var iii = 0; iii < file.length; iii++) {
      formData.append("files", file[iii]);
    }

    console.log('FORMDATA: ', formData);
    return this.http.post(this.url, formData);
  }


  addElectronicBookFileFromCalendar(value, file: FileList, electronicbookid, centralproid, signatureFiles: FileList) {
    console.log("Description: ", value.description);
    console.log("File Type: ", value.fileType);
    const formData = new FormData();
    formData.append('ElectronicBookId', electronicbookid);
    formData.append('Step', value.step);
    formData.append('Status', value.status);
    formData.append('QuestionPeople', value.questionPeople);
    formData.append('CentralPolicyProvinceId', centralproid);
    formData.append('Description', value.description);
    formData.append('Type', value.fileType);


    if (file != null) {
      for (var iii = 0; iii < file.length; iii++) {
        formData.append("files", file[iii]);
      }
    }

    if (signatureFiles != null) {
      for (var index = 0; index < signatureFiles.length; index++) {
        formData.append("signatureFiles", file[index]);
      }
    }


    console.log('FORMDATA: ', formData);
    return this.http.post(this.url + "calendarfile", formData);
  }

  deleteElectronicBook(id) {
    return this.http.delete(this.url + id)
  }

  getElectronicBookDetail(electID): Observable<any> {
    return this.http.get<any>(this.url + 'getElectronicBookById/' + electID);
  }

  editElectronicBookDetail(value, electID, file: FileList, ) {
    console.log("EDIT VALUE: ", value);
    // const formData = {
    //   Detail: value.eBookDetail,
    //   Status: value.Status
    // }

    const formData = new FormData();
    // formData.append('Detail', value.eBookDetail);
    formData.append('Status', value.Status);
    formData.append('Description', value.description);
    formData.append('Type', value.fileType);

    if (file != null) {
      for (var i = 0; i < file.length; i++) {
        formData.append("files", file[i]);
      }
    }

    return this.http.put(this.url + 'editElectronicBookDetail/' + electID, formData)
  }

  addSuggestion(value, electID, subjectCentralpolicyID) {
    console.log("EDIT VALUE: ", value);
    console.log("SubjectCentralPolicyID: ", subjectCentralpolicyID);

    // const formData = {
    //   Detail: value.eBookDetail,
    //   Status: value.Status
    // }

    const formData = new FormData();
    formData.append('ElectID', electID);
    formData.append('Detail', value.checkDetail);
    formData.append('Problem', value.Problem);
    formData.append('Suggestion', value.Suggestion);
    formData.append('SubjectCentralPolicyProvinceId', subjectCentralpolicyID);

    return this.http.post(this.url + 'addSuggestion', formData)
  }

  editSuggestion(value, electID, subjectCentralpolicyID) {
    console.log("EDIT VALUE: ", value);

    const formData = new FormData();
    formData.append('ElectID', electID);
    formData.append('Detail', value.checkDetail);
    formData.append('Problem', value.Problem);
    formData.append('Suggestion', value.Suggestion);
    formData.append('SubjectCentralPolicyProvinceId', subjectCentralpolicyID);

    return this.http.put(this.url + 'editSuggestion', formData)
  }

  editSuggestionOwn(value, electID, subjectCentralpolicyID) {
    console.log("EDIT VALUE Own: ", value);

    const formData = new FormData();
    formData.append('ElectID', electID);
    formData.append('Detail', value.checkDetail);
    formData.append('Problem', value.Problem);
    formData.append('Suggestion', value.Suggestion);
    formData.append('Status', value.Status);

    return this.http.put(this.url + 'editSuggestionown', formData)
  }

  getNotSelectedInspectionPlan(id) {
    console.log(id);
    return this.http.get(this.url + "getcentralpolicyfromprovince/" + id)
  }

  deleteFile(id) {
    return this.http.delete(this.url + 'deletefile/' + id);
  }

  getCalendarFile(electID) {
    console.log("SERVICE EID: ", electID);

    return this.http.get<any>(this.url + "getCalendarFile/" + electID)
  }

  getElectronicbookFile(electID) {
    console.log("SERVICE EID: ", electID);

    return this.http.get<any>(this.url + "getElectronicbookFile/" + electID)
  }

  getElectronicBookProvince(userId) {
    return this.http.get<any[]>(this.url + "province/" + userId)
  }

  getSuggestionDetailById(subjectCentralPolicyProvinceID, electID) {
    return this.http.get<any>(this.url + "suggestiondetail/" + subjectCentralPolicyProvinceID + "/" + electID)
  }

  getElectOwnCreate(electID) {
    return this.http.get<any>(this.url + "getElectronicBookOwn/" + electID)
  }

  getElectOwnDetail(centralPolicyProvinceId) {
    return this.http.get<any>(this.url + "getelectronicbookdetailown/" + centralPolicyProvinceId)
  }

  getSignatureFile(electID) {
    return this.http.get(this.url + "getSignatureFile/" + electID)
  }

  addSignatureFile(electID, file: FileList, provinceData) {
    // alert(provinceData.provinceSuggestion);
    const formData = new FormData();
    if (file != null) {
      for (var i = 0; i < file.length; i++) {
        formData.append("files", file[i]);
      }
    }
    formData.append("ElectID", electID);
    formData.append("Description", provinceData.provinceSuggestion)
    return this.http.post(this.url + "addSignature", formData)
  }

  getexportport(userId) {
    return this.http.get(this.url + "export/" + userId)
  }

  addReportTable(reportData, suggestionForm, centralPolicyId) {
    console.log("ProvinceId: ", reportData.centralPolicyProvinces[0].provinceId);
    console.log("ReportTitle: ", reportData.title);
    console.log("ReportYear: ", reportData.fiscalYear.year);
    console.log("ReportUserId: ", reportData.createdBy);
    console.log("ReportStatus: ", reportData.status);
    console.log("suggestionForm: ", suggestionForm);

    const formData = {
      ReportProvinceId: reportData.centralPolicyProvinces[0].provinceId,
      ReportTitle: reportData.title,
      ReportYear: reportData.fiscalYear.year,
      ReportUserId: reportData.createdBy,
      ReportStatus: reportData.status,
      ReportCentralPolicyId: centralPolicyId
    }
    return this.http.post(this.url + "addReport", formData)
  }

  acceptelectronicbook(bookgroupid, userid) {
    const formData = new FormData();
    formData.append('bookgroupid', bookgroupid);
    formData.append('userid', userid)
    console.log('FORMDATA: ' + formData.get("name"));
    return this.http.post(this.url + "accept", formData);
  }

  getReportExcelElectronicBook(id) {
    return this.http.get<any>(this.url + "exportexcel/" + id)
  }

  addProceed(value, electID, userid) {
    const formData = new FormData();
    formData.append('ElectID', electID);
    formData.append('proceed', value.proceed);
    formData.append('userid', userid);
    return this.http.post(this.url + 'addProceed', formData)
  }

  getProceed(eid) {
    return this.http.get<any>(this.url + 'proceed/' + eid);
  }
}


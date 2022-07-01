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
    return this.http.get<any[]>(this.url + userId)
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
    // formData.append('Step', value.step);
    formData.append('Status', value.status);
    formData.append('QuestionPeople', value.questionPeople);
    formData.append('CentralPolicyProvinceId', centralproid);
    formData.append('Description', value.description);
    formData.append('Type', value.fileType);


    function getFileExtension2(filename) {
      return filename.split('.').pop();
    }
    if (value.fileData != null) {
      for (var iii = 0; iii < value.fileData.length; iii++) {
        var filename: string = value.fileData[iii].ebookFile.name;
        console.log('.ebookFile.name', value.fileData[iii].ebookFile.name);
        console.log('getFileExtension2', getFileExtension2(filename));
        formData.append("files", value.fileData[iii].ebookFile, `${value.fileData[iii].fileDescription}.${getFileExtension2(filename)}`);
        // formData.append("fileDescription", value.fileData[iii].fileDescription);
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

  getInvitedPeople(inspectionPlanEvent): Observable<any> {
    console.log("inspectData: ", inspectionPlanEvent);
    const formData = new FormData();
    for (var i = 0; i < inspectionPlanEvent.length; i++) {
      formData.append("inspectionPlanEventId", inspectionPlanEvent[i].inspectionPlanEventId);
    }
    return this.http.post<any>(this.url + 'getInvitedPeople', formData);
  }

  editElectronicBookDetail(value, electID, file: FileList,) {
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

  addSuggestion(value, electID, centralPolicyEventId) {
    console.log("Suggestion Value: ", value);
    console.log("ElectId: ", electID);
    console.log("centralPolicyEventId: ", centralPolicyEventId);

    const formData = new FormData();
    formData.append('ElectID', electID);
    formData.append('Suggestion', value.suggestion);
    formData.append('CentralEventId', centralPolicyEventId);

    return this.http.post(this.url + 'addSuggestion', formData)
  }

  editSuggestion(value, electID) {
    console.log("EDIT VALUE: ", value);
    console.log("user: ", value.user.length);


    const formData = new FormData();
    formData.append('Detail', value.detail);
    formData.append('Problem', value.problem);
    formData.append('Suggestion', value.suggestion);
    formData.append('Status', value.Status);
    formData.append('ElectID', electID);

    for (var i = 0; i < value.user.length; i++) {
      formData.append("userId", value.user[i].userId);
    }

    function getFileExtension2(filename) {
      return filename.split('.').pop();
    }
    if (value.fileData != null) {
      for (var iii = 0; iii < value.fileData.length; iii++) {
        var filename: string = value.fileData[iii].ebookFile.name;
        console.log('.ebookFile.name', value.fileData[iii].ebookFile.name);
        console.log('getFileExtension2', getFileExtension2(filename));
        formData.append("files", value.fileData[iii].ebookFile, `${value.fileData[iii].fileDescription}.${getFileExtension2(filename)}`);
        // formData.append("fileDescription", value.fileData[iii].fileDescription);
      }
    }

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

  getCalendarFile(planId, cenproid) {
    console.log("SERVICE EID: ", planId);

    return this.http.get<any>(this.url + "getCalendarFile/" + planId + "/" + cenproid)
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

  getSubjectReport() {
    return this.http.get(this.url + "subjectImport");
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

  getCentralPolicyEbook(userId) {
    return this.http.get<any>(this.url + "centralPolicyEbook/" + userId)
  }

  getCentralPolicyEbook2(startDate, userId) {
    const formData = new FormData();
    formData.append('startDate', startDate);
    formData.append('User', userId);
    return this.http.post<any>(this.url + "centralPolicyEbook2", formData)
  }

  createElectronicBook(value, userId) {
    // alert(value.description);
    // alert( value.fileType)
    console.log("Add EBook: ", value);

    var inputdate: Array<any> = value.inputdate.map((item, index) => {
      return {
        StartDate: item.start_date.date.year + '-' + item.start_date.date.month + '-' + item.start_date.date.day,
        EndDate: item.end_date.date.year + '-' + item.end_date.date.month + '-' + item.end_date.date.day,
      }
    })

    const formData = new FormData();
    formData.append('Detail', value.checkDetail);
    formData.append('Problem', value.Problem);
    formData.append('Suggestion', value.Suggestion);
    formData.append('Status', value.Status);
    // formData.append('centralPolicyEventId', value.centralPolicyEventId);
    formData.append('Description', value.description);
    formData.append('Type', value.fileType);
    formData.append('id', userId);

    for (var i = 0; i < inputdate.length; i++) {
      console.log("ii: ", i);
      // console.log("inputdateii: ", inputdate[ii].StartDate);
      formData.append('StartDate', inputdate[i].StartDate);
      formData.append('EndDate', inputdate[i].EndDate);
    }

    for (var i = 0; i < value.centralPolicyEventId.length; i++) {
      formData.append('CentralPolicyEventId', value.centralPolicyEventId[i]);
    }

    console.log("detail", formData.getAll("Detail"));
    console.log("Problem", formData.getAll("Problem"));
    console.log("Suggestion", formData.getAll("Suggestion"));
    console.log("id", formData.getAll("id"));
    console.log("Status", formData.getAll("Status"));

    // if (value.fileData != null) {
    //   for (var iii = 0; iii < value.fileData.length; iii++) {
    //     formData.append("files", value.fileData[iii].ebookFile);
    //     formData.append("fileDescription", value.fileData[iii].fileDescription);
    //   }
    // }

    // var inputfile: any = [];

    // if (value.fileData != null) {
    //   for (var iii = 0; iii < value.fileData.length; iii++) {
    //     inputfile.push({ files2: value.fileData[iii].ebookFile, fileDescription2: value.fileData[iii].fileDescription });
    //   }
    // }

    // for(var key in inputfile) {
    //   formData.append('inputfile', inputfile[key])
    // }

    // formData.append('inputfile', JSON.stringify(inputfile));

    // console.log("fileNew", formData.getAll("files"));
    // console.log("inputfil222", formData.getAll("inputfile"));

    console.log('FORMDATA: ', formData);
    return this.http.post<any>(this.url + "CreateElectronicBook", formData);
  }

  addElectronicBookImage(formData, electId) {

    return this.http.post(this.url + "imageDescription/" + electId, formData);
  }

  createElectronicBookOwn(value, file: FileList, userId) {
    // alert(value.description);
    // alert( value.fileType)
    console.log("Add EBook Own: ", value);

    var inputdate: Array<any> = value.inputdate.map((item, index) => {
      return {
        StartDate: item.start_date.date.year + '-' + item.start_date.date.month + '-' + item.start_date.date.day,
        // EndDate: item.end_date.date.year + '-' + item.end_date.date.month + '-' + item.end_date.date.day,
      }
    })

    const formData = new FormData();
    formData.append('Detail', value.checkDetail);
    formData.append('Problem', value.Problem);
    formData.append('Suggestion', value.Suggestion);
    formData.append('Status', value.Status);
    formData.append('centralPolicyEventTitle', value.centralPolicy);
    formData.append('Description', value.description);
    formData.append('Type', value.fileType);
    formData.append('id', userId);
    formData.append('ProvincialDepartmentId', value.provincialDepartment);
    formData.append('userCreate', userId);

    console.log("own 1: ");

    for (var i = 0; i < value.provincialDepartment.length; i++) {
      formData.append('provincialDepartmentIdAr', value.provincialDepartment[i]);
    }

    console.log("own 2: ");

    for (var i = 0; i < value.ProvinceId.length; i++) {
      formData.append('electProvinceId', value.ProvinceId[i]);
    }

    console.log("own 3: ");

    for (var i = 0; i < inputdate.length; i++) {
      console.log("ii: ", i);
      // console.log("inputdateii: ", inputdate[ii].StartDate);
      formData.append('StartDate', inputdate[i].StartDate);
      // formData.append('EndDate', inputdate[i].EndDate);
    }

    console.log("own 4: ");

    // for (var i = 0; i < value.centralPolicyEventId.length; i++) {
    //   formData.append('CentralPolicyEventId', value.centralPolicyEventId[i]);
    // }

    console.log("detail", formData.getAll("Detail"));
    console.log("Problem", formData.getAll("Problem"));
    console.log("Suggestion", formData.getAll("Suggestion"));
    console.log("id", formData.getAll("id"));
    console.log("Status", formData.getAll("Status"));
    function getFileExtension2(filename) {
      return filename.split('.').pop();
    }
    if (value.fileData != null) {
      for (var iii = 0; iii < value.fileData.length; iii++) {
        var filename: string = value.fileData[iii].ebookFile.name;
        console.log('.ebookFile.name', value.fileData[iii].ebookFile.name);
        console.log('getFileExtension2', getFileExtension2(filename));
        formData.append("files", value.fileData[iii].ebookFile, `${value.fileData[iii].fileDescription}.${getFileExtension2(filename)}`);
        // formData.append("fileDescription", value.fileData[iii].fileDescription);
      }
    }

    if (value.fileData2 != null) {
      for (var iii = 0; iii < value.fileData2.length; iii++) {
        var filename: string = value.fileData2[iii].ebookFile.name;
        console.log('.ebookFile.name', value.fileData2[iii].ebookFile.name);
        console.log('getFileExtension2', getFileExtension2(filename));
        formData.append("files", value.fileData2[iii].ebookFile, `${value.fileData2[iii].fileDescription}.${getFileExtension2(filename)}`);
        // formData.append("fileDescription", value.fileData[iii].fileDescription);
      }
    }

    var sendOrNot: any;
    if (value.SendToProvince == true) {
      sendOrNot = "ส่งให้จังหวัด";
    } else {
      sendOrNot = "ไม่ส่งให้จังหวัด"
    }
    formData.append('sendToProvince', sendOrNot);

    console.log('FORMDATA: ', formData);
    return this.http.post<any>(this.url + "CreateElectronicBookOwn", formData);
  }

  addOpinion(value, ebookInviteId) {
    console.log("value: ", value);
    console.log("ebookInviteId: ", ebookInviteId);

    const formData = new FormData();
    formData.append('Description', value.opinion);
    formData.append('approve', value.accept);
    formData.append('inviteId', ebookInviteId);

    return this.http.put(this.url + "addEbookInvite", formData);
  }

  getElectronicBookInviteOpinion(electId, userId) {
    const formData = new FormData();
    formData.append('ElectID', electId);
    formData.append('userInvite', userId);

    return this.http.post<any>(this.url + "getElectronicBookInviteOpinion", formData);
  }

  sendToProvince(electId, userId, provinceId, provincialDepartmentId) {
    console.log("Provinces: ", provinceId);

    const formData = new FormData();
    formData.append('ElectID', electId);
    formData.append('userCreate', userId);

    for (var i = 0; i < provinceId.length; i++) {
      formData.append("electProvinceId", provinceId[i]);
    }
    console.log("PID: ", formData.getAll("electProvinceId"));

    for (var i = 0; i < provincialDepartmentId.length; i++) {
      formData.append("provincialDepartmentIdAr", provincialDepartmentId[i]);
    }
    console.log("PDID: ", formData.getAll("provincialDepartmentIdAr"));

    return this.http.post<any>(this.url + "sendElectronicBookToProvince", formData)
  }

  getSendedElectronicBookProvince(provinceId, provincialDepartmentId) {
    console.log("provinceID: ", provinceId);
    return this.http.get<any[]>(this.url + "electronicbookprovince/" + provinceId + "/" + provincialDepartmentId)

  }

  getSendedElectronicBookOtherDepartment(provincialDepartmentId) {
    console.log("provinceID: ", provincialDepartmentId);
    return this.http.get<any[]>(this.url + "electronicbookotherprovince/" + provincialDepartmentId)

  }

  addSubjectEventFile(value, file: FileList, electronicbookid, centralproid, signatureFiles: FileList) {

    // alert(JSON.stringify(value.notificationpeoplequestiondate))

    console.log("Description: ", value.description);
    console.log("File Type: ", value.fileType);
    const formData = new FormData();
    formData.append('ElectronicBookId', electronicbookid);
    // formData.append('Step', value.step);
    formData.append('Status', value.status);
    formData.append('QuestionPeople', value.questionPeople);
    formData.append('CentralPolicyProvinceId', centralproid);
    formData.append('Description', value.description);
    formData.append('Type', value.fileType);

    formData.append('Suggestion', value.suggestion);
    formData.append('StatusSuggestion', value.statussuggestion);

    formData.append('NotificationSubjectDate', value.notificationsubjectdate);
    formData.append('DeadlineSubjectDate', value.deadlinesubjectdate);

    formData.append('NotificationPeopleQuestiontDate', value.notificationpeoplequestiondate);
    formData.append('DeadlinePeopleQuestiontDate', value.deadlinepeoplequestiondate);

    // console.log("PDID: ", formData.getAll("provincialDepartmentIdAr"));

    function getFileExtension2(filename) {
      return filename.split('.').pop();
    }
    if (value.fileData != null) {
      for (var iii = 0; iii < value.fileData.length; iii++) {
        var filename: string = value.fileData[iii].ebookFile.name;
        console.log('.ebookFile.name', value.fileData[iii].ebookFile.name);
        console.log('getFileExtension2', getFileExtension2(filename));
        formData.append("files", value.fileData[iii].ebookFile, `${value.fileData[iii].fileDescription}.${getFileExtension2(filename)}`);
        // formData.append("fileDescription", value.fileData[iii].fileDescription);
      }
    }

    if (signatureFiles != null) {
      for (var index = 0; index < signatureFiles.length; index++) {
        formData.append("signatureFiles", file[index]);
      }
    }

    console.log('FORMDATA: ', formData);
    return this.http.post(this.url + "subjecteventfile", formData);
  }

  getSubjectEventFile(subjectgroupId, cenproid) {
    console.log("SERVICE EID: ", subjectgroupId);

    return this.http.get<any>(this.url + "getSubjectEventFile/" + subjectgroupId + "/" + cenproid)
  }

  provinceAddSignature(value, file: FileList, electId, userId, provinceId) {
    console.log("sign: ", value);
    console.log("file: ", file);
    console.log("provinceId: ", provinceId);


    const formData = new FormData();
    formData.append('Description', value.description);
    formData.append('ElectID', electId);
    formData.append('userCreate', userId);
    formData.append('ProvinceId', provinceId);

    if (file != null) {
      for (var iii = 0; iii < file.length; iii++) {
        formData.append("files", file[iii]);
      }
    }
    return this.http.post<any>(this.url + "addProvinceSignature", formData)
  }

  getProvincialDepartment() {
    return this.http.get<any>(this.url + "getProvincialDepartment")
  }

  sendToOtherProvince(value, userId, electAcceptId, userProvince) {
    console.log("value: ", value);
    console.log("UserId", userId);
    console.log("Accept", electAcceptId);

    const formData = new FormData();
    formData.append('electAcceptId', electAcceptId);
    formData.append('userCreate', userId);
    formData.append('provincialDepartmentId', value.provincialDepartment);
    formData.append('Description', value.description);
    formData.append('ProvinceId', userProvince);

    return this.http.post<any>(this.url + "sendElectronicBookToOtherProvince", formData)
  }

  agreeOtherDepartment(value, userId, ebookAcceptId) {
    console.log("value: ", value);
    console.log("electAcceptId: ", ebookAcceptId);
    console.log("USERID: ", userId);


    const formData = new FormData();
    formData.append('Description', value.description);
    formData.append('userCreate', userId);
    formData.append('electAcceptId', ebookAcceptId);

    return this.http.put(this.url + "agreeOtherDepartment", formData);
  }

  createElectronicBook2(value, userId) {
    // alert(value.description);
    // alert( value.fileType)
    console.log("Add EBook: ", value);
    console.log("File document: ", value.fileData);
    console.log("File image: ", value.fileData2);

    var inputdate: Array<any> = value.inputdate.map((item, index) => {
      return {
        StartDate: item.start_date.date.year + '-' + item.start_date.date.month + '-' + item.start_date.date.day,
        // EndDate: item.end_date.date.year + '-' + item.end_date.date.month + '-' + item.end_date.date.day,
      }
    })

    const formData = new FormData();
    formData.append('Detail', value.checkDetail);
    formData.append('Problem', value.Problem);
    formData.append('Suggestion', value.Suggestion);
    formData.append('Status', value.Status);
    // formData.append('centralPolicyEventId', value.centralPolicyEventId);
    formData.append('Description', value.description);
    formData.append('Type', value.fileType);
    formData.append('id', userId);

    for (var i = 0; i < inputdate.length; i++) {
      console.log("ii: ", i);
      // console.log("inputdateii: ", inputdate[ii].StartDate);
      formData.append('StartDate', inputdate[i].StartDate);
      // formData.append('EndDate', inputdate[i].EndDate);
    }

    for (var i = 0; i < value.centralPolicyEventId.length; i++) {
      formData.append('CentralPolicyEventId', value.centralPolicyEventId[i]);
    }

    console.log("detail", formData.getAll("Detail"));
    console.log("Problem", formData.getAll("Problem"));
    console.log("Suggestion", formData.getAll("Suggestion"));
    console.log("id", formData.getAll("id"));
    console.log("Status", formData.getAll("Status"));
    function getFileExtension2(filename) {
      return filename.split('.').pop();
    }
    if (value.fileData != null) {
      for (var iii = 0; iii < value.fileData.length; iii++) {
        var filename: string = value.fileData[iii].ebookFile.name;
        console.log('.ebookFile.name', value.fileData[iii].ebookFile.name);
        console.log('getFileExtension2', getFileExtension2(filename));
        formData.append("files", value.fileData[iii].ebookFile, `${value.fileData[iii].fileDescription}.${getFileExtension2(filename)}`);
        // formData.append("fileDescription", value.fileData[iii].fileDescription);
      }
    }

    if (value.fileData2 != null) {
      for (var iii = 0; iii < value.fileData2.length; iii++) {
        var filename: string = value.fileData2[iii].ebookFile.name;
        console.log('.ebookFile.name', value.fileData2[iii].ebookFile.name);
        console.log('getFileExtension2', getFileExtension2(filename));
        formData.append("files", value.fileData2[iii].ebookFile, `${value.fileData2[iii].fileDescription}.${getFileExtension2(filename)}`);
        // formData.append("fileDescription", value.fileData[iii].fileDescription);
      }
    }

    var sendOrNot: any;
    if (value.SendToProvince == true) {
      sendOrNot = "ส่งให้จังหวัด";
    } else {
      sendOrNot = "ไม่ส่งให้จังหวัด"
    }
    formData.append('sendToProvince', sendOrNot);

    // var inputfile: any = [];

    // if (value.fileData != null) {
    //   for (var iii = 0; iii < value.fileData.length; iii++) {
    //     inputfile.push({ files2: value.fileData[iii].ebookFile, fileDescription2: value.fileData[iii].fileDescription });
    //   }
    // }

    // for(var key in inputfile) {
    //   formData.append('inputfile', inputfile[key])
    // }

    // formData.append('inputfile', JSON.stringify(inputfile));

    // console.log("fileNew", formData.getAll("files"));
    // console.log("inputfil222", formData.getAll("inputfile"));

    console.log('FORMDATA: ', formData);
    return this.http.post<any>(this.url + "CreateElectronicBook2", formData);
  }

  getSendedElectronicBookDepartment(provincialDepartmentId) {
    console.log("provincialDepartmentId: ", provincialDepartmentId);
    return this.http.get<any>(this.url + "electronicbookdepartment/" + provincialDepartmentId)
  }

  departmentAddSignature(value, file: FileList, electId, userId, electProvincialId) {
    console.log("sign: ", value);
    console.log("file: ", file);
    console.log("electProvincialId =>>> ", electProvincialId);

    const formData = new FormData();
    formData.append('Description', value.description);
    formData.append('ElectID', electId);
    formData.append('userCreate', userId);
    formData.append('electProvincialId', electProvincialId);


    if (file != null) {
      for (var iii = 0; iii < file.length; iii++) {
        formData.append("files", file[iii]);
      }
    }
    return this.http.post<any>(this.url + "addDepartmentSignature", formData)
  }

  getElectronicBookDepartmentById(electId, provincialId) {
    console.log("ProvincialDepartmentID: ", provincialId);

    const formData = new FormData();
    formData.append('ElectID', electId);
    formData.append('provincialDepartmentId', provincialId);

    return this.http.post<any>(this.url + "GetElectronicBookDepartmentById", formData);
  }

  inviteMorePeople(value, electID) {
    console.log("inviteMore: ", value);
    console.log("ElectID: ", electID);
    const formData = new FormData();
    formData.append('ElectID', electID);

    if (value.UserMinistryId != null) {
      for (var i = 0; i < value.UserMinistryId.length; i++) {
        formData.append("userId", value.UserMinistryId[i]);
      }
    }

    if (value.UserDepartmentId != null) {
      for (var i = 0; i < value.UserDepartmentId.length; i++) {
        formData.append("userId", value.UserDepartmentId[i]);
      }
    }

    return this.http.post(this.url + 'inviteMorePeople', formData)
  }

  deleteMoreInvitedPeople(id) {
    return this.http.delete(this.url + 'deleteMoreInvited/' + id)
  }

  getElectronicBookProvincialDepartmentById(electID) {
    return this.http.get(this.url + "getElectronicBookDepartmentById/" + electID)
  }

  getElectronicBookOtherById(electID) {
    return this.http.get(this.url + "getElectronicBookOtherById/" + electID)
  }

  getElectronicBookAll() {
    return this.http.get(this.url + "all")
  }

  sortDate(userId) {
    return this.http.get(this.url + "sortDate/" + userId)
  }

  sortDateDESC(userId) {
    return this.http.get(this.url + "sortDateDESC/" + userId)
  }
}


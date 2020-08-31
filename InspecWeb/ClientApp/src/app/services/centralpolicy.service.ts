import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CentralpolicyService {

  url = "";
  files: FileList

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.url = baseUrl + 'api/centralpolicy/';
  }

  getcentralpolicydata(): Observable<any[]> {
    return this.http.get<any[]>(this.url)
  }
  getcentralpolicyfiscalyeardata(id): Observable<any[]> {
    return this.http.get<any[]>(this.url + "fiscalfear/" + id)
  }
  getcentralpolicysubjectcount(id): Observable<any[]> {
    return this.http.get<any[]>(this.url + "subjectcount/" + id)
  }
  getdetailcentralpolicydata(id): Observable<any> {
    console.log("CentralID: ", id);

    return this.http.get<any>(this.url + id)
  }
  detailcentralpolicydata(id) {
    console.log(id);
    return this.http.get(this.url + id)
  }
  addCentralpolicy(centralpolicyData, file: FileList, userid) {
    // alert(JSON.stringify(file))
    var inputdate: Array<any> = centralpolicyData.inputdate.map((item, index) => {
      return {
        StartDate: item.start_date.date.year + '-' + item.start_date.date.month + '-' + item.start_date.date.day,
        EndDate: item.end_date.date.year + '-' + item.end_date.date.month + '-' + item.end_date.date.day,
      }
    })
    console.log("DDD", centralpolicyData);

    // for (var i = 0; i < file.length; i++) {
    //   // formData.append("files", file[i]);
    // }
    // const formData = {
    //   Title: centralpolicyData.title,
    //   // StartDate: centralpolicyData.start_date.date.year + '-' + centralpolicyData.start_date.date.month + '-' + centralpolicyData.start_date.date.day,
    //   // EndDate: centralpolicyData.end_date.date.year + '-' + centralpolicyData.end_date.date.month + '-' + centralpolicyData.end_date.date.day,
    //   Type: centralpolicyData.type,
    //   ProvinceId : centralpolicyData.ProvinceId,
    //   FiscalYearId: centralpolicyData.year,
    //   Status: centralpolicyData.status,
    //   files: file,
    //   inputdate: inputdate,
    // }

    const formData = new FormData();
    formData.append('UserID', userid);
    formData.append('Title', centralpolicyData.title);
    formData.append('Type', centralpolicyData.type);
    for (var i = 0; i < centralpolicyData.ProvinceId.length; i++) {
      formData.append('ProvinceId', centralpolicyData.ProvinceId[i]);
    }
    formData.append('FiscalYearNewId', centralpolicyData.year);
    formData.append('Status', centralpolicyData.status);
    // formData.append('files',centralpolicyData.file);
    for (var ii = 0; ii < inputdate.length; ii++) {
      console.log("ii: ", ii);
      // console.log("inputdateii: ", inputdate[ii].StartDate);
      formData.append('StartDate2', inputdate[ii].StartDate);
      formData.append('EndDate2', inputdate[ii].EndDate);
    }
    // if (file != null) {
    //   for (var iii = 0; iii < file.length; iii++) {
    //     formData.append("files", file[iii]);
    //   }
    // }
    function getFileExtension2(filename) {
      return filename.split('.').pop();
    }
    if (centralpolicyData.fileData != null) {
      for (var iii = 0; iii < centralpolicyData.fileData.length; iii++) {
        var filename: string = centralpolicyData.fileData[iii].CentralpolicyFile.name
        formData.append("files", centralpolicyData.fileData[iii].CentralpolicyFile, `${centralpolicyData.fileData[iii].fileDescription}.${getFileExtension2(filename)}`);
        // formData.append("fileDescription", value.fileData[iii].fileDescription);
      }
    }
    formData.append('Class', centralpolicyData.Class);
    console.log('FORMDATA: ', formData.get("inputdate"));
    console.log('FORMDATA: ', formData.get("ProvinceId"));
    return this.http.post(this.url, formData)
  }

  addCentralpolicyEbook(centralpolicyData, file: FileList, userid) {
    // alert(JSON.stringify(file))
    console.log("FNAJA: ", centralpolicyData);

    var inputdate: Array<any> = centralpolicyData.inputdate.map((item, index) => {
      return {
        StartDate: item.start_date.date.year + '-' + item.start_date.date.month + '-' + item.start_date.date.day,
        EndDate: item.end_date.date.year + '-' + item.end_date.date.month + '-' + item.end_date.date.day,
      }
    })
    console.log("DDD", inputdate);

    const formData = new FormData();
    formData.append('UserID', userid);
    formData.append('Title', centralpolicyData.title);
    formData.append('Type', centralpolicyData.type);
    // for (var i = 0; i < centralpolicyData.ProvinceId.length; i++) {
    //   formData.append('ProvinceId', centralpolicyData.ProvinceId[i]);
    // }
    formData.append('ProvinceId', centralpolicyData.ProvinceId);
    formData.append('FiscalYearId', centralpolicyData.year);
    formData.append('Status', centralpolicyData.status);
    // formData.append('files',centralpolicyData.file);
    for (var ii = 0; ii < inputdate.length; ii++) {
      console.log("ii: ", ii);
      // console.log("inputdateii: ", inputdate[ii].StartDate);
      formData.append('StartDate2', inputdate[ii].StartDate);
      formData.append('EndDate2', inputdate[ii].EndDate);
    }

    if (file != null) {
      for (var iii = 0; iii < file.length; iii++) {
        formData.append("files", file[iii]);
      }
    }

    for (var iiii = 0; iiii < centralpolicyData.SubjectId.length; iiii++) {
      formData.append("SubjectId", centralpolicyData.SubjectId[iiii]);
    }
    formData.append('Class', centralpolicyData.Class);
    console.log('Class: ', formData.get("Class"));
    console.log('FORMDATA: ', formData.get("ProvinceId"));
    return this.http.post<any>(this.url, formData)
  }

  editCentralpolicy(centralpolicyData, file: FileList, id, userId, removeProvine, addProvince) {
    console.log("test: ", centralpolicyData);
    console.log("id", id);
    console.log("ff: ", file);
    console.log("userid: ", userId);
    console.log("removeProvince: ", removeProvine);
    console.log("addProvince: ", addProvince);

    var inputdate: Array<any> = centralpolicyData.inputdate.map((item, index) => {
      return {
        StartDate: item.start_date.year + '-' + item.start_date.month + '-' + item.start_date.day,
        EndDate: item.end_date.year + '-' + item.end_date.month + '-' + item.end_date.day,
      }
    })
    console.log("DDD", inputdate);

    const formData = new FormData();
    formData.append('Title', centralpolicyData.title);
    formData.append('Type', centralpolicyData.type);

    for (var i = 0; i < centralpolicyData.ProvinceId.length; i++) {
      formData.append('ProvinceId', centralpolicyData.ProvinceId[i]);
    }

    for (var i = 0; i < removeProvine.length; i++) {
      formData.append('RemoveProvince', removeProvine[i]);
    }

    for (var i = 0; i < addProvince.length; i++) {
      formData.append('AddProvince', addProvince[i]);
    }

    formData.append('FiscalYearNewId', centralpolicyData.year);
    formData.append('Status', centralpolicyData.status);
    formData.append('UserID', userId);
    for (var ii = 0; ii < inputdate.length; ii++) {
      console.log("ii: ", ii);
      // console.log("inputdateii: ", inputdate[ii].StartDate);
      formData.append('StartDate2', inputdate[ii].StartDate);
      formData.append('EndDate2', inputdate[ii].EndDate);
    }

    // if (file != null) {
    //   console.log('infile');

    //   for (var iii = 0; iii < file.length; iii++) {
    //     formData.append("files", file[iii]);
    //   }
    // }
    function getFileExtension2(filename) {
      return filename.split('.').pop();
    }
    if (centralpolicyData.fileData != null) {
      for (var iii = 0; iii < centralpolicyData.fileData.length; iii++) {
        var filename: string = centralpolicyData.fileData[iii].CentralpolicyFile.name
        formData.append("files", centralpolicyData.fileData[iii].CentralpolicyFile, `${centralpolicyData.fileData[iii].fileDescription}.${getFileExtension2(filename)}`);
        // formData.append("fileDescription", value.fileData[iii].fileDescription);
      }
    }

    console.log("formTitle: ", formData.getAll('Title'));
    console.log("formType: ", formData.getAll('Type'));
    console.log("formProvinceId: ", formData.getAll('ProvinceId'));
    console.log("formFiscalYearId: ", formData.getAll('FiscalYearId'));
    console.log("formStatus: ", formData.getAll('Status'));
    console.log("formStartDate2: ", formData.getAll('StartDate2'));
    console.log("formEndDate2: ", formData.getAll('EndDate2'));
    console.log("formfiles: ", formData.getAll('files'));
    console.log("removeProvince: ", formData.getAll('RemoveProvince'));
    console.log("addProvince: ", formData.getAll('AddProvince'));

    let path = this.url + id;
    console.log("path: ", path);


    return this.http.put(path, formData)
  }

  deleteFile(id) {
    return this.http.delete(this.url + 'deletefile/' + id);
  }

  deleteCentralPolicy(id) {
    return this.http.delete(this.url + id);
  }

  addCentralpolicyUser(data, id, userid, planId) {
    console.log("datAAAAA: ", data);

    const formData = {
      CentralPolicyId: id,
      UserId: data.UserPeopleId,
      // ElectronicBookId: electronicbookid,
      UserDepartmentId: data.UserDepartmentId,
      UserMinistryId: data.UserMinistryId,
      UserProvincialDepartmentId: data.UserProvincialDepartmentId,
      InviteBy: userid,
      planId: planId
    }
    console.log('FORMDATAddddddd: ' + formData);
    return this.http.post(this.url + "users", formData);
  }

  getcentralpolicyuserdata(id): Observable<any[]> {
    return this.http.get<any[]>(this.url + "users/" + id)
  }

  getcentralpolicyprovinceuserdata(id, planId): Observable<any[]> {
    return this.http.get<any[]>(this.url + "usersprovince/" + id + "/" + planId)
  }

  getcentralpolicyfromprovince(id): Observable<any[]> {
    return this.http.get<any[]>(this.url + "getcentralpolicyfromprovince/" + id)
  }

  getcentralpolicyuserinviteddata(id, planid): Observable<any[]> {
    return this.http.get<any[]>(this.url + "usersinvited/" + id + "/" + planid)
  }

  getdetailacceptcentralpolicydata(id): Observable<any> {
    return this.http.get<any>(this.url + "detailaccept/" + id)
  }
  getdetailuseracceptcentralpolicydata(id): Observable<any> {
    return this.http.get<any>(this.url + "detailaccept/users/" + id)
  }
  acceptCentralpolicy(answer, id, userid) {
    const formData = new FormData();

    formData.append('status', answer);
    formData.append('id', id);
    formData.append('userid', userid);

    return this.http.put(this.url + "acceptcentralpolicy/" + id, formData);
  }

  acceptCentralpolicy2(answer, id, userid, data) {
    const formData = new FormData();

    formData.append('status', answer);
    formData.append('id', id);
    formData.append('userid', userid);
    formData.append('report', data.answer);

    return this.http.put(this.url + "acceptcentralpolicy2/" + id, formData);
  }

  getdetailcentralpolicyprovincedata(id): Observable<any> {
    // alert('hi')
    return this.http.get<any>(this.url + "centralpolicyprovince/" + id)
  }

  getSubjectCentralPolicyProvince(id): Observable<any> {
    return this.http.get<any>(this.url + "subjectcentralpolicyprovince/" + id)
  }

  getUserFiles(userId) {
    return this.http.get<any>(this.url + "userfile/" + userId)
  }

  sendReport(reportData, file: FileList, id) {
    console.log("REPORTDATA: ", reportData);

    if (file != null) {
      const formData = new FormData();
      formData.append('Report', reportData.report);
      formData.append('DraftStatus', reportData.Status);
      formData.append("files", null);
      formData.append('Description', reportData.description);
      formData.append('fileType', reportData.fileType);
      for (var i = 0; i < file.length; i++) {
        formData.append("files", file[i]);
      }
      return this.http.put(this.url + "reportcentralpolicy/" + id, formData)
    } else {
      const formData = new FormData();
      formData.append('Report', reportData.report);
      formData.append('DraftStatus', reportData.Status);
      formData.append("files", null);
      formData.append('Description', reportData.description);
      formData.append('fileType', reportData.fileType);
      return this.http.put(this.url + "reportcentralpolicy/" + id, formData)
    }


  }

  deleteUserFile(id) {
    return this.http.delete(this.url + 'deleteuserfile/' + id);
  }

  sendAssign(value, id, userid) {
    console.log("value", value);

    const formData = new FormData();
    formData.append('assign', value.assign);

    formData.append('department', value.department);
    formData.append('position', value.position);
    formData.append('phone', value.phone);
    formData.append('email', value.email);
    formData.append('userid', userid);

    return this.http.put(this.url + 'sendassign/' + id, formData)
  }

  sendAssignInternal(value, id, userid) {
    // alert(JSON.stringify(value))
    console.log("value", value);

    const formData = new FormData();

    formData.append('assignuserid', value.UserId);

    // formData.append('department', value.department);
    // formData.append('position', value.position);
    // formData.append('phone', value.phone);
    // formData.append('email', value.email);
    formData.append('userid', userid);

    return this.http.put(this.url + 'sendassigninternal/' + id, formData)
  }


  getAssign(id) {
    return this.http.get<any>(this.url + 'getassign/' + id);
  }

  addDepartment(data, subjectid) {
    // alert(data.DepartmentId)
    const formData = {
      DepartmentId: data.DepartmentId,
      SubjectCentralPolicyProvinceId: subjectid,
    }
    // const formData = new FormData();
    // formData.append('SubjectCentralPolicyProvinceId', subjectid);
    // formData.append('DepartmentId', data.DepartmentId);

    console.log('FORMDATA: ' + formData);
    return this.http.post(this.url + "adddepartment", formData);
  }

  addPeopleAnswer(data, subjectid) {
    const formData = {
      UserId: data.peopleanswer,
      SubjectCentralPolicyProvinceId: subjectid,
    }
    console.log('FORMDATA: ' + formData);
    return this.http.post(this.url + "addpeopleanswer", formData);
  }

  getcentralidandprovinceid(centralpolicyprovinceid) {
    return this.http.get<any>(this.url + 'getcentralidandprovinceid/' + centralpolicyprovinceid);
  }

  addeditDepartment(data, subjectid) {
    const formData = {
      DepartmentId: data.DepartmentId,
      SubjectCentralPolicyProvinceId: subjectid,
      Box: data.Box
    }
    console.log('FORMDATA: ' + formData);
    return this.http.post(this.url + "addeditdepartment", formData);
  }
  getComment(centralpolicyprovinceid) {
    return this.http.get<any>(this.url + 'comment/' + centralpolicyprovinceid);
  }

  getAnswer(centralPolicyProvinceId) {
    return this.http.get<any>(this.url + "getAnswerPeople/" + centralPolicyProvinceId);
  }
  getSubjecteventdetaildata(id, subjectgroupid): Observable<any> {
    // alert('hi')
    return this.http.get<any>(this.url + "subjectevent/" + id + "/" + subjectgroupid)
  }
  getquestionpeople(cenproid, planid) {
    return this.http.get<any>(this.url + 'getquestionpeople/' + cenproid + "/" + planid);
  }
  addPeoplequestion(cenproid, planid, data) {

    const formData = new FormData();
    formData.append('cenproid', cenproid);
    formData.append('planid', planid);
    formData.append('question', data.question);

    // formData.append('notificationdate', data.notificationdate);
    // formData.append('deadlinedate', data.deadlinedate);
    // formData.append('notificationdate', data.notificationdate.date.year + '-' + data.notificationdate.date.month + '-' + data.notificationdate.date.day);
    // formData.append('deadlinedate', data.deadlinedate.date.year + '-' + data.deadlinedate.date.month + '-' + data.deadlinedate.date.day);

    console.log("form", formData.getAll("cenproid"));
    console.log("form", formData.getAll("planid"));
    console.log("form", formData.getAll("question"));
    console.log("form", formData.getAll("notificationdate"));
    console.log("form", formData.getAll("deadlinedate"));

    return this.http.post(this.url + "addPeoplequestion", formData);
  }
  deleteDepartment(id) {
    return this.http.delete(this.url + "deletedepartment/" + id);
  }

  deletecentralpolicyuser(id) {
    return this.http.delete(this.url + "deletecentralpolicyuser/" + id);
  }

  getcentralpolicyministrydata(id): Observable<any[]> {
    return this.http.get<any[]>(this.url + "ministry/" + id);
  }
  getcentralpolicydepartmentdata(id): Observable<any[]> {
    return this.http.get<any[]>(this.url + "department/" + id);
  }
  getcentralpolicypeopledata(id): Observable<any[]> {
    return this.http.get<any[]>(this.url + "people/" + id);
  }

  getcentralpolicyprovincialdepartmentdata(id): Observable<any[]> {
    return this.http.get<any[]>(this.url + "provincialdepartment/" + id);
  }

}

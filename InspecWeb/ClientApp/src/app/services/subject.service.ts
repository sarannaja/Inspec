import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class SubjectService {

  url = "";

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.url = baseUrl + 'api/subject/';
  }
  getsubjectdata(id) {
    return this.http.get<any[]>(this.url + id)
  }
  getsubjectdetaildata(id) {
    return this.http.get(this.url + "subjectdetail/" + id)
  }
  addSubject(subjectData, centralpolicyid, userid) {
    var subjectdepartment = subjectData.inputsubjectdepartment
    console.log('subjectData: ', subjectdepartment);
    var departmentId = []
    var test = []
    var testsubjectdepartment = []
    testsubjectdepartment = subjectdepartment.map((item, index) => {
      return {
        box: index,
        departmentId: item.departmentId,
        explanation: item.explanation,
        // inputquestionopen: item.inputquestionopen,
        inputquestionclose: item.inputquestionclose
      }
    })
    console.log("testsubjectdepartment", testsubjectdepartment);
    for (var i = 0; i < testsubjectdepartment.length; i++) {
      for (var j = 0; j < testsubjectdepartment[i].departmentId.length; j++) {
        departmentId.push({
          box: testsubjectdepartment[i].box,
          departmentId: testsubjectdepartment[i].departmentId[j],
          explanation: testsubjectdepartment[i].explanation,
          inputsubjectdepartment: testsubjectdepartment[i]
        })
      }
    }

    console.log("departmentId", departmentId);


    test = departmentId.map((item, index) => {
      return {
        box: item.box,
        departmentId: item.departmentId,
        explanation: item.explanation,
        // inputquestionopen: item.inputsubjectdepartment.inputquestionopen,
        inputquestionclose: item.inputsubjectdepartment.inputquestionclose
      }
    })
    console.log("test", test);

    // const formData = new FormData();
    // formData.append('Name', subjectData.name);
    // formData.append('Answer', subjectData.name);
    // formData.append('CentralPolicyId', parseInt(centralpolicyid));
    // formData.append('CentralPolicyDateId', subjectData.centralpolicydateid);
    // for (var i = 0; i < subjectdepartment.length; i++) {
    //   for (var j = 0; j < subjectdepartment[i].departmentId.length; j++) {
    //     departmentId.push({ departmentId: subjectdepartment[i].departmentId[j], inputsubjectdepartment: subjectdepartment[i] })
    //   }
    // formData.append('inputsubjectdepartment', departmentId);

    const formData = {
      Name: subjectData.name,
      Answer: subjectData.name,
      Status: subjectData.status,
      Explanation: subjectData.explanation,
      CentralPolicyId: parseInt(centralpolicyid),
      CentralPolicyDateId: subjectData.centralpolicydateid,
      UserID: userid,
      inputsubjectdepartment: test,
      // test: departmentId
      // inputquestionopen: subjectdepartment.inputquestionopen,
      // inputquestionclose: subjectdepartment.inputquestionclose,
      // inputanswerclose: subjectdepartment.inputanswerclose,
      // subjectdepartment: subjectdepartment
    }
    //     formData.append('Name', subjectData.name);
    //     formData.append('Answer', subjectData.name);
    //     formData.append('CentralPolicyId', centralpolicyid);
    //     formData.append('CentralPolicyDateId', subjectData.centralpolicydateid);
    // ``
    console.log('FORMDATA: ', formData);
    return this.http.post<any>(this.url, formData);
  }
  addFiles(subjectid, SubjectfileData) {
    console.log("SubjectfileData", SubjectfileData);

    const formData = new FormData();
    for (var i = 0; i < subjectid.length; i++) {
      formData.append('SubjectCentralPolicyProvinceId', subjectid[i]);
    }
    // if (file != null) {
    //   for (var ii = 0; ii < file.length; ii++) {
    //     formData.append("files", file[ii]);
    //   }
    // }
    // if (SubjectfileData.fileData != null) {
    //   for (var iii = 0; iii < SubjectfileData.fileData.length; iii++) {
    //     formData.append("files", SubjectfileData.fileData[iii].SubjectFile,SubjectfileData.fileData[iii].fileDescription);
    //     // formData.append("fileDescription", value.fileData[iii].fileDescription);
    //   }
    // }
    function getFileExtension2(filename) {
      return filename.split('.').pop();
    }
    if (SubjectfileData.fileData != null) {
      for (var iii = 0; iii < SubjectfileData.fileData.length; iii++) {
        var filename: string = SubjectfileData.fileData[iii].SubjectFile.name
        formData.append("files", SubjectfileData.fileData[iii].SubjectFile, `${SubjectfileData.fileData[iii].fileDescription}.${getFileExtension2(filename)}`);
        // formData.append("fileDescription", value.fileData[iii].fileDescription);
      }
    }
    // if (SubjectfileData.fileData != null) {
    //   for (var iii = 0; iii < SubjectfileData.fileData.length; iii++) {
    //     var filename: string = SubjectfileData.fileData[iii].SubjectFile.name
    //     if (SubjectfileData.fileData[iii].fileDescription != null || SubjectfileData.fileData[iii].fileDescription != "") {

    //       formData.append("files", SubjectfileData.fileData[iii].SubjectFile, `${SubjectfileData.fileData[iii].fileDescription}.${getFileExtension2(filename)}`);
    //     } else {
    //       formData.append("files", SubjectfileData.fileData[iii].SubjectFile, `ไม่มีคำอธิบาย.${getFileExtension2(filename)}`);

    //     }
    //     // formData.append("fileDescription", value.fileData[iii].fileDescription);
    //   }
    // }
    return this.http.post(this.url + "addfiles", formData);
  }
  AddDepartmentQuestion(DepartmentQuestiondata, Box, subjectid) {
    console.log("DepartmentQuestiondata", DepartmentQuestiondata);
    var testsubjectdepartment = []
    var test = []
    var departmentId = []
    testsubjectdepartment = DepartmentQuestiondata.inputsubjectdepartment.map((item, index) => {
      return {
        box: index,
        departmentId: item.departmentId,
        inputquestionopen: item.inputquestionopen,
        inputquestionclose: item.inputquestionclose
      }
    })
    console.log("testsubjectdepartment", testsubjectdepartment);
    for (var i = 0; i < testsubjectdepartment.length; i++) {
      for (var j = 0; j < testsubjectdepartment[i].departmentId.length; j++) {
        departmentId.push({ box: testsubjectdepartment[i].box, departmentId: testsubjectdepartment[i].departmentId[j], inputsubjectdepartment: testsubjectdepartment[i] })
      }
    }
    test = departmentId.map((item, index) => {
      return {
        subjectid: subjectid,
        box: Box,
        departmentId: item.departmentId,
        inputquestionopen: item.inputsubjectdepartment.inputquestionopen,
        inputquestionclose: item.inputsubjectdepartment.inputquestionclose
      }
    })
    // test = DepartmentQuestiondata.inputsubjectdepartment.map((item, index) => {
    //   return {
    //     subjectid: subjectid,
    //     box: Box,
    //     departmentId: item.departmentId,
    //     inputquestionopen: item.inputquestionopen,
    //     inputquestionclose: item.inputquestionclose
    //   }
    // })

    console.log("test", test);
    const formData = {
      inputsubjectdepartment: test,
    }
    console.log('FORMDATA: ', formData);
    return this.http.post(this.url + "adddepartmentquestion", formData);
  }
  addSubquestionopen(Subquestionopendata) {
    const formData = new FormData();
    formData.append('subjectId', Subquestionopendata.subjectId);
    formData.append('name', Subquestionopendata.name)
    console.log('FORMDATA: ' + formData.get("name"));
    return this.http.post(this.url + "addsubquestionopen", formData);
  }
  addSubquestionclose(Subquestionclosedata) {
    const formData = {
      SubjectId: Subquestionclosedata.subjectId,
      Name: Subquestionclosedata.name,
      inputanswerclose2: Subquestionclosedata.inputanswerclose
    }
    return this.http.post(this.url + "addsubquestionclose", formData);
  }
  addChoices(Choicesdata) {
    const formData = new FormData();
    formData.append('subquestionid', Choicesdata.subquestionid);
    formData.append('name', Choicesdata.name)
    console.log('FORMDATA: ' + formData.get("name"));
    return this.http.post(this.url + "addchoices", formData);
  }
  deleteSubquestionopen(id) {
    return this.http.delete(this.url + "deletesubquestionopen/" + id);
  }
  deleteChoices(id) {
    return this.http.delete(this.url + "deletechoices/" + id);
  }
  deleteSubject(id) {
    return this.http.delete(this.url + id);
  }
  deletePeopleanswer(id) {
    return this.http.delete(this.url + "deletepeopleanswer/" + id);
  }
  deleteFile(id) {
    return this.http.delete(this.url + "deletefile/" + id);
  }
  storesubjectprovince(centralpolicyid, provincevalue) {
    // alert(JSON.stringify(provincevalue))
    const formData = new FormData();
    formData.append('centralpolicyid', centralpolicyid);
    formData.append('provincevalue', provincevalue)
    return this.http.post<any>(this.url + "subjectprovince/", formData)
  }
  editSubject(Subjectdata, id) {
    console.log("TEST: ", Subjectdata);

    const formData = new FormData();
    formData.append('Name', Subjectdata.name);

    for (var i = 0; i < Subjectdata.centralPolicyDateId.length; i++) {
      formData.append('CentralPolicyDateId', Subjectdata.centralPolicyDateId[i]);
    }

    // formData.append('CentralPolicyDateId', Subjectdata.centralPolicyDateId);
    console.log('FORMDATA: ' + JSON.stringify(formData));
    console.log("CID: ", formData.getAll('CentralPolicyDateId'));
    console.log("Name: ", formData.getAll('Name'));

    return this.http.put(this.url + "editsubject/" + id, formData);
  }
  editSubquestionopen(SubquestionopenData, id) {
    const formData = new FormData();
    formData.append(name, SubquestionopenData.name);
    console.log('FORMDATA: ' + JSON.stringify(formData));
    return this.http.put(this.url + "editsubquestionopen/" + id, formData);
  }
  editChoices(ChoicesData, id) {
    const formData = new FormData();
    formData.append(name, ChoicesData.name);
    console.log('FORMDATA: ' + JSON.stringify(formData));
    return this.http.put(this.url + "editchoices/" + id, formData);
  }

  editsubquestionprovince(data, id) {
    // alert(JSON.stringify(data))
    const formData = new FormData();
    formData.append('name', data.subquestionclose);

    return this.http.put(this.url + "editsunquestionprovince/" + id, formData);
  }

  editsubquestionchoiceprovince(data, id) {
    // alert(JSON.stringify(data))
    const formData = new FormData();
    formData.append('name', data.subquestionclosechoice);

    return this.http.put(this.url + "editsunquestionchoiceprovince/" + id, formData);
  }

  editsubjectchoiceprovince(data, id) {
    console.log("subject: ", data);

    const formData = new FormData();
    formData.append('name', data.subject);

    return this.http.put(this.url + "editsubjectchoiceprovince/" + id, formData);
  }

  editsubjectquestionopenchoiceprovince(data, id) {
    console.log("subjectquestionopen: ", data);

    const formData = new FormData();
    formData.append('name', data.subjectquestionopen);

    return this.http.put(this.url + "editsubjectquestionopenchoiceprovince/" + id, formData);
  }

  editAnswer(data, id) {
    console.log("answerData: ", data);
    console.log("answerID: ", id);

    const formData = new FormData();
    formData.append('answer', data.answer);
    // formData.append('id', id);

    return this.http.put(this.url + "editAnswer/" + id, formData);
  }


  deleteProvincial(id) {
    return this.http.delete(this.url + "deleteprovincial/" + id);
  }

  deletesubjectrole3(id) {
    return this.http.delete(this.url + "deletesubjectrole3/" + id);
  }

  deletequestionrole3(id) {
    return this.http.delete(this.url + "deletequestionrole3/" + id);
  }

  deleteoptionrole3(id) {
    console.log()

    return this.http.delete(this.url + "test" + null);
  }
  adddeleteDate(datedata, centralPolicyDateId, subjectid) {
    console.log(centralPolicyDateId);

    // var centralPolicyDateIddata = []
    // for (var i = 0; i < centralPolicyDateId.length; i++) {
    //   centralPolicyDateIddata.push(centralPolicyDateId[i])
    // }
    // console.log("test", centralPolicyDateIddata);

    const formData = new FormData();
    for (var i = 0; i < datedata.length; i++) {
      formData.append('id', datedata[i].value);
    }
    for (var ii = 0; ii < centralPolicyDateId.centralPolicyDateId.length; ii++) {
      formData.append('CentralPolicyDateId', centralPolicyDateId.centralPolicyDateId[ii]);
      // console.log(centralPolicyDateId[i]);

    }
    formData.append('subjectid', subjectid);
    // console.log(formData);
    return this.http.post(this.url + "deletedate", formData);
  }

  addSubjectRole3(subjectData) {

    const formData = new FormData();
    formData.append('SubjectCentralPolicyProvinceId', subjectData.subjectId);
    formData.append('Name', subjectData.name);
    formData.append('Box', subjectData.box);

    for (var ii = 0; ii < subjectData.inputanswerclose.length; ii++) {
      formData.append('answerclose', subjectData.inputanswerclose[ii].answerclose);
    }

    for (var ii = 0; ii < subjectData.DepartmentId.length; ii++) {
      formData.append('DepartmentId', subjectData.DepartmentId[ii]);
    }

    console.log('FORMDATA: ', formData);
    return this.http.post<any>(this.url + 'addsubjectrole3', formData);
  }
  getsubjectfromprovince(proid) {
    return this.http.get(this.url + "getsubjectfromprovince/" + proid)
  }

  subjectevent(value, userid) {
    console.log("value", value);
    console.log("value", value.province);
    const formData = {
      Land: value.land,
      CentralpolicyId: value.CentralpolicyId,
      ProvinceId: parseInt(value.province),
      // startdate: value.startdate.date.year + '-' + value.startdate.date.month + '-' + value.startdate.date.day,
      // enddate: value.enddate.date.year + '-' + value.enddate.date.month + '-' + value.enddate.date.day,
      startdate: value.startdate,
      enddate: value.enddate,
      CreatedBy: userid
    }
    return this.http.post<any>(this.url + 'subjectevent', formData);
  }

  subjecteventnoland(value, userid) {
    console.log("value", value);
    console.log("value", value.province);
    const formData = {
      Land: value.land,
      CentralpolicyId: value.CentralpolicyId,
      ProvinceId: parseInt(value.province),
      // startdate: value.startdate.date.year + '-' + value.startdate.date.month + '-' + value.startdate.date.day,
      // enddate: value.enddate.date.year + '-' + value.enddate.date.month + '-' + value.enddate.date.day,
      CreatedBy: userid
    }
    return this.http.post<any>(this.url + 'subjecteventnoland', formData);
  }

  subjecteventnolandOtherland(value, userid) {
    console.log("value", value);
    console.log("value", value.province);
    const formData = {
      Land: value.land,
      CentralpolicyId: value.CentralpolicyId,
      ProvinceId: parseInt(value.province),
      Title: value.centralPolicyOther,
      startdate: value.startdate,
      enddate: value.enddate,
      CreatedBy: userid
    }
    return this.http.post<any>(this.url + 'postsubjecteventOtherland', formData);
  }

  subjecteventnolandOther(value, userid) {
    console.log("value", value);
    console.log("value", value.province);
    const formData = {
      Land: value.land,
      CentralpolicyId: value.CentralpolicyId,
      ProvinceId: parseInt(value.province),
      Title: value.centralPolicyOther,
      // startdate: value.startdate.date.year + '-' + value.startdate.date.month + '-' + value.startdate.date.day,
      // enddate: value.enddate.date.year + '-' + value.enddate.date.month + '-' + value.enddate.date.day,
      CreatedBy: userid
    }
    return this.http.post<any>(this.url + 'postsubjecteventOther', formData);
  }

  getsubjectevent(id) {
    return this.http.get<any[]>(this.url + "getevent/" + id)
  }
  getsubjecteventprovince(id, provinceid) {
    return this.http.get<any[]>(this.url + "geteventprovice/" + id + "/" + provinceid)
  }
  geteventfromcalendar(id): Observable<any> {
    return this.http.get<any[]>(this.url + "geteventfromcalendar/" + id)
  }

  postsubjecteventfromcalendar(value, userid) {
    // alert(JSON.stringify(value))
    console.log("valuevaluevaluevaluevaluevaluevalue", value);

    var CentralpolicySelect2 = []
    CentralpolicySelect2 = value.CentralpolicyId2.map((item, index) => {
      return {
        centralpolicyId: item.centralPolicyId,
        CentralPolicyeventId: item.centralPolicyeventId,
      }
    })

    const formData = {
      Land: "ลงพื้นที่",
      CentralpolicySelect: CentralpolicySelect2,
      // CentralPolicyeventId:value.centralPolicyeventId,
      ProvinceId: parseInt(value.province2),
      CreatedBy: userid
    }
    // console.log('JSON.parse(value.CentralpolicyId2)',value.CentralpolicyId2[0])
    return this.http.post<any>(this.url + 'postsubjecteventfromcalendar', formData);
  }
  editSubject2(Subjectdata, id) {
    console.log("id: ", id);
    console.log("Subjectdata: ", Subjectdata);


    const formData = new FormData();
    formData.append('Name', Subjectdata.name);
    formData.append('Status', Subjectdata.status);
    formData.append('Explanation', Subjectdata.explanation);

    return this.http.put(this.url + "editsubject2/" + id, formData);
  }

  delsubjecteventnoland(id) {
    return this.http.delete(this.url + "delsubjecteventnoland/" + id);
  }

}


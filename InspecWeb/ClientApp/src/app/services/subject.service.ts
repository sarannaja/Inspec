import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class SubjectService {

  url = "";

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.url = baseUrl + 'api/subject/';
  }
  getsubjectdata(id) {
    return this.http.get(this.url + id)
  }
  getsubjectdetaildata(id) {
    return this.http.get(this.url + "subjectdetail/" + id)
  }
  addSubject(subjectData, centralpolicyid) {
    var subjectdepartment = subjectData.inputsubjectdepartment
    console.log('subjectData: ', subjectdepartment);

    // var subjectdepartment: Array<any> = subjectdepartmentId.map((item, index) => {
    //   return {
    //     provincialdepartmentprovinceid: item.value 
    //   }
    // })
    // console.log("ARRAY: ", subjectdepartment);
    var departmentId = []
    for (var i = 0; i < subjectdepartment.length; i++) {
      for (var j = 0; j < subjectdepartment[i].departmentId.length; j++) {
        departmentId.push({ departmentId: subjectdepartment[i].departmentId[j], inputsubjectdepartment: subjectdepartment[i] })
      }

    }
    console.log("test", departmentId);

    const formData = {
      Name: subjectData.name,
      Answer: subjectData.name,
      CentralPolicyId: parseInt(centralpolicyid),
      CentralPolicyDateId: subjectData.centralpolicydateid,
      inputsubjectdepartment: departmentId,
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
    return this.http.post(this.url, formData);

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
}


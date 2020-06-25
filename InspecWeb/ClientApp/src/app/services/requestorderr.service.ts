import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RequestorderrService {
  url: string = ""
  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.url = baseUrl + 'api/RequestOrder/';
  }

//สำหรับ superadmin
getrequestorderdata(): Observable<any[]> {
  return this.http.get<any[]>(this.url)
}

//สำหรับผู้สั่งการ
getrequestordercommandeddata(id): Observable<any> {
  return this.http.get<any>(this.url + "commanded/" + id)
}
//สำหรับผู้ตอบ /roleผู้ตรวจ
getrequestorderanswereddata(id): Observable<any> {
  return this.http.get<any>(this.url + "answered/" + id)
}

//สำหรับเพิ่มข้อคำร้อง
addrequestorder(requestorderData, file: FileList) {
  //alert(2);
  const formData = new FormData();
  formData.append('Commanded_date', requestorderData.Commanded_date.date.year + '-' + requestorderData.Commanded_date.date.month + '-' + requestorderData.Commanded_date.date.day);
  formData.append('Commanded_by', requestorderData.Commanded_by);
  formData.append('Subject', requestorderData.Subject);
  formData.append('Subjectdetail', requestorderData.Subjectdetail);
  formData.append('Answer_by', requestorderData.Answer_by);
  for (var iii = 0; iii < file.length; iii++) {
    formData.append("files", file[iii]);
  }
   return this.http.post<any>(this.url, formData);
}

//สำหรับตอบกลับข้อคำร้อง
answerrequestorder(requestorderData, file: FileList, id) {
  // alert(2);
  // alert(requestorderData.Answerdetail);
  // alert(requestorderData.AnswerProblem);
  // alert(requestorderData.AnswerCounsel);
  const formData = new FormData();
  formData.append('id', id);
  formData.append('Answerdetail', requestorderData.Answerdetail);
  formData.append('AnswerProblem', requestorderData.AnswerProblem);
  formData.append('AnswerCounsel', requestorderData.AnswerCounsel);
  for (var iii = 0; iii < file.length; iii++) {
    formData.append("files", file[iii]);
  }
  return this.http.put<any>(this.url, formData);
}




  editAnswerrequestorder(detailrequestorderData: any, id) {
    // return detailrequestorderData
    var file: FileList = detailrequestorderData.files
    var formData = new FormData();
    formData.append('id', id);
    formData.append('AnswerDetail', detailrequestorderData.AnswerDetail);
    formData.append('AnswerProblem', detailrequestorderData.AnswerProblem);
    formData.append('AnswerCounsel', detailrequestorderData.AnswerCounsel);
    for (var iii = 0; iii < file.length; iii++) {
      formData.append("files", file[iii]);
    }
   
    return this.http.put(this.url + 'edit', formData);
  }

  getdetailrequestorderdata(id): Observable<any> {
    return this.http.get<any>(this.url + "detail/" + id)
  }
  getviewrequestorderdata(id): Observable<any> {
  
    return this.http.get<any>(this.url + "view/" + id)
  }
 
  getCentralpolicydata(id): Observable<any> {
    //alert(id);
    return this.http.get<any>(this.url +"ex/"+ id)
  }

  getprovince(id): Observable<any> {
    return this.http.get<any>(this.url + "province/" + id)
  }

  adddetailrequestorder(detailrequestorderData, file: FileList, centralpolicyid,userid) {

    var formData = new FormData();
    formData.append('Name', detailrequestorderData.name);
    formData.append('CentralpolicyId', centralpolicyid);
    formData.append('ProvinceId', detailrequestorderData.provinceId);
    formData.append('Userid', userid);
    for (var iii = 0; iii < file.length; iii++) {
      formData.append("files", file[iii]);
      
    }
    // console.log('Name: ' + formData.get("Name"));
    // console.log('CentralpolicyId: ' + formData.get("CentralpolicyId"));
    // console.log('ProvinceId: ' + formData.get("ProvinceId"));
    // console.log('files: ', formData.get("files"));
    return this.http.post<any>(this.url, formData);
  }

  // answerrequestorder(detailrequestorderData, file: FileList, id) {

  //   // console.log('xxx' + detailrequestorderData)
  //   const formData = new FormData();
  //   formData.append('id', id);
  //   formData.append('AnswerDetail', detailrequestorderData.AnswerDetail);
  //   formData.append('AnswerProblem', detailrequestorderData.AnswerProblem);
  //   formData.append('AnswerCounsel', detailrequestorderData.AnswerCounsel);
  //   for (var iii = 0; iii < file.length; iii++) {
  //     formData.append("files", file[iii]);
  //   }
  
  //   return this.http.put<any>(this.url, formData);
  // }
  getdetailrequestorderdatarole3(id, userid): Observable<any> {
    return this.http.get<any>(this.url + "detailforinspector/" + id + "/" + userid)
  }

  getrequest1(Id) {
    console.log('Id in service',Id);
    // alert(Id);
    var boby ={
      Id 
    }
    return this.http.post<any>(this.url + "exportrequest1",boby)
    
  }
  getrequest3(Id) {
    console.log('Id in service',Id);
    // alert(Id);
    var boby ={
      Id 
    }
    return this.http.post<any>(this.url + "exportrequest3",boby)
  }

  getrequest2(id){
    return this.http.get<any>(this.url + "exportrequest2/" + id)
  }
}

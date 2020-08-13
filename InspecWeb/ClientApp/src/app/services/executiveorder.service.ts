import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Executiveordercommanded } from '../models/excucommand';
@Injectable({
  providedIn: 'root'
})
export class ExecutiveorderService {
  url = "";
  files: FileList

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    this.url = baseUrl + 'api/ExecutiveOrder/';
  }

  //สำหรับ superadmin
  getexecutiveorderdata(): Observable<any[]> {
    return this.http.get<any[]>(this.url)
  }

  //สำหรับผู้สั่งการ
  getexecutiveordercommandeddata(id): Observable<Executiveordercommanded[]> {
    return this.http.get<Executiveordercommanded[]>(this.url + "commanded/" + id)
  }
  //สำหรับผู้ตอบ /roleผู้ตรวจ
  getexecutiveorderanswereddata(id): Observable<any> {
    return this.http.get<any>(this.url + "answered/" + id)
  }

    //<!-- ดูรายละเอียด -->
    getexecutiveorderdetaildata(id): Observable<any> {
      return this.http.get<any>(this.url + "excutiveorderdetail/" + id)
    }
     //<!-- END ดูรายละเอียด -->
  
  //สำหรับเพิ่มข้อสั่งการ
  addexecutiveorder(executiveorderData, file: FileList,Commanded_by) {
    //alert(2);
    const formData = new FormData();
    formData.append('Commanded_date', executiveorderData.Commanded_date.date.year + '-' + executiveorderData.Commanded_date.date.month + '-' + executiveorderData.Commanded_date.date.day);
    formData.append('Commanded_by', Commanded_by);
    formData.append('Subject', executiveorderData.Subject);
    formData.append('Subjectdetail', executiveorderData.Subjectdetail);
    formData.append('Draft', executiveorderData.Draft);

    for (var i = 0; i < executiveorderData.Answer_by.length; i++) {
     // alert('Answer_by :'+ executiveorderData.Answer_by[i]);
      formData.append('Answer_by', executiveorderData.Answer_by[i]); 
    }

    if(file != null){
      for (var iii = 0; iii < file.length; iii++) {
        formData.append("files", file[iii]);
      }     
    }

     return this.http.post<any>(this.url, formData);
  }
  //สำหรับแก้ไขข้อสั่งการ
  updateexecutiveorder(executiveorderData, file: FileList, id) {
   // alert(2)
    const formData = new FormData();
    formData.append('id', id);
    formData.append('Commanded_date', executiveorderData.Commanded_date.date.year + '-' + executiveorderData.Commanded_date.date.month + '-' + executiveorderData.Commanded_date.date.day);
    formData.append('Commanded_by', executiveorderData.Commanded_by);
    formData.append('Subject', executiveorderData.Subject);
    formData.append('Subjectdetail', executiveorderData.Subjectdetail);
    formData.append('Draft', executiveorderData.Draft);

    for (var i = 0; i < executiveorderData.Answer_by.length; i++) {
     // alert(executiveorderData.Answer_by[i]);
      formData.append('Answer_by', executiveorderData.Answer_by[i]); //
    }
    if(file != null){
      for (var iii = 0; iii < file.length; iii++) {
        formData.append("files", file[iii]);
      }
    }
    return this.http.put<any>(this.url+"updateexecutiveorder", formData);
  }
  
  //สำหรับตอบกลับข้อสั่งการ
  answerexecutiveorder(executiveorderData, file: FileList, idexecutiveorderanswer) {
    //alert(2);
    const formData = new FormData();
    formData.append('ExecutiveOrderAnswerId', idexecutiveorderanswer);
    formData.append('Answerdetail', executiveorderData.Answerdetail);
    formData.append('AnswerProblem', executiveorderData.AnswerProblem);
    formData.append('AnswerCounsel', executiveorderData.AnswerCounsel);

    if(file != null){
      for (var iii = 0; iii < file.length; iii++) {
        formData.append("files", file[iii]);
      }
    }

    return this.http.put<any>(`${this.url}answerexecutiveorder`, formData);
  }

  //สำหรับยกเลิกข้อสั่งการ
  cancelexecutiveorder(executiveorderData,id) {
    //alert(2);
    const formData = new FormData();
    formData.append('id', id);
    formData.append('Canceldetail', executiveorderData.canceldetail);
     return this.http.put<any>(`${this.url}cancelexecutiveorder`, formData);
  }

  //สำหรับรับทราบข้อสั่งการ
  gotitexecutiveorder(id,ExecutiveOrderAnswerId) {
   // alert(2);
    const formData = new FormData();
    formData.append('id', id);
    formData.append('ExecutiveOrderAnswerId', ExecutiveOrderAnswerId);
     return this.http.put<any>(`${this.url}gotitexecutiveorder`, formData);
  }

  getexcutive1(Id) {
    console.log('Id in service',Id);
    // alert(Id);
    var boby ={
      Id 
    }
    return this.http.post<any>(this.url + "export1",boby)
    // return this.http.get<any>("https://localhost:5001/api/ExecutiveOrder/export1/8b843646-d1f3-4a63-a1a4-a024f97138b8")
  }

  getexcutive3(Id) {
    console.log('Id in service',Id);
    // alert(Id);
    var boby ={
      Id 
    }
    return this.http.post<any>(this.url + "export3",boby)
    // return this.http.get<any>("https://localhost:5001/api/ExecutiveOrder/export1/8b843646-d1f3-4a63-a1a4-a024f97138b8")
  }

  exportExecutive(userId) {
    console.log("in service: ", userId);

    const formData = {
      userId: userId,
      typeId: "1",
    }

    return this.http.post<any>(this.url + "/exportexecutive", formData)
  }
  getexcutive2(id): Observable<any> {
    return this.http.get<any>(this.url + "export2/" + id)
  }
}


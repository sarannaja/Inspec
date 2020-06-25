import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
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
  getexecutiveordercommandeddata(id): Observable<any> {
    return this.http.get<any>(this.url + "commanded/" + id)
  }
  //สำหรับผู้ตอบ /roleผู้ตรวจ
  getexecutiveorderanswereddata(id): Observable<any> {
    return this.http.get<any>(this.url + "answered/" + id)
  }
  
  //สำหรับเพิ่มข้อสั่งการ
  addexecutiveorder(executiveorderData, file: FileList) {
    //alert(2);
    //console.log('datatest',executiveorderData);
    const formData = new FormData();
    formData.append('Commanded_date', executiveorderData.Commanded_date.date.year + '-' + executiveorderData.Commanded_date.date.month + '-' + executiveorderData.Commanded_date.date.day);
    formData.append('Commanded_by', executiveorderData.Commanded_by);
    formData.append('Subject', executiveorderData.Subject);
    formData.append('Subjectdetail', executiveorderData.Subjectdetail);
    formData.append('Answer_by', executiveorderData.Answer_by);
    for (var iii = 0; iii < file.length; iii++) {
      formData.append("files", file[iii]);
    }
     return this.http.post<any>(this.url, formData);
  }

  
  //สำหรับตอบกลับข้อสั่งการ
  answerexecutiveorder(executiveorderData, file: FileList, id) {
    const formData = new FormData();
    formData.append('id', id);
    formData.append('Answerdetail', executiveorderData.Answerdetail);
    formData.append('AnswerProblem', executiveorderData.AnswerProblem);
    formData.append('AnswerCounsel', executiveorderData.AnswerCounsel);
    for (var iii = 0; iii < file.length; iii++) {
      formData.append("files", file[iii]);
    }
    return this.http.put<any>(this.url, formData);
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


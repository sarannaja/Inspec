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

  //<!-- ดูรายละเอียด -->
  getrequestorderdetaildata(id): Observable<any> {
    return this.http.get<any>(this.url + "requestorderdetail/" + id)
  }
   //<!-- END ดูรายละเอียด -->

//สำหรับเพิ่มข้อคำร้อง
addrequestorder(requestorderData, file: FileList,Commanded_by) {
  const formData = new FormData();
  formData.append('Commanded_date', requestorderData.Commanded_date.date.year + '-' + requestorderData.Commanded_date.date.month + '-' + requestorderData.Commanded_date.date.day);
  formData.append('Commanded_by', Commanded_by);
  formData.append('Subject', requestorderData.Subject);
  formData.append('Subjectdetail', requestorderData.Subjectdetail);
  formData.append('Draft', requestorderData.Draft);

  for (var i = 0; i < requestorderData.Answer_by.length; i++) {
    formData.append('Answer_by', requestorderData.Answer_by[i]); 
  }

  if(file != null){
    for (var iii = 0; iii < file.length; iii++) {
      formData.append("files", file[iii]);
    }     
  }

   return this.http.post<any>(this.url, formData);
}


  //สำหรับแก้ไขคำร้องขอ
  updaterequestorder(requestorderData, file: FileList, id) {
    const formData = new FormData();
    formData.append('id', id);
    formData.append('Commanded_date', requestorderData.Commanded_date.date.year + '-' + requestorderData.Commanded_date.date.month + '-' + requestorderData.Commanded_date.date.day);
    formData.append('Commanded_by', requestorderData.Commanded_by);
    formData.append('Subject', requestorderData.Subject);
    formData.append('Subjectdetail', requestorderData.Subjectdetail);
    formData.append('Draft', requestorderData.Draft);

    for (var i = 0; i < requestorderData.Answer_by.length; i++) {
      formData.append('Answer_by', requestorderData.Answer_by[i]); //
    }
    if(file != null){
      for (var iii = 0; iii < file.length; iii++) {
        formData.append("files", file[iii]);
      }
    }
    return this.http.put<any>(this.url+"updaterequestorder", formData);
  }

  //สำหรับยกเลิก
  cancelrequestorder(requestorderData,id) {
    const formData = new FormData();
    formData.append('id', id);
    formData.append('Canceldetail', requestorderData.canceldetail);
     return this.http.put<any>(`${this.url}cancelrequestorder`, formData);
  }

  //สำหรับรับทราบ
  gotitrequestorder(id,RequestOrderAnswerId) {
      const formData = new FormData();
      formData.append('id', id);
      formData.append('RequestOrderAnswerId', RequestOrderAnswerId);
      return this.http.put<any>(`${this.url}gotitrequestorder`, formData);
    }

      //สำหรับตอบกลับ
    answerrequestorder(requestorderData, file: FileList, idrequestderanswer) {
    const formData = new FormData();
    formData.append('RequestOrderAnswerId', idrequestderanswer);
    formData.append('Answerdetail', requestorderData.Answerdetail);
    formData.append('AnswerProblem', requestorderData.AnswerProblem);
    formData.append('AnswerCounsel', requestorderData.AnswerCounsel);

    if(file != null){
      for (var iii = 0; iii < file.length; iii++) {
        formData.append("files", file[iii]);
      }
    }

    return this.http.put<any>(`${this.url}answerrequestorder`, formData);
  }
   
  getrequest1(Id) {
    //alert(2);
    var boby ={
      Id 
    }
    return this.http.post<any>(this.url + "exportrequest1",boby)
    
  }
  getrequest3(Id) {
    var boby ={
      Id 
    }
    return this.http.post<any>(this.url + "exportrequest3",boby)
  }

  getrequest2(id,userId){
    return this.http.get<any>(this.url + "exportrequest2/" + id + "/" + userId)
  }

  deleterequestorder(id) {
    return this.http.delete(this.url + id);
  }
}

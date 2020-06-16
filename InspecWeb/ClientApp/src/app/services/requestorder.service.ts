import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class RequestorderService {
  url = "";
  files: FileList

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    this.url = baseUrl + 'api/RequestOrder/';
    // api/requestorder/centralpolicyprovinceid/
  }
  editAnswerrequestorder(detailrequestorderData: any, id) {
    // return detailrequestorderData
    var file: FileList = detailrequestorderData.files
    var formData = new FormData();
    formData.append('id', id);
    formData.append('AnswerDetail', detailrequestorderData.AnswerDetail);
    formData.append('AnswerProblem', detailrequestorderData.AnswerProblem);
    formData.append('AnswerCounsel', detailrequestorderData.AnswerCounsel);
    formData.append('AnswerUserId', detailrequestorderData.byuserid);
    for (var iii = 0; iii < file.length; iii++) {
      formData.append("files", file[iii]);
    }
    return this.http.put(this.url + 'edit', formData);
  }
  getcentralpolicyprovinceiddata(id){
    
    return this.http.get<any[]>(this.url + 'centralpolicyprovinceid/'+ id);
    }
  }



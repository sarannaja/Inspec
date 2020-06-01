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
    console.log('FORMDATA: ', formData.getAll);
    // console.log('AnswerDetail: ' + formData.get("AnswerDetail"));
    // console.log('AnswerProblem: ' + formData.get("AnswerProblem"));
    // console.log('AnswerCounsel: ' + formData.get("AnswerCounsel"));
    // console.log('files: ', formData.get("files"));
    return this.http.put(this.url + 'test', formData);
  }
}


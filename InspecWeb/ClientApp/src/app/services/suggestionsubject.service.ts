import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class SuggestionsubjectService {

  url = "";

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.url = baseUrl + 'api/suggestionsubject/';
  }
  addSuggestion(SuggestioData, SubjectCentralPolicyProvinceId, UserId) {
    const formData = new FormData();
    console.log("Suggestion", SuggestioData.Suggestion);
    console.log("SubjectCentralPolicyProvinceId", SubjectCentralPolicyProvinceId);
    console.log("UserId", UserId);

    formData.append('SubjectCentralPolicyProvinceId', SubjectCentralPolicyProvinceId);
    formData.append('UserId', UserId);
    formData.append('Suggestion', SuggestioData.Suggestion);

    return this.http.post(this.url, formData);
  }
}

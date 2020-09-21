import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class TrainingLoginService {
  url = "";

  constructor(private http: HttpClient,
    @Inject('BASE_URL') baseUrl: string
  ) {
    this.url = baseUrl + 'api/traininglogin/';
  }

  getTrainingData(trainingPhaseId) {
    return this.http.get(this.url + "getTrainingByPhaseId/" + trainingPhaseId);
  }

  signInTraining(trainingData, trainingPhaseId, trainingProgramLoginId) {
    console.log("Training Data: ", trainingData);
    console.log("Training PhaseId: ", trainingPhaseId);

    const formData = new FormData();
    formData.append('username', trainingData.username);
    formData.append('trainingPhaseId', trainingPhaseId);
    formData.append('trainingProgramLoginId', trainingProgramLoginId);

    console.log('FORMDATA: ', formData);
    return this.http.post<any>(this.url + "trainingSignin", formData);
  }
}

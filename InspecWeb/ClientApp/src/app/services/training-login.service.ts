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

  getTrainingProgramLogin(trainingprogramloginid) {
    return this.http.get<any>(this.url + "TrainingProgramLogin/get/" + trainingprogramloginid);
  }

  signInTraining(trainingData, trainingPhaseId, trainingProgramLoginId, dateType) {
    console.log("Training Data: ", trainingData);
    console.log("Training PhaseId: ", trainingPhaseId);
    console.log("Date Type: ", dateType);

    const formData = new FormData();
    formData.append('username', trainingData.username);
    formData.append('trainingPhaseId', trainingPhaseId);
    formData.append('trainingProgramLoginId', trainingProgramLoginId);
    formData.append('dateType', dateType);

    console.log('FORMDATA: ', formData);
    return this.http.post<any>(this.url + "trainingSignin", formData);
  }

  getUserLogIn(id, programType, trainingId) {
    return this.http.get(this.url + "getUserLogIn/" + id + "/" + programType + "/" + trainingId);
  }

  getTrainingProgramDate(trainingid) {
    return this.http.get<any>(this.url + "TrainingProgramDate/get/" + trainingid);
  }


  getTrainingProgramDate2(trainingid) {
    return this.http.get<any>(this.url + "TrainingProgramDate2/get/" + trainingid);
  }

  register(userData, trainingProgramLoginId, dateType) {
    console.log("Training Data: ", userData);
    console.log("Date Type: ", dateType);

    const formData = new FormData();
    formData.append('username', userData.userName);
    formData.append('trainingProgramLoginId', trainingProgramLoginId);
    formData.append('dateType', dateType);
    formData.append('trainingId', userData.trainingId);

    console.log('FORMDATA: ', formData);
    return this.http.post<any>(this.url + "trainingSignin2", formData);
  }
}

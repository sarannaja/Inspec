import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ExampleService {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  sendExample(body) {
    let data = body
    return this.http.post(this.baseUrl + 'api/example/example', data)
  }
}

import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  count = 0
  url = "";
  base = "";
  private subject = new Subject<any>();
  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.url = baseUrl + 'api/user/getuser/';
    this.base = baseUrl  + 'api/user/';
  }

  getuserdata(id: any): Observable<any[]> {
    return this.http.get<any[]>(this.url + id)
  }
  getprovincedata(id): Observable<any[]> {
    return this.http.get<any[]>(this.base + 'province/' + id)
  }


    sendNav(roleId: string) {
        this.subject.next({ roleId: roleId });
    }

    clearUserNav() {
        this.subject.next();
    }

    getUserNav(): Observable<any> {
        return this.subject.asObservable();
    }
}

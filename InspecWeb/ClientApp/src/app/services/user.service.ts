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
  urlfirst = "";
  urllist = "";
  //files: FileList

  private subject = new Subject<any>();
  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.url = baseUrl + 'api/user/getuser/';
    this.urllist = baseUrl + 'api/user/getuserlist/';
    this.base = baseUrl + 'api/user/';
  }

  getuserdata(id: any): Observable<any[]> { //role
    return this.http.get<any[]>(this.url + id)
  }
  getuserselectforexecutiveorderandrequestorder(): Observable<any[]> { //role
    return this.http.get<any[]>(this.base +'getuserselectforexecutiveorderandrequestorder')
  }
  getuserdataforrequestorder(): Observable<any[]> { //role
    return this.http.get<any[]>(this.base +'getuserforrequestorder')
  }
  getprovincedata(id): Observable<any[]> {
    return this.http.get<any[]>(this.base + 'province/' + id)
  }
  getuserlistdata(id: any): Observable<any[]> {
    return this.http.get<any[]>(this.urllist + id)
  }

  getuserfirstdata(id: any): Observable<any[]> {
    return this.http.get<any[]>(this.base + 'getuserfirst/' + id)
  }

  getplancount(id: any): Observable<any[]> {
    return this.http.get<any[]>(this.base + 'getplancount/' + id)
  }

  getprovincialdepartment(id: any): Observable<any> {
    return this.http.get<any>(this.base + 'getprovincialdepartment/' + id)
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

  addUser(userData, file: FileList, roleId) {
    console.log("servicelog: ", userData);
    const formData = new FormData();
    formData.append('Email', userData.Email); //email   
    formData.append('Role_id', roleId); //role

    if(userData.Prefix == "อื่นๆ"){     
    formData.append('Prefix', userData.Othertext);
    }else{
      formData.append('Prefix', userData.Prefix); 
    }

    formData.append('Firstnameth', userData.FName); //ชื่อ
    formData.append('Lastnameth', userData.LName); //ชื่อ
    formData.append('Position', userData.Position);//ตำแหน่ง
    if(userData.Position2 != null){
    formData.append('Position2', userData.Position2);//ผู้ตรวจหรือผู้ช่วยผู้ตรวจ
    }else{
      formData.append('Position2', "ผู้ตรวจราชการ");
    }
    formData.append('Educational', '');
    formData.append('Officephonenumber', '');
    formData.append('PhoneNumber', userData.PhoneNumber);
    formData.append('Telegraphnumber', '');
    formData.append('Housenumber', '');
    formData.append('Rold', '');
    formData.append('Alley', '');
    formData.append('Postalcode', '');
    formData.append('Autocreateuser', userData.Autocreateuser); // 20200823
    formData.append('UserName', userData.UserName); // 20200823
     //<!-- เลขที่คำสั่ง 20200915 -->
     if (userData.Commandnumber != null) {
      formData.append('Commandnumber', userData.Commandnumber);
    } else {
      formData.append('Commandnumber', '');
    }
    //<!-- END เลขที่คำสั่ง 20200915 -->
    //<!-- ด้าน -->
    if (userData.SideId != null) {
      formData.append('SideId', userData.SideId);
    } else {
      formData.append('SideId', '1');
    }
    //<!-- END ด้าน -->
    formData.append('Img', '');
    formData.append('startdate', userData.Startdate.date.year + '-' + userData.Startdate.date.month + '-' + userData.Startdate.date.day);

    if (userData.Enddate != null) {
      formData.append('enddate', userData.Enddate.date.year + '-' + userData.Enddate.date.month + '-' + userData.Enddate.date.day);
    } else {
      formData.append('enddate', null)
    }

    if (userData.Commandnumberdate != null) {
      formData.append('Commandnumberdate', userData.Commandnumberdate.date.year + '-' + userData.Commandnumberdate.date.month + '-' + userData.Commandnumberdate.date.day);
    } else {
      formData.append('Commandnumberdate', null)
    }

    if (userData.UserRegion != null) {
      for (var i = 0; i < userData.UserRegion.length; i++) {
        //alert(userData.UserRegion[i]);
        formData.append('UserRegion', userData.UserRegion[i]); //เขตที่รับผิดชอบมีได้หลายอัน
      }
    } else {
      formData.append('UserRegionId', '1');
    }

    if (userData.UserProvince != null) {
      // alert(userData.UserProvince);
      if (roleId == 9) {
        for (var i = 0; i < userData.UserProvince.length; i++) {
          formData.append('UserProvince', userData.UserProvince[i]); //จังหวัดที่รับผิดชอบมีได้หลายอัน
        }
      } else {
        formData.append('UserProvinceId', userData.UserProvince); //จังหวัดที่รับผิดชอบมีได้หลายอัน
      }

    } else {
      // alert(1);
      formData.append('UserProvinceId', '1');
    }

    if (userData.ProvinceId == null) {
      formData.append('ProvinceId', '1');
    } else {
      formData.append('ProvinceId', userData.ProvinceId); //จังหวัดมีได้อันเดียว
    }

    if (userData.DistrictId == null) {
      formData.append('DistrictId', '1');
    } else {
      formData.append('DistrictId', userData.DistrictId); //อำเภอมีได้อันเดียว
    }

    if (userData.SubdistrictId == null) {
      formData.append('SubdistrictId', '1');
    } else {
      formData.append('SubdistrictId', userData.SubdistrictId); //ตำบลมีได้อันเดียว
    }

    if (userData.MinistryId == null) {
      if (roleId == 4 || roleId == 5) {
        formData.append('MinistryId', '13');
      } else {
        formData.append('MinistryId', '1');
      }

    } else {
      if (roleId == 4 || roleId == 5) {
        formData.append('MinistryId', '13');
      } else {
        formData.append('MinistryId', userData.MinistryId); //กระทรวงมีได้อันเดียว
      }
    }

    if (userData.DepartmentId == null) {
      formData.append('DepartmentId', '6');
    } else {
      formData.append('DepartmentId', userData.DepartmentId);//กรมมีได้อันเดียว
    }

    if (userData.FiscalYear == null) {
      formData.append('FiscalYearId', '1');
    } else {
      //alert(userData.FiscalYear);
      formData.append('FiscalYearId', userData.FiscalYear);//ปีงบ
    }

    if (userData.ProvincialDepartmentId == null) {
      formData.append('ProvincialDepartmentId', '1');
    } else {
      formData.append('ProvincialDepartmentId', userData.ProvincialDepartmentId);//หน่วยงานภูมิภาคมีได้อันเดียว
    }

    if (file != null) {
      for (var iii = 0; iii < file.length; iii++) {
        formData.append("files", file[iii]);
      }
    } else {
      formData.append("files", null);
    }

    return this.http.post<any>(this.base, formData);
  }

  editprofile(userData, file: FileList, file2: FileList, userId) {
   // alert('2 :' + userData.FName + '*' +userData.LName)
    //console.log("servicelog: ", userData);
    const formData = new FormData();
    formData.append('Role_id', userData.Role_id); //role
    if(userData.Prefix == "อื่นๆ"){     
      formData.append('Prefix', userData.Othertext);
      }else{
        formData.append('Prefix', userData.Prefix); 
      }
    formData.append('Firstnameth', userData.FName); //ชื่อ
    formData.append('Lastnameth', userData.LName); //นามสกุล
    formData.append('Position', userData.Position);
    if(userData.Position2 != null){
      formData.append('Position2', userData.Position2);//ผู้ตรวจหรือผู้ช่วยผู้ตรวจ
      }else{
        formData.append('Position2', "ผู้ตรวจราชการ");
      }
    formData.append('PhoneNumber', userData.PhoneNumber);
    formData.append('Formprofile', userData.Formprofile);// สำหรับเช็ค user หรือแอดมิน เป็นคนเพิ่ม
    formData.append('Email', userData.Email); //email  
    formData.append('Educational', '');
    formData.append('Officephonenumber', '');
    formData.append('Telegraphnumber', '');
    formData.append('Housenumber', '');
    formData.append('Rold', '');
    formData.append('Alley', '');
    formData.append('Postalcode', '');
    formData.append('Img', userData.Img);
    formData.append('Signature', userData.Signature);
    formData.append('Autocreateuser', userData.Autocreateuser); // 20200823
    formData.append('UserName', userData.UserName); // 20200823
    //<!-- เลขที่คำสั่ง -->
    if (userData.Commandnumber != null) {
      formData.append('Commandnumber', userData.Commandnumber);
    } else {
      formData.append('Commandnumber', '');
    }
    //<!-- END เลขที่คำสั่ง -->
    //<!-- ด้าน -->
    if (userData.SideId != null) {
      formData.append('SideId', userData.SideId);
    } else {
      formData.append('SideId', '1');
    }
    //<!-- END ด้าน -->

    if (userData.Startdate != null) {
      formData.append('startdate', userData.Startdate.date.year + '-' + userData.Startdate.date.month + '-' + userData.Startdate.date.day);
    } else {
      formData.append('startdate', null)
    }

    if (userData.Enddate != null) {
      formData.append('enddate', userData.Enddate.date.year + '-' + userData.Enddate.date.month + '-' + userData.Enddate.date.day);
    } else {
      formData.append('enddate', null)
    }

    if (userData.Commandnumberdate != null) {
      formData.append('Commandnumberdate', userData.Commandnumberdate.date.year + '-' + userData.Commandnumberdate.date.month + '-' + userData.Commandnumberdate.date.day);
    } else {
      formData.append('Commandnumberdate', null)
    }

    if (userData.UserRegion != null) {
      for (var i = 0; i < userData.UserRegion.length; i++) {
        formData.append('UserRegion', userData.UserRegion[i]); //เขตที่รับผิดชอบมีได้หลายอัน
      }
    } else {
      formData.append('UserRegionId', '1');
    }

    if (userData.UserProvince != null) {
      // alert(userData.UserProvince);
      if (userData.Role_id == 9) {
        for (var i = 0; i < userData.UserProvince.length; i++) {
        //  alert(1);
          formData.append('UserProvince', userData.UserProvince[i]); //จังหวัดที่รับผิดชอบมีได้หลายอัน
        }
      } else {
       // alert(2);
        formData.append('UserProvinceId', userData.UserProvince); //จังหวัดที่รับผิดชอบมีได้หลายอัน
      }
    } else {
      formData.append('UserProvinceId', '1');
    }

    if (userData.ProvinceId == null) {
      formData.append('ProvinceId', '1');
    } else {
      formData.append('ProvinceId', userData.ProvinceId); //จังหวัดมีได้อันเดียว
    }

    if (userData.DistrictId == null) {
      formData.append('DistrictId', '1');
    } else {
      formData.append('DistrictId', userData.DistrictId); //อำเภอมีได้อันเดียว
    }

    if (userData.SubdistrictId == null) {
      formData.append('SubdistrictId', '1');
    } else {
      formData.append('SubdistrictId', userData.SubdistrictId); //ตำบลมีได้อันเดียว
    }

    if (userData.MinistryId == null) {
      if (userData.Role_id == 4 || userData.Role_id == 5) {
        formData.append('MinistryId', '13');
      } else {
        formData.append('MinistryId', '1');
      }

    } else {
      if (userData.Role_id == 4 || userData.Role_id == 5) {
        formData.append('MinistryId', '13');
      } else {
        formData.append('MinistryId', userData.MinistryId); //กระทรวงมีได้อันเดียว
      }
    }


    if (userData.DepartmentId == null) {
      formData.append('DepartmentId', '6');
    } else {
      formData.append('DepartmentId', userData.DepartmentId);//กรมมีได้อันเดียว
    }

    if (userData.FiscalYear == null) {
      formData.append('FiscalYearId', '1');
    } else {
      formData.append('FiscalYearId', userData.FiscalYear);//คำสั่งกำหนดเขตตรวจราชการ
    }

    if (userData.ProvincialDepartmentId == null) {
      formData.append('ProvincialDepartmentId', '1');
    } else {
      formData.append('ProvincialDepartmentId', userData.ProvincialDepartmentId);//หน่วยงานภูมิภาคมีได้อันเดียว
    }
    if (file != null) {
      for (var iii = 0; iii < file.length; iii++) {
        formData.append("files", file[iii]);
      }
    } else {
      formData.append("files", userData.Img);
    }

    if (file2 != null) {
      for (var iii = 0; iii < file2.length; iii++) {
        formData.append("files2", file2[iii]);
      }
    } else {
      formData.append("files2", userData.Signature);
    }

    let path = this.base + userId;
    return this.http.put<any>(path, formData)
  }
  deleteUser(id) {
    return this.http.delete(this.base + id);
  }

  //<!-- ข้อมูลผู้ติดต้อ ผู้ตรวจราชการ -->
  getuserinspectordata(regionid,provinceid): Observable<any[]> {
    return this.http.get<any[]>(this.base + 'inspector/' + regionid+ '/' + provinceid)
  }

   //<!-- map -->
  getuserinspectorformapdata(provincename): Observable<any[]> {
    return this.http.get<any[]>(this.base + 'inspectorformap/' + provincename)
  }

  getuserdistrictofficerformapdata(provincename): Observable<any[]> {
    return this.http.get<any[]>(this.base + 'districtofficerformap/' + provincename)
  }

  getuserregionalagencyformapdata(provincename): Observable<any[]> {
    return this.http.get<any[]>(this.base + 'regionalagencyformap/' + provincename)
  }

  getuserpublicsectoradvisorformapdata(provincename): Observable<any[]> {
    return this.http.get<any[]>(this.base + 'publicsectoradvisorformap/' + provincename)
  }
   //<!-- -->
  getusermapdata(provincename): Observable<any[]> {
    return this.http.get<any[]>(this.base + 'map/' + provincename)
  }

  //<!-- END map -->

  //<!-- ข้อมูลผู้ติดต้อ เจ้าหน้าที่ประจำเขตตรวจราชการ -->
  getuserdistrictofficerdata(regionid,provinceid): Observable<any[]> {
    return this.http.get<any[]>(this.base + 'districtofficer/'+ regionid + '/' + provinceid)
  }
  //<!-- END ข้อมูลผู้ติดต้อ เจ้าหน้าที่ประจำเขตตรวจราชการ -->

  //<!-- ข้อมูลผู้ติดต้อ ที่ปรึกษาผู้ตรวจราชการภาคประชาชน -->
  getuserpublicsectoradvisordata(regionid,provinceid): Observable<any[]> {
    return this.http.get<any[]>(this.base + 'publicsectoradvisor/'+regionid +'/'+ provinceid)
  }
  //<!-- END ข้อมูลผู้ติดต้อ ที่ปรึกษาผู้ตรวจราชการภาคประชาชน -->

    //<!-- ข้อมูลผู้ติดต่อ หน่วยงานภูมิภาค หรือ หน่วยรับตรวจ -->
    getuserregionalagencydata(regionid,provinceid): Observable<any[]> {
      return this.http.get<any[]>(this.base + 'regionalagency/' + regionid + '/' + provinceid)
    }
    //<!-- END ข้อมูลผู้ติดต่อ หน่วยงานภูมิภาค หรือ หน่วยรับตรวจ -->

  getuserdataSameMinistry(id: any, ministryId): Observable<any[]> { //role6
    return this.http.get<any[]>(this.base + "getuserSameMinistry/" + id + "/" + ministryId)
  }

  getuserdataDepartmentInMinistry(id: any, ministryId): Observable<any[]> { //role10
    return this.http.get<any[]>(this.base + "getuserSameMinistry/" + id + "/" + ministryId)
  }
  getuserdataDepartmentInDepartment(id: any, departmentId): Observable<any[]> { //role10
    return this.http.get<any[]>(this.base + "getuserSameDepartment/" + id + "/" + departmentId)
  }

  resetpassword(id) {
    const formData = new FormData();
    formData.append('id', id);
    
    return this.http.put(`${this.base}resetpassword`, formData);

  }
  changepassword(userData , id) {
    //alert(userData.Password);
    const formData = new FormData();
    formData.append('id', id);
    formData.append('Password', userData.Password);
    
    return this.http.put(`${this.base}changepassword`, formData);

  }
}

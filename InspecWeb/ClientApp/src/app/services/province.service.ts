import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProvinceService {

  count = 0
  url = "";


  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.url = baseUrl + 'api/province/';
  }

  getprovincedata2(): Observable<any[]> {
    return this.http.get<any[]>(this.url)
  }
  getprovincedata2(): Observable<Province[]> {
    return this.http.get<Province[]>(this.url + "centralpolicyprovinces")
  }
  getonlyprovince(): Observable<any[]> {
    return this.http.get<any[]>(this.url + 'getonlyprovince')
  }
  getsectordata(): Observable<any[]> {
    return this.http.get<any[]>(this.url + 'getsectordata')
  }
  getprovincegroupdata(): Observable<any[]> {
    return this.http.get<any[]>(this.url + 'getprovincegroupdata')
  }
  addProvince(provinceData) {
    // alert(2 +":" +provinceData.Provincegroup);
    const formData = new FormData();
    formData.append('Name', provinceData.provincename);
    formData.append('NameEN', provinceData.NameEN);
    formData.append('Link', provinceData.provincelink)
    formData.append('SectorId', provinceData.Sector);
    formData.append('ProvincesGroupId', provinceData.Provincegroup);
    formData.append('ShortnameEN', provinceData.ShortnameEN);
    formData.append('ShortnameTH', provinceData.ShortnameTH);

    return this.http.post(this.url, formData);
  }
  deleteProvince(id) {
    return this.http.delete(this.url + id);
  }
  editProvince(provinceData, id) {
    //console.log(provinceData);
    const formData = new FormData();
    formData.append('name', provinceData.provincename);
    formData.append('NameEN', provinceData.NameEN);
    formData.append('link', provinceData.provincelink)
    formData.append('SectorId', provinceData.Sector);
    formData.append('ProvincesGroupId', provinceData.Provincegroup);
    formData.append('ShortnameEN', provinceData.ShortnameEN);
    formData.append('ShortnameTH', provinceData.ShortnameTH);
    return this.http.put(this.url + id, formData);
  }
  getRegionMock() {
    return [

      {
        name: "กรุงเทพมหานคร",
        region: "ตอนพิเศษ"
      },
      {
        name: "สมุทรปราการ",
        region: "ตอนพิเศษ"

      },
      {
        name: "นนทบุรี",
        region: "ตอนพิเศษ"
      },
      {
        name: "ปทุมธานี",
        region: "ตอนพิเศษ"
      },
      {
        name: "พระนครศรีอยุธยา",
        region: "ภาคกลาง"
      },
      {
        name: "อ่างทอง",
        region: "ภาคกลาง"
      },
      {
        name: "ลพบุรี",
        region: "ภาคกลาง"
      },
      {
        name: "สิงห์บุรี",
        region: "ภาคกลาง"
      },
      {
        name: "ชัยนาท",
        region: "ภาคกลาง"
      },
      {
        name: "สระบุรี",
        region: "ภาคกลาง"
      },
      {
        name: "ชลบุรี",
        region: "ภาคกลาง"
      },
      {
        name: "ระยอง",
        region: "ภาคกลาง"
      },
      {
        name: "จันทบุรี",
        region: "ภาคกลาง"
      },
      {
        name: "ตราด",
        region: "ภาคกลาง"
      },
      {
        name: "ฉะเชิงเทรา",
        region: "ภาคกลาง"
      },
      {
        name: "ปราจีนบุรี",
        region: "ภาคกลาง"
      },
      {
        name: "นครนายก",
        region: "ภาคกลาง"
      },
      {
        name: "สระแก้ว",
        region: "ภาคกลาง"
      },
      {
        name: "ราชบุรี",
        region: "ภาคกลาง"
      },
      {
        name: "กาญจนบุรี",
        region: "ภาคกลาง"
      },
      {
        name: "สุพรรณบุรี",
        region: "ภาคกลาง"
      },
      {
        name: "นครปฐม",
        region: "ภาคกลาง"
      },
      {
        name: "สมุทรสาคร",
        region: "ภาคกลาง"
      },
      {
        name: "สมุทรสงคราม",
        region: "ภาคกลาง"
      },
      {
        name: "เพชรบุรี",
        region: "ภาคกลาง"
      },
      {
        name: "ประจวบคีรีขันธ์",
        region: "ภาคกลาง"
      },
      {
        name: "เชียงใหม่",
        region: "ภาคเหนือ"
      },
      {
        name: "ลำพูน",
        region: "ภาคเหนือ"
      },
      {
        name: "ลำปาง",
        region: "ภาคเหนือ"
      },
      {
        name: "อุตรดิตถ์",
        region: "ภาคเหนือ"
      },
      {
        name: "แพร่",
        region: "ภาคเหนือ"
      },
      {
        name: "น่าน",
        region: "ภาคเหนือ"
      },
      {
        name: "พะเยา",
        region: "ภาคเหนือ"
      },
      {
        name: "เชียงราย",
        region: "ภาคเหนือ"
      },
      {
        name: "แม่ฮ่องสอน",
        region: "ภาคเหนือ"
      },
      {
        name: "นครสวรรค์",
        region: "ภาคเหนือ"
      },
      {
        name: "อุทัยธานี",
        region: "ภาคเหนือ"
      },
      {
        name: "กำแพงเพชร",
        region: "ภาคเหนือ"
      },
      {
        name: "ตาก",
        region: "ภาคเหนือ"
      },
      {
        name: "สุโขทัย",
        region: "ภาคเหนือ"
      },
      {
        name: "พิษณุโลก",
        region: "ภาคเหนือ"
      },
      {
        name: "พิจิตร",
        region: "ภาคเหนือ"
      },
      {
        name: "เพชรบูรณ์",
        region: "ภาคเหนือ"
      },
      {
        name: "นครราชสีมา",
        region: "ภาคตะวันออกเฉียงเหนือ"
      },

      {
        name: "บุรีรัมย์",
        region: "ภาคตะวันออกเฉียงเหนือ"
      },

      {
        name: "สุรินทร์",
        region: "ภาคตะวันออกเฉียงเหนือ"
      },

      {
        name: "ศรีสะเกษ",
        region: "ภาคตะวันออกเฉียงเหนือ"
      },

      {
        name: "อุบลราชธานี",
        region: "ภาคตะวันออกเฉียงเหนือ"
      },

      {
        name: "ยโสธร",
        region: "ภาคตะวันออกเฉียงเหนือ"
      },

      {
        name: "ชัยภูมิ",
        region: "ภาคตะวันออกเฉียงเหนือ"
      },

      {
        name: "อำนาจเจริญ",
        region: "ภาคตะวันออกเฉียงเหนือ"
      },

      {
        name: "หนองบัวลำภู",
        region: "ภาคตะวันออกเฉียงเหนือ"
      },

      {
        name: "ขอนแก่น",
        region: "ภาคตะวันออกเฉียงเหนือ"
      },

      {
        name: "อุดรธานี",
        region: "ภาคตะวันออกเฉียงเหนือ"
      },

      {
        name: "เลย",
        region: "ภาคตะวันออกเฉียงเหนือ"
      },

      {
        name: "หนองคาย",
        region: "ภาคตะวันออกเฉียงเหนือ"
      },

      {
        name: "มหาสารคาม",
        region: "ภาคตะวันออกเฉียงเหนือ"
      },

      {
        name: "ร้อยเอ็ด",
        region: "ภาคตะวันออกเฉียงเหนือ"
      },

      {
        name: "กาฬสินธุ์",
        region: "ภาคตะวันออกเฉียงเหนือ"
      },

      {
        name: "สกลนคร",
        region: "ภาคตะวันออกเฉียงเหนือ"
      },

      {
        name: "นครพนม",
        region: "ภาคตะวันออกเฉียงเหนือ"
      },

      {
        name: "มุกดาหาร",
        region: "ภาคตะวันออกเฉียงเหนือ"
      },
      {
        name: "นครศรีธรรมราช",
        region: "ภาคใต้"
      },

      {
        name: "กระบี่",
        region: "ภาคใต้"
      },

      {
        name: "พังงา",
        region: "ภาคใต้"
      },

      {
        name: "ภูเก็ต",
        region: "ภาคใต้"
      },

      {
        name: "สุราษฎร์ธานี",
        region: "ภาคใต้"
      },

      {
        name: "ระนอง",
        region: "ภาคใต้"
      },

      {
        name: "ชุมพร",
        region: "ภาคใต้"
      },

      {
        name: "สงขลา",
        region: "ภาคใต้"
      },

      {
        name: "สตูล",
        region: "ภาคใต้"
      },

      {
        name: "ตรัง",
        region: "ภาคใต้"
      },

      {
        name: "พัทลุง",
        region: "ภาคใต้"
      },

      {
        name: "ปัตตานี",
        region: "ภาคใต้"
      },

      {
        name: "ยะลา",
        region: "ภาคใต้"
      },

      {
        name: "นราธิวาส",
        region: "ภาคใต้"
      },
      {
        name: "บึงกาฬ",
        region: "ภาคตะวันออกเฉียงเหนือ"
      },


    ]
  }

  wordprovince():Observable<any>{
    return this.http.get<any>(this.url + "wordprovince")
  }
}
export interface Province {
  id: number;
  name: string;
  link: string;
  createdAt: null;
  regoin?: string;
}

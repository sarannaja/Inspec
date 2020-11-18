import { MenuService } from './../services/menu.service';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormControl, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { allNav, NavBar } from '../../app/default-layout/default-layout/_nav'
import * as _ from 'lodash';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})

export class MenuComponent implements OnInit {
  loading = false;
  result: any[] = [];
  status: any[] = [
    { name: 'Active', value: 1 },
    { name: 'InActive', value: 0 },
  ]
  modalRef: BsModalRef;
  Form: FormGroup;
  id: any;
  m1: any = 1;
  m2: any = 1;
  m3: any = 1;
  m4: any = 1;
  m5: any = 1;
  m6: any = 1;
  m7: any = 1;
  m8: any = 1;
  m9: any = 1;
  m10: any = 1;
  m11: any = 1;
  m12: any = 1;
  m13: any = 1;
  m14: any = 1;
  m15: any = 1;
  m16: any = 1;
  m17: any = 1;
  m18: any = 1;
  m19: any = 1;
  m20: any = 1;
  m21: any = 1;
  m22: any = 1;
  m23: any = 1;
  m24: any = 1;
  m25: any = 1;
  m26: any = 1;
  m27: any = 1;
  m28: any = 1;
  m29: any = 1;
  m30: any = 1;
  m31: any = 1;
  m32: any = 1;
  m33: any = 1;
  m34: any = 1;
  dtOptions: any = {};
  arraynav: NavBar[] = []
  check_value: any[] = []
  constructor(
    private menuService: MenuService,
    private modalService: BsModalService,
    private fb: FormBuilder,
  ) {
    allNav.map(result => {
      for (let i = 0; i < result.length; i++) {
        this.arraynav.push(result[i])
      }
    })

    this.arraynav = _.uniqBy(this.arraynav, function (e) {
      return e.url
    }).concat(this.arraynav.filter(result => result.url == null))
  }

  ngOnInit() {
    this.getlogdata();
    this.form();
  }

  getlogdata() {
    this.menuService.getmenulistdata()
      .subscribe(result => {
        console.log('momomo', result);
        this.result = result;
        this.loading = true
      })
  }

  // openeditModal(template: TemplateRef<any>, id, m1, m2, m3, m4, m5, m6, m7,
  //   m8, m9, m10, m11, m12, m13, m14, m15, m16, m17, m18, m19,m20,m21,m22,m23,m24,m25,m26,m27) {
  //     this.id = id;
  //     this.m1 = m1;
  //     this.m2 = m2;
  //     this.m3 = m3;
  //     this.m4 = m4;
  //     this.m5 = m5;
  //     this.m6 = m6;
  //     this.m7 = m7;
  //     this.m8 = m8;
  //     this.m9 = m9;
  //     this.m10 = m10;
  //     this.m11 = m11;
  //     this.m12 = m12;
  //     this.m13 = m13;
  //     this.m14 = m14;
  //     this.m15 = m15;
  //     this.m16 = m16;
  //     this.m17 = m17;
  //     this.m18 = m18;
  //     this.m19 = m19;
  //     this.m20 = m20;
  //     this.m21 = m21;
  //     this.m22 = m22;
  //     this.m23 = m23;
  //     this.m24 = m24;
  //     this.m25 = m25;
  //     this.m26 = m26;
  //     this.m27 = m27;

  //     // this.Form.patchValue({
  //     //   m1: m1,
  //     //   m2: m2,
  //     //   m3: m3,
  //     //   m4: m4,
  //     //   m5: m5,
  //     //   m6: m6,
  //     //   m7: m7,
  //     //   m8: m8,
  //     //   m9: m9,
  //     //   m10: m10,
  //     //   m11: m11,
  //     //   m12: m12,
  //     //   m13: m13,
  //     //   m14: m14,
  //     //   m15: m15,
  //     //   m16: m16,
  //     //   m17: m17,
  //     //   m18: m18,
  //     //   m19: m19,
  //     //   m20: m20,
  //     //   m21: m21,
  //     //   m22: m22,
  //     //   m23: m23,
  //     //   m24: m24,
  //     //   m25: m25,
  //     //   m26: m26,
  //     //   m27: m27,
  //     // })

  //   this.modalRef = this.modalService.show(template);
  // }
  openeditModal(template: TemplateRef<any>, id, item) {
    this.id = id;
    let valueofitem = item
    // delete this.valueofitem.createdAt
    // delete this.valueofitem.role_id
    // delete this.valueofitem.id
    let mock_menu_disable: any[] = []
    // Object.values(valueofitem)
    // .map((item, index, xxx) => {
    //   console.log('xxx' + index, xxx);

    //   return { menuname: 'm' + (index + 1), status: item }
    // })
    for (const [key, value] of Object.entries(valueofitem)) {
      if (key != 'createdAt' && key != 'role_id' && key != 'id') {
        // console.log(key, value);
        mock_menu_disable.push({ menuname: key, status: value })
      }
    }

    // .map(r=> )
    // .filter(j => { return Object.values(j)[0] == 1 })
    // .map(j => Object.values(j)[0])
    //สำหรับฟิลเตอร์ nav 

    this.check_value = mock_menu_disable.map((item) => {
      let obj = this.arraynav.find(x => x.menuname == item.menuname)
      return { ...item, menu_map: obj }

    });

    // console.log(this.check_value);

    this.modalRef = this.modalService.show(template);
  }
  updateMenuActive() {
    // this.menuService.update(this.check_value,this.id)
    // .subscribe(result => { 

    // })
    this.menuService.update(this.check_value, this.id).subscribe(result => {
      this.modalRef.hide()
      this.getlogdata();
    })
    // console.log(this.id);
    // this.check_value.map(result => {
    //   if (result.status == 0) {

    //     // console.log(result.menuname)
    //   }
    // })

  }
  form() {
    this.Form = this.fb.group({
      m1: new FormControl(null, [Validators.required]),
      m2: new FormControl(null, [Validators.required]),
      m3: new FormControl(null, [Validators.required]),
      m4: new FormControl(null, [Validators.required]),
      m5: new FormControl(null, [Validators.required]),
      m6: new FormControl(null, [Validators.required]),
      m7: new FormControl(null, [Validators.required]),
      m8: new FormControl(null, [Validators.required]),
      m9: new FormControl(null, [Validators.required]),
      m10: new FormControl(null, [Validators.required]),
      m11: new FormControl(null, [Validators.required]),
      m12: new FormControl(null, [Validators.required]),
      m13: new FormControl(null, [Validators.required]),
      m14: new FormControl(null, [Validators.required]),
      m15: new FormControl(null, [Validators.required]),
      m16: new FormControl(null, [Validators.required]),
      m17: new FormControl(null, [Validators.required]),
      m18: new FormControl(null, [Validators.required]),
      m19: new FormControl(null, [Validators.required]),
      m20: new FormControl(null, [Validators.required]),
      m21: new FormControl(null, [Validators.required]),
      m22: new FormControl(null, [Validators.required]),
      m23: new FormControl(null, [Validators.required]),
      m24: new FormControl(null, [Validators.required]),
      m25: new FormControl(null, [Validators.required]),
      m26: new FormControl(null, [Validators.required]),
      m27: new FormControl(null, [Validators.required]),
      m28: new FormControl(null, [Validators.required]),
      m29: new FormControl(null, [Validators.required]),
      m30: new FormControl(null, [Validators.required]),
      m31: new FormControl(null, [Validators.required]),
      m32: new FormControl(null, [Validators.required]),
      m33: new FormControl(null, [Validators.required]),
      m34: new FormControl(null, [Validators.required]),

    })
  }

}

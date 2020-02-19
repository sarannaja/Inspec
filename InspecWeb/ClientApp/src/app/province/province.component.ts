import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { ProvinceService } from '../services/province.service';
import { Router } from '@angular/router';
import { NotificationService } from '../services/Pipe/alert.service';
import { SnotifyService, SnotifyToastConfig, SnotifyPosition } from 'ng-snotify';

@Component({
  selector: 'app-province',
  templateUrl: './province.component.html',
  styleUrls: ['./province.component.css']

})
export class ProvinceComponent implements OnInit {

  resultprovince: any[] = []
  delid: any
  name: any
  link: any
  modalRef: BsModalRef;
  Form: FormGroup;
  EditForm: FormGroup;
  loading = false;
  dtOptions: DataTables.Settings = {};
  forbiddenUsernames = ['admin', 'test', 'xxxx'];
  constructor(
    private modalService: BsModalService,
    private fb: FormBuilder,
    private provinceservice: ProvinceService,
    public share: ProvinceService,
    private router: Router,
    private snotifyService: NotificationService,
    private snotifyService2: SnotifyService,

  ) { }

  ngOnInit() {
    console.log("in");
    // this.onSuccess()
    // this.snotifyService.onSuccess("test")
    this.dtOptions = {
      pagingType: 'full_numbers',
      columnDefs: [
        {
          targets: [4],
          orderable: false
        }
      ]

    };

    this.Form = this.fb.group({
      "provincename": new FormControl(null, [Validators.required]),
      "provincelink": new FormControl(null, [Validators.required])
      // "test" : new FormControl(null,[Validators.required,this.forbiddenNames.bind(this)])
    })

    //แก้ไข


    this.provinceservice.getprovincedata()
      .subscribe(result => {

        this.resultprovince = result
        this.loading = true
        console.log(this.resultprovince);
      })
    this.Form.patchValue({
      // test: "testest"
    })
  }

  // onSuccess() {
  //   this.snotifyService2.success('ssssss', 'ssss', this.getConfig());
  // }
  // style = 'material';
  // title = 'Snotify title!';
  // body = 'Lorem ipsum dolor sit amet!';
  // timeout = 3000;
  // position: SnotifyPosition = SnotifyPosition.rightBottom;
  // progressBar = true;
  // closeClick = true;
  // newTop = true;
  // filterDuplicates = false;
  // backdrop = -1;
  // dockMax = 8;
  // blockMax = 6;
  // pauseHover = true;
  // titleMaxLength = 15;
  // bodyMaxLength = 80;
  // getConfig(): SnotifyToastConfig {
  //   this.snotifyService2.setDefaults({
  //     global: {
  //       newOnTop: this.newTop,
  //       maxAtPosition: this.blockMax,
  //       maxOnScreen: this.dockMax,
  //       // filterDuplicates: this.filterDuplicates
  //     }
  //   });
  //   return {
  //     bodyMaxLength: this.bodyMaxLength,
  //     titleMaxLength: this.titleMaxLength,
  //     backdrop: this.backdrop,
  //     position: this.position,
  //     timeout: this.timeout,
  //     showProgressBar: this.progressBar,
  //     closeOnClick: this.closeClick,
  //     pauseOnHover: this.pauseHover
  //   };
  // }

  District(id) {
    this.router.navigate(['/district', id])
  }

  openModal(template: TemplateRef<any>, id, name, link) {
    this.delid = id;
    this.name = name;
    this.link = link
    console.log(this.delid);
    console.log(this.name);

    this.modalRef = this.modalService.show(template);
  }
  forbiddenNames(control: FormControl): { [s: string]: boolean } {
    if (this.forbiddenUsernames.indexOf(control.value) !== -1) {
      return { 'forbiddenNames': true };
    }
    return null;
  }

  storeProvince(value) {
    this.provinceservice.addProvince(value).subscribe(response => {

      console.log(value);

      this.Form.reset()
      this.modalRef.hide()
      this.loading = false

      this.provinceservice.getprovincedata().subscribe(result => {
        this.resultprovince = result
        this.loading = true
        console.log(this.resultprovince);
      })
    })
  }
  deleteProvince(value) {
    this.provinceservice.deleteProvince(value).subscribe(response => {
      console.log(value);
      this.modalRef.hide()
      this.loading = false
      this.provinceservice.getprovincedata().subscribe(result => {
        this.resultprovince = result
        this.loading = true
        console.log(this.resultprovince);
      })
    })
  }
  editModal(template: TemplateRef<any>, id, name, link) {
    this.delid = id;
    this.name = name;
    this.link = link
    console.log(this.delid);
    console.log(this.name);

    this.modalRef = this.modalService.show(template);
    this.EditForm = this.fb.group({
      "provincename": new FormControl(null, [Validators.required]),
      "provincelink": new FormControl(null, [Validators.required])
      // "test" : new FormControl(null,[Validators.required,this.forbiddenNames.bind(this)])
    })
    this.EditForm.patchValue({
      "provincename": name,
      "provincelink": link
    })
  }
  editProvince(value, delid) {
    console.clear();
    console.log("kkkk" + JSON.stringify(value));
    this.provinceservice.editProvince(value, delid).subscribe(response => {
      this.Form.reset()
      this.modalRef.hide()
      this.loading = false
      this.provinceservice.getprovincedata().subscribe(result => {
        this.resultprovince = result
        this.loading = true
      })
    })
  }
}

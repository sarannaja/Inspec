import { Component, Inject, OnInit, TemplateRef } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { NotofyService } from '../services/notofy.service';
import { RetrospectiveReportService } from '../services/retrospective-report.service';
import { AuthorizeService } from 'src/api-authorization-new/authorize.service';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-retrospective-report',
  templateUrl: './retrospective-report.component.html',
  styleUrls: ['./retrospective-report.component.css']
})
export class RetrospectiveReportComponent implements OnInit {
  modalRef: BsModalRef;
  reportForm: FormGroup;
  editForm: FormGroup;
  loading = false;
  dtOptions: any = {};
  submitted: any;
  fileForm: FormGroup;
  userid: string;
  oldReports: any = [];
  url: any;
  oldReportId: any;
  fileName: any

  constructor(
    private modalService: BsModalService,
    private spinner: NgxSpinnerService,
    private fb: FormBuilder,
    private _NotofyService: NotofyService,
    private retrospectiveReportService: RetrospectiveReportService,
    private authorize: AuthorizeService,
    private userService: UserService,
    @Inject('BASE_URL') baseUrl: string,
  ) {
    this.url = baseUrl;
  }

  ngOnInit() {
    this.dtOptions = {
      pagingType: 'full_numbers',
      columnDefs: [
        {
          targets: [6],
          orderable: false
        }
      ],
      "language": {
        "lengthMenu": "แสดง  _MENU_  รายการ",
        "search": "ค้นหา:",
        // "info": "แสดง _PAGE_ ของ _PAGES_ รายการ",
        // "info": "แสดง _PAGE_ ของ _PAGES_ รายการ จาก _TOTAL_ แถว",
        "info": "แสดง _START_ ถึง _END_ จาก _TOTAL_ แถว",
        // "info": "แสดง _START_ ถึง _END_ จาก _TOTAL_ แถว",
        "infoEmpty": "แสดง 0 ของ 0 รายการ",
        "zeroRecords": "ไม่พบข้อมูล",
        "paginate": {
          "first": "หน้าแรก",
          "last": "หน้าสุดท้าย",
          "next": "ต่อไป",
          "previous": "ย้อนกลับ"
        },
      },
      scrollX: true,
    };

    this.authorize.getUser().subscribe(result => {
      console.log("res user: ", result);
      this.userid = result.sub
    })
    this.reportForm = this.fb.group({
      year: new FormControl(null, [Validators.required]),
      centralPolicyType: new FormControl(null, [Validators.required]),
      name: new FormControl(null, [Validators.required]),
      reportType: new FormControl(null, [Validators.required]),
      round: new FormControl(null, [Validators.required]),
    })

    this.editForm = this.fb.group({
      year: new FormControl(null, [Validators.required]),
      centralPolicyType: new FormControl(null, [Validators.required]),
      name: new FormControl(null, [Validators.required]),
      reportType: new FormControl(null, [Validators.required]),
      round: new FormControl(null, [Validators.required]),
    })

    this.fileForm = this.fb.group({
      files: [null, [Validators.required]]
    })

    this.getOldReports();
  }

  getOldReports() {
    this.retrospectiveReportService.getOldReports().subscribe(res => {
      console.log('res => ', res);
      this.oldReports = res.reverse();
      this.loading = true;
    })
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template, { class: 'modal-md' });
  }

  submitReport(value) {
    console.log('value => ', value);
    console.log('invalid => ', this.fileForm.controls.files.errors);
    console.log('reportForm.controls.name.errors => ', this.reportForm.controls.name.errors);
    if (this.reportForm.invalid) {
      this.submitted = true;
      console.log("invalid");
      return;
    } else {
      console.log("Report Value: ", value);
      this.spinner.show();
      this.retrospectiveReportService.importOldReport(value, this.userid, this.fileForm.value.files).subscribe(res => {
        console.log('insert res => ', res);
        this.reportForm.reset();
        this.loading = false;
        this.getOldReports()
        this.modalRef.hide();
        this._NotofyService.onSuccess("เพื่มข้อมูล",)
        this.spinner.hide()

        // this.notificationService.addNotification(1, 1, this.userid, 14, res.importId)
        //   .subscribe(response => {
        //     console.log("Noti: ", response);
        //   });
      })
    }
  }

  uploadFile(event) {
    const file = (event.target as HTMLInputElement).files;
    this.fileForm.patchValue({
      files: file
    })
    this.fileForm.get('files').updateValueAndValidity()
  }

  closeModal() {
    this.modalRef.hide()
    this.submitted = false
    this.reportForm.reset()
    this.editForm.reset()
    this.fileForm.reset()
  }

  openFile(fileName) {
    window.open(this.url + "Uploads/" + fileName[0].name);
  }

  openEditModal(template: TemplateRef<any>, oldReportId) {
    this.modalRef = this.modalService.show(template, { class: 'modal-md' });
    this.oldReportId = oldReportId
    console.log('id ja => ', this.oldReportId);

    this.retrospectiveReportService.getOldReportById(this.oldReportId).subscribe(res => {
      console.log('res edit => ', res);
      this.editForm.patchValue({
        year: res.year,
        centralPolicyType: res.centralPolicyType,
        name: res.name,
        reportType: res.reportType,
        round: res.round,
      })
      this.fileName = res.oldReportFiles[0].description
    })


  }

  editOldReport(value) {
    this.retrospectiveReportService.editOldReport(value, this.fileForm.value.files, this.oldReportId).subscribe(res => {
      this.editForm.reset();
      this.loading = false;
      this.getOldReports()
      this.modalRef.hide();
      this._NotofyService.onSuccess("แก้ไขข้อมูล",)
      this.spinner.hide()
    })
  }

  openDeleteModal(template: TemplateRef<any>, oldReportId) {
    this.modalRef = this.modalService.show(template, { class: 'modal-md' });
    this.oldReportId = oldReportId
  }

  deleteOldReport() {
    this.retrospectiveReportService.deleteOldReport(this.oldReportId).subscribe(res => {
      console.log('delete res => ', res);

      this.loading = false;
      this.getOldReports()
      this.modalRef.hide();
      this._NotofyService.onSuccess("ลบข้อมูล",)
      this.spinner.hide()
    })
  }
}


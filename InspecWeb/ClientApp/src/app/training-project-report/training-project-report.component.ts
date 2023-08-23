import { Component, Inject, OnInit, TemplateRef } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { AuthorizeService } from 'src/api-authorization-new/authorize.service';
import { NotofyService } from '../services/notofy.service';
import { UserService } from '../services/user.service';
import { TrainingProjectReportService } from '../services/training-project-report.service';
import * as _ from 'lodash';

@Component({
  selector: 'app-training-project-report',
  templateUrl: './training-project-report.component.html',
  styleUrls: ['./training-project-report.component.css']
})
export class TrainingProjectReportComponent implements OnInit {
  modalRef: BsModalRef;
  trainingReportForm: FormGroup;
  editForm: FormGroup;
  loading = false;
  dtOptions: any = {};
  submitted: any;
  fileTrainingProjectReportForm: FormGroup;
  fileModelDirectoryForm: FormGroup;
  filePracticeGuideForm: FormGroup;
  fileProjectDocumentForm: FormGroup;
  fileTrainingDetailForm: FormGroup;
  userid: string;
  oldReports: any = [];
  url: any;
  trainingProjectReportId: any;
  trainingProjectReports: any;
  allFile: any = [];
  fileId: any;
  fileType: any;
  deleteFileStatus: any = 0;
  test:any = []
  constructor(
    private modalService: BsModalService,
    private spinner: NgxSpinnerService,
    private fb: FormBuilder,
    private _NotofyService: NotofyService,
    private trainingProjectReportService: TrainingProjectReportService,
    private authorize: AuthorizeService,
    private userService: UserService,
    @Inject('BASE_URL') baseUrl: string,
  ) {
    this.url = baseUrl;
  }

  ngOnInit() {
    this.spinner.show();
    this.dtOptions = {
      pagingType: 'full_numbers',
      columnDefs: [
        {
          targets: [5],
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
    this.trainingReportForm = this.fb.group({
      year: new FormControl(null, [Validators.required]),
      courseType: new FormControl(null, [Validators.required]),
    })

    this.authorize.getUser().subscribe(result => {
      console.log("res user: ", result);
      this.userid = result.sub
    })

    this.editForm = this.fb.group({
      year: new FormControl(null, [Validators.required]),
      courseType: new FormControl(null, [Validators.required]),
    })

    this.fileTrainingProjectReportForm = this.fb.group({
      files: [null]
    })
    this.fileModelDirectoryForm = this.fb.group({
      files: [null]
    })
    this.filePracticeGuideForm = this.fb.group({
      files: [null]
    })
    this.fileProjectDocumentForm = this.fb.group({
      files: [null]
    })
    this.fileTrainingDetailForm = this.fb.group({
      files: [null]
    })
    this.getTrainingProjectReportService();
  }

  getTrainingProjectReportService() {
    this.trainingProjectReportService.getTrainingProjectReports().subscribe(res => {
      console.log('res get => ', res);

      this.trainingProjectReports = res.reverse();
      this.loading = true;
      this.spinner.hide();
    })
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template, { class: 'modal-md' });
  }

  submitReport(value) {
    console.log('value => ', value);
    // console.log('report Form => ', this.trainingReportForm);
    console.log('fileProjectDocumentForm Form => ', this.fileProjectDocumentForm);
    console.log('fileTrainingDetailForm Form => ', this.fileTrainingDetailForm);

    if (this.trainingReportForm.invalid) {
      this.submitted = true;
      console.log("invalid");
      return;
    } else {
      console.log("Report Value: ", value);
      this.spinner.show();
      this.trainingProjectReportService.addTrainingProjectReport(value, this.userid,
        this.fileTrainingProjectReportForm.value.files,
        this.fileProjectDocumentForm.value.files,
        this.fileTrainingDetailForm.value.files,
        this.fileModelDirectoryForm.value.files,
        this.filePracticeGuideForm.value.files,
      ).subscribe(res => {
        console.log('insert res => ', res);
        this.trainingReportForm.reset();
        this.fileTrainingProjectReportForm.reset()
        this.fileModelDirectoryForm.reset()
        this.filePracticeGuideForm.reset()
        this.fileProjectDocumentForm.reset()
        this.fileTrainingDetailForm.reset()
        this.loading = false;
        this.getTrainingProjectReportService();
        this.modalRef.hide();
        this._NotofyService.onSuccess("สำเร็จ", "เพิ่มข้อมูล")
        this.spinner.hide()

        // this.notificationService.addNotification(1, 1, this.userid, 14, res.importId)
        //   .subscribe(response => {
        //     console.log("Noti: ", response);
        //   });
      })
    }
  }

  uploadFile(event, fileType) {
    const file = (event.target as HTMLInputElement).files;
    if (fileType == "trainingProjectReport") {
      this.fileTrainingProjectReportForm.patchValue({
        files: file
      })
      this.fileTrainingProjectReportForm.get('files').updateValueAndValidity()
    } else if (fileType == "modelDirectory") {
      this.fileModelDirectoryForm.patchValue({
        files: file
      })
      this.fileModelDirectoryForm.get('files').updateValueAndValidity()
    } else if (fileType == "practiceGuide") {
      this.filePracticeGuideForm.patchValue({
        files: file
      })
      this.filePracticeGuideForm.get('files').updateValueAndValidity()

    } else if (fileType == "projectDocument") {
      this.fileProjectDocumentForm.patchValue({
        files: file
      })
      this.fileProjectDocumentForm.get('files').updateValueAndValidity()

    } else if (fileType == "trainingDetail") {
      this.fileTrainingDetailForm.patchValue({
        files: file
      })
      this.fileTrainingDetailForm.get('files').updateValueAndValidity()
    }
  }

  closeModal() {
    this.modalRef.hide()
    this.submitted = false
    this.trainingReportForm.reset()
    this.editForm.reset()
    this.fileTrainingProjectReportForm.reset()
    this.fileModelDirectoryForm.reset()
    this.filePracticeGuideForm.reset()
    this.fileProjectDocumentForm.reset()
    this.fileTrainingDetailForm.reset()
  }

  openFile(fileName) {
    window.open(this.url + "Uploads/" + fileName);
  }

  openEditModal(template: TemplateRef<any>, trainingProjectReportId) {
    this.modalRef = this.modalService.show(template, { class: 'modal-md' });
    this.trainingProjectReportId = trainingProjectReportId
    console.log('id ja => ', this.trainingProjectReportId);

    this.trainingProjectReportService.getTrainingProjectReportById(this.trainingProjectReportId).subscribe(res => {
      console.log('res edit => ', res);
      this.editForm.patchValue({
        year: res.year,
        courseType: res.courseType,
      })
      this.allFile = res;
    })


  }

  editTrainingProjectReport(value) {
    this.spinner.show();
    this.trainingProjectReportService.editTrainingProjectReport(value, this.userid,
      this.fileTrainingProjectReportForm.value.files,
      this.fileProjectDocumentForm.value.files,
      this.fileTrainingDetailForm.value.files,
      this.fileModelDirectoryForm.value.files,
      this.filePracticeGuideForm.value.files,
      this.trainingProjectReportId
    ).subscribe(res => {
      this.editForm.reset();
      this.fileTrainingProjectReportForm.reset()
      this.fileModelDirectoryForm.reset()
      this.filePracticeGuideForm.reset()
      this.fileProjectDocumentForm.reset()
      this.fileTrainingDetailForm.reset()
      this.loading = false;
      this.getTrainingProjectReportService()
      this.modalRef.hide();
      this._NotofyService.onSuccess("สำเร็จ", "แก้ไขข้อมูล")
      this.spinner.hide()
    })
  }

  openDeleteModal(template: TemplateRef<any>, trainingProjectReportId) {
    this.modalRef = this.modalService.show(template, { class: 'modal-md' });
    this.trainingProjectReportId = trainingProjectReportId
  }

  confirmDeleteFile(item, fileType) {
    if (!_.isNil(item.confirmDelete)) {
      item.confirmDelete = !item.confirmDelete;
    } else {
      item.confirmDelete = true;
    }
  }

  deleteFile(fileId, fileType) {
    this.trainingProjectReportService.deleteTrainingProjectReportFile(fileId, fileType).subscribe(res => {
      this._NotofyService.onSuccess("สำเร็จ", "ลบเอกสาร")
      this.trainingProjectReportService.getTrainingProjectReportById(this.trainingProjectReportId).subscribe(res => {
        this.allFile = res;
      })
    })
  }

  openDetailModal(template: TemplateRef<any>, trainingProjectReportId) {
    this.modalRef = this.modalService.show(template, { class: 'modal-lg' });
    this.trainingProjectReportService.getTrainingProjectReportById(trainingProjectReportId).subscribe(res => {
      this.allFile = res;
    })
  }

  deleteTrainingProjectReport() {
    this.spinner.show();
    this.trainingProjectReportService.deleteTrainingProjectReport(this.trainingProjectReportId).subscribe(res => {
      console.log('delete res => ', res);

      this.loading = false;
      this.getTrainingProjectReportService()
      this.modalRef.hide();
      this._NotofyService.onSuccess("สำเร็จ", "ลบข้อมูล")
      this.spinner.hide()
    })
  }
}

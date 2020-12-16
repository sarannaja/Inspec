import { Component, OnInit, TemplateRef, Inject } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { InspectorService } from '../services/inspector.service';
import { UserService } from '../services/user.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { RegionService } from '../services/region.service';
import { ProvinceService } from '../services/province.service';

@Component({
  selector: 'app-inspector',
  templateUrl: './inspector.component.html',
  styleUrls: ['./inspector.component.css']
})
export class InspectorComponent implements OnInit {

  resultInspector: any = [];
  selectdataregion:any=[];
  selectdataprovince:any=[];
  region:any;
  province:any;
  delid: any;
  name: any;
  loading = false;
  phonenumber: any;
  regionId: any;
  createBy: any;
  modalRef: BsModalRef;
  Form: FormGroup;
  imgprofileUrl :any;
  dtOptions: any = {};
  constructor(
    private modalService: BsModalService, 
    private fb: FormBuilder, 
    private inspectorservice: InspectorService,
    public share: InspectorService, 
    private userService: UserService,
    private spinner: NgxSpinnerService,
    private regionService: RegionService,
    private provinceService: ProvinceService,
    @Inject('BASE_URL') baseUrl: string
    ) 
    { 
      this.imgprofileUrl = baseUrl + '/imgprofile';
    }

    ngOnInit() {
      this.dtOptions = {
        pagingType: 'full_numbers',
        "language": {
          "lengthMenu": "แสดง  _MENU_  รายการ",
          "search": "ค้นหา:",
          "info": "แสดง _START_ ถึง _END_ จาก _TOTAL_ แถว",
          "infoEmpty": "แสดง 0 ของ 0 รายการ",
          "zeroRecords": "ไม่พบข้อมูล",
          "paginate": {
            "first": "หน้าแรก",
            "last": "หน้าสุดท้าย",
            "next": "ต่อไป",
            "previous": "ย้อนกลับ"
          },
        },
        dom: 'Bfrtip',
        buttons: [
          { extend: 'excel', text: 'Excel', className: 'btn btn-success glyphicon glyphicon-list-alt' },
          { extend: 'pdf', text: 'Pdf', className: 'btn btn-primary glyphicon glyphicon-file' },
          { extend: 'print', text: 'Print', className: 'btn btn-primary glyphicon glyphicon-print' }
        ]
  
      };
    this.getDataRegionsAndProvince()
    
    this.Form = this.fb.group({
      "name": new FormControl(null, [Validators.required]),
      "phonenumber": new FormControl(null, [Validators.required]),
      "regionId": new FormControl(null, [Validators.required]),
      "createBy": new FormControl(null, [Validators.required]),
    })
  }

  getdata(regionid,provinceid){
    this.spinner.show();
    this.userService.getuserinspectordata(regionid,provinceid).subscribe(result=>{
      this.resultInspector = result
      this.loading = true;
      this.spinner.hide();
    })
  }

  getDataRegionsAndProvince() {
    this.regionService.getregiondataforuser().subscribe(res => {

      this.selectdataregion = res.importFiscalYearRelations.filter(
        (thing, i, arr) => arr.findIndex(t => t.regionId === thing.regionId) === i
      ).map((item, index) => {
        return {
          value: item.region.id,
          label: item.region.name
        }
      });
       this.region = 0;
    })


    this.provinceService.getprovincedata2()
     .subscribe(result => {
       this.selectdataprovince = result;
       this.province = 0;
      
     });
     this.getdata(this.region,this.province);
  }

  Changeregion(event){
    this.region = event.target.value;
    this.province = 0;
    this.loading = false;
    this.getdata(event.target.value,this.province);
  }

  Changeprovince(event){
    this.province = event.target.value;
    this.loading = false;
    this.getdata( this.region,event.target.value);
  }


  openModal(template: TemplateRef<any>, modalType:string = 'edit') {
    modalType != 'edit' ? this.Form.reset() : null;
    this.modalRef = this.modalService.show(template);
    
  }

  onEdit(modaleditInspector,item){
    this.openModal(modaleditInspector)
    this.delid = item.id;
    this.name =item.name;
    this.phonenumber =item.phonenumber
    this.regionId =item.regionId
    this.createBy =item.createBy
    // console.log(this.delid);
    // console.log(this.name);
    // console.log(this.phonenumber);
    // console.log(this.regionId);
    // console.log(this.createBy);
    this.Form.patchValue(item)

  }
  storeInspector(value) {
    // alert(JSON.stringify(value));
    this.inspectorservice.addInspector(value).subscribe(response => {
     // console.log(value);
      this.Form.reset()
      this.modalRef.hide()
      this.inspectorservice.getinspectordata().subscribe(result => {
        this.resultInspector = result
       // console.log(this.resultInspector);
      })
    })
  }
  deleteInspecor(value) {
    this.inspectorservice.deleteInspector(value).subscribe(response => {
     // console.log(value);
      this.modalRef.hide()
      this.inspectorservice.getinspectordata().subscribe(result => {
        this.resultInspector = result
      //  console.log(this.resultInspector);
      })
    })
  }
  editInspector(value,delid) {
    // console.clear();
    // console.log(value);
    this.inspectorservice.editInspector(value,delid).subscribe(response => {
      this.Form.reset()
      this.modalRef.hide()
      this.inspectorservice.getinspectordata().subscribe(result => {
        this.resultInspector = result
       
      })
    })
  }
  excel(){
    window.location.href = '/api/user/excelinspector';
  }
}

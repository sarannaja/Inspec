import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { SubjectService } from '../../services/subject.service';
import { AuthorizeService } from 'src/api-authorization-new/authorize.service';
import { InspectionplanService } from '../../services/inspectionplan.service';
import { RegionService } from '../../services/region.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-summary-central-policy-detail',
  templateUrl: './summary-central-policy-detail.html',
  styleUrls: ['./summary-central-policy-detail.css']
})
export class SummaryCentralPolicyDetailComponent implements OnInit {

  // ==========================================
  // Zone Tabs
  // ==========================================

  zones = [];

  selectedZone = 1;


  // ==========================================
  // DataTable
  // ==========================================

  dtOptions: any = {};


  // ==========================================
  // Loading
  // ==========================================

  loading = false;


  // ==========================================
  // Data
  // ==========================================

  resultsubjectevent: any[] = [];


  // ==========================================
  // User
  // ==========================================

  userid: string;
  resultuser: any[];

  // ==========================================
  // Constructor
  // ==========================================
  regionId
  centralPolicyId

  constructor(
    private spinner: NgxSpinnerService,
    private subjectservice: SubjectService,
    private authorize: AuthorizeService,
    private inspectionplanservice: InspectionplanService,
    private router: Router,
    private regionService: RegionService,
    private userService: UserService,
    private activatedRoute: ActivatedRoute
  ) {
    this.centralPolicyId = activatedRoute.snapshot.paramMap.get('centralpolicyid');
    this.regionId = activatedRoute.snapshot.paramMap.get('regionid');
    console.log('centralPolicyId =>', this.centralPolicyId);
    console.log('regionId =>', this.regionId);
  }


  // ==========================================
  // On Init
  // ==========================================

  ngOnInit() {

    this.spinner.show();

    this.authorize.getUser()
      .subscribe(result => {


        this.userid = result.sub
        this.userService.getuserfirstdata(this.userid)
          .subscribe(result => {
            console.log('user data =>', result);
            // this.resultuser = result.userRegion
            this.zones = result[0].userRegion.map(region => ({
              id: region.regionId,
              name: region.region.name
            }));

            this.selectedZone = this.zones.length > 0 ? this.zones[0].id : null;
          })

        // this.userid = result.sub;

        // console.log('user data =>', result);

      });


    this.dtOptions = {

      pagingType: 'full_numbers',

      ordering: true,

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

      }

    };

    this.getRegionData();
    this.getSubjectevent();
  }
  region: any[] = [];

  getRegionData() {
    this.regionService.getregiondataforuser().subscribe(res => {

      console.log('region data =>', res);
      console.log('is array =>', Array.isArray(res));

      this.region = res.importFiscalYearRelations;
    });
  }
  // ==========================================
  // Get Subject Event
  // ==========================================
  getSubjectevent() {

    this.subjectservice
      .subjectgroupbycentralpolicyandregion(this.regionId, this.centralPolicyId)
      .subscribe(result => {

        console.log("SUBJECTEVENT ==> ", result);

        this.resultsubjectevent = result;

        this.loading = true;
        this.spinner.hide();

      });

  }


  // ==========================================
  // Subject Event Detail
  // ==========================================

    Subjectevent(id, centralPolicyId, provinceId) {
    this.inspectionplanservice.getcentralpolicyprovinceid(centralPolicyId, provinceId).subscribe(result => {
      // this.centralpolicyprovinceid = result
      this.router.navigate(['/subjectevent/detail/' + result, { subjectgroupid: id, }])
    })
  }


  // ==========================================
  // Select Zone
  // ==========================================

  selectZone(zoneId: number): void {

    this.selectedZone = zoneId;

  }


}
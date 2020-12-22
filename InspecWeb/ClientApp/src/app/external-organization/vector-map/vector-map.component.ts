import { Component, OnInit, Inject } from '@angular/core';
declare var jQuery: any;
import mapdata from "../../../assets/jvectormap/mappass"
import code_color from "../../../assets/jvectormap/code_color"
import { ExternalOrganizationService } from 'src/app/services/external-organization.service';
import { Province, ProvinceFiscalYears, ProvinceFiscalYear, FiscalYears } from '../models/otps';
import { UserService } from 'src/app/services/user.service';
import * as _ from 'lodash'

@Component({
  selector: 'app-vector-map',
  templateUrl: './vector-map.component.html',
  styleUrls: ['./vector-map.component.css']
})
export class VectorMapComponent implements OnInit {

  title = 'jvectormapTest';
  resultInspector: any = [];
  resultdistrictofficer: any = [];
  resultregionalagency: any = [];
  resultpublicsectoradvisor: any = [];
  ministryGroup: any[] = []
  resultall: any = [];
  imgprofileUrl: any;
  provinceName: any;
  show = 0;
  code_color = code_color;
  mapdata: Array<Province> = []
  dtOptions: any = {};
  provincesData: Array<ProvinceFiscalYears> = []
  constructor(
    private extranal: ExternalOrganizationService,
    private userService: UserService,
    @Inject('BASE_URL') baseUrl: string
  ) {
    this.imgprofileUrl = baseUrl + '/imgprofile';
  }

  ngOnInit() {
    this.getData()
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
      }
    };

  }

  getData() {
    this.extranal.getProvinces().pipe()
      .subscribe(result => {
        this.mapdata = result
        //console.log(this.mapdata);

      })

  }
  clickProvince(province: Province = { id: 1, name: "กรุงเทพมหานคร", iso: "TH-01" }) {

    ////<!-- ของบักปามทำไว้ -->
    // this.extranal.getProvince(province.id)
    //   .subscribe(result => {

    //     this.provincesData = result.fiscalYears
    //   })
    ////<!-- END ของบักปามทำไว้ -->

    this.show = 1;
    this.provinceName = province.name;

    // this.userService.getuserinspectorformapdata(province.name).subscribe(result=>{
    //   this.resultInspector = result
    // })

    // this.userService.getuserdistrictofficerformapdata(province.name)
    // .subscribe(result => {
    //   this.resultdistrictofficer = result;
    // })

    // this.userService.getuserregionalagencyformapdata(province.name)
    // .subscribe(result => {
    //   this.resultregionalagency = result;
    // })

    // this.userService.getuserpublicsectoradvisorformapdata(province.name)
    // .subscribe(result => {
    //   this.resultpublicsectoradvisor = result;
    // })

    this.userService.getusermapdata(province.name)
      .subscribe(result => {
        this.resultall = result;
        console.log(result, 'result');
        this.ministryGroup = _.chain(result)
          .groupBy("ministryId")
          .map((value, key) => ({ ministrie: value[0].ministries, ministries: value }))
          .value()
        console.log(this.ministryGroup);

      })

  }

  ngAfterViewInit() {
    const self = this;
    (function ($) {
      // self = this; 
      $(document).ready(function () {

        //console.log(self);

        let mapoption2 = {
          map: "th_mill",
          zoomButtons: false,
          zoomOnScroll: false,
          backgroundColor: "none",
          markerStyle: {
            initial: {
              fill: "#ff0000",
              stroke: "#000000"
            }
          },
          labels: {
            regions: {
              render: function (code, region) {
                var doNotShow = [];
                // var doNotShow = ['TH-30'];

                if (doNotShow.indexOf(code) === -1) {
                  // return code.split('-')[1];
                  return mapdata[code].name
                  // return region
                }
              },
              offsets: function (code, region) {
                console.log(code,'code ');
                
                // return region
                return {
                  'TH-50': [10, -70],
                  'TH-63': [20, -50],
                
                }
                [code];
              }
            }
          },
          regionStyle: {
            initial: {
              fill: "#999999",
              "fill-opacity": 1,
              stroke: "#000000",
              "stroke-width": 1,
              "stroke-opacity": 1
            },
            hover: {
              cursor: "pointer"
            }
          },
          series: {
            regions: [
              {
                normalizeFunction: "polynomial",
                attribute: "fill",
                values: self.code_color
              }
            ]
          },
          onRegionClick: function (element, code, region) {
            self.clickProvince(self.mapdata.filter((item) => {
              // console.log(item , code);
              return item.iso == code
            })[0]
            )
          },
          // onRegionTipShow: function(e, el, code){
          //   // el.html(el.html()+' (GDP - '+e[code]+')');
          // }
        }
        $("#thai-map").vectorMap(mapoption2);
      })
    })(jQuery);

  }

}

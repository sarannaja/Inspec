import { Component, OnInit } from '@angular/core';
declare var jQuery: any;
import mapdata from "../../../assets/jvectormap/mappass"
import code_color from "../../../assets/jvectormap/code_color"
import { ExternalOrganizationService } from 'src/app/services/external-organization.service';
import { Province, ProvinceFiscalYears, ProvinceFiscalYear, FiscalYears } from 'src/app/models/otps';
@Component({
  selector: 'app-vector-map',
  templateUrl: './vector-map.component.html',
  styleUrls: ['./vector-map.component.css']
})
export class VectorMapComponent implements OnInit {

  title = 'jvectormapTest';
  code_color = code_color
  mapdata: Array<Province>
  provincesData: Array<ProvinceFiscalYears> = []
  constructor(private extranal: ExternalOrganizationService) {

  }

  ngOnInit() {
    this.getData()
    console.log(mapdata, code_color);

  }

  getData() {
    this.extranal.getProvinces().pipe()
      .subscribe(result => {
        this.mapdata = result
        console.log(this.mapdata);

      })

  }
  clickProvince(province: Province) {
    console.log('click regoin', province);
    this.extranal.getProvince(province.id)
      .subscribe(result => {
       
        this.provincesData = result.fiscalYears
        console.log(this.provincesData,result.fiscalYears);

      })
    //  ("You hover " + province);
  }


  ngAfterViewInit() {
    const self = this;
    (function ($) {
      // self = this; 
      $(document).ready(function () {

        console.log(self);

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
          //   // el.html(el.html()+' (GDP - '+gdpData[code]+')');
          // }
        }
        $("#thai-map").vectorMap(mapoption2);
      })
    })(jQuery);

  }

}

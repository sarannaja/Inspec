import { Component, OnInit } from '@angular/core';
import { Province, ProvinceService } from 'src/app/services/province.service';

@Component({
  selector: 'app-select-component',
  templateUrl: './select-component.component.html',
  styleUrls: ['./select-component.component.css']
})
export class SelectComponentComponent implements OnInit {
  province: Province[] = [];
  selectedProvince = [];
  constructor(private provinceservice: ProvinceService) { }

  ngOnInit() {
    this.getDataProvince()
  }
  getDataProvince() {
    this.provinceservice.getprovincedata()
      .subscribe(result => {
        this.province = result.map(result => {
          console.log(
            result.name
          );
          var region = this.provinceservice.getRegionMock().filter(
            (thing, i, arr) => arr.findIndex(t => t.name === result.name) === i
          )[0].region
          console.log(
            region
          );


          return { ...result, region: region }
        })
        console.log(this.province);


      })
    console.log(this.provinceservice.getRegionMock());
  }
}

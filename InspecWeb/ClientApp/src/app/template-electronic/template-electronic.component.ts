import { Component, OnInit, Inject } from '@angular/core';
import { WordService } from '../services/word.service';
import { ActivatedRoute } from '@angular/router';
import { Chart } from 'chart.js';

@Component({
  selector: 'app-template-electronic',
  templateUrl: './template-electronic.component.html',
  styleUrls: ['./template-electronic.component.css']
})
export class TemplateElectronicComponent implements OnInit {

  loading = false;



  role_id

  provinId
  elecId
  ThemeImage: any;
  url = ""
  Data = [{ id: 1, name: "01.jpg" }, { id: 2, name: "02.jpg" }, { id: 3, name: "03.jpg" }, { id: 4, name: "04.jpg" }, { id: 5, name: "05.jpg" }
    , { id: 6, name: "06.jpg" }, { id: 7, name: "07.jpg" }, { id: 8, name: "08.jpg" }, { id: 9, name: "09.jpg" }, { id: 10, name: "10.jpg" }]




  constructor(@Inject('BASE_URL') baseUrl: string,
    private wordService: WordService,
    private activatedRoute: ActivatedRoute, ) {
    this.elecId = activatedRoute.snapshot.paramMap.get('id')
    // this.elecId = activatedRoute.snapshot.paramMap.get('electronicBookId')
    this.url = baseUrl
  }

  ngOnInit() {

  }


  getImagePath(filePath) {
    // console.log("FILEPATH: ", filePath);
    // this.ThemeImage = this.url + "Theme/" + filePath;
    // console.log("IMG: ", this.ThemeImage);
    // return this.ThemeImage;
  }


  exportToWord(id) {
    // alert(id)

    // alert(this.elecId)
    this.wordService.exportWord(id, this.elecId).subscribe(results => {
      // alert(results)
      console.log("res: ", results);
      // alert(id)
      // window.location.href=this.url + results.data );
      window.open(this.url + "Uploads/" + results.data);
      // this.loading = true;
    })


  }

}

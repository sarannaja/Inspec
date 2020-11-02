import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { split } from 'lodash';
import { TrainingService } from 'src/app/services/training.service';

@Component({
  selector: 'app-name-plate-preview',
  templateUrl: './name-plate-preview.component.html',
  styleUrls: ['./name-plate-preview.component.css']
})
export class NamePlatePreviewComponent implements OnInit {
  people: any = [];
  printData: any = [];
  test: any = [];
  loading = false;
  constructor(
    private activatedRoute: ActivatedRoute,
    private trainingservice: TrainingService,
    private router: Router,
  ) {
    this.people = activatedRoute.snapshot.paramMap.get('selectedPeople');
  }

  ngOnInit() {
    console.log("people : ", this.people);
    this.getPrintData();
  }

  getPrintData() {
    var test = this.people.split(",");
    console.log("test: ", test);

    this.trainingservice.printNamePlate(test).subscribe(res => {
      console.log("PrintData: ", res);
      this.printData = res.filter(res => res);
      this.loading = true;
      // await this.printNamePlate();
    });

    // for (let index = 0; index < this.people.length; index++) {
    //   const element = this.people[index];
    //   this.trainingservice.printNamePlatebyPalm(element).subscribe(res => {
    //     this.printData.push(res)
    //   })
    // }
    // this.loading = true;


    console.log("printData => ", this.printData);

  }

  exportHTML() {
    var header = "<html xmlns:o='urn:schemas-microsoft-com:office:office' " +
      "xmlns:w='urn:schemas-microsoft-com:office:word' " +
      "xmlns='https://www.w3.org/TR/REC-html40'>" +
      "<head><meta charset='utf-8'><title>Export HTML to Word Document with JavaScript</title></head><body>";
    var footer = "</body></html>";
    var sourceHTML = header + document.getElementById("source-html").innerHTML + footer;

    var source = 'data:application/vnd.ms-word;charset=utf-8,' + encodeURIComponent(sourceHTML);
    var fileDownload = document.createElement("a");
    document.body.appendChild(fileDownload);
    fileDownload.href = source;
    fileDownload.download = 'document.doc';
    fileDownload.click();
    document.body.removeChild(fileDownload);
  }

  print(): void {
    // let printContents, popupWin;
    // printContents = document.getElementById('print-section').innerHTML;
    // popupWin = window.open('', '_blank', 'top=0,left=0,height=auto,width=100%');
    // popupWin.document.open();
    // popupWin.document.write(`
    //   <html>
    //     <head>
    //       <title>Print tab</title>
    //       <style>
    //       //........Customized style.......
    //       </style>
    //     </head>
    // <body onload="window.print();window.close()">${printContents}</body>
    //   </html>`
    // );
    // popupWin.document.close();

    // const printContent = document.getElementById("print-section");
    // const WindowPrt = window.open('', '', 'left=0,top=0,width=900,height=900,toolbar=0,scrollbars=0,status=0');
    // WindowPrt.document.write(printContent.innerHTML);
    // WindowPrt.document.close();
    // WindowPrt.focus();
    // WindowPrt.print();
    // WindowPrt.close();

    window.print();

  }

  gotoBack() {
    window.history.back();
  }

}

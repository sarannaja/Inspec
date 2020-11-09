import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';

@Component({
  selector: 'app-iframe',
  templateUrl: './iframe.component.html',
  styleUrls: ['./iframe.component.css']
})
export class IframeComponent implements OnInit {
  loading: boolean = false
  iframe: SafeResourceUrl;
  forcopy: string = ""
  clicked: boolean = false
  mock: Mock[] = [
    {
      url: `${location.origin}/inspectionplanevent/all/noauth`,
      name: 'กำหนดการตรวจราชการ',

    },
    {
      url: `${location.origin}/allreportiframe`,
      name: 'ทะเบียนรายงานผลการตรวจราชการ',
    },
    {
      url: `${location.origin}/vector/thaimaps`,
      name: 'เขตตรวจแต่ล่ะจังหวัด',
    },
  ]
  constructor(public sanitizer: DomSanitizer, private http: HttpClient) { }

  ngOnInit() {

    setTimeout(() => { this.loading = true }, 500)
    // this.iframe = this.mock[0].url
    this.iframe = this.sanitizer.bypassSecurityTrustResourceUrl(this.mock[0].url);
    this.forcopy = `<iframe width="100%" height="600px" src="${this.mock[0].url}" frameborder="0"></iframe>`
    console.log(location.origin);

  }
  safeHTML(content) {
    return this.sanitizer.bypassSecurityTrustHtml(content);
  }
  show(index) {
    this.iframe = this.sanitizer.bypassSecurityTrustResourceUrl(this.mock[index].url);
    this.forcopy = `<iframe width="100%" height="600px" src="${this.mock[index].url}" frameborder="0"></iframe>`
  }

  /* To copy Text from Textbox */
  copyMessage(val: string) {
    this.clicked = true
    const selBox = document.createElement('textarea');
    selBox.style.position = 'fixed';
    selBox.style.left = '0';
    selBox.style.top = '0';
    selBox.style.opacity = '0';
    selBox.value = val;
    document.body.appendChild(selBox);
    selBox.focus();
    selBox.select();
    document.execCommand('copy');
    document.body.removeChild(selBox);
    setTimeout(() => { this.clicked = false }, 1500)
  }
}

interface Mock {
  url: string
  name: string
}
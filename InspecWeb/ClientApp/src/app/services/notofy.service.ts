import { Injectable } from '@angular/core';

import { SnotifyService, SnotifyPosition, SnotifyToastConfig } from 'ng-snotify';

const MSG_SAVE_SUCCESS = 'สำเร็จแล้ว';
const MSG_SAVE_FALSE = 'ไม่สำเร็จ';
const MSG_SAVE_ERROR = 'ผิดพลาด';


@Injectable({
  providedIn: 'root'
})
export class NotofyService {

  style = 'material';
  title = 'Snotify title!';
  body = 'Lorem ipsum dolor sit amet!';
  timeout = 5000;
  position: SnotifyPosition = SnotifyPosition.rightBottom;
  progressBar = true;
  closeClick = true;
  newTop = true;
  filterDuplicates = false;
  backdrop = -1;
  dockMax = 8;
  blockMax = 6;
  pauseHover = true;
  titleMaxLength = 15;
  bodyMaxLength = 80;

  constructor(private snotifyService: SnotifyService) { }

  getConfig(): SnotifyToastConfig {
    this.snotifyService.setDefaults({
      global: {
        newOnTop: this.newTop,
        maxAtPosition: this.blockMax,
        maxOnScreen: this.dockMax,
      }
    });
    return {
      bodyMaxLength: this.bodyMaxLength,
      titleMaxLength: this.titleMaxLength,
      backdrop: this.backdrop,
      position: this.position,
      timeout: this.timeout,
      // showProgressBar: this.progressBar,
      closeOnClick: this.closeClick,
      pauseOnHover: this.pauseHover
    };
  }

  getConfigError(): SnotifyToastConfig {
    this.snotifyService.setDefaults({
      global: {
        newOnTop: this.newTop,
        maxAtPosition: this.blockMax,
        maxOnScreen: this.dockMax,
      }
    });
    return {
      bodyMaxLength: this.bodyMaxLength,
      titleMaxLength: this.titleMaxLength,
      backdrop: this.backdrop,
      position: this.position,
      // timeout: this.timeout,
      showProgressBar: this.progressBar,
      closeOnClick: this.closeClick,
      pauseOnHover: this.pauseHover,
    };
  }

  onSimple(title: string, body: string) {
    console.log("onSimple");

    // const icon = `assets/custom-svg.svg`;
    const icon = `https://placehold.it/48x100`;

    this.snotifyService.simple(body, title, {
      ...this.getConfig(),
      // icon: icon
    });
  }

  onSuccess(title: string = '', body: string = MSG_SAVE_SUCCESS) {
    this.snotifyService.success(title, body, this.getConfig());
  }

  onFalse(title: string = '', body: string = MSG_SAVE_FALSE) {
    this.snotifyService.error(body, title, this.getConfig());
  }

  onInfo(title: string, body: string) {
    this.snotifyService.info(body, title, this.getConfig());
  }

  onError(title: string = '', body: string = MSG_SAVE_ERROR) {
    this.snotifyService.error('', title, this.getConfigError());
  }

  onWarning(title: string, body: string) {
    this.snotifyService.warning(body, title, this.getConfig());
  }

  onConfirm() {
    // return this.snotifyService.confirm('Example body content', 'Example title', {
    //   timeout: 5000,
    //   showProgressBar: true,
    //   closeOnClick: false,
    //   pauseOnHover: true,
    //   buttons: [
    //     {text: 'Yes', action: () => console.log('Clicked: Yes'), bold: false},
    //     {text: 'No', action: () => console.log('Clicked: No')},
    //     {text: 'Later', action: (toast) => {console.log('Clicked: Later'); service.remove(toast.id); } },
    //     {text: 'Close', action: (toast) => {console.log('Clicked: No'); service.remove(toast.id); }, bold: true},
    //   ]
    // });
  }


  onClear() {
    this.snotifyService.clear();
  }
}

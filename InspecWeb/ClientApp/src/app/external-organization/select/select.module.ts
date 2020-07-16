import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SelectComponentComponent } from './select-component/select-component.component';
import { NgSelectModule } from '@ng-select/ng-select';
import { ProvinceService } from 'src/app/services/province.service';
import { FormsModule } from '@angular/forms';



@NgModule({
  declarations: [SelectComponentComponent],
  imports: [
    CommonModule,
    NgSelectModule,
    FormsModule
  ],
  providers: [ProvinceService],
  // entryComponents:[SelectComponentComponent]
  exports: [SelectComponentComponent]
})
export class SelectSSSModule { }

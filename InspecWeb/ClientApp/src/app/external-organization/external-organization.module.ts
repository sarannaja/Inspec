import { NgModule } from '@angular/core';
import { CommonModule, CurrencyPipe, DatePipe } from '@angular/common';
import { GgcOpmComponent } from './ggc-opm/ggc-opm.component';
import { Opm1111Component } from './opm1111/opm1111.component';
import { OtpsComponent } from './otps/otps.component';
// import { ExternalOrganizationRoutingModule } from './external-organization-routing.module';
import { ExternalOrganizationService } from '../services/external-organization.service';
import { ExternalOrganizationRoutingModule } from './external-organization-routing.module';
import { MinisterTableComponent } from './otps/minister-table/minister-table.component';
import { DataTablesModule } from 'angular-datatables';
import { NgxSpinnerModule } from 'ngx-spinner';
import { CabinetTableComponent } from './otps/cabinet-table/cabinet-table.component';
import { MinisterModalComponent } from './otps/modals/minister-modal/minister-modal.component';
import { TitleModalComponent } from './otps/modals/title-modal/title-modal.component';
import { ModalModule } from 'ngx-bootstrap/modal';
import { GccOpmTableComponent } from './ggc-opm/gcc-opm-table/gcc-opm-table.component';
import { FormsModule } from '@angular/forms';
import { TestComponent } from './test/test.component';
import { JoaComponent } from './joa/joa.component';
import { Gcc1111TableComponent } from './opm1111/gcc1111-table/gcc1111-table.component';
import { ThaiDatePipe } from '../services/Pipe/thaidate.service';
import { VectorMapComponent } from './vector-map/vector-map.component';
import { CustomCurrencyPipe } from '../services/Pipe/customecurrency.service';
import { RegionComponent } from './otps/modals/region/region.component';
import { ChartsModule } from 'ng2-charts';
import { ProvinceOtpsComponent } from './otps/province-otps/province-otps.component';
import { ProvinceOtpsTableComponent } from './otps/province-otps-table/province-otps-table.component';
import { ThaiDatePipe2 } from '../services/Pipe/thaidate2.service';
import { OpmCaseDetailComponent } from './opm1111/modals/detail-modal/detail-modal.component';
import { OpmDetailTitleModalComponent } from './opm1111/modals/title-modal/title-modal.component';
import { MyDatePickerTHModule } from 'mydatepicker-th';
import { MyDateRangePickerModule } from 'mydaterangepicker';
import { VectorMapComponent2 } from './vector-map2/vector-map.component';



@NgModule({
  declarations: [
    GgcOpmComponent,
    Opm1111Component,
    OtpsComponent,
    MinisterTableComponent,
    CabinetTableComponent,
    MinisterModalComponent,
    TitleModalComponent,
    GccOpmTableComponent,
    TestComponent,
    JoaComponent,
    Gcc1111TableComponent,
    VectorMapComponent,
    CustomCurrencyPipe,
    RegionComponent,
    ProvinceOtpsComponent,
    ProvinceOtpsTableComponent,
    ThaiDatePipe2,
    OpmCaseDetailComponent,
    OpmDetailTitleModalComponent,
    VectorMapComponent2
  ],
  imports: [
    CommonModule,
    ExternalOrganizationRoutingModule,
    DataTablesModule,
    NgxSpinnerModule,
    ModalModule.forRoot(),
    FormsModule,
    ChartsModule,
    MyDatePickerTHModule,
    MyDateRangePickerModule
    // MyDateRangePickerTHModule
  ],
  exports:[MinisterModalComponent,CustomCurrencyPipe,ThaiDatePipe2,VectorMapComponent2],
  providers: [ExternalOrganizationService,CurrencyPipe,DatePipe],
  entryComponents: [MinisterModalComponent,RegionComponent,OpmCaseDetailComponent]
  // entryComponents:[GgcOpmComponent,Opm1111Component,OtpsComponent],
  // exports: [GgcOpmComponent, Opm1111Component, OtpsComponent, MinisterTableComponent],
})
export class ExternalOrganizationModule { }

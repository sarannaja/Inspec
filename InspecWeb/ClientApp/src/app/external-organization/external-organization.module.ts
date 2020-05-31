import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
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
    
  ],
  imports: [
    CommonModule,
    ExternalOrganizationRoutingModule,
    DataTablesModule,
    NgxSpinnerModule,
    ModalModule.forRoot(),
    FormsModule
  ],
  exports:[MinisterModalComponent],
  providers: [ExternalOrganizationService],
  entryComponents: [MinisterModalComponent]
  // entryComponents:[GgcOpmComponent,Opm1111Component,OtpsComponent],
  // exports: [GgcOpmComponent, Opm1111Component, OtpsComponent, MinisterTableComponent],
})
export class ExternalOrganizationModule { }

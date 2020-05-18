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



@NgModule({
  declarations: [GgcOpmComponent, Opm1111Component, OtpsComponent, MinisterTableComponent],
  imports: [
    CommonModule,
    ExternalOrganizationRoutingModule,
    DataTablesModule,
    NgxSpinnerModule
  ],

  providers: [ExternalOrganizationService],
  // entryComponents:[GgcOpmComponent,Opm1111Component,OtpsComponent],
  // exports: [GgcOpmComponent, Opm1111Component, OtpsComponent, MinisterTableComponent],
})
export class ExternalOrganizationModule { }

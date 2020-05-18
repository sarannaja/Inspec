import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GgcOpmComponent } from './ggc-opm/ggc-opm.component';
import { Opm1111Component } from './opm1111/opm1111.component';
import { OtpsComponent } from './otps/otps.component';
// import { ExternalOrganizationRoutingModule } from './external-organization-routing.module';
import { ExternalOrganizationService } from '../services/external-organization.service';
import { ExternalOrganizationRoutingModule } from './external-organization-routing.module';



@NgModule({
  declarations: [GgcOpmComponent,Opm1111Component,OtpsComponent],
  imports: [
    CommonModule,
    ExternalOrganizationRoutingModule
  ],
  providers:[ExternalOrganizationService],
  // entryComponents:[GgcOpmComponent,Opm1111Component,OtpsComponent],
  // exports:[GgcOpmComponent,Opm1111Component,OtpsComponent]
})
export class ExternalOrganizationModule { }

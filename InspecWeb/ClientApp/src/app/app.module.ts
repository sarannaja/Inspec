import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { ApiAuthorizationModule } from 'src/api-authorization/api-authorization.module';
import { AuthorizeGuard } from 'src/api-authorization/authorize.guard';
import { AuthorizeInterceptor } from 'src/api-authorization/authorize.interceptor';
import { MainComponent } from './main/main.component';
import { DefaultLayoutComponent } from './default-layout/default-layout/default-layout.component';
import { CreateCentralPolicyComponent } from './central-policy/create-central-policy/create-central-policy.component';
import { CreateInspectionPlanComponent } from './inspection-plan/create-inspection-plan/create-inspection-plan.component';
import { EditInspectionPlanComponent } from './inspection-plan/edit-inspection-plan/edit-inspection-plan.component';
import { MinistryComponent } from './ministry/ministry.component';
import { ProvinceComponent } from './province/province.component';
import { RegionComponent } from './region/region.component';
import { ModalModule } from 'ngx-bootstrap/modal';
import { UserComponent } from './user/user.component';
import { FiscalyearComponent } from './fiscalyear/fiscalyear.component';
import { SelectModule } from 'ng-select'
import { DetailCentralPolicyComponent } from './central-policy/detail-central-policy/detail-central-policy.component';
import { LoginComponent } from './login/login.component';
import { SupportGovernmentComponent } from './support-government/support-government.component';
import { CentralPolicyComponent } from './central-policy/central-policy.component';
import { InspectionPlanComponent } from './inspection-plan/inspection-plan.component';
import { MyDatePickerTHModule } from 'mydatepicker-th';
import { InspectionorderComponent } from './inspectionorder/inspectionorder.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    MainComponent,
    DefaultLayoutComponent,
    CreateCentralPolicyComponent,
    CreateInspectionPlanComponent,
    EditInspectionPlanComponent,
    MinistryComponent,
    ProvinceComponent,
    RegionComponent,
    UserComponent,
    FiscalyearComponent,
    DetailCentralPolicyComponent,
    LoginComponent,
    SupportGovernmentComponent,
    CentralPolicyComponent,
    InspectionPlanComponent,
    InspectionorderComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ApiAuthorizationModule,
    SelectModule,
    ReactiveFormsModule,
    MyDatePickerTHModule,
    RouterModule.forRoot([
      { path: '', component: LoginComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent, canActivate: [AuthorizeGuard] },
      { path: 'login', component: LoginComponent },
      {
        path: '',
        component: DefaultLayoutComponent,
        data: {
          title: 'หน้าหลัก'
        },
        children: [
          { path: 'main', component: MainComponent },
          { path: 'centralpolicy/createcentralpolicy', component: CreateCentralPolicyComponent },
          { path: 'inspectionplan/createinspectionplan', component: CreateInspectionPlanComponent },
          { path: 'inspectionplan/editinspectionplan/:id', component: EditInspectionPlanComponent },
          { path: 'ministry', component: MinistryComponent },
          { path: 'province', component: ProvinceComponent },
          { path: 'region', component: RegionComponent },
          { path: 'user', component: UserComponent },
          { path: 'fiscalyear',component: FiscalyearComponent},
          { path: 'centralpolicy/detailcentralpolicy/:id', component: DetailCentralPolicyComponent },
          { path: 'supportgovernment', component: SupportGovernmentComponent },
          { path: 'centralpolicy', component: CentralPolicyComponent },
          { path: 'inspectionplan', component: InspectionPlanComponent },
          { path: 'inspectionorder', component: InspectionorderComponent },

        ]
      }
    ]),
    ModalModule.forRoot()
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

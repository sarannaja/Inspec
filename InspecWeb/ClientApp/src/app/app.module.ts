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
import { CreateInstructionorderComponent } from './instruction-order/create-instructionorder/create-instructionorder.component';
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
import { GovernmentinspectionplanComponent } from './governmentinspectionplan/governmentinspectionplan.component';
import { InspectionorderComponent } from './inspectionorder/inspectionorder.component';
import { InstructionOrderComponent } from './instruction-order/instruction-order.component';
import { DistrictComponent } from './district/district.component';
import { SubdistrictComponent } from './subdistrict/subdistrict.component';
import { DataTablesModule } from 'angular-datatables';
import { TrainingComponent } from './training/training.component';
import { CreateTrainingComponent } from './training/create-training/create-training.component';
import { ThaiDatePipe } from './services/Pipe/thaidate.service';
import { SnotifyModule, ToastDefaults, SnotifyService } from 'ng-snotify';
import { NotificationService } from './services/Pipe/alert.service';
import { SubjectComponent } from './subject/subject.component';
import { SubquestionComponent } from './subquestion/subquestion.component';
import { DetailFiscalyearComponent } from './fiscalyear/detail-fiscalyear/detail-fiscalyear.component';
import { InspectionPlanEventComponent } from './inspection-plan-event/inspection-plan-event.component';
import { CreateInspectionPlanEventComponent } from './inspection-plan-event/create-inspection-plan-event/create-inspection-plan-event.component';
import { TrainComponent } from './train/train.component';
import { DefaultLayoutTrainComponent } from './default-layout-train/default-layout-train.component';
import { InspectorComponent } from './inspector/inspector.component';
import { ExecutiveOrderComponent } from './executive-order/executive-order.component';
import { DetailExecutiveOrderComponent } from './executive-order/detail-executive-order/detail-executive-order.component';
import { NgxSpinnerModule } from "ngx-spinner";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AcceptCentralPolicyComponent } from './central-policy/accept-central-policy/accept-central-policy.component';



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
    CreateInstructionorderComponent,
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
    GovernmentinspectionplanComponent,
    InspectionorderComponent,
    InstructionOrderComponent,
    DistrictComponent,
    SubdistrictComponent,
    TrainingComponent,
    CreateTrainingComponent,
    ThaiDatePipe,
    SubjectComponent,
    SubquestionComponent,
    DetailFiscalyearComponent,
    InspectionPlanEventComponent,
    CreateInspectionPlanEventComponent,
    TrainComponent,
    DefaultLayoutTrainComponent,
    InspectorComponent,
    ExecutiveOrderComponent,
    DetailExecutiveOrderComponent,
    AcceptCentralPolicyComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ApiAuthorizationModule,
    SelectModule,
    ReactiveFormsModule,
    MyDatePickerTHModule,
    BrowserModule,
    SnotifyModule,
    NgxSpinnerModule,
    DataTablesModule,
    BrowserAnimationsModule,
    RouterModule.forRoot([
      { path: '', redirectTo: 'main', pathMatch: 'full' },
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
          { path: 'main', component: MainComponent, canActivate: [AuthorizeGuard] }, //ออเทน
          { path: 'centralpolicy/createcentralpolicy', component: CreateCentralPolicyComponent },
          { path: 'inspectionplan/createinspectionplan/:id', component: CreateInspectionPlanComponent },
          { path: 'instructionorder/createinstuctionorder', component: CreateInstructionorderComponent },
          { path: 'inspectionplan/editinspectionplan/:id', component: EditInspectionPlanComponent },
          { path: 'ministry', component: MinistryComponent },
          { path: 'province', component: ProvinceComponent },
          { path: 'region', component: RegionComponent },
          { path: 'user/:id', component: UserComponent },
          { path: 'fiscalyear', component: FiscalyearComponent },
          { path: 'centralpolicy/detailcentralpolicy/:id', component: DetailCentralPolicyComponent },
          { path: 'supportgovernment', component: SupportGovernmentComponent },
          { path: 'centralpolicy', component: CentralPolicyComponent },
          { path: 'inspectionplan/:id', component: InspectionPlanComponent },
          { path: 'instructionorder', component: InstructionOrderComponent },
          { path: 'govermentinpectionplan', component: GovernmentinspectionplanComponent },
          { path: 'inspectionorder', component: InspectionorderComponent },
          { path: 'InstructionOrder', component: InstructionOrderComponent },
          { path: 'district/:id', component: DistrictComponent },
          { path: 'subdistrict/:id', component: SubdistrictComponent },
          { path: 'training', component: TrainingComponent },
          { path: 'training/createtraining', component: CreateTrainingComponent },
          { path: 'subject/:id', component: SubjectComponent },
          { path: 'subquestion/:id', component: SubquestionComponent },
          { path: 'fiscalyear/detailfiscalyear/:id',component: DetailFiscalyearComponent},
          { path: 'inspectionplanevent', component: InspectionPlanEventComponent },
          { path: 'inspectionplanevent/create', component: CreateInspectionPlanEventComponent },
          { path: 'inspector', component: InspectorComponent },
          { path: 'executiveorder', component: ExecutiveOrderComponent },
          { path: 'executiveorder/detailexecutiveorder/:id', component: DetailExecutiveOrderComponent},
          { path: 'acceptcentralpolicy', component: AcceptCentralPolicyComponent},
        ]
      },
      {
        path: 'train',
        component: DefaultLayoutTrainComponent,
        data: {
          title: 'หน้าหลัก'
        },
        children: [
          { path: 'maintrain', component: TrainComponent, canActivate: [AuthorizeGuard] }, //ออเทน
        ]
      }
    ]),
    ModalModule.forRoot()
  ], exports: [
    ThaiDatePipe,],
  providers: [
    { provide: 'SnotifyToastConfig', useValue: ToastDefaults },
    SnotifyService, NotificationService,
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true },

  ],

  bootstrap: [AppComponent]
})
export class AppModule { }

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
import { CabinetComponent } from './cabinet/cabinet.component';
import { InspectorComponent } from './inspector/inspector.component';
import { ExecutiveOrderComponent } from './executive-order/executive-order.component';
import { DetailExecutiveOrderComponent } from './executive-order/detail-executive-order/detail-executive-order.component';
import { NgxSpinnerModule } from "ngx-spinner";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MinistermonitoringComponent } from './ministermonitoring/ministermonitoring.component';
import { AcceptCentralPolicyComponent } from './central-policy/accept-central-policy/accept-central-policy.component';
import { EditCentralPolicyComponent } from './central-policy/edit-central-policy/edit-central-policy.component';
import { UserManager } from 'oidc-client';
import { LogoutComponent } from 'src/api-authorization/logout/logout.component';
import { UserCentralPolicyComponent } from './central-policy/user-central-policy/user-central-policy.component';
import { EditSubjectComponent } from './subject/edit-subject/edit-subject.component';
import { DetailCentralPolicyProvinceComponent } from './central-policy/detail-central-policy-province/detail-central-policy-province.component';
import { DetailSubjectComponent } from './subject/detail-subject/detail-subject.component';
import { ReportCentralPolicyComponent } from './central-policy/accept-central-policy/report-central-policy/report-central-policy.component';
import { ElectronicBookComponent } from './electronic-book/electronic-book.component';
import { CreateElectronicBookComponent } from './electronic-book/create-electronic-book/create-electronic-book.component';
import { EditElectronicBookComponent } from './electronic-book/edit-electronic-book/edit-electronic-book.component';
import { DetailElectronicBookComponent } from './electronic-book/detail-electronic-book/detail-electronic-book.component';


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
    DetailCentralPolicyProvinceComponent,
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
    CabinetComponent,
    InspectorComponent,
    ExecutiveOrderComponent,
    DetailExecutiveOrderComponent,
    MinistermonitoringComponent,
    AcceptCentralPolicyComponent,
    EditCentralPolicyComponent,
    UserCentralPolicyComponent,
    EditSubjectComponent,
    DetailSubjectComponent,
    ReportCentralPolicyComponent,
    ElectronicBookComponent,
    CreateElectronicBookComponent,
    EditElectronicBookComponent,
    DetailElectronicBookComponent

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
          { path: 'centralpolicy/createcentralpolicy', component: CreateCentralPolicyComponent, canActivate: [AuthorizeGuard] },
          { path: 'inspectionplan/createinspectionplan/:id', component: CreateInspectionPlanComponent, canActivate: [AuthorizeGuard] },
          { path: 'instructionorder/createinstuctionorder', component: CreateInstructionorderComponent, canActivate: [AuthorizeGuard] },
          { path: 'inspectionplan/editinspectionplan/:id', component: EditInspectionPlanComponent, canActivate: [AuthorizeGuard] },
          { path: 'ministry', component: MinistryComponent, canActivate: [AuthorizeGuard] },
          { path: 'province', component: ProvinceComponent, canActivate: [AuthorizeGuard] },
          { path: 'region', component: RegionComponent, canActivate: [AuthorizeGuard] },
          { path: 'user/:id', component: UserComponent, canActivate: [AuthorizeGuard] },
          { path: 'fiscalyear', component: FiscalyearComponent, canActivate: [AuthorizeGuard] },
          { path: 'centralpolicy/detailcentralpolicy/:id', component: DetailCentralPolicyComponent, canActivate: [AuthorizeGuard] },
          { path: 'centralpolicy/detailcentralpolicyprovince/:result', component: DetailCentralPolicyProvinceComponent, canActivate: [AuthorizeGuard] },
          { path: 'supportgovernment', component: SupportGovernmentComponent, canActivate: [AuthorizeGuard] },
          { path: 'centralpolicy', component: CentralPolicyComponent, canActivate: [AuthorizeGuard] },
          { path: 'inspectionplan/:id', component: InspectionPlanComponent, canActivate: [AuthorizeGuard] },
          { path: 'instructionorder', component: InstructionOrderComponent, canActivate: [AuthorizeGuard] },
          { path: 'govermentinpectionplan', component: GovernmentinspectionplanComponent, canActivate: [AuthorizeGuard] },
          { path: 'inspectionorder', component: InspectionorderComponent, canActivate: [AuthorizeGuard] },
          { path: 'InstructionOrder', component: InstructionOrderComponent, canActivate: [AuthorizeGuard] },
          { path: 'district/:id', component: DistrictComponent, canActivate: [AuthorizeGuard] },
          { path: 'subdistrict/:id', component: SubdistrictComponent, canActivate: [AuthorizeGuard] },
          { path: 'training', component: TrainingComponent, canActivate: [AuthorizeGuard] },
          { path: 'training/createtraining', component: CreateTrainingComponent, canActivate: [AuthorizeGuard] },
          { path: 'subject/:id', component: SubjectComponent, canActivate: [AuthorizeGuard] },
          { path: 'subquestion/:id', component: SubquestionComponent, canActivate: [AuthorizeGuard] },
          { path: 'fiscalyear/detailfiscalyear/:id', component: DetailFiscalyearComponent, canActivate: [AuthorizeGuard] },
          { path: 'inspectionplanevent', component: InspectionPlanEventComponent, canActivate: [AuthorizeGuard] },
          { path: 'inspectionplanevent/create', component: CreateInspectionPlanEventComponent, canActivate: [AuthorizeGuard] },
          { path: 'cabinet', component: CabinetComponent, canActivate: [AuthorizeGuard] },
          { path: 'inspector', component: InspectorComponent, canActivate: [AuthorizeGuard] },
          { path: 'executiveorder', component: ExecutiveOrderComponent, canActivate: [AuthorizeGuard] },
          { path: 'executiveorder/detailexecutiveorder/:id', component: DetailExecutiveOrderComponent, canActivate: [AuthorizeGuard] },
          { path: 'ministermonitoring', component: MinistermonitoringComponent, canActivate: [AuthorizeGuard] },
          { path: 'acceptcentralpolicy/:id', component: AcceptCentralPolicyComponent, canActivate: [AuthorizeGuard] },
          { path: 'usercentralpolicy', component: UserCentralPolicyComponent, canActivate: [AuthorizeGuard] },
          { path: 'test/logout', component: LogoutComponent},
          { path: 'centralpolicy/editcentralpolicy/:id', component: EditCentralPolicyComponent, canActivate: [AuthorizeGuard] },
          { path: 'subject/editsubject/:id', component: EditSubjectComponent, canActivate: [AuthorizeGuard] },
          { path: 'subject/detailsubject/:id', component: DetailSubjectComponent, canActivate: [AuthorizeGuard] },
          { path: 'reportcentralpolicy/:id', component: ReportCentralPolicyComponent, canActivate: [AuthorizeGuard] },
          { path: 'electronicbook', component: ElectronicBookComponent, canActivate: [AuthorizeGuard] },
          { path: 'electronicbook/create', component: CreateElectronicBookComponent, canActivate: [AuthorizeGuard] },
          { path: 'electronicbook/edit/:id', component: EditElectronicBookComponent, canActivate: [AuthorizeGuard] },
          { path: 'electronicbook/detail/:id', component: DetailElectronicBookComponent, canActivate: [AuthorizeGuard] },
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
    UserManager
  ],

  bootstrap: [AppComponent]
})
export class AppModule { }

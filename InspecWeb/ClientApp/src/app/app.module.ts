import { BrowserModule } from '@angular/platform-browser';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
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
// import { SelectModule } from 'ng-select'
import { DetailCentralPolicyComponent } from './central-policy/detail-central-policy/detail-central-policy.component';
import { LoginComponent } from './login/login.component';
import { SupportGovernmentComponent } from './support-government/support-government.component';
import { CentralPolicyComponent } from './central-policy/central-policy.component';
import { InspectionPlanComponent } from './inspection-plan/inspection-plan.component';
import { MyDatePickerTHModule } from 'mydatepicker-th';
import { GovernmentinspectionplanComponent } from './governmentinspectionplan/governmentinspectionplan.component';
import { InspectionorderComponent } from './inspectionorder/inspectionorder.component';
import { InstructionorderComponent } from './instructionorder/instructionorder.component';
import { DistrictComponent } from './district/district.component';
import { SubdistrictComponent } from './subdistrict/subdistrict.component';
import { DataTablesModule } from 'angular-datatables';

//----Training----
import { TrainingComponent } from './training/training.component';
import { CreateTrainingComponent } from './training/create-training/create-training.component';
import { TrainingRegisterComponent } from './training-register/training-register.component';
import { ListTrainingRegisterComponent } from './training-register/list-training-register/list-training-register.component';
import { TrainingSurveyComponent } from './training-survey/training-survey.component';
import { ListTrainingSurveyComponent } from './training-survey/list-training-survey/list-training-survey.component';
import { DefaultLayoutTrainComponent } from './default-layout-train/default-layout-train.component';
import { DetailDefaultLayoutTrainComponent } from './default-layout-train/detail-default-layout-train/detail-default-layout-train.component';
import { ProgramDefaultLayoutTrainComponent } from './default-layout-train/program-default-layout-train/program-default-layout-train.component';
import { TrainComponent } from './train/train.component';
import { ListDefaultLayoutTrainComponent } from './default-layout-train/list-default-layout-train/list-default-layout-train.component';
import { PreviewTrainingSurveyComponent } from './training-survey/preview-training-survey/preview-training-survey.component';
import { TrainingDocumentComponent } from './training-document/training-document.component';
import { ListTrainingDocumentComponent } from './training-document/list-training-document/list-training-document.component';
import { RegisterDefaultLayoutTrainComponent } from './default-layout-train/register-default-layout-train/register-default-layout-train.component';
import { SurveyDefaultLayoutTrainComponent } from './default-layout-train/survey-default-layout-train/survey-default-layout-train.component';
import { SuccessDefaultLayoutTrainComponent } from './default-layout-train/success-default-layout-train/success-default-layout-train.component';
import { TrainingReportComponent } from './training-report/training-report.component';
import { ListTrainingReportComponent } from './training-report/list-training-report/list-training-report.component';
import { ListRegisterTrainingReportComponent } from './training-report/list-training-report/list-register-training-report/list-register-training-report.component';
import { HistoryTrainingReportComponent } from './training-report/history-training-report/history-training-report.component';
import { ProgramTrainingComponent } from './training/program-training/program-training.component';
import { LecturerTrainingComponent } from './training/lecturer-training/lecturer-training.component';
import { GroupTrainingRegisterComponent } from './training-register/group-training-register/group-training-register.component';
import { ProgramTrainingRegisterComponent } from './training-register/program-training-register/program-training-register.component';
import { ChartTrainingSurveyComponent } from './training-survey/chart-training-survey/chart-training-survey.component';
import { PhaseTrainingComponent } from './training/phase-training/phase-training.component';
import { TrainingConditionComponent } from './training-condition/training-condition.component';
import { TrainingManageComponent } from './training-manage/training-manage.component';
import { TrainingProgramLoginComponent } from './training-programlogin/training-programlogin.component';
import { TrainingLecturerListComponent } from './training-lecturerlist/training-lecturerlist.component';

import { TrainingIDCodeComponent } from './training-idcode/training-idcode.component';
import { RateloginTrainingReportComponent } from './training-report/ratelogin-training-report/ratelogin-training-report.component';
import { TrainingSurveyLecturerComponent } from './training-surveylecturer/training-surveylecturer.component';
import { TrainingProcessingComponent } from './training-processing/training-processing.component';

//----------------

import { ThaiDatePipe } from './services/Pipe/thaidate.service';
import { SnotifyModule, ToastDefaults, SnotifyService } from 'ng-snotify';
import { NotificationService } from './services/Pipe/alert.service';
import { SubjectComponent } from './subject/subject.component';
import { SubquestionComponent } from './subquestion/subquestion.component';
import { DetailFiscalyearComponent } from './fiscalyear/detail-fiscalyear/detail-fiscalyear.component';
import { InspectionPlanEventComponent } from './inspection-plan-event/inspection-plan-event.component';
import { CreateInspectionPlanEventComponent } from './inspection-plan-event/create-inspection-plan-event/create-inspection-plan-event.component';
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
import { AnswerSubjectComponent } from './answer-subject/answer-subject.component';
import { CalendarUserComponent } from './calendar-user/calendar-user.component';
import { AdviserCivilSectorComponent } from './adviser-civil-sector/adviser-civil-sector.component';
import { RequestOrderComponent } from './request-order/request-order.component';
//import { DetailRequestOrderComponent } from './request-order/detail-request-order/detail-request-order.component';
import { OfficerInspectionComponent } from './officer-inspection/officer-inspection.component';
import { InfomationProvinceComponent } from './infomation-province/infomation-province.component';
import { InfoDistrictComponent } from './info-district/info-district.component';
import { InfoSubdistrictComponent } from './info-subdistrict/info-subdistrict.component';
import { AnswerSubjectListComponent } from './answer-subject/answer-subject-list/answer-subject-list.component';
import { AnswerSubjectDetailComponent } from './answer-subject/answer-subject-detail/answer-subject-detail.component';
import { ElectronicBookProvinceComponent } from './electronic-book-province/electronic-book-province.component';
import { AnswerOutsiderComponent } from './answer-subject/answer-outsider/answer-outsider.component';
import { ExternalOrganizationModule } from './external-organization/external-organization.module';
import { OtpsComponent } from './external-organization/otps/otps.component';
import { Opm1111Component } from './external-organization/opm1111/opm1111.component';
import { GgcOpmComponent } from './external-organization/ggc-opm/ggc-opm.component';
import { TemplateElectronicComponent } from './template-electronic/template-electronic.component';
import { ReportImportComponent } from './report-import/report-import.component';
import { ReportExportComponent } from './report-export/report-export.component';
import { AnswerOutsideThankComponent } from './answer-subject/answer-outside-thank/answer-outside-thank.component';
import { AnswerPeopleComponent } from './answer-subject/answer-people/answer-people.component';
import { AnswerPeopleListComponent } from './answer-subject/answer-people-list/answer-people-list.component';
import { InformationoperationComponent } from './informationoperation/informationoperation.component';
import { NationalstrategyComponent } from './nationalstrategy/nationalstrategy.component';
import { MainComponent } from './main/main.component';
import { ExcelService } from './services/excel.service';
import { AnswerPeopleDetailComponent } from './answer-subject/answer-people-detail/answer-people-detail.component';
import { AnswerCentralPolicyProvinceComponent } from './answer-subject/answer-central-policy-province/answer-central-policy-province.component';
import { InvitedElectronicBookComponent } from './electronic-book/invited-electronic-book/invited-electronic-book.component';
import { InspectionPlanEventProvinceComponent } from './inspection-plan-event/inspection-plan-event-province/inspection-plan-event-province.component';
import { InfoMinistryComponent } from './info-ministry/info-ministry.component';
import { GraphAnswerSubjectComponent } from './subject/graph-answer-subject/graph-answer-subject.component';
import { ChartsModule } from 'ng2-charts';
import { InfoDepartmentComponent } from './info-department/info-department.component';
import { InfoVillageComponent } from './info-village/info-village.component';
import { StatepolicyComponent } from './statepolicy/statepolicy.component';
import { DocumenttemplateComponent } from './documenttemplate/documenttemplate.component';
import { MeetinginformationComponent } from './meetinginformation/meetinginformation.component';
import { PremierorderComponent } from './premierorder/premierorder.component';
import { ExcelGeneraterService } from './services/excel-generater.service';
import { DatePipe } from '@angular/common';
import { ReportInspectionPlanEventComponent } from './inspection-plan-event/report-inspection-plan-event/report-inspection-plan-event.component';
import { CentralPolicyFiscalyearComponent } from './central-policy/central-policy-fiscalyear/central-policy-fiscalyear.component';

import { ExecutiveOrderExport1Component } from './executive-order/executive-order-export1/executive-order-export1.component';
import { ExecutiveOrderExport3Component } from './executive-order/executive-order-export3/executive-order-export3.component';
import { CommanderReportComponent } from './commander-report/commander-report.component';
import { SubjecteventComponent } from './subjectevent/subjectevent.component';
import { DetailSubjecteventComponent } from './subjectevent/detail-subjectevent/detail-subjectevent.component';
import { DetailInvitedElectronicBookComponent } from './electronic-book/invited-electronic-book/detail-invited-electronic-book/detail-invited-electronic-book.component';

import { RequestOrderExport1Component } from './request-order/request-order-export1/request-order-export1.component';
import { RequestOrderExport3Component } from './request-order/request-order-export3/request-order-export3.component';

import { LogComponent } from './log/log.component';
// import { AnswerCentralPolicyProvinceEditComponent } from './answer-subject/answer-central-policy-province-edit/answer-central-policy-province-edit.component';

import { AnswerCentralPolicyProvinceEditComponent } from './answer-subject/answer-central-policy-province-edit/answer-central-policy-province-edit.component';
import { ElectronicBookProvinceDetailComponent } from './electronic-book-province/electronic-book-province-detail/electronic-book-province-detail.component';
import { ReportImportDeatailComponent } from './report-import/report-import-deatail/report-import-deatail.component';
import { CommanderReportDetailComponent } from './commander-report/commander-report-detail/commander-report-detail.component';
import { DetailCentralPolicyProvinceMinistryComponent } from './central-policy/detail-central-policy-province-ministry/detail-central-policy-province-ministry.component';
import { ExternalRegisterComponent } from './external-register/external-register.component';
import { AnswerSubjectEditComponent } from './answer-subject/answer-subject-edit/answer-subject-edit.component';
import { InspectionPlanMinistryComponent } from './inspection-plan-ministry/inspection-plan-ministry.component';
import { ReportSubjectComponent } from './report/report-subject/report-subject.component';
import { ReportPerformanceComponent } from './report/report-performance/report-performance.component';
import { ReportSuggestionsComponent } from './report/report-suggestions/report-suggestions.component';
import { ReportSuggestionResultComponent } from './report/report-suggestion-result/report-suggestion-result.component';
import { ReportQuestionnaireComponent } from './report/report-questionnaire/report-questionnaire.component';
import { ReportCommentComponent } from './report/report-comment/report-comment.component';
import { CookieService } from 'ngx-cookie-service'
import { ElectronicBookOtherComponent } from './electronic-book-province/electronic-book-other/electronic-book-other.component';
import { ElectronicBookOtherDetailComponent } from './electronic-book-province/electronic-book-other/electronic-book-other-detail/electronic-book-other-detail.component';
import { ProvinceService } from './services/province.service';
import { SelectSSSModule } from './external-organization/select/select.module';
import { NgSelectModule } from '@ng-select/ng-select';
import { DetailrowCentralPolicyComponent } from './central-policy/detailrow-central-policy/detailrow-central-policy.component';
import { DepartmentComponent } from './department/department.component';
import { ProvincialDepartmentComponent } from './provincialdepartment/provincialdepartment.component';
import { InspectionPlanDepartmentComponent } from './inspection-plan-department/inspection-plan-department.component';
import { DetailCentralPolicyProvinceDepartmentComponent } from './central-policy/detail-central-policy-province-department/detail-central-policy-province-department.component';
import { ElectronicBookDepartmentComponent } from './electronic-book-department/electronic-book-department.component';
import { ElectronicBookDepartmentDetailComponent } from './electronic-book-department/electronic-book-department-detail/electronic-book-department-detail.component';
import { InspectionPlanEventAllComponent } from './inspection-plan-event-all/inspection-plan-event-all.component';
import { NotofyService } from './services/notofy.service';
import { CircularletterComponent } from './circularletter/circularletter.component';
import { InformationinspectionComponent } from './informationinspection/informationinspection.component';
import { ApprovaldocumentComponent } from './approvaldocument/approvaldocument.component';
import { FiscalyearnewComponent } from './fiscalyearnew/fiscalyearnew.component';
import { SideComponent } from './side/side.component';
import { SectorComponent } from './sector/sector.component';
import { TypeexamibationplanComponent } from './typeexamibationplan/typeexamibationplan.component';
import { ProvincesgroupComponent } from './provincesgroup/provincesgroup.component';
import { NumberDirective } from './services/numbers-only.directive';
import { AllReportComponent } from './all-report/all-report.component';
import { AllReportDetailComponent } from './all-report/all-report-detail/all-report-detail.component';
import { TimepickerModule } from 'ngx-bootstrap/timepicker';
import { NgxPrintModule } from 'ngx-print';
import { AnswerRecommendationinSpectorComponent } from './answer-subject/answer-recommendationin-spector/answer-recommendationin-spector.component';
import { AnswerRecommendationinSpectorDetailComponent } from './answer-subject/answer-recommendationin-spector-detail/answer-recommendationin-spector-detail.component';
import { AnswerRecommendationinSpectorEditComponent } from './answer-subject/answer-recommendationin-spector-edit/answer-recommendationin-spector-edit.component';
import { NgxMaterialTimepickerModule } from 'ngx-material-timepicker';
import { ConfirmationDialogService } from './services/confirmation-dialog/confirmation-dialog.service';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

// import { PopoverModule } from 'ngx-bootstrap/popover';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { SortPipe } from './services/Pipe/sort';
import { DateLengthComponent } from './services/components/date-length/date-length.component';
import { PlanTrainingComponent } from './training/plan-training/plan-training.component';
import { NamePlateComponent } from './name-plate/name-plate.component';
import { NamePlatePreviewComponent } from './name-plate/name-plate-preview/name-plate-preview.component';
import { NameLabelPreviewComponent } from './name-plate/name-label-preview/name-label-preview.component';
import { TrainingLoginComponent } from './training-login/training-login.component';
import { TrainingLoginSuccessComponent } from './training-login/training-login-success/training-login-success.component';
import { ChartComponent } from './subjectevent/detail-subjectevent/chart/chart.component';

import { FusionChartsModule } from "angular-fusioncharts";

import * as FusionCharts from "fusioncharts";
import * as charts from "fusioncharts/fusioncharts.charts";
import * as FusionTheme from "fusioncharts/themes/fusioncharts.theme.fusion";
import { TrainingLoginListComponent } from './training-login/training-login-list/training-login-list.component';
import { TrainingLoginListDetailComponent } from './training-login/training-login-list/training-login-list-detail/training-login-list-detail.component';

// Pass the fusioncharts library and chart modules
FusionChartsModule.fcRoot(FusionCharts, charts, FusionTheme);
import { QRCodeModule } from 'angularx-qrcode';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { DemoMaterialModule } from './materail-module';
import { TrainingIdcodeModule } from './training-idcode/training-idcode.module';
import { RegionalagencyComponent } from './regionalagency/regionalagency.component';
import { ConfirmationDialogComponent } from './services/confirmation-dialog/confirmation-dialog.component';
import { RegisterTrainingComponent } from './training/register-training/register-training.component';
import { MenuComponent } from './menu/menu.component';
import { ElectronicBookAllComponent } from './electronic-book-all/electronic-book-all.component';
import { ElectronicBookAllDetailComponent } from './electronic-book-all/electronic-book-all-detail/electronic-book-all-detail.component';
const ExternalOrganization = [
  GgcOpmComponent, Opm1111Component, OtpsComponent
]

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
    DetailCentralPolicyProvinceComponent,
    LoginComponent,
    SupportGovernmentComponent,
    CentralPolicyComponent,
    InspectionPlanComponent,
    GovernmentinspectionplanComponent,
    InspectionorderComponent,
    InstructionorderComponent,
    DistrictComponent,
    SubdistrictComponent,
    //----Training----
    TrainingComponent,
    CreateTrainingComponent,
    TrainingRegisterComponent,
    ListTrainingRegisterComponent,
    TrainingSurveyComponent,
    ListTrainingSurveyComponent,
    DefaultLayoutTrainComponent,
    DetailDefaultLayoutTrainComponent,
    ProgramDefaultLayoutTrainComponent,
    TrainComponent,
    ListDefaultLayoutTrainComponent,
    PreviewTrainingSurveyComponent,
    TrainingDocumentComponent,
    ListTrainingDocumentComponent,
    RegisterDefaultLayoutTrainComponent,
    SurveyDefaultLayoutTrainComponent,
    SuccessDefaultLayoutTrainComponent,
    TrainingReportComponent,
    ListTrainingReportComponent,
    ListRegisterTrainingReportComponent,
    HistoryTrainingReportComponent,
    ProgramTrainingComponent,
    LecturerTrainingComponent,
    RegisterTrainingComponent,
    GroupTrainingRegisterComponent,
    ProgramTrainingRegisterComponent,
    ChartTrainingSurveyComponent,
    PhaseTrainingComponent,
    TrainingConditionComponent,
    TrainingManageComponent,
    TrainingProgramLoginComponent,

    TrainingLecturerListComponent,
    RateloginTrainingReportComponent,
    TrainingSurveyLecturerComponent,
    TrainingProcessingComponent,
    // TrainingIDCodeComponent,
    //---------------
    ThaiDatePipe,
    SubjectComponent,
    SubquestionComponent,
    DetailFiscalyearComponent,
    InspectionPlanEventComponent,
    CreateInspectionPlanEventComponent,
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
    DetailElectronicBookComponent,
    AnswerSubjectComponent,
    CalendarUserComponent,
    AdviserCivilSectorComponent,
    RequestOrderComponent,
    //DetailRequestOrderComponent,
    OfficerInspectionComponent,
    InfomationProvinceComponent,
    InfoDistrictComponent,
    InfoSubdistrictComponent,
    AnswerSubjectListComponent,
    AnswerSubjectDetailComponent,
    ElectronicBookProvinceComponent,
    AnswerOutsiderComponent,
    TemplateElectronicComponent,
    ReportImportComponent,
    ReportExportComponent,
    AnswerOutsiderComponent,
    AnswerOutsideThankComponent,
    AnswerPeopleComponent,
    AnswerPeopleListComponent,
    InformationoperationComponent,
    NationalstrategyComponent,
    AnswerPeopleDetailComponent,
    AnswerCentralPolicyProvinceComponent,
    InvitedElectronicBookComponent,
    InspectionPlanEventProvinceComponent,
    InfoMinistryComponent,
    GraphAnswerSubjectComponent,
    InfoDepartmentComponent,
    InfoVillageComponent,
    StatepolicyComponent,
    DocumenttemplateComponent,
    MeetinginformationComponent,
    PremierorderComponent,
    ReportInspectionPlanEventComponent,
    ReportSubjectComponent,
    CentralPolicyFiscalyearComponent,
    CommanderReportComponent,
    SortPipe,
    // DatePipe
    InfoDepartmentComponent,
    InfoVillageComponent,
    StatepolicyComponent,
    DocumenttemplateComponent,
    MeetinginformationComponent,
    ReportInspectionPlanEventComponent,
    ExecutiveOrderExport1Component,
    ExecutiveOrderExport3Component,
    InfoMinistryComponent,
    InfoVillageComponent,
    StatepolicyComponent,
    SubjecteventComponent,
    DetailSubjecteventComponent,
    DetailInvitedElectronicBookComponent,
    RequestOrderExport1Component,
    RequestOrderExport3Component,
    LogComponent,
    AnswerSubjectEditComponent,
    AnswerCentralPolicyProvinceEditComponent,
    ElectronicBookProvinceDetailComponent,
    ReportImportDeatailComponent,
    CommanderReportDetailComponent,
    InspectionPlanMinistryComponent,
    DetailCentralPolicyProvinceMinistryComponent,
    ExternalRegisterComponent,
    ReportPerformanceComponent,
    ReportSuggestionsComponent,
    ReportSuggestionResultComponent,
    ReportQuestionnaireComponent,
    ReportCommentComponent,
    ElectronicBookOtherComponent,
    ElectronicBookOtherDetailComponent,
    DetailrowCentralPolicyComponent,
    DepartmentComponent,
    ProvincialDepartmentComponent,
    InspectionPlanDepartmentComponent,
    DetailCentralPolicyProvinceDepartmentComponent,
    ElectronicBookDepartmentComponent,
    ElectronicBookDepartmentDetailComponent,
    InspectionPlanEventAllComponent,
    CircularletterComponent,
    InformationinspectionComponent,
    ApprovaldocumentComponent,
    FiscalyearnewComponent,
    SideComponent,
    SectorComponent,
    TypeexamibationplanComponent,
    ProvincesgroupComponent,
    NumberDirective,
    AllReportComponent,
    AllReportDetailComponent,
    AnswerRecommendationinSpectorComponent,
    AnswerRecommendationinSpectorDetailComponent,
    AnswerRecommendationinSpectorEditComponent,
    ConfirmationDialogComponent,
    DateLengthComponent,
    NamePlateComponent,
    NamePlatePreviewComponent,
    NameLabelPreviewComponent,
    PlanTrainingComponent,
    TrainingLoginComponent,
    TrainingLoginSuccessComponent,
    ChartComponent,
    TrainingLoginListComponent,
    TrainingLoginListDetailComponent,
    RegionalagencyComponent,
    ConfirmationDialogComponent,
    MenuComponent,
    ElectronicBookAllComponent,
    ElectronicBookAllDetailComponent,
  ],

  imports: [
    // BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    BrowserModule,
    HttpClientModule,
    FormsModule,
    ApiAuthorizationModule,
    // SelectModule,
    ReactiveFormsModule,
    ChartsModule,
    NgxPrintModule,
    MyDatePickerTHModule,
    // BrowserModule,
    SnotifyModule,
    NgxSpinnerModule,
    // PopoverModule.forRoot(),
    DataTablesModule,
    BrowserAnimationsModule,
    NgxMaterialTimepickerModule,
    // TrainingIdcodeModule,
    RouterModule.forRoot([
      { path: '', redirectTo: 'main', pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent, canActivate: [AuthorizeGuard] },
      // { path: 'login', component: LoginComponent },
      { path: 'answersubject/outsider/:id/:userid', component: AnswerOutsiderComponent },
      { path: 'ty', component: AnswerOutsideThankComponent },
      { path: 'training/external/register', component: ExternalRegisterComponent },
      { path: 'inspectionplanevent/all/noauth', component: InspectionPlanEventAllComponent },
      {
        path: 'vector', loadChildren: () => import('./external-organization/external-organization.module')
          .then(m => m.ExternalOrganizationModule)
      },
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
          { path: 'inspectionplan/editinspectionplan/:id', component: EditInspectionPlanComponent, canActivate: [AuthorizeGuard] },
          { path: 'ministry', component: MinistryComponent, canActivate: [AuthorizeGuard] },
          { path: 'province', component: ProvinceComponent, canActivate: [AuthorizeGuard] },
          { path: 'region', component: RegionComponent, canActivate: [AuthorizeGuard] },
          { path: 'user/:id', component: UserComponent, canActivate: [AuthorizeGuard] },
          { path: 'fiscalyear', component: FiscalyearComponent, canActivate: [AuthorizeGuard] },
          { path: 'centralpolicy/detailcentralpolicy/:id', component: DetailCentralPolicyComponent, canActivate: [AuthorizeGuard] },
          { path: 'centralpolicy/detailcentralpolicyprovince/:result', component: DetailCentralPolicyProvinceComponent, canActivate: [AuthorizeGuard] },
          { path: 'centralpolicy/detailcentralpolicyprovince/ministry/:result', component: DetailCentralPolicyProvinceMinistryComponent, canActivate: [AuthorizeGuard] },
          { path: 'centralpolicy/detailcentralpolicyprovince/department/:result', component: DetailCentralPolicyProvinceDepartmentComponent, canActivate: [AuthorizeGuard] },
          { path: 'supportgovernment', component: SupportGovernmentComponent, canActivate: [AuthorizeGuard] },
          { path: 'centralpolicy', component: CentralPolicyComponent, canActivate: [AuthorizeGuard] },
          { path: 'inspectionplan/:id/:provinceid/:watch', component: InspectionPlanComponent, canActivate: [AuthorizeGuard] },
          { path: 'inspectionplan/inspectorministry/:id/:provinceid/:watch', component: InspectionPlanMinistryComponent, canActivate: [AuthorizeGuard] },
          { path: 'inspectionplan/inspectordepartment/:id/:provinceid/:watch', component: InspectionPlanDepartmentComponent, canActivate: [AuthorizeGuard] },
          { path: 'instructionorder', component: InstructionorderComponent, canActivate: [AuthorizeGuard] },
          { path: 'supportgovernment/govermentinspectionplan', component: GovernmentinspectionplanComponent, canActivate: [AuthorizeGuard] },
          { path: 'inspectionorder', component: InspectionorderComponent, canActivate: [AuthorizeGuard] },
          { path: 'district/:id', component: DistrictComponent, canActivate: [AuthorizeGuard] },
          { path: 'subdistrict/:id', component: SubdistrictComponent, canActivate: [AuthorizeGuard] },
          //----Training----
          { path: 'training', component: TrainingComponent, canActivate: [AuthorizeGuard] },
          { path: 'training/createtraining', component: CreateTrainingComponent, canActivate: [AuthorizeGuard] },
          { path: 'training/register', component: TrainingRegisterComponent, canActivate: [AuthorizeGuard] },
          { path: 'training/registerlist/:id', component: ListTrainingRegisterComponent, canActivate: [AuthorizeGuard] },
          { path: 'training/survey', component: TrainingSurveyComponent, canActivate: [AuthorizeGuard] },
          { path: 'training/surveylist/:id', component: ListTrainingSurveyComponent, canActivate: [AuthorizeGuard] },
          { path: 'training/survey/preview/:id', component: PreviewTrainingSurveyComponent, canActivate: [AuthorizeGuard] },
          { path: 'training/document', component: TrainingDocumentComponent, canActivate: [AuthorizeGuard] },
          { path: 'training/documentlist/:id', component: ListTrainingDocumentComponent, canActivate: [AuthorizeGuard] },
          { path: 'training/reportmemu/:id', component: TrainingReportComponent, canActivate: [AuthorizeGuard] },
          { path: 'training/report/list', component: ListTrainingReportComponent, canActivate: [AuthorizeGuard] },
          { path: 'training/report/list/:id', component: ListRegisterTrainingReportComponent, canActivate: [AuthorizeGuard] },
          { path: 'training/report/history', component: HistoryTrainingReportComponent, canActivate: [AuthorizeGuard] },
          { path: 'training/report/loginrate/:id', component: RateloginTrainingReportComponent, canActivate: [AuthorizeGuard] },
          { path: 'training/phase/program/:phaseid/:id', component: ProgramTrainingComponent, canActivate: [AuthorizeGuard] },
          { path: 'training/lecturer', component: LecturerTrainingComponent, canActivate: [AuthorizeGuard] },

          { path: 'training/register/program/group/:id', component: GroupTrainingRegisterComponent, canActivate: [AuthorizeGuard] },
          { path: 'training/register/program/:id', component: ProgramTrainingRegisterComponent, canActivate: [AuthorizeGuard] },
          { path: 'training/survey/chart/:id', component: ChartTrainingSurveyComponent, canActivate: [AuthorizeGuard] },
          { path: 'training/phase/:id', component: PhaseTrainingComponent, canActivate: [AuthorizeGuard] },
          { path: 'training/condition/:id', component: TrainingConditionComponent, canActivate: [AuthorizeGuard] },
          { path: 'training/phase/plan/:id', component: PlanTrainingComponent, canActivate: [AuthorizeGuard] },
          { path: 'training/manage/:id', component: TrainingManageComponent, canActivate: [AuthorizeGuard] },
          { path: 'training/programlogin/:id', component: TrainingProgramLoginComponent, canActivate: [AuthorizeGuard] },
          { path: 'training/lecturerlist/:id', component: TrainingLecturerListComponent, canActivate: [AuthorizeGuard] },
          { path: 'training/surveylecturer', component: TrainingSurveyLecturerComponent, canActivate: [AuthorizeGuard] },
          { path: 'training/survey/processing/:id', component: TrainingProcessingComponent, canActivate: [AuthorizeGuard] },
          
          //---------------
          { path: 'training/idcode/:id', loadChildren: () => import('./training-idcode/training-idcode.module').then(m => m.TrainingIdcodeModule) },
          { path: 'subject/:id', component: SubjectComponent, canActivate: [AuthorizeGuard] },
          { path: 'subquestion/:id', component: SubquestionComponent, canActivate: [AuthorizeGuard] },
          { path: 'fiscalyear/detailfiscalyear/:id', component: DetailFiscalyearComponent, canActivate: [AuthorizeGuard] },
          { path: 'inspectionplanevent', component: InspectionPlanEventComponent, canActivate: [AuthorizeGuard] },
          { path: 'inspectionplanevent/all', component: InspectionPlanEventAllComponent, canActivate: [AuthorizeGuard] },
          { path: 'inspectionplanevent/create', component: CreateInspectionPlanEventComponent, canActivate: [AuthorizeGuard] },
          {
            path: 'cabinet', loadChildren: () => import('./external-organization/external-organization.module')
              .then(m => m.ExternalOrganizationModule), canActivate: [AuthorizeGuard]
          },
          { path: 'inspector', component: InspectorComponent, canActivate: [AuthorizeGuard] },
          { path: 'executiveorder', component: ExecutiveOrderComponent, canActivate: [AuthorizeGuard] },
          { path: 'executiveorder/detailexecutiveorder/:id', component: DetailExecutiveOrderComponent, canActivate: [AuthorizeGuard] },
          { path: 'ministermonitoring', component: MinistermonitoringComponent, canActivate: [AuthorizeGuard] },
          { path: 'acceptcentralpolicy/:id', component: AcceptCentralPolicyComponent, canActivate: [AuthorizeGuard] },
          { path: 'usercentralpolicy', component: UserCentralPolicyComponent, canActivate: [AuthorizeGuard] },
          { path: 'test/logout', component: LogoutComponent },
          { path: 'usercentralpolicy/:id/:provinceid', component: UserCentralPolicyComponent, canActivate: [AuthorizeGuard] },
          { path: 'test/logout', component: LogoutComponent },
          { path: 'centralpolicy/editcentralpolicy/:id', component: EditCentralPolicyComponent, canActivate: [AuthorizeGuard] },
          { path: 'subject/editsubject/:id', component: EditSubjectComponent, canActivate: [AuthorizeGuard] },
          { path: 'subject/detailsubject/:id', component: DetailSubjectComponent, canActivate: [AuthorizeGuard] },
          { path: 'reportcentralpolicy/:id', component: ReportCentralPolicyComponent, canActivate: [AuthorizeGuard] },
          { path: 'electronicbook', component: ElectronicBookComponent, canActivate: [AuthorizeGuard] },
          { path: 'electronicbook/create', component: CreateElectronicBookComponent, canActivate: [AuthorizeGuard] },
          { path: 'electronicbook/edit/:id', component: EditElectronicBookComponent, canActivate: [AuthorizeGuard] },
          { path: 'electronicbook/detail/:id', component: DetailElectronicBookComponent, canActivate: [AuthorizeGuard] },
          { path: 'electronicbook/theme/:id', component: TemplateElectronicComponent, canActivate: [AuthorizeGuard] },
          {
            path: 'external', loadChildren: () => import('./external-organization/external-organization.module')
              .then(m => m.ExternalOrganizationModule), canActivate: [AuthorizeGuard]
          },
          { path: 'answersubject', component: AnswerSubjectComponent, canActivate: [AuthorizeGuard] },
          { path: 'calendaruser', component: CalendarUserComponent, canActivate: [AuthorizeGuard] },
          { path: 'officerinspection', component: OfficerInspectionComponent, canActivate: [AuthorizeGuard] },
          { path: 'informationprovince', component: InfomationProvinceComponent, canActivate: [AuthorizeGuard] },
          { path: 'infodistrict/:id', component: InfoDistrictComponent, canActivate: [AuthorizeGuard] },
          { path: 'infosubdistrict/:id', component: InfoSubdistrictComponent, canActivate: [AuthorizeGuard] },
          { path: 'answersubject/list/:id', component: AnswerSubjectListComponent, canActivate: [AuthorizeGuard] },
          { path: 'answersubject/detail/:id', component: AnswerSubjectDetailComponent, canActivate: [AuthorizeGuard] },
          { path: 'electronicbookprovince', component: ElectronicBookProvinceComponent, canActivate: [AuthorizeGuard] },
          { path: 'reportimport', component: ReportImportComponent, canActivate: [AuthorizeGuard] },
          { path: 'reportexport', component: ReportExportComponent, canActivate: [AuthorizeGuard] },
          { path: 'advisercivilsector', component: AdviserCivilSectorComponent, canActivate: [AuthorizeGuard] },
          { path: 'requestorder', component: RequestOrderComponent, canActivate: [AuthorizeGuard] },
          //{ path: 'requestorder/detailrequestorder/:id', component: DetailRequestOrderComponent, canActivate: [AuthorizeGuard] },
          { path: 'answerpeople', component: AnswerPeopleComponent, canActivate: [AuthorizeGuard] },
          { path: 'answerpeople/list/:id', component: AnswerPeopleListComponent, canActivate: [AuthorizeGuard] },
          { path: 'informationoperation', component: InformationoperationComponent, canActivate: [AuthorizeGuard] },
          { path: 'nationalstrategy', component: NationalstrategyComponent, canActivate: [AuthorizeGuard] },
          { path: 'answerpeople/detail/:id', component: AnswerPeopleDetailComponent, canActivate: [AuthorizeGuard] },
          { path: 'answerpeople/centralpolicyprovince/:result/:inspectionplaneventid', component: AnswerCentralPolicyProvinceComponent, canActivate: [AuthorizeGuard] },
          { path: 'electronicbook/invited', component: InvitedElectronicBookComponent, canActivate: [AuthorizeGuard] },
          { path: 'inspectionplaneventprovince/:id', component: InspectionPlanEventProvinceComponent, canActivate: [AuthorizeGuard] },
          { path: 'infoministry', component: InfoMinistryComponent, canActivate: [AuthorizeGuard] },
          { path: 'subject/graph/:id', component: GraphAnswerSubjectComponent, canActivate: [AuthorizeGuard] },
          { path: 'infoministry/:id/infodepartment', component: InfoDepartmentComponent },
          { path: 'infovillage/:id', component: InfoVillageComponent, canActivate: [AuthorizeGuard] },
          { path: 'statepolicy', component: StatepolicyComponent, canActivate: [AuthorizeGuard] },
          { path: 'documenttemplate', component: DocumenttemplateComponent, canActivate: [AuthorizeGuard] },
          { path: 'meetinginformation', component: MeetinginformationComponent, canActivate: [AuthorizeGuard] },
          { path: 'supportgovernment/premierorder', component: PremierorderComponent, canActivate: [AuthorizeGuard] },
          { path: 'reportinspectionplanevent', component: ReportInspectionPlanEventComponent, canActivate: [AuthorizeGuard] },
          { path: 'reportsubject', component: ReportSubjectComponent, canActivate: [AuthorizeGuard] },
          { path: 'centralpolicyfiscalyear/:id', component: CentralPolicyFiscalyearComponent, canActivate: [AuthorizeGuard] },
          { path: 'infoministry/:id/infodepartment', component: InfoDepartmentComponent },
          { path: 'infovillage/:id', component: InfoVillageComponent, canActivate: [AuthorizeGuard] },
          { path: 'statepolicy', component: StatepolicyComponent, canActivate: [AuthorizeGuard] },
          { path: 'documenttemplate', component: DocumenttemplateComponent, canActivate: [AuthorizeGuard] },
          { path: 'meetinginformation', component: MeetinginformationComponent, canActivate: [AuthorizeGuard] },
          { path: 'reportinspectionplanevent', component: ReportInspectionPlanEventComponent, canActivate: [AuthorizeGuard] },
          { path: 'executiveorderexport1', component: ExecutiveOrderExport1Component, canActivate: [AuthorizeGuard] },
          { path: 'executiveorderexport3', component: ExecutiveOrderExport3Component, canActivate: [AuthorizeGuard] },
          { path: 'infoministry', component: InfoMinistryComponent, canActivate: [AuthorizeGuard] },
          { path: 'infovillage/:id', component: InfoVillageComponent, canActivate: [AuthorizeGuard] },
          { path: 'statepolicy', component: StatepolicyComponent, canActivate: [AuthorizeGuard] },
          { path: 'commanderreport', component: CommanderReportComponent, canActivate: [AuthorizeGuard] },
          { path: 'subjectevent', component: SubjecteventComponent, canActivate: [AuthorizeGuard] },
          { path: 'subjectevent/detail/:result', component: DetailSubjecteventComponent, canActivate: [AuthorizeGuard] },
          { path: 'electronicbook/invitedetail/:id', component: DetailInvitedElectronicBookComponent, canActivate: [AuthorizeGuard] },
          { path: 'exportrequestorderforadminprovince', component: RequestOrderExport1Component, canActivate: [AuthorizeGuard] },
          { path: 'exportrequestorderforinspector', component: RequestOrderExport3Component, canActivate: [AuthorizeGuard] },
          { path: 'log', component: LogComponent, canActivate: [AuthorizeGuard] },
          { path: 'answersubject/edit/:id', component: AnswerSubjectEditComponent, canActivate: [AuthorizeGuard] },
          { path: 'answerpeople/editcentralpolicyprovince/:result/:inspectionplaneventid', component: AnswerCentralPolicyProvinceEditComponent, canActivate: [AuthorizeGuard] },
          { path: 'electronicbook/provincedetail/:id', component: ElectronicBookProvinceDetailComponent, canActivate: [AuthorizeGuard] },
          { path: 'reportimport/detail/:id', component: ReportImportDeatailComponent, canActivate: [AuthorizeGuard] },
          { path: 'commanderreport/detail/:id', component: CommanderReportDetailComponent, canActivate: [AuthorizeGuard] },
          { path: 'reportperformance', component: ReportPerformanceComponent, canActivate: [AuthorizeGuard] },
          { path: 'reportsuggestions', component: ReportSuggestionsComponent, canActivate: [AuthorizeGuard] },
          { path: 'reportsuggestionsresult', component: ReportSuggestionResultComponent, canActivate: [AuthorizeGuard] },
          { path: 'reportquestionnaire', component: ReportQuestionnaireComponent, canActivate: [AuthorizeGuard] },
          { path: 'reportcomment', component: ReportCommentComponent, canActivate: [AuthorizeGuard] },
          { path: 'electronicbook/other', component: ElectronicBookOtherComponent, canActivate: [AuthorizeGuard] },
          { path: 'electronicbook/otherdetail/:id', component: ElectronicBookOtherDetailComponent, canActivate: [AuthorizeGuard] },
          { path: 'centralpolicy/detailrowcentralpolicy/:id', component: DetailrowCentralPolicyComponent, canActivate: [AuthorizeGuard] },
          { path: 'ministry/:id/department', component: DepartmentComponent, canActivate: [AuthorizeGuard] },
          { path: 'ministry/department/:id/provincialdepartment', component: ProvincialDepartmentComponent, canActivate: [AuthorizeGuard] },
          { path: 'electronicbookdepartment', component: ElectronicBookDepartmentComponent, canActivate: [AuthorizeGuard] },
          { path: 'electronicbook/departmentdetail/:id', component: ElectronicBookDepartmentDetailComponent, canActivate: [AuthorizeGuard] },
          { path: 'supportgovernment/circularletter', component: CircularletterComponent, canActivate: [AuthorizeGuard] },
          { path: 'supportgovernment/informationinspection', component: InformationinspectionComponent, canActivate: [AuthorizeGuard] },
          { path: 'supportgovernment/approvaldocument', component: ApprovaldocumentComponent, canActivate: [AuthorizeGuard] },
          { path: 'fiscalyearnew', component: FiscalyearnewComponent, canActivate: [AuthorizeGuard] },
          { path: 'side', component: SideComponent, canActivate: [AuthorizeGuard] },
          { path: 'sector', component: SectorComponent, canActivate: [AuthorizeGuard] },
          { path: 'typeexamibationplan', component: TypeexamibationplanComponent, canActivate: [AuthorizeGuard] },
          { path: 'provincesgroup', component: ProvincesgroupComponent, canActivate: [AuthorizeGuard] },
          { path: 'allreport', component: AllReportComponent, canActivate: [AuthorizeGuard] },
          { path: 'allreport/detail/:id', component: AllReportDetailComponent, canActivate: [AuthorizeGuard] },
          { path: 'answerrecommendationinspector', component: AnswerRecommendationinSpectorComponent, canActivate: [AuthorizeGuard] },
          { path: 'answerrecommendationinspector/detail/:id', component: AnswerRecommendationinSpectorDetailComponent, canActivate: [AuthorizeGuard] },
          { path: 'answerrecommendationinspector/edit/:id', component: AnswerRecommendationinSpectorEditComponent, canActivate: [AuthorizeGuard] },
          { path: 'nameplate/:id', component: NamePlateComponent, canActivate: [AuthorizeGuard] },
          { path: 'nameplatepreview', component: NamePlatePreviewComponent, canActivate: [AuthorizeGuard] },
          { path: 'namelabelpreview', component: NameLabelPreviewComponent, canActivate: [AuthorizeGuard] },
          { path: 'training/login/list/:trainingid', component: TrainingLoginListComponent, canActivate: [AuthorizeGuard] },
          { path: 'training/login/list/detail/:programid', component: TrainingLoginListDetailComponent, canActivate: [AuthorizeGuard] },
          { path: 'regionalagency', component: RegionalagencyComponent, canActivate: [AuthorizeGuard] },
          { path: 'electronicbookall', component: ElectronicBookAllComponent, canActivate: [AuthorizeGuard] },
          { path: 'electronicbookall/detail/:id', component: ElectronicBookAllDetailComponent, canActivate: [AuthorizeGuard] },
          { path: 'iframe', loadChildren: () => import('./iframe/iframe.module').then(m => m.IframeModule), canActivate: [AuthorizeGuard] },
        ]
      },
      { path: 'train/detail/:id', component: DetailDefaultLayoutTrainComponent },
      { path: 'train/detail/phase/:id', component: ProgramDefaultLayoutTrainComponent },
      { path: 'train/register/:id', component: RegisterDefaultLayoutTrainComponent, canActivate: [AuthorizeGuard] },
      { path: 'train/register-external/:id', component: RegisterDefaultLayoutTrainComponent },
      { path: 'train/list/:id', component: ListDefaultLayoutTrainComponent },
      { path: 'train/survey/:id/:suveyjoinlecid', component: SurveyDefaultLayoutTrainComponent, canActivate: [AuthorizeGuard] },
      { path: 'train/register-success/:id', component: SuccessDefaultLayoutTrainComponent, canActivate: [AuthorizeGuard] },
      {
        path: 'train', component: DefaultLayoutTrainComponent,
        data: { title: 'หน้าหลัก' },
        children: [
          { path: 'maintrain', component: TrainComponent, canActivate: [AuthorizeGuard] }, //ออเทน
        ]
      },
      { path: 'training/login/:phaseid/:dateid/:datetype', component: TrainingLoginComponent, canActivate: [AuthorizeGuard] },
      { path: 'training/login-success', component: TrainingLoginSuccessComponent, canActivate: [AuthorizeGuard] },
    ]),
    TimepickerModule.forRoot(),
    FusionChartsModule,
    ModalModule.forRoot(),
    // SelectSSSModule
    NgSelectModule,
    NgbModule,
    TooltipModule.forRoot(),
    QRCodeModule,
  ], exports: [
    ThaiDatePipe,
    SnotifyModule,
    // DragDropModule,
    // DemoMaterialModule,
    // SelectSSSModule
    // DatePipe
  ],
  entryComponents: [ConfirmationDialogComponent],
  providers: [
    { provide: 'SnotifyToastConfig', useValue: ToastDefaults },
    SnotifyService, NotificationService,
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true },
    ExcelGeneraterService,
    DatePipe, CookieService,
    ProvinceService,
    NotofyService,
    ConfirmationDialogService,
    // UserManager,
    SortPipe,
    // ConfirmationDialogService
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],

  bootstrap: [AppComponent]
})
export class AppModule { }

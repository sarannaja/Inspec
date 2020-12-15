import { BrowserModule } from '@angular/platform-browser';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppComponent } from './app.component';
import { ApiAuthorizationModule } from 'src/api-authorization-new/api-authorization.module';
import { ModalModule } from 'ngx-bootstrap/modal';
// import { SelectModule } from 'ng-select'
import { MyDatePickerTHModule } from 'mydatepicker-th';
import { DataTablesModule } from 'angular-datatables';



import { SnotifyModule, ToastDefaults, SnotifyService } from 'ng-snotify';
import { NotificationService } from './services/Pipe/alert.service';
import { NgxSpinnerModule } from "ngx-spinner";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { OtpsComponent } from './external-organization/otps/otps.component';
import { Opm1111Component } from './external-organization/opm1111/opm1111.component';
import { GgcOpmComponent } from './external-organization/ggc-opm/ggc-opm.component';
// import { ChartsModule } from 'ng2-charts';
import { ExcelGeneraterService } from './services/excel-generater.service';
import { DatePipe } from '@angular/common';
import { CookieService } from 'ngx-cookie-service'
import { ProvinceService } from './services/province.service';
import { NgSelectModule } from '@ng-select/ng-select';
import { NotofyService } from './services/notofy.service';
import { TimepickerModule } from 'ngx-bootstrap/timepicker';
import { NgxPrintModule } from 'ngx-print';
import { NgxMaterialTimepickerModule } from 'ngx-material-timepicker';
import { ConfirmationDialogService } from './services/confirmation-dialog/confirmation-dialog.service';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

// import { PopoverModule } from 'ngx-bootstrap/popover';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { SortPipe } from './services/Pipe/sort';

import { FusionChartsModule } from "angular-fusioncharts";

import * as FusionCharts from "fusioncharts";
import * as charts from "fusioncharts/fusioncharts.charts";
import * as FusionTheme from "fusioncharts/themes/fusioncharts.theme.fusion";

// Pass the fusioncharts library and chart modules
FusionChartsModule.fcRoot(FusionCharts, charts, FusionTheme);
import { QRCodeComponent, QRCodeModule } from 'angularx-qrcode';
import { ConfirmationDialogComponent } from './services/confirmation-dialog/confirmation-dialog.component';

import { AppRoutingModule } from './app-routing.module';
import { AdviserCivilSectorComponent } from './adviser-civil-sector/adviser-civil-sector.component';
import { AllReportIframeDetailComponent } from './all-report-iframe/all-report-iframe-detail/all-report-iframe-detail.component';
import { VilageComponent } from './vilage/vilage.component';
import { AllReportIframeComponent } from './all-report-iframe/all-report-iframe.component';
import { AllReportDetailComponent } from './all-report/all-report-detail/all-report-detail.component';
import { AllReportComponent } from './all-report/all-report.component';
import { AnswerCentralPolicyProvinceEditComponent } from './answer-subject/answer-central-policy-province-edit/answer-central-policy-province-edit.component';
import { AnswerCentralPolicyProvinceComponent } from './answer-subject/answer-central-policy-province/answer-central-policy-province.component';
import { AnswerOutsideThankComponent } from './answer-subject/answer-outside-thank/answer-outside-thank.component';
import { AnswerOutsiderComponent } from './answer-subject/answer-outsider/answer-outsider.component';
import { AnswerPeopleDetailComponent } from './answer-subject/answer-people-detail/answer-people-detail.component';
import { AnswerPeopleListComponent } from './answer-subject/answer-people-list/answer-people-list.component';
import { AnswerPeopleComponent } from './answer-subject/answer-people/answer-people.component';
import { AnswerRecommendationinSpectorDetailComponent } from './answer-subject/answer-recommendationin-spector-detail/answer-recommendationin-spector-detail.component';
import { AnswerRecommendationinSpectorEditComponent } from './answer-subject/answer-recommendationin-spector-edit/answer-recommendationin-spector-edit.component';
import { AnswerRecommendationinSpectorComponent } from './answer-subject/answer-recommendationin-spector/answer-recommendationin-spector.component';
import { AnswerSubjectDetailComponent } from './answer-subject/answer-subject-detail/answer-subject-detail.component';
import { AnswerSubjectEditComponent } from './answer-subject/answer-subject-edit/answer-subject-edit.component';
import { AnswerSubjectListComponent } from './answer-subject/answer-subject-list/answer-subject-list.component';
import { AnswerSubjectComponent } from './answer-subject/answer-subject.component';
import { ApprovaldocumentComponent } from './approvaldocument/approvaldocument.component';
import { CabinetComponent } from './cabinet/cabinet.component';
import { CalendarUserComponent } from './calendar-user/calendar-user.component';
import { AcceptCentralPolicyComponent } from './central-policy/accept-central-policy/accept-central-policy.component';
import { ReportCentralPolicyComponent } from './central-policy/accept-central-policy/report-central-policy/report-central-policy.component';
import { CentralPolicyFiscalyearComponent } from './central-policy/central-policy-fiscalyear/central-policy-fiscalyear.component';
import { CentralPolicyComponent } from './central-policy/central-policy.component';
import { CreateCentralPolicyComponent } from './central-policy/create-central-policy/create-central-policy.component';
import { DetailCentralPolicyProvinceDepartmentComponent } from './central-policy/detail-central-policy-province-department/detail-central-policy-province-department.component';
import { DetailCentralPolicyProvinceMinistryComponent } from './central-policy/detail-central-policy-province-ministry/detail-central-policy-province-ministry.component';
import { DetailCentralPolicyProvinceComponent } from './central-policy/detail-central-policy-province/detail-central-policy-province.component';
import { DetailCentralPolicyComponent } from './central-policy/detail-central-policy/detail-central-policy.component';
import { DetailrowCentralPolicyComponent } from './central-policy/detailrow-central-policy/detailrow-central-policy.component';
import { EditCentralPolicyComponent } from './central-policy/edit-central-policy/edit-central-policy.component';
import { UserCentralPolicyComponent } from './central-policy/user-central-policy/user-central-policy.component';
import { CircularletterComponent } from './circularletter/circularletter.component';
import { CommanderReportDetailComponent } from './commander-report/commander-report-detail/commander-report-detail.component';
import { CommanderReportComponent } from './commander-report/commander-report.component';
import { CounterComponent } from './counter/counter.component';
import { ConditionDefaultLayoutTrainComponent } from './default-layout-train/condition-default-layout-train/condition-default-layout-train.component';
import { DefaultLayoutTrainComponent } from './default-layout-train/default-layout-train.component';
import { DetailDefaultLayoutTrainComponent } from './default-layout-train/detail-default-layout-train/detail-default-layout-train.component';
import { ListDefaultLayoutTrainComponent } from './default-layout-train/list-default-layout-train/list-default-layout-train.component';
import { ProgramDefaultLayoutTrainComponent } from './default-layout-train/program-default-layout-train/program-default-layout-train.component';
import { RegisterDefaultLayoutTrainComponent } from './default-layout-train/register-default-layout-train/register-default-layout-train.component';
import { SuccessDefaultLayoutTrainComponent } from './default-layout-train/success-default-layout-train/success-default-layout-train.component';
import { SurveyDefaultLayoutTrainComponent } from './default-layout-train/survey-default-layout-train/survey-default-layout-train.component';
import { DefaultLayoutComponent } from './default-layout/default-layout/default-layout.component';
import { DepartmentComponent } from './department/department.component';
import { DistrictComponent } from './district/district.component';
import { DocumenttemplateComponent } from './documenttemplate/documenttemplate.component';
import { ElectronicBookAllDetailComponent } from './electronic-book-all/electronic-book-all-detail/electronic-book-all-detail.component';
import { ElectronicBookAllComponent } from './electronic-book-all/electronic-book-all.component';
import { ElectronicBookDepartmentDetailComponent } from './electronic-book-department/electronic-book-department-detail/electronic-book-department-detail.component';
import { ElectronicBookDepartmentComponent } from './electronic-book-department/electronic-book-department.component';
import { ElectronicBookOtherDetailComponent } from './electronic-book-province/electronic-book-other/electronic-book-other-detail/electronic-book-other-detail.component';
import { ElectronicBookOtherComponent } from './electronic-book-province/electronic-book-other/electronic-book-other.component';
import { ElectronicBookProvinceDetailComponent } from './electronic-book-province/electronic-book-province-detail/electronic-book-province-detail.component';
import { ElectronicBookProvinceComponent } from './electronic-book-province/electronic-book-province.component';
import { CreateElectronicBookComponent } from './electronic-book/create-electronic-book/create-electronic-book.component';
import { DetailElectronicBookComponent } from './electronic-book/detail-electronic-book/detail-electronic-book.component';
import { EditElectronicBookComponent } from './electronic-book/edit-electronic-book/edit-electronic-book.component';
import { ElectronicBookComponent } from './electronic-book/electronic-book.component';
import { DetailInvitedElectronicBookComponent } from './electronic-book/invited-electronic-book/detail-invited-electronic-book/detail-invited-electronic-book.component';
import { InvitedElectronicBookComponent } from './electronic-book/invited-electronic-book/invited-electronic-book.component';
import { DetailExecutiveOrderComponent } from './executive-order/detail-executive-order/detail-executive-order.component';
import { ExecutiveOrderExport1Component } from './executive-order/executive-order-export1/executive-order-export1.component';
import { ExecutiveOrderExport3Component } from './executive-order/executive-order-export3/executive-order-export3.component';
import { ExecutiveOrderComponent } from './executive-order/executive-order.component';
import { ExternalRegisterComponent } from './external-register/external-register.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { DetailFiscalyearComponent } from './fiscalyear/detail-fiscalyear/detail-fiscalyear.component';
import { FiscalyearComponent } from './fiscalyear/fiscalyear.component';
import { FiscalyearnewComponent } from './fiscalyearnew/fiscalyearnew.component';
import { DetailGovernmentinspectionareaComponent } from './governmentinspectionarea/detail-governmentinspectionarea/detail-governmentinspectionarea.component';
import { GovernmentinspectionareaComponent } from './governmentinspectionarea/governmentinspectionarea.component';
import { GovernmentinspectionplanComponent } from './governmentinspectionplan/governmentinspectionplan.component';
import { HomeComponent } from './home/home.component';
import { InfoDepartmentComponent } from './info-department/info-department.component';
import { InfoDistrictComponent } from './info-district/info-district.component';
import { InfoMinistryComponent } from './info-ministry/info-ministry.component';
import { InfoSubdistrictComponent } from './info-subdistrict/info-subdistrict.component';
import { InfoVillageComponent } from './info-village/info-village.component';
import { InfomationProvinceComponent } from './infomation-province/infomation-province.component';
import { InformationinspectionComponent } from './informationinspection/informationinspection.component';
import { InformationoperationComponent } from './informationoperation/informationoperation.component';
import { InspectionPlanDepartmentComponent } from './inspection-plan-department/inspection-plan-department.component';
import { InspectionPlanEventAllComponent } from './inspection-plan-event-all/inspection-plan-event-all.component';
import { CreateInspectionPlanEventComponent } from './inspection-plan-event/create-inspection-plan-event/create-inspection-plan-event.component';
import { InspectionPlanEventProvinceComponent } from './inspection-plan-event/inspection-plan-event-province/inspection-plan-event-province.component';
import { InspectionPlanEventComponent } from './inspection-plan-event/inspection-plan-event.component';
import { ReportInspectionPlanEventComponent } from './inspection-plan-event/report-inspection-plan-event/report-inspection-plan-event.component';
import { InspectionPlanMinistryComponent } from './inspection-plan-ministry/inspection-plan-ministry.component';
import { CreateInspectionPlanComponent } from './inspection-plan/create-inspection-plan/create-inspection-plan.component';
import { EditInspectionPlanComponent } from './inspection-plan/edit-inspection-plan/edit-inspection-plan.component';
import { InspectionPlanComponent } from './inspection-plan/inspection-plan.component';
import { InspectionorderComponent } from './inspectionorder/inspectionorder.component';
import { InspectorComponent } from './inspector/inspector.component';
import { InstructionorderComponent } from './instructionorder/instructionorder.component';
import { LogComponent } from './log/log.component';
import { MainComponent } from './main/main.component';
import { MeetinginformationComponent } from './meetinginformation/meetinginformation.component';
import { MenuComponent } from './menu/menu.component';
import { MinistermonitoringComponent } from './ministermonitoring/ministermonitoring.component';
import { MinistryComponent } from './ministry/ministry.component';
import { NameLabelPreviewComponent } from './name-plate/name-label-preview/name-label-preview.component';
import { NamePlatePreviewComponent } from './name-plate/name-plate-preview/name-plate-preview.component';
import { NamePlateComponent } from './name-plate/name-plate.component';
import { NationalstrategyComponent } from './nationalstrategy/nationalstrategy.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { OfficerInspectionComponent } from './officer-inspection/officer-inspection.component';
import { PremierorderComponent } from './premierorder/premierorder.component';
import { ProvinceComponent } from './province/province.component';
import { ProvincesgroupComponent } from './provincesgroup/provincesgroup.component';
import { ProvincialDepartmentComponent } from './provincialdepartment/provincialdepartment.component';
import { RegionComponent } from './region/region.component';
import { RegionalagencyComponent } from './regionalagency/regionalagency.component';
import { ReportExportComponent } from './report-export/report-export.component';
import { ReportImportDeatailComponent } from './report-import/report-import-deatail/report-import-deatail.component';
import { ReportImportComponent } from './report-import/report-import.component';
import { ReportCommentComponent } from './report/report-comment/report-comment.component';
import { ReportPerformanceComponent } from './report/report-performance/report-performance.component';
import { ReportQuestionnaireComponent } from './report/report-questionnaire/report-questionnaire.component';
import { ReportSubjectComponent } from './report/report-subject/report-subject.component';
import { ReportSuggestionResultComponent } from './report/report-suggestion-result/report-suggestion-result.component';
import { ReportSuggestionsComponent } from './report/report-suggestions/report-suggestions.component';
import { RequestOrderExport1Component } from './request-order/request-order-export1/request-order-export1.component';
import { RequestOrderExport3Component } from './request-order/request-order-export3/request-order-export3.component';
import { RequestOrderComponent } from './request-order/request-order.component';
import { SectorComponent } from './sector/sector.component';
import { DateLengthComponent } from './services/components/date-length/date-length.component';
import { NumberDirective } from './services/numbers-only.directive';
import { ThaiDatePipe } from './services/Pipe/thaidate.service';
import { SideComponent } from './side/side.component';
import { StatepolicyComponent } from './statepolicy/statepolicy.component';
import { SubdistrictComponent } from './subdistrict/subdistrict.component';
import { DetailSubjectComponent } from './subject/detail-subject/detail-subject.component';
import { EditSubjectComponent } from './subject/edit-subject/edit-subject.component';
import { GraphAnswerSubjectComponent } from './subject/graph-answer-subject/graph-answer-subject.component';
import { SubjectComponent } from './subject/subject.component';
import { ChartComponent } from './subjectevent/detail-subjectevent/chart/chart.component';
import { DetailSubjecteventComponent } from './subjectevent/detail-subjectevent/detail-subjectevent.component';
import { SubjecteventComponent } from './subjectevent/subjectevent.component';
import { SubquestionComponent } from './subquestion/subquestion.component';
import { SupportGovernmentComponent } from './support-government/support-government.component';
import { TemplateElectronicComponent } from './template-electronic/template-electronic.component';
import { TrainComponent } from './train/train.component';
import { TrainingConditionComponent } from './training-condition/training-condition.component';
import { ListTrainingDocumentComponent } from './training-document/list-training-document/list-training-document.component';
import { TrainingDocumentComponent } from './training-document/training-document.component';
import { TrainingLecturerListComponent } from './training-lecturerlist/training-lecturerlist.component';
import { TrainingLecturerTypeComponent } from './training-lecturertype/training-lecturertype.component';
import { TrainingLoginListDetailComponent } from './training-login/training-login-list/training-login-list-detail/training-login-list-detail.component';
import { TrainingLoginListComponent } from './training-login/training-login-list/training-login-list.component';
import { TrainingLoginSuccessComponent } from './training-login/training-login-success/training-login-success.component';
import { TrainingLoginComponent } from './training-login/training-login.component';
import { TrainingManageComponent } from './training-manage/training-manage.component';
import { TrainingProcessingComponent } from './training-processing/training-processing.component';
import { TrainingProgramLoginComponent } from './training-programlogin/training-programlogin.component';
import { TrainingProgramTypeComponent } from './training-programtype/training-programtype.component';
import { GroupTrainingRegisterComponent } from './training-register/group-training-register/group-training-register.component';
import { ListTrainingRegisterComponent } from './training-register/list-training-register/list-training-register.component';
import { ProgramTrainingRegisterComponent } from './training-register/program-training-register/program-training-register.component';
import { TrainingRegisterComponent } from './training-register/training-register.component';
import { HistoryTrainingReportComponent } from './training-report/history-training-report/history-training-report.component';
import { ListRegisterTrainingReportComponent } from './training-report/list-training-report/list-register-training-report/list-register-training-report.component';
import { ListTrainingReportComponent } from './training-report/list-training-report/list-training-report.component';
import { RateloginTrainingReportComponent } from './training-report/ratelogin-training-report/ratelogin-training-report.component';
import { TrainingReportComponent } from './training-report/training-report.component';
import { ChartTrainingSurveyComponent } from './training-survey/chart-training-survey/chart-training-survey.component';
import { ListTrainingSurveyComponent } from './training-survey/list-training-survey/list-training-survey.component';
import { PreviewTrainingSurveyComponent } from './training-survey/preview-training-survey/preview-training-survey.component';
import { TrainingSurveyComponent } from './training-survey/training-survey.component';
import { TrainingSurveyLecturerComponent } from './training-surveylecturer/training-surveylecturer.component';
import { CreateTrainingComponent } from './training/create-training/create-training.component';
import { EditTrainingComponent } from './training/edit-training/edit-training.component';
import { LecturerTrainingComponent } from './training/lecturer-training/lecturer-training.component';
import { PhaseTrainingComponent } from './training/phase-training/phase-training.component';
import { PlanTrainingComponent } from './training/plan-training/plan-training.component';
import { ProgramTrainingComponent } from './training/program-training/program-training.component';
import { RegisterTrainingComponent } from './training/register-training/register-training.component';
import { TrainingComponent } from './training/training.component';
import { TrainingSummaryReportComponent } from './training-summary-report/training-summary-report.component';
import { TrainingSummaryReportPhaseComponent } from './training-summary-report/training-summary-report-phase/training-summary-report-phase.component';
import { TrainingSummaryReportGroupComponent } from './training-summary-report/training-summary-report-group/training-summary-report-group.component';
import { TrainingSummaryReportProjectComponent } from './training-summary-report-project/training-summary-report-project.component';


import { TypeexamibationplanComponent } from './typeexamibationplan/typeexamibationplan.component';
import { UserComponent } from './user/user.component';
import { UserManager } from 'oidc-client';
import { HttpRequestInterceptor } from 'src/api-authorization-sss/HttpRequestInterceptor';
import { AuthorizeInterceptor } from 'src/api-authorization-new/authorize.interceptor';
import { BeforeCentralPolicyComponent } from './central-policy/before-central-policy/before-central-policy.component';
import { BeforeSubjectComponent } from './subject/before-subject/before-subject.component';



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

    TrainingLecturerListComponent,
    RateloginTrainingReportComponent,
    TrainingSurveyLecturerComponent,
    TrainingProcessingComponent,
    EditTrainingComponent,
    TrainingProgramTypeComponent,
    ConditionDefaultLayoutTrainComponent,
    TrainingLecturerTypeComponent,
    TrainingSummaryReportComponent,
    TrainingSummaryReportPhaseComponent,
    TrainingSummaryReportGroupComponent,
    TrainingSummaryReportProjectComponent,
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
    StatepolicyComponent,
    DocumenttemplateComponent,
    MeetinginformationComponent,
    ReportInspectionPlanEventComponent,
    ExecutiveOrderExport1Component,
    ExecutiveOrderExport3Component,
    InfoMinistryComponent,
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
    GovernmentinspectionareaComponent, //yochigang20201106
    DetailGovernmentinspectionareaComponent, //yochigang20201106
    AllReportIframeComponent,
    AllReportIframeDetailComponent,
    VilageComponent,
    // NewLoginComponent,
    DateLengthComponent ,
    BeforeCentralPolicyComponent,
    BeforeSubjectComponent,
  ],
  
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    // BrowserModule,
    HttpClientModule,
    FormsModule,
    ApiAuthorizationModule,
    // SelectModule,
    ReactiveFormsModule,
    // ChartsModule,
    NgxPrintModule,
    MyDatePickerTHModule,
    // BrowserModule,
    SnotifyModule,
    NgxSpinnerModule,
    // PopoverModule.forRoot(),
    DataTablesModule,
    BrowserAnimationsModule,
    NgxMaterialTimepickerModule,
    TimepickerModule.forRoot(),
    FusionChartsModule,
    ModalModule.forRoot(),
    // SelectSSSModule
    NgSelectModule,
    NgbModule,
    TooltipModule.forRoot(),
    QRCodeModule,
    AppRoutingModule

  ], exports: [
    SnotifyModule,
    // DragDropModule,
    // DemoMaterialModule,
    // SelectSSSModule
    // DatePipe
  ],
  entryComponents: [
    ConfirmationDialogComponent, QRCodeComponent,
  ],
  providers: [
    // { provide: HTTP_INTERCEPTORS, useClass: HttpRequestInterceptor, multi: true },
    { provide: 'SnotifyToastConfig', useValue: ToastDefaults },
    SnotifyService, NotificationService,
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: HttpRequestInterceptor, multi: true },
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

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

import { EditTrainingComponent } from './training/edit-training/edit-training.component';
import { TrainingProgramTypeComponent } from './training-programtype/training-programtype.component';
import { TrainingLecturerTypeComponent } from './training-lecturertype/training-lecturertype.component';
import { ConditionDefaultLayoutTrainComponent } from './default-layout-train/condition-default-layout-train/condition-default-layout-train.component';
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
import { RegionalagencyComponent } from './regionalagency/regionalagency.component';
import { ConfirmationDialogComponent } from './services/confirmation-dialog/confirmation-dialog.component';
import { RegisterTrainingComponent } from './training/register-training/register-training.component';
import { MenuComponent } from './menu/menu.component';
import { ElectronicBookAllComponent } from './electronic-book-all/electronic-book-all.component';
import { ElectronicBookAllDetailComponent } from './electronic-book-all/electronic-book-all-detail/electronic-book-all-detail.component';
import { GovernmentinspectionareaComponent } from './governmentinspectionarea/governmentinspectionarea.component'; //yochigang20201106
import { DetailGovernmentinspectionareaComponent } from './governmentinspectionarea/detail-governmentinspectionarea/detail-governmentinspectionarea.component';//yochigang20201106
import { AllReportIframeComponent } from './all-report-iframe/all-report-iframe.component';
import { AllReportIframeDetailComponent } from './all-report-iframe/all-report-iframe-detail/all-report-iframe-detail.component';
import { AppRoutingModule } from './app-routing.module';


const ExternalOrganization = [
  GgcOpmComponent, Opm1111Component, OtpsComponent
]

@NgModule({
  declarations: [],

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
    // RouterModule.forRoot(
    // ),
    TimepickerModule.forRoot(),
    FusionChartsModule,
    ModalModule.forRoot(),
    // SelectSSSModule
    NgSelectModule,
    NgbModule,
    TooltipModule.forRoot(),
    QRCodeModule,
    NgxPrintModule,
    AppRoutingModule

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

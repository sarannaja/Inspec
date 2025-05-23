import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';
import { AuthorizeGuard } from 'src/api-authorization-new/authorize.guard';
// import { LogoutComponent } from 'src/api-authorization/logout/logout.component';
import { AdviserCivilSectorComponent } from './adviser-civil-sector/adviser-civil-sector.component';
import { AllReportIframeDetailComponent } from './all-report-iframe/all-report-iframe-detail/all-report-iframe-detail.component';
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
import { SideComponent } from './side/side.component';
import { StatepolicyComponent } from './statepolicy/statepolicy.component';
import { SubdistrictComponent } from './subdistrict/subdistrict.component';
import { DetailSubjectComponent } from './subject/detail-subject/detail-subject.component';
import { EditSubjectComponent } from './subject/edit-subject/edit-subject.component';
import { GraphAnswerSubjectComponent } from './subject/graph-answer-subject/graph-answer-subject.component';
import { SubjectComponent } from './subject/subject.component';
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
import { TrainingComponent } from './training/training.component';
import { TrainingSummaryReportComponent } from './training-summary-report/training-summary-report.component';
import { TrainingSummaryReportPhaseComponent } from './training-summary-report/training-summary-report-phase/training-summary-report-phase.component';
import { TrainingSummaryReportGroupComponent } from './training-summary-report/training-summary-report-group/training-summary-report-group.component';
import { TrainingSummaryReportProjectComponent } from './training-summary-report-project/training-summary-report-project.component';



import { TypeexamibationplanComponent } from './typeexamibationplan/typeexamibationplan.component';
import { UserComponent } from './user/user.component';
import { AppComponent } from './app.component';
import { CabinetComponent } from './cabinet/cabinet.component';
import { HomeComponent } from './home/home.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { DateLengthComponent } from './services/components/date-length/date-length.component';
import { ConfirmationDialogComponent } from './services/confirmation-dialog/confirmation-dialog.component';
import { NumberDirective } from './services/numbers-only.directive';
import { SortPipe } from './services/Pipe/sort';
import { ThaiDatePipe } from './services/Pipe/thaidate.service';
import { ChartComponent } from './subjectevent/detail-subjectevent/chart/chart.component';
import { RegisterTrainingComponent } from './training/register-training/register-training.component';
import { VilageComponent } from './vilage/vilage.component';
import { BeforeLoginComponent } from './before-login/before-login.component';
import { BeforeCentralPolicyComponent } from './central-policy/before-central-policy/before-central-policy.component';
import { BeforeSubjectComponent } from './subject/before-subject/before-subject.component';
import { GovernmentinspectionplaniframeComponent } from './governmentinspectionplaniframe/governmentinspectionplaniframe.component'; //แผนการตรวจราชการประจำปี iframe
import { InformationinspectioniframeComponent } from './informationinspectioniframe/informationinspectioniframe.component';
import { MaincabinetComponent } from './main-cabinet/main-cabinet.component';
import { RetrospectiveReportComponent } from './retrospective-report/retrospective-report.component';
import { TrainingProjectReportComponent } from './training-project-report/training-project-report.component';

const routes: Routes = [
  { path: '', redirectTo: '/', pathMatch: 'full' },
  { path: '', loadChildren: () => import('../app/before-login/before-login.module').then(m => m.BeforeLoginModule) },

  { path: 'main', redirectTo: 'main/thaimap', pathMatch: 'full' },

  // { path: '', redirectTo: 'video', pathMatch: 'full' },
  // { path: ApplicationPaths.Login, component: NewLoginComponent },
  // { path: ApplicationPaths.Login, component: NewLoginComponent },
  // { path: 'xlogin', component: LoginComponent },
  // { path: ApplicationPaths.Login, loadChildren: () => import('../api-authorization/new-login/new-login.module').then(m => m.NewLoginModule) },

  { path: 'counter', component: CounterComponent },
  { path: 'fetch-data', component: FetchDataComponent, canActivate: [AuthorizeGuard] },
  // { path: 'login', component: LoginComponent },
  { path: 'answersubject/outsider/:id/:userid', component: AnswerOutsiderComponent },
  { path: 'ty', component: AnswerOutsideThankComponent },
  { path: 'training/external/register', component: ExternalRegisterComponent },
  { path: 'inspectionplanevent/all/noauth', component: InspectionPlanEventAllComponent },

  { path: 'noauth/inspectionplan/:id/:provinceid/:watch', component: InspectionPlanComponent },
  { path: 'noauth/inspectionplan/inspectorministry/:id/:provinceid/:watch', component: InspectionPlanMinistryComponent },
  { path: 'noauth/inspectionplan/inspectordepartment/:id/:provinceid/:watch', component: InspectionPlanDepartmentComponent },

  { path: 'centralpolicy/detailcentralpolicyprovince/noauth/:result', component: DetailCentralPolicyProvinceComponent },
  { path: 'centralpolicy/detailcentralpolicyprovince/ministry/noauth/:result', component: DetailCentralPolicyProvinceMinistryComponent },
  { path: 'centralpolicy/detailcentralpolicyprovince/department/noauth/:result', component: DetailCentralPolicyProvinceDepartmentComponent },
  // {
  //   path: 'main/video', loadChildren: () => import('./video/video.module').then(m => m.VideoModule)
  // },
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


      // { path: 'main', component: MainComponent, canActivate: [AuthorizeGuard] }, //ออเทน
      // { path: '', loadChildren: () => import('../api-authorization/api-authorization.module') },
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
      { path: 'district/:id/:name', component: DistrictComponent, canActivate: [AuthorizeGuard] },
      { path: 'subdistrict/:id/:provincename/:districtname', component: SubdistrictComponent, canActivate: [AuthorizeGuard] },
      { path: 'vilage/:iddistrict/:subdistrictid/:provincename/:districtname/:subdistrictname', component: VilageComponent, canActivate: [AuthorizeGuard] },
      { path: 'cabinetserver', component: CabinetComponent, canActivate: [AuthorizeGuard] },
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
      {
        path: 'training/programlogin/:id', loadChildren: () => import('./training-programlogin/training-programlogin.module')
          .then(m => m.TrainingProgramloginModule),
          //canActivate: [AuthorizeGuard]
      },
      // { path: 'training/programlogin/:id', component: TrainingProgramLoginComponent, canActivate: [AuthorizeGuard] },
      { path: 'training/lecturerlist/:id', component: TrainingLecturerListComponent, canActivate: [AuthorizeGuard] },
      { path: 'training/surveylecturer', component: TrainingSurveyLecturerComponent, canActivate: [AuthorizeGuard] },
      { path: 'training/survey/processing/:id', component: TrainingProcessingComponent, canActivate: [AuthorizeGuard] },
      { path: 'training/edittraining/:id', component: EditTrainingComponent, canActivate: [AuthorizeGuard] },

      { path: 'training/programtype', component: TrainingProgramTypeComponent, canActivate: [AuthorizeGuard] },
      { path: 'training/lecturertype', component: TrainingLecturerTypeComponent, canActivate: [AuthorizeGuard] },
      { path: 'training/report/summary/phase/:trainingid', component: TrainingSummaryReportComponent, canActivate: [AuthorizeGuard] },
      { path: 'training/report/summary/phase/group/:trainingid/:phaseid', component: TrainingSummaryReportPhaseComponent, canActivate: [AuthorizeGuard] },
      { path: 'training/report/summary/phase/group/detail/:trainingid/:phaseid/:group', component: TrainingSummaryReportGroupComponent, canActivate: [AuthorizeGuard] },
      { path: 'training/report/summary/project/:trainingid', component: TrainingSummaryReportProjectComponent, canActivate: [AuthorizeGuard] },



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
      {
        path: 'video', loadChildren: () => import('./video/video.module').then(m => m.VideoModule)
      },
      { path: 'inspector', component: InspectorComponent, canActivate: [AuthorizeGuard] },
      { path: 'inspector', component: InspectorComponent, canActivate: [AuthorizeGuard] },
      { path: 'executiveorder', component: ExecutiveOrderComponent, canActivate: [AuthorizeGuard] },
      { path: 'executiveorder/detailexecutiveorder/:id', component: DetailExecutiveOrderComponent, canActivate: [AuthorizeGuard] },
      { path: 'ministermonitoring', component: MinistermonitoringComponent, canActivate: [AuthorizeGuard] },
      { path: 'acceptcentralpolicy/:id', component: AcceptCentralPolicyComponent, canActivate: [AuthorizeGuard] },
      { path: 'usercentralpolicy', component: UserCentralPolicyComponent, canActivate: [AuthorizeGuard] },
      // { path: 'test/logout', component: LogoutComponent },
      { path: 'usercentralpolicy/:id/:provinceid', component: UserCentralPolicyComponent, canActivate: [AuthorizeGuard] },
      // { path: 'test/logout', component: LogoutComponent },
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
      {
        path: 'main', loadChildren: () => import('./external-organization/external-organization.module')
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
      { path: 'menu', component: MenuComponent, canActivate: [AuthorizeGuard] },
      { path: 'retrospective-report', component: RetrospectiveReportComponent, canActivate: [AuthorizeGuard] },
      { path: 'training-project-report', component: TrainingProjectReportComponent, canActivate: [AuthorizeGuard] },
      { path: 'supportgovernment/governmentinspectionarea', component: GovernmentinspectionareaComponent, canActivate: [AuthorizeGuard] },
      { path: 'supportgovernment/governmentinspectionarea/detail/:id', component: DetailGovernmentinspectionareaComponent, canActivate: [AuthorizeGuard] },
      { path: 'maincabinet', component: MaincabinetComponent,canActivate: [AuthorizeGuard] }, //yochigang20220525
    ]
  },
  { path: 'train/detail/:id', component: DetailDefaultLayoutTrainComponent },
  { path: 'train/detail/phase/:id/:trainingid', component: ProgramDefaultLayoutTrainComponent },
  { path: 'train/detail/condition/:id', component: ConditionDefaultLayoutTrainComponent },
  { path: 'train/register/:id', component: RegisterDefaultLayoutTrainComponent, canActivate: [AuthorizeGuard] },
  { path: 'train/register-external/:id', component: RegisterDefaultLayoutTrainComponent },
  { path: 'train/list/:id', component: ListDefaultLayoutTrainComponent },
  { path: 'train/survey/:id/:suveyjoinlecid/:surveytopicid', component: SurveyDefaultLayoutTrainComponent, canActivate: [AuthorizeGuard] },
  { path: 'train/register-success/:id', component: SuccessDefaultLayoutTrainComponent, canActivate: [AuthorizeGuard] },
  {
    path: 'train', component: DefaultLayoutTrainComponent,
    data: { title: 'หน้าหลัก' },
    children: [
      { path: 'maintrain', component: TrainComponent, canActivate: [AuthorizeGuard] }, //ออเทน
    ]
  },
  { path: 'training/login/:phaseid/:dateid/:datetype', component: TrainingLoginComponent },
  { path: 'training/login-success', component: TrainingLoginSuccessComponent },
  { path: 'allreportiframe', component: AllReportIframeComponent },
  { path: 'allreportiframe/detail/:id', component: AllReportIframeDetailComponent },
  // { path: 'homepage', component: BeforeLoginComponent },
  { path: 'maincentralpolicy', component: BeforeCentralPolicyComponent },
  { path: 'maincentralpolicy/detailrowcentralpolicy/:id', component: DetailrowCentralPolicyComponent },
  { path: 'mainsubject/:id', component: BeforeSubjectComponent },
  { path: 'mainsubject/detailsubject/:id', component: DetailSubjectComponent },
  { path: 'supportgovernment/governmentinspectionareamain', component: GovernmentinspectionareaComponent },
  { path: 'supportgovernment/governmentinspectionareamain/detail/:id', component: DetailGovernmentinspectionareaComponent },
  { path: 'supportgovernment/circularlettermain', component: CircularletterComponent },
  { path: 'infoministrymain', component: InfoMinistryComponent },
  { path: 'infoministrymain/:id/infodepartment', component: InfoDepartmentComponent },
  { path: 'informationprovincemain', component: InfomationProvinceComponent },
  { path: 'infodistrictmain/:id', component: InfoDistrictComponent },
  { path: 'infosubdistrictmain/:id', component: InfoSubdistrictComponent },
  { path: 'infovillagemain/:id', component: InfoVillageComponent },
  { path: 'inspectormain', component: InspectorComponent },
  { path: 'inspectionordermain', component: InspectionorderComponent },
  { path: 'governmentinspectionplaniframe', component: GovernmentinspectionplaniframeComponent }, //แผนการตรวจราชการประจำปี iframe
  { path: 'informationinspectioniframe', component: InformationinspectioniframeComponent }, //ข้อมูลประกอบการตรวจราชการ iframe
  { path: '**', redirectTo: 'main/thaimap', pathMatch: 'full' },

]

@NgModule({

  imports: [
    // CommonModule,
    RouterModule.forRoot(routes)
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }

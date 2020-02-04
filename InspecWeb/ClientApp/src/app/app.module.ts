import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
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
import { CreateCentralPolicyComponent } from './main/create-central-policy/create-central-policy.component';
import { CreateInspectionPlanComponent } from './main/create-inspection-plan/create-inspection-plan.component';
import { EditInspectionPlanComponent } from './main/edit-inspection-plan/edit-inspection-plan.component';
import { ProvinceComponent } from './province/province.component';
import { RegionComponent } from './region/region.component';
import { ModalModule } from 'ngx-bootstrap/modal';

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
    ProvinceComponent,
    RegionComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ApiAuthorizationModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent, canActivate: [AuthorizeGuard] },

      {
        path: '',
        component: DefaultLayoutComponent,
        data: {
          title: 'หน้าหลัก'
        },
        children: [
          { path: 'main', component: MainComponent },
          { path: 'main/createcentralpolicy', component: CreateCentralPolicyComponent },
          { path: 'main/createinspectionplan', component: CreateInspectionPlanComponent },
          { path: 'main/editinspectionplan/:id', component: EditInspectionPlanComponent },
          { path: 'province', component: ProvinceComponent },
          { path: 'region',component: RegionComponent }
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

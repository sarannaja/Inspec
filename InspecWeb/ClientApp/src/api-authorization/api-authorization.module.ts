import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginMenuComponent } from './login-menu/login-menu.component';
import { LoginComponent } from './login/login.component';
import { LogoutComponent } from './logout/logout.component';
import { RouterModule } from '@angular/router';
import { ApplicationPaths } from './api-authorization.constants';
import { HttpClientModule } from '@angular/common/http';
import { AuthorizeService } from './authorize.service';
import { UserManager } from 'oidc-client';
import { NgxSpinnerModule } from "ngx-spinner";
import { NewLoginComponent } from './new-login/new-login.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
  imports: [
    CommonModule,
    HttpClientModule,
    NgxSpinnerModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forChild(
      [
        { path: ApplicationPaths.Register, component: NewLoginComponent },
        { path: ApplicationPaths.Profile, component: NewLoginComponent },
        { path: ApplicationPaths.Login, component: NewLoginComponent },
        { path: 'xlogin', component: LoginComponent },
        { path: ApplicationPaths.LoginFailed, component: NewLoginComponent },
        { path: ApplicationPaths.LoginCallback, component: NewLoginComponent },
        { path: ApplicationPaths.LogOut, component: LogoutComponent },
        { path: ApplicationPaths.LoggedOut, component: LogoutComponent },
        { path: ApplicationPaths.LogOutCallback, component: LogoutComponent }
      ]
    )
  ],
  declarations: [LoginMenuComponent,NewLoginComponent, LoginComponent, LogoutComponent],
  exports: [LoginMenuComponent,NewLoginComponent, LoginComponent, LogoutComponent],
  providers:[
    AuthorizeService,
  ]
})
export class ApiAuthorizationModule { }

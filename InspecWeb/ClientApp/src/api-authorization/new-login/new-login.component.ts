import { Component, OnInit } from '@angular/core';
import { AuthorizeService, AuthenticationResultStatus } from '../authorize.service';
import { ActivatedRoute, Router } from '@angular/router';
import { BehaviorSubject, Observable } from 'rxjs';
import { LoginActions, QueryParameterNames, ApplicationPaths, ReturnUrlType } from '../api-authorization.constants';
import { NgxSpinnerService } from 'ngx-spinner';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';
import { PasswordStrengthValidator } from './password-strength.validators';

// The main responsibility of this component is to handle the user's login process.
// This is the starting point for the login process. Any component that needs to authenticate
// a user can simply perform a redirect to this component with a returnUrl query parameter and
// let the component perform the login and return back to the return url.
@Component({
  selector: 'app-new-login',
  templateUrl: './new-login.component.html',
  styleUrls: ['./new-login.component.css']
})
export class NewLoginComponent implements OnInit {
  public message = new BehaviorSubject<string>(null);

  public isAuthenticated: Observable<boolean>;
  loginForm: FormGroup;
  loading = false;
  submitted = false;
  returnUrl: string;
  loginfail: any
  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private authorize: AuthorizeService,
    private spinner: NgxSpinnerService,

  ) {
    // redirect to home if already logged in
    // if (authorize.isAuthenticated()) {

    //   this.route.snapshot.queryParams['returnUrl']
    //   // this.router.navigate(['/']);
    // }
  }

  async ngOnInit() {

    this.authorize.isAuthenticated().subscribe(result => {
      console.log('isAuthenticated', result);

    });
    const action = this.route.snapshot.url[1];
    switch (action.path) {
      case LoginActions.Login:
        console.log(' LoginActions.Login:');
        await this.login(this.getReturnUrl());
        break;
      case LoginActions.LoginCallback:
        console.log(' LoginActions.LoginCallback:');

        await this.processLoginCallback();
        break;
      case LoginActions.LoginFailed:
        console.log('LoginActions.LoginFailed:');
        const message = this.route.snapshot.queryParamMap.get(QueryParameterNames.Message);
        this.message.next(message);
        break;
      case LoginActions.Profile:
        console.log('LoginActions.Profile:');

        this.redirectToProfile();
        break;
      case LoginActions.Register:
        console.log('LoginActions.Register:');
        this.redirectToRegister();
        break;
      default:
        throw new Error(`Invalid action '${action}'`);
    }

    this.loginForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', [Validators.required, PasswordStrengthValidator]],
    });

    // get return url from route parameters or default to '/'
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
    this.spinner.hide()

  }

  // convenience getter for easy access to form fields
  get f() { return this.loginForm.controls; }

  onSubmit() {
    this.submitted = true;
    this.loginfail = null
    // this.authorize.signIn("Success")
    // stop here if form is invalid
    if (this.loginForm.invalid) {
      return;
    }
    this.loading = true;
    this.spinner.show()

    this.authorize.newLogin(this.loginForm.value.username, this.loginForm.value.password, true, this.returnUrl)
      .subscribe(result => {
        if (result.status) {
          // this.login(this.returnUrl)
          // this.router.navigate([this.returnUrl])
          const state: INavigationState = { returnUrl: this.returnUrl };
          this.authorize.signIn(state).then(result => {
            console.log(result);
            this.navigateToReturnUrl(this.returnUrl);
          });



        } else {
          this.spinner.hide()

          this.submitted = false;
          this.loading = false
          this.loginForm.get('password').reset()
          this.loginfail = result.message
          // alert(result.message)
        }
      })

  }

  private async login(returnUrl: string): Promise<void> {
    this.spinner.show()

    const state: INavigationState = { returnUrl };
    const result = await this.authorize.signIn(state);
    // alert(result.status)

    this.message.next(undefined);
    switch (result.status) {
      case AuthenticationResultStatus.Redirect:
        break;
      case AuthenticationResultStatus.Success:
        await this.navigateToReturnUrl(returnUrl);
        break;
      case AuthenticationResultStatus.Fail:
        await this.router.navigate(ApplicationPaths.LoginFailedPathComponents, {
          queryParams: { [QueryParameterNames.Message]: result.message }
        });
        break;
      default:
        throw new Error(`Invalid status result ${(result as any).status}.`);
    }
  }

  private async processLoginCallback(): Promise<void> {
    this.spinner.show()
    const url = window.location.href;
    const result = await this.authorize.completeSignIn(url);
    switch (result.status) {
      case AuthenticationResultStatus.Redirect:
        // There should not be any redirects as completeSignIn never redirects.
        throw new Error('Should not redirect.');
      case AuthenticationResultStatus.Success:
        await this.navigateToReturnUrl(this.getReturnUrl(result.state));
        break;
      case AuthenticationResultStatus.Fail:
        this.message.next(result.message);
        break;
    }
  }

  private redirectToRegister(): any {
    this.redirectToApiAuthorizationPath(
      `${ApplicationPaths.IdentityRegisterPath}?returnUrl=${encodeURI('/' + ApplicationPaths.Login)}`);
  }

  private redirectToProfile(): void {
    this.redirectToApiAuthorizationPath(ApplicationPaths.IdentityManagePath);
  }

  private async navigateToReturnUrl(returnUrl: string) {
    // It's important that we do a replace here so that we remove the callback uri with the
    // fragment containing the tokens from the browser history.
    await this.router.navigateByUrl(returnUrl, {
      replaceUrl: true
    });
  }

  private getReturnUrl(state?: INavigationState): string {
    const fromQuery = (this.route.snapshot.queryParams as INavigationState).returnUrl;
    // If the url is comming from the query string, check that is either
    // a relative url or an absolute url
    if (fromQuery &&
      !(fromQuery.startsWith(`${window.location.origin}/`) ||
        /\/[^\/].*/.test(fromQuery))) {
      // This is an extra check to prevent open redirects.
      throw new Error('Invalid return url. The return url needs to have the same origin as the current page.');
    }
    return (state && state.returnUrl) ||
      fromQuery ||
      ApplicationPaths.DefaultLoginRedirectPath;
  }

  private redirectToApiAuthorizationPath(apiAuthorizationPath: string) {
    // It's important that we do a replace here so that when the user hits the back arrow on the
    // browser they get sent back to where it was on the app instead of to an endpoint on this
    // component.
    const redirectUrl = `${window.location.origin}${apiAuthorizationPath}`;
    window.location.replace(redirectUrl);
  }

  externalRegister() {
    this.router.navigate(['training/external/register'])
  }
}



interface INavigationState {
  [ReturnUrlType]: string;
}

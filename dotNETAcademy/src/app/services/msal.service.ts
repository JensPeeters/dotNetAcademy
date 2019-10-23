import { Injectable } from '@angular/core';

import * as Msal from 'msal';

declare var bootbox: '';
@Injectable()
export class MsalService {

    B2CTodoAccessTokenKey = 'b2c.access.token';

    tenantConfig = {
        domain: 'https://dotnetacademy.b2clogin.com/tfp/',
        tenant: 'dotnetacademy.onmicrosoft.com',
        // Replace this with your client id 
        clientID: '4c8ac317-e97b-45ce-93fd-e9e6775cfded',
        signInPolicy: 'B2C_1_signin',
        signUpPolicy: 'B2C_1_signup',
        redirectUri: 'http://localhost:4200',
        b2cScopes: ['https://dotnetacademy.onmicrosoft.com/access-api/user_impersonation']
    };

    // Configure the authority for Azure AD B2C
    authority = this.tenantConfig.domain + this.tenantConfig.tenant + '/' + this.tenantConfig.signInPolicy;

    /*
     * B2C SignIn SignUp Policy Configuration
     */
    clientApplication = new Msal.UserAgentApplication(
        this.tenantConfig.clientID, this.authority,
        function (errorDesc: any, token: any, error: any, tokenType: any) {
      },
      {
          validateAuthority: false
      }
    );

    public login(): void {
      this.clientApplication.authority = this.tenantConfig.domain + this.tenantConfig.tenant + '/' + this.tenantConfig.signInPolicy;
      this.authenticate();
    }

    public signup(): void {
      this.clientApplication.authority = this.tenantConfig.domain + this.tenantConfig.tenant + '/' + this.tenantConfig.signUpPolicy;
      this.authenticate();
    }

    public authenticate(): void {
        var _this = this;
        this.clientApplication.loginPopup(this.tenantConfig.b2cScopes).then(function (idToken: any) {
            _this.clientApplication.acquireTokenSilent(_this.tenantConfig.b2cScopes).then(
                function (accessToken: any) {
                    _this.saveAccessTokenToCache(accessToken);
                }, function (error: any) {
                    _this.clientApplication.acquireTokenPopup(_this.tenantConfig.b2cScopes).then(
                        function (accessToken: any) {
                            _this.saveAccessTokenToCache(accessToken);
                        }, function (error: any) {
                            console.log('error: ', error);
                        });
                })
        }, function (error: any) {
            console.log('error: ', error);
        });
    }

    saveAccessTokenToCache(accessToken: string): void {
        sessionStorage.setItem(this.B2CTodoAccessTokenKey, accessToken);
    }

    logout(): void {
        this.clientApplication.logout();
    }

    isLoggedIn(): boolean {
        return this.clientApplication.getUser() != null;
    }

    getUserEmail(): string{
       return this.getUser().idToken['emails'][0];
    }

    getUser(){
      return this.clientApplication.getUser();
    }

    getUserFirstName() {
        return this.getUser().idToken['given_name'];
    }
}
import { Injectable } from '@angular/core';
import * as Msal from 'msal';
import { environment } from 'src/environments/environment';
import { UserService } from './user.service';

@Injectable()
export class MsalService {

    constructor(private userService: UserService) {
        this.isAdmin();
    }

    B2CTodoAccessTokenKey = 'b2c.access.token';

    admin: boolean = false;

    tenantConfig = environment.tenantConfig;

    // Configure the authority for Azure AD B2C
    authority = this.tenantConfig.domain + '/' + this.tenantConfig.signInPolicy;

    /*
     * B2C SignIn SignUp Policy Configuration
     */
    clientApplication = new Msal.UserAgentApplication(
        this.tenantConfig.clientID, this.authority,
        (errorDesc: any, token: any, error: any, tokenType: any) => {
        },
        {
            validateAuthority: false
        }
    );

    public login(): void {
        this.clientApplication.authority = this.tenantConfig.domain + this.tenantConfig.signInPolicy;
        this.authenticate();
    }

    public signup(): void {
        this.clientApplication.authority = this.tenantConfig.domain + this.tenantConfig.signUpPolicy;
        this.authenticate();
    }

    public resetPassword(): void {
        this.clientApplication.authority = this.tenantConfig.domain + this.tenantConfig.resetPasswordPolicy;
        this.authenticate();
    }

    public editProfile(): void {
        this.clientApplication.authority = this.tenantConfig.domain + this.tenantConfig.editProfilePolicy;
        this.authenticate();
    }

    public authenticate(): void {
        const THIS = this;
        this.clientApplication.loginPopup(this.tenantConfig.b2cScopes).then((idToken: any) => {
            THIS.clientApplication.acquireTokenSilent(THIS.tenantConfig.b2cScopes).then(
                (accessToken: any) => {
                    THIS.saveAccessTokenToCache(accessToken);
                }, (error: any) => {
                    THIS.clientApplication.acquireTokenPopup(THIS.tenantConfig.b2cScopes).then(
                        (accessToken: any) => {
                            THIS.saveAccessTokenToCache(accessToken);
                        }, (error: any) => {
                            console.log('error: ', error);
                        });
                });
        }, (errorDesc: any) => {
            console.log('error: ', errorDesc);
            if (errorDesc.indexOf('AADB2C90118') > -1) {
                THIS.resetPassword();
            } else if (errorDesc.indexOf('AADB2C90077') > -1) {
                // Expired Token
                THIS.logout();
            }
        });
    }

    saveAccessTokenToCache(accessToken: string): void {
        sessionStorage.setItem(this.B2CTodoAccessTokenKey, accessToken);
        if (this.isNew()) {
            this.userService.saveKlantInDb(this.getUserObjectId()).subscribe();
        }
        this.isAdmin();
    }

    logout(): void {
        this.clientApplication.logout();
    }

    getUser() {
        return this.clientApplication.getUser();
    }

    isLoggedIn(): boolean {
        return this.clientApplication.getUser() != null;
    }

    isNew() {
        if (this.getUser().idToken['newUser']) {
            return true;
        }
        return false;
    }

    isAdmin() {
        if (this.isLoggedIn()) {
            this.userService.isadmin(this.getUserObjectId()).subscribe(res => {
                this.admin = true;
                return true;
            },
                err => {
                    this.admin = false;
                    return false;
                });
        }
        return false;
    }

    getUserObjectId() {
        return this.getUser().idToken['oid'];
    }

    getUserFirstName() {
        return this.getUser().idToken['given_name'];
    }

    getUserFamilyName() {
        return this.getUser().idToken['family_name'];
    }

    getUserEmail(): string {
        return this.getUser().idToken['emails'][0];
    }
}

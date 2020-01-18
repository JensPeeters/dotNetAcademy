// This file can be replaced during build by using the `fileReplacements` array.
// `ng build --prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  production: false,
  domain: "https://localhost:44334/api",
  tenantConfig: {
    domain: 'https://dotnetacademy.b2clogin.com/tfp/dotnetacademy.onmicrosoft.com/',
    // Replace this with your client id
    clientID: '4c8ac317-e97b-45ce-93fd-e9e6775cfded',
    signInPolicy: 'B2C_1_signin',
    signUpPolicy: 'B2C_1_signup',
    resetPasswordPolicy: 'B2C_1_resetpassword',
    editProfilePolicy: 'B2C_1_editprofile',
    redirectUri: 'https://dotnetacademy.azurewebsites.net',
    b2cScopes: ['https://dotnetacademy.onmicrosoft.com/api/user_impersonation']
  }
};

/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/dist/zone-error';  // Included with Angular CLI.

export const environment = {
  production: true,
  domain: 'https://dotnetacademy-api.azurewebsites.net/api',
  tenantConfig: {
    domain: 'https://dotnetacademy.b2clogin.com/tfp/dotnetacademy.onmicrosoft.com/',
    // Replace this with your client id
    clientID: '4c8ac317-e97b-45ce-93fd-e9e6775cfded',
    signInPolicy: 'B2C_1_signin',
    signUpPolicy: 'B2C_1_signup',
    resetPasswordPolicy: 'B2C_1_resetpassword',
    editProfilePolicy: 'B2C_1_editprofile',
    redirectUri: 'https://dotnetacademy.azurewebsites.net',
    b2cScopes: ['https://dotnetacademy.onmicrosoft.com/access-api/user_impersonation']
  }
};

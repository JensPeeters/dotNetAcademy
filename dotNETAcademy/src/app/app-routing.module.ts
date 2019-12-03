import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { ProductenlijstComponent } from './productenlijst/productenlijst.component';
import { ProductInfoComponent } from './product-info/product-info.component';
import { WinkelmandComponent } from './winkelmand/winkelmand.component';
import { BestellingenComponent } from './bestellingen/bestellingen.component';
import { ProfielComponent } from './profiel/profiel.component';
import { MsalGuard } from './guard/msal.guard';
import { AdminPanelComponent } from './admin-panel/admin-panel.component';
import { NoAdminComponent } from './no-admin/no-admin.component';
import { AdminGuard } from './guard/admin.guard';
import { CreateUserComponent } from './admin-panel/create-user/create-user.component';
import { UpdateUserComponent } from './admin-panel/update-user/update-user.component';
import { DeleteUserComponent } from './admin-panel/delete-user/delete-user.component';

const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: 'producten/:currentRoute', component: ProductenlijstComponent },
  { path: 'producten/:currentRoute/:searchParam', component: ProductenlijstComponent },
  { path: 'product/:currentRoute/:productId/:productName', component: ProductInfoComponent },
  { path: 'winkelmand', component: WinkelmandComponent },
  { path: 'bestellingen', component: BestellingenComponent },
  { path: 'profiel', component: ProfielComponent, canActivate: [MsalGuard] },
  { path: 'adminpanel', component: AdminPanelComponent/*, canActivate: [AdminGuard]*/ },
  { path: 'adminpanel/createUser', component: CreateUserComponent/*, canActivate: [AdminGuard]*/ },
  { path: 'adminpanel/updateUser', component: UpdateUserComponent/*, canActivate: [AdminGuard]*/ },
  { path: 'adminpanel/deleteUser', component: DeleteUserComponent/*, canActivate: [AdminGuard]*/ },
  { path: 'no-admin', component: NoAdminComponent },
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: '**', component: NotFoundComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

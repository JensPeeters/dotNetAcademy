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

const routes: Routes = [
  {path: 'home', component: HomeComponent},
  {path: 'producten/:currentRoute', component: ProductenlijstComponent},
  {path: 'producten/:currentRoute/:searchParam', component: ProductenlijstComponent},
  {path: 'product/:currentRoute/:productId/:productName', component: ProductInfoComponent},
  {path: 'winkelmand', component: WinkelmandComponent},
  {path: 'bestellingen', component: BestellingenComponent},
  {path: 'profiel', component: ProfielComponent, canActivate: [MsalGuard]},
  {path: 'adminpanel', component: AdminPanelComponent, canActivate: [MsalGuard]},
  {path: '', redirectTo: 'home', pathMatch: 'full'},
  {path: '**', component: NotFoundComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

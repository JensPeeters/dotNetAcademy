import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { ProductenlijstComponent } from './productenlijst/productenlijst.component';
import { ProductInfoComponent } from './product-info/product-info.component';

const routes: Routes = [{path:"home", component:HomeComponent},
                        {path: "producten/:currentRoute", component: ProductenlijstComponent},
                        {path: "producten/:currentRoute/:searchParam", component:ProductenlijstComponent},
                        {path: "product/:currentRoute/:productId/:productName", component:ProductInfoComponent},
                        {path: "", redirectTo: "home", pathMatch:"full"},
                        {path: "**", component: NotFoundComponent}];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

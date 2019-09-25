import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ToolbarComponent } from './toolbar/toolbar.component';
import { HomeComponent } from './home/home.component';
import { CursussenlijstComponent } from './cursussenlijst/cursussenlijst.component';
import { TrajectenlijstComponent } from './trajectenlijst/trajectenlijst.component';
import { CursusComponent } from './cursussenlijst/cursus/cursus.component';
import { TrajectComponent } from './trajectenlijst/traject/traject.component';
import { NotFoundComponent } from './not-found/not-found.component';


@NgModule({
  declarations: [
    AppComponent,
    ToolbarComponent,
    HomeComponent,
    CursussenlijstComponent,
    TrajectenlijstComponent,
    CursusComponent,
    TrajectComponent,
    NotFoundComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

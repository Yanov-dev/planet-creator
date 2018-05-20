import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { EditorPageComponent } from './pages/editor-page/editor-page.component';
import {AppRoutingModule} from './app-routing.module';
import { PlanetViewerComponent } from './components/planet-viewer/planet-viewer.component';
import {MatSidenavModule} from '@angular/material';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';

@NgModule({
  declarations: [
    AppComponent,
    EditorPageComponent,
    PlanetViewerComponent
  ],
  imports: [
    MatSidenavModule,
    AppRoutingModule,
    BrowserAnimationsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

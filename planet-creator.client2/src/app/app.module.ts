import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';

import {
  MatToolbarModule,
  MatButtonModule,
  MatDialogModule,
  MatAutocompleteModule,
  MatInputModule,
  MatSelectModule,
  MatSortModule,
  MatGridListModule,
  MatListModule,
  MatCardModule,
  MatSliderModule
} from '@angular/material';
import {ColorPickerModule} from 'ngx-color-picker';
import {MatTableModule} from '@angular/material/table';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {AppComponent} from './app.component';
import {AppRoutingModule} from './app-routing.module';
import {TestPageComponent} from './pages/test-page/test.page';
import {Cretor2DPageComponent} from './pages/2d-creator-page/creator.2d.page';
import {Generator2dService} from './services/generator2dService';
import {HttpClientModule} from '@angular/common/http';
import {HomePageComponent} from './pages/home-page/home.page';
import {LayerEditorComponent} from './components/layer.editor.component/layer.editor.component';
import {LayersListComponent} from './components/layers.list.component/layers.list.component';
import {Planet2dRenderComponent} from './components/planet2d.render.component/planet2d.render.component';
import {Planet2dPropertiesComponent} from './components/planet2d.properties.component/planet2d.properties.component';
import {OverviewPageComponent} from './pages/overview.page/overview.page';
import {CreateProjectDialogComponent} from './dialogs/create.project.dialog/craete.project.dialog.component';

@NgModule({
  declarations: [
    AppComponent,
    Planet2dPropertiesComponent,
    Planet2dRenderComponent,
    TestPageComponent,
    OverviewPageComponent,
    HomePageComponent,
    LayersListComponent,
    LayerEditorComponent,
    Cretor2DPageComponent,
    CreateProjectDialogComponent
  ],
  entryComponents: [
    CreateProjectDialogComponent
  ],
  imports: [
    ColorPickerModule,
    MatSliderModule,
    MatCardModule,
    MatListModule,
    MatGridListModule,
    MatTableModule,
    HttpClientModule,
    MatToolbarModule,
    MatButtonModule,
    MatDialogModule,
    MatAutocompleteModule,
    MatInputModule,
    MatSelectModule,
    MatSortModule,
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule
  ],
  providers: [
    Generator2dService
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}

import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';

import {AppComponent} from './app.component';
import {EditorPageComponent} from './pages/editor-page/editor-page.component';
import {AppRoutingModule} from './app-routing.module';
import {PlanetViewerComponent} from './components/planet-viewer/planet-viewer.component';
import {MatSidenavModule, MatGridListModule, MatCardModule, MatButtonModule, MatTableModule, MatSliderModule} from '@angular/material';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {PlanetGenerationService} from './services/planet-generation.service';
import {HttpClientModule} from '@angular/common/http';
import {GenerationPanelComponent} from './components/generation-panel/generation-panel.component';
import {LayerEditorComponent} from './components/layer-editor/layer-editor.component';
import {ColorPickerModule} from 'ngx-color-picker';

@NgModule({
  declarations: [
    AppComponent,
    EditorPageComponent,
    PlanetViewerComponent,
    GenerationPanelComponent,
    LayerEditorComponent
  ],
  imports: [
    ColorPickerModule,
    HttpClientModule,
    MatSliderModule,
    MatTableModule,
    MatButtonModule,
    MatCardModule,
    MatGridListModule,
    MatSidenavModule,
    AppRoutingModule,
    BrowserAnimationsModule
  ],
  providers: [
    PlanetGenerationService
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}

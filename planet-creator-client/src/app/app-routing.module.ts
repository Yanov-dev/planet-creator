import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {EditorPageComponent} from './pages/editor-page/editor-page.component';

const appRoutes: Routes = [
  {
    path: '',
    redirectTo: 'editor',
    pathMatch: 'full'
  },
  {
    path: 'editor',
    component: EditorPageComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(appRoutes, {enableTracing: false})],
  exports: [RouterModule]
})
export class AppRoutingModule {
}


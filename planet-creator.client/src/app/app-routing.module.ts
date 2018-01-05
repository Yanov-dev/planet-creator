import {NgModule} from '@angular/core';
import {Routes, RouterModule} from '@angular/router';
import {TestPageComponent} from './pages/test-page/test.page';

const appRoutes: Routes = [
  {
    path: '',
    component: TestPageComponent,
    pathMatch: 'full',
  }
];

@NgModule({
  imports: [RouterModule.forRoot(appRoutes, {enableTracing: false})],
  exports: [RouterModule]
})
export class AppRoutingModule {
}


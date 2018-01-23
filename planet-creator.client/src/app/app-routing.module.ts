import {NgModule} from '@angular/core';
import {Routes, RouterModule} from '@angular/router';
import {TestPageComponent} from './pages/test-page/test.page';
import {OverviewPageComponent} from './pages/overview.page/overview.page';

const appRoutes: Routes = [
  {
    path: '',
    component: OverviewPageComponent,
    pathMatch: 'full',
  }
];

@NgModule({
  imports: [RouterModule.forRoot(appRoutes, {enableTracing: false})],
  exports: [RouterModule]
})
export class AppRoutingModule {
}


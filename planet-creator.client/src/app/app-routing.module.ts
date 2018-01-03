import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { HomePageComponent } from './pages/home-page/home.page';
import { TestPageComponent } from './pages/test-page/test.page';

const appRoutes: Routes = [
    {
        path: '',
        component: TestPageComponent,
        pathMatch: 'full',
    }
];

@NgModule({
    imports: [RouterModule.forRoot(appRoutes, { enableTracing: false })],
    exports: [RouterModule]
})
export class AppRoutingModule {
}


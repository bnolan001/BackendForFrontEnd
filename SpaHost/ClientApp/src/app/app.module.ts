import { BrowserModule } from '@angular/platform-browser';
import { NgModule, APP_INITIALIZER } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { FetchAuthenticatedDataComponent } from './fetch-authenticated-data/fetch-authenticated-data.component';
import { AppInitService } from './services/app-init.service';
import { GuardedRouteComponent } from './guarded-route/guarded-route.component';
import { AuthGuard } from './auth-guard';

export function initializeApp(appInitService: AppInitService) {
  return (): Promise<any> => {
    return appInitService.load();
  }
}

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    FetchDataComponent,
    FetchAuthenticatedDataComponent,
    GuardedRouteComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'fetch-authenticated-data', component: FetchAuthenticatedDataComponent },
      { path: 'guarded-route', component: GuardedRouteComponent, canActivate: [AuthGuard] }
    ])
  ],
  providers: [
    AppInitService,
    {
      provide: APP_INITIALIZER,
      useFactory: initializeApp,
      deps: [AppInitService],
      multi: true
    }],
  bootstrap: [AppComponent]
})
export class AppModule { }

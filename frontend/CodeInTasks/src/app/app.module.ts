import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { interceptorProviders } from './web-api/interceptors/interceptor-providers';
import { serviceProviders } from './web-api/services/service-providers';
import { AppHeaderComponent } from './common/app-header/app-header.component';

@NgModule({
  declarations: [
    AppComponent,
    AppHeaderComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [
    serviceProviders,
    interceptorProviders
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

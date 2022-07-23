import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { interceptorProviders } from './web-api/interceptors/interceptor-providers';
import { serviceProviders } from './web-api/services/service-providers';

@NgModule({
  declarations: [
    AppComponent
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

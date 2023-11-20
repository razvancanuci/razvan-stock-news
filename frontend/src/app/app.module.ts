import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginFormComponent } from './user/components/login-form/login-form.component';
import { AddUserFormComponent } from './user/components/add-user-form/add-user-form.component';
import { NotFoundComponent } from './notfound/component/not-found.component';
import { AuthInterceptor } from './user/interceptors/auth.interceptor';
import { AlreadySubscribedComponent } from './already-subscribed/component/already-subscribed/already-subscribed.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginFormComponent,
    AddUserFormComponent,
    NotFoundComponent,
    AlreadySubscribedComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }

  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

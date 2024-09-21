import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './Components/home/home.component';
import { NavigationComponent } from './Components/navigation/navigation.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LoginComponent } from './Components/login/login.component';
import { RegisterComponent } from './Components/register/register.component';
import { ScheduleComponent } from './Components/schedule/schedule.component';
import { UserDashboardComponent } from './Components/user-dashboard/user-dashboard.component';
import { MedicineDemandComponent } from './Components/medicine-demand/medicine-demand.component';
import { MedicineSupplyComponent } from './Components/medicine-supply/medicine-supply.component';
import {HTTP_INTERCEPTORS, HttpClientModule} from '@angular/common/http';
import { TokenInterceptor } from './interceptors/token.interceptor';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    NavigationComponent,
    LoginComponent,
    RegisterComponent,
    ScheduleComponent,
    UserDashboardComponent,
    MedicineDemandComponent,
    MedicineSupplyComponent    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true, // Ensure it works with other interceptors if any
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

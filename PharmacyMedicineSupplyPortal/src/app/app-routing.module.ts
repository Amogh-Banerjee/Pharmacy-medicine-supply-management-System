import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './Components/home/home.component';
import { LoginComponent } from './Components/login/login.component';
import { RegisterComponent } from './Components/register/register.component';
import { ScheduleComponent } from './Components/schedule/schedule.component';
import { UserDashboardComponent } from './Components/user-dashboard/user-dashboard.component';
import { MedicineDemandComponent } from './Components/medicine-demand/medicine-demand.component';
import { MedicineSupplyComponent } from './Components/medicine-supply/medicine-supply.component';
import { AuthGuard } from './guards/auth.guard';

const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'dashboard', component: UserDashboardComponent, canActivate: [AuthGuard] },
  { path: 'schedule', component: ScheduleComponent, canActivate: [AuthGuard] },
  { path: 'medicineDemand', component: MedicineDemandComponent, canActivate: [AuthGuard] },
  { path: 'medicineSupply', component: MedicineSupplyComponent, canActivate: [AuthGuard] },
  { path: '', redirectTo: '/home', pathMatch: 'full' } // Default route
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

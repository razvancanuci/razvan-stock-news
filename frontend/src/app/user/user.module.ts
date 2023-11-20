import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UserRoutingModule } from './user-routing.module';
import { UserPageComponent } from './components/user-page/user-page.component';
import { UserPageUserComponentComponent } from './components/user-page-user-component/user-page-user-component.component';
import { UserPageAdminComponentComponent } from './components/user-page-admin-component/user-page-admin-component.component';
import { DeleteUserComponent } from './components/delete-user/delete-user.component';
import { ReactiveFormsModule } from '@angular/forms';
import { ChartModule } from 'primeng/chart';
import { BarChartComponent } from './components/charts/bar-chart/bar-chart.component';
import { PieChartComponent } from './components/charts/pie-chart/pie-chart.component';
import { FactorPageComponent } from './components/factor-page/factor-page.component';


@NgModule({
  declarations: [
    UserPageComponent,
    UserPageUserComponentComponent,
    UserPageAdminComponentComponent,
    DeleteUserComponent,
    BarChartComponent,
    PieChartComponent,
    FactorPageComponent
  ],
  imports: [
    CommonModule,
    UserRoutingModule,
    ReactiveFormsModule,
    ChartModule
  ]
})
export class UserModule { }

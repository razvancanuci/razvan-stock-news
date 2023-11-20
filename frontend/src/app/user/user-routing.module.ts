import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddUserFormComponent } from './components/add-user-form/add-user-form.component';
import { DeleteUserComponent } from './components/delete-user/delete-user.component';
import { FactorPageComponent } from './components/factor-page/factor-page.component';
import { LoginFormComponent } from './components/login-form/login-form.component';
import { UserPageComponent } from './components/user-page/user-page.component';
import { AuthGuard } from './guards/auth.guard';
import { IsAdminGuard } from './guards/is-admin.guard';
import { IsFactorGuard } from './guards/is-factor.guard';
import { IsUserAdminGuard } from './guards/is-user-admin.guard';
import { LoginGuard } from './guards/login.guard';

const routes: Routes = [
  { path: '', component: UserPageComponent, canActivate: [AuthGuard, IsUserAdminGuard], data: { roles: ["Admin", "User"] } },
  { path: 'add-user', component: AddUserFormComponent, canActivate: [AuthGuard, IsAdminGuard], data: { role: "Admin" } },
  { path: 'delete-user', component: DeleteUserComponent, canActivate: [AuthGuard, IsAdminGuard], data: { role: "Admin" } },
  { path: 'login', component: LoginFormComponent, canActivate: [LoginGuard] },
  { path: '2factor', component: FactorPageComponent, canActivate: [AuthGuard, IsFactorGuard], data: { role: "2FA" } }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserRoutingModule { }

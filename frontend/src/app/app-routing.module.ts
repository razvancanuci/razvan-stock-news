import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NotFoundComponent } from './notfound/component/not-found.component';
import { AlreadySubscribedComponent } from './already-subscribed/component/already-subscribed/already-subscribed.component';

const routes: Routes = [
  { path: '', redirectTo: 'subscription/subscribe', pathMatch: 'full' },
  { path: 'user', loadChildren: () => import('./user/user.module').then((m) => m.UserModule) },
  { path: 'subscription', loadChildren: () => import('./subscribe/subscribe.module').then((m) => m.SubscribeModule) },
  { path: 'already-subscribed', component: AlreadySubscribedComponent },
  { path: '**', component: NotFoundComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

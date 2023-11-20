import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SubscriberPageComponent } from './components/subscriber-page/subscriber-page.component';
import { UnsubscribePageComponent } from './components/unsubscribe-page/unsubscribe-page.component';
import { FeedbackComponent } from './components/feedback/feedback.component';
import { SubscriberValidGuard } from './guards/subscriber-valid.guard';

const routes: Routes = [
  { path: 'subscribe', component: SubscriberPageComponent },
  { path: 'unsubscribe/:id', component: UnsubscribePageComponent },
  { path: 'questionnaire/:id', component: FeedbackComponent, canActivate: [SubscriberValidGuard] }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SubscribeRoutingModule { }

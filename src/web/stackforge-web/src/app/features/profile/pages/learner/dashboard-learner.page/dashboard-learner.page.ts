import { Component } from '@angular/core';
import { Header } from '../../../../../layout/header/header';
import { LearnerProfileSummaryComponent } from "../../../components/learner/learner-profile-summary.component/learner-profile-summary.component";

@Component({
  selector: 'app-dashboard-learner.page',
  imports: [Header, LearnerProfileSummaryComponent],
  templateUrl: './dashboard-learner.page.html',
  styleUrl: './dashboard-learner.page.scss',
})
export class DashboardLearnerPage {

}

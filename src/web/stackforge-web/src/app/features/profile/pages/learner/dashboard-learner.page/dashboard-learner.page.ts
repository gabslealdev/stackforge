import { Component, inject, OnInit, signal } from '@angular/core';
import { Header } from '../../../../../layout/header/header';
import { LearnerProfileSummaryComponent } from "../../../components/learner/learner-profile-summary.component/learner-profile-summary.component";
import { GetCurrentLearnerResponse } from '../../../models/Learner/learner-profile.response';
import { LearnerProfileServiceTs } from '../../../services/learner-profile.service.ts';
import { SearchMentorComponent } from '../../../../mentorship/components/search-mentor.component/search-mentor.component';
import { SentMentorshipRequestsComponent } from '../../../../mentorship/components/sent-mentorship-requests.component/sent-mentorship-requests.component';
type LearnerDashboardSection  = 'search' | 'requests' | 'mentorships';

@Component({
  selector: 'app-dashboard-learner.page',
  imports: [Header, LearnerProfileSummaryComponent, SearchMentorComponent, SentMentorshipRequestsComponent],
  templateUrl: './dashboard-learner.page.html',
  styleUrl: './dashboard-learner.page.scss',
})
export class DashboardLearnerPage implements OnInit{
  private readonly _learnerProfileService = inject(LearnerProfileServiceTs)

  ngOnInit(): void {
    this.loadLearner();
  }

  learner = signal<GetCurrentLearnerResponse | null>(null);
  isLoading = signal(false);
  errorMessage = signal('');
  currentSection = signal<LearnerDashboardSection>('search');

  selectSection(section: LearnerDashboardSection){
    this.currentSection.set(section);
  }

  loadLearner(){
    this.isLoading.set(true);
    this.errorMessage.set('');
    this.learner.set(null);
  
    this._learnerProfileService
      .getCurrentLearner()
      .subscribe({
        next: (response) => {
          console.log('Sucesso', response)
          this.learner.set(response);
          this.isLoading.set(false)
        },
        error: () => {
          console.log('erro');
          this.errorMessage.set('Não foi possível carregar os dados do learner');
          this.isLoading.set(false);
        },
        complete: () => {
          console.log('Observable finalizado');
        }
      });
  }

}

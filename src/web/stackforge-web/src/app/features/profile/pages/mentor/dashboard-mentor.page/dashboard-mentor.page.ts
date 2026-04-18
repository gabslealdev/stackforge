import { Component, inject, OnInit, signal} from '@angular/core';
import { Header } from "../../../../../layout/header/header";
import { MentorProfileSummaryComponent } from '../../../components/mentor/mentor-profile-summary.component/mentor-profile-summary.component';
import { MentorAddStackComponent } from '../../../components/mentor/mentor-add-stack.component/mentor-add-stack.component';
import { MentorProfileService } from '../../../services/mentor-profile.service';
import { GetCurrentMentorResponse } from '../../../models/Mentor/mentor-profile.response';
import { CommonModule } from '@angular/common';
import { finalize } from 'rxjs';

@Component({
  selector: 'app-dashboard-mentor.page',
  imports: [Header, MentorProfileSummaryComponent, MentorAddStackComponent, CommonModule],
  templateUrl: './dashboard-mentor.page.html',
  styleUrl: './dashboard-mentor.page.scss',
})
export class DashboardMentorPage implements OnInit {
  private readonly _mentorProfileService = inject(MentorProfileService);


  mentor = signal<GetCurrentMentorResponse | null>(null);
  isLoading = signal(false)
  errorMessage = signal('');



  ngOnInit(): void {
    this.loadMentor();
  }

  loadMentor() {
    this.isLoading.set(true);
    this.errorMessage.set('');
    this.mentor.set(null);


    this._mentorProfileService
      .getCurrentMentor()
      .subscribe({
        next: (response) => {
          console.log('Sucesso', response);
          this.mentor.set(response)
          this.isLoading.set(false)
        },
        error: () => {
          console.log('erro');
          this.errorMessage.set('Não foi possível carregar os dados do mentor.');
          this.isLoading.set(false);
        },
        complete: () => {
          console.log('Observable finalizado');
        }
      });
  }

}

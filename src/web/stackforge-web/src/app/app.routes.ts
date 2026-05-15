import { Routes } from '@angular/router';
import { SelectProfilePage } from './features/identity/pages/select-profile.page/select-profile.page';
import { RegisterUserPage } from './features/identity/pages/register-user.page/register-user.page';
import { RegisterLearnerPage } from './features/profile/pages/learner/register-learner.page/register-learner.page';
import { RegisterMentorPage } from './features/profile/pages/mentor/register-mentor.page/register-mentor.page';
import { LoginUserPage } from './features/identity/pages/login-user.page/login-user.page';
import { DashboardMentorPage } from './features/profile/pages/mentor/dashboard-mentor.page/dashboard-mentor.page';
import { DashboardLearnerPage } from './features/profile/pages/learner/dashboard-learner.page/dashboard-learner.page';

export const routes: Routes = [
    {path: '', redirectTo: 'login', pathMatch: 'full' },
    {path: 'register/select-profile', component: SelectProfilePage},
    {path: 'register/user', component: RegisterUserPage},
    {path: 'register/user/learner', component: RegisterLearnerPage},
    {path: 'register/user/mentor', component: RegisterMentorPage},
    {path: 'login', component: LoginUserPage},
    {path: 'mentor/dashboard', component: DashboardMentorPage},
    {path: 'learner/dashboard', component: DashboardLearnerPage}
];
    
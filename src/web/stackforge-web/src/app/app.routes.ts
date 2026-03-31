import { Routes } from '@angular/router';
import { SelectProfilePage } from './features/identity/pages/select-profile.page/select-profile.page';
import { RegisterUserPage } from './features/identity/pages/register-user.page/register-user.page';
import { RegisterLearnerPage } from './features/profile/pages/learner/register-learner.page/register-learner.page';
import { RegisterMentorPage } from './features/profile/pages/mentor/register-mentor.page/register-mentor.page';

export const routes: Routes = [
    {path: '', redirectTo: 'register/select-profile', pathMatch: 'full' },
    {path: 'register/select-profile', component: SelectProfilePage},
    {path: 'register/user', component: RegisterUserPage},
    {path: 'register/user/learner', component: RegisterLearnerPage},
    {path: 'register/user/mentor', component: RegisterMentorPage}
];

import { Routes } from '@angular/router';
import { LoginPage } from './features/auth/ui/login-page/login-page';
import { MentorRegisterPage } from './features/mentor/ui/mentor-register-page/mentor-register-page';
import { LearnerRegisterPage } from './features/learner/ui/learner-register-page/learner-register-page';
import { SelectProfileType } from './features/auth/ui/pages/select-profile-type/select-profile-type';
import { RegisterUser } from './features/auth/ui/pages/register-user/register-user';

export const routes: Routes = [
    { path: '', redirectTo: 'register/select-profile', pathMatch: 'full' },
    {path: 'login', component: LoginPage},

    { path: 'register/select-profile', component: SelectProfileType },
    { path: 'register/user', component: RegisterUser },
    {path: 'register/mentor', component: MentorRegisterPage},
    {path: 'register/learner', component: LearnerRegisterPage},
    {path: '**', redirectTo: 'login'},
];
 
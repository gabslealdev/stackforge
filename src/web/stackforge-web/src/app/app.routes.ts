import { Routes } from '@angular/router';
import { LoginPage } from './features/auth/ui/login-page/login-page';
import { MentorRegisterPage } from './features/mentor/ui/mentor-register-page/mentor-register-page';
import { LearnerRegisterPage } from './features/learner/ui/learner-register-page/learner-register-page';

export const routes: Routes = [
    {path: '', redirectTo: 'login', pathMatch: 'full'},
    {path: 'login', component: LoginPage},
    {path: 'mentor/register', component: MentorRegisterPage},
    {path: 'learner/register', component: LearnerRegisterPage},
    {path: '**', redirectTo: 'login'},
];

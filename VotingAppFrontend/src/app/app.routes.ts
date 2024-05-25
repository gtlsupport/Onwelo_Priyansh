import { Routes } from '@angular/router';
import { AddCandidateComponent } from './features/add-candidate/add-candidate.component';
import { HomeComponent } from './features/home/home.component';

export const routes: Routes = [
    {
        path: 'addCandidate',
        component: AddCandidateComponent
    }
];

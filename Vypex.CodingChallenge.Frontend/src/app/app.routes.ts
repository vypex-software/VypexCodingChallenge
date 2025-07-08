import { Routes } from '@angular/router';
import { PROJECTS_ROUTES } from './projects/projects.routes';

export const routes: Routes = [
  {
    path: 'projects',
    children: PROJECTS_ROUTES
  },
  {
    path: '**',
    redirectTo: 'projects',
  }
];

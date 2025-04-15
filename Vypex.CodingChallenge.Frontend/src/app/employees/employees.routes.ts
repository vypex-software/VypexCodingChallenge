import { Routes } from '@angular/router';
import { EmployeeListComponent } from './employees.component';

export const EMPLOYEES_ROUTES: Routes = [
  {
    path: '',
    component: EmployeeListComponent,
  },
];

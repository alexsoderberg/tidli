import { Routes } from '@angular/router';
import { Layout } from './shared/components/layout/layout';
import { Project } from './shared/components/project/project';

export const routes: Routes = [
  {
    path: '',
    component: Layout,
    children: [
      { path: 'projects/:id', component: Project },
      { path: '', redirectTo: 'projects/1', pathMatch: 'full' },
    ],
  },
];

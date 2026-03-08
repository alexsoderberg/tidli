import { Injectable, signal } from '@angular/core';
import { Stage } from '../../shared/components/stage/stage.model';

export interface Project {
  id: string;
  name: string;
  stages: Stage[];
}

@Injectable({
  providedIn: 'root',
})
export class ProjectService {
  private projects = signal<Project[]>([
    {
      id: '1',
      name: 'Project A',
      stages: [
        { id: 'todo', title: 'To Do', tasks: [] },
        { id: 'in-progress', title: 'In Progress', tasks: [] },
        { id: 'done', title: 'Done', tasks: [] },
      ],
    },
    {
      id: '2',
      name: 'Project B',
      stages: [
        { id: 'todo', title: 'To Do', tasks: [] },
        { id: 'in-progress', title: 'In Progress', tasks: [] },
        { id: 'done', title: 'Done', tasks: [] },
      ],
    },
    {
      id: '3',
      name: 'Project C',
      stages: [
        { id: 'todo', title: 'To Do', tasks: [] },
        { id: 'in-progress', title: 'In Progress', tasks: [] },
        { id: 'done', title: 'Done', tasks: [] },
      ],
    },
  ]);

  getProject(id: string): Project | undefined {
    return this.projects().find(p => p.id === id);
  }

  getProjects(): Project[] {
    return this.projects();
  }
}

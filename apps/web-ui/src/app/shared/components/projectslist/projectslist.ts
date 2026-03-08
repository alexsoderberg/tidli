import { Component, signal } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { CommonModule } from '@angular/common';

interface Project {
  id: string;
  name: string;
}

@Component({
  selector: 'app-projectslist',
  imports: [CommonModule, RouterLink, RouterLinkActive],
  templateUrl: './projectslist.html',
  styleUrl: './projectslist.css',
})
export class Projectslist {
  projects = signal<Project[]>([
    { id: '1', name: 'Project A' },
    { id: '2', name: 'Project B' },
    { id: '3', name: 'Project C' },
  ]);

  showCreateModal = signal(false);

  openCreateModal(): void {
    this.showCreateModal.set(true);
  }

  closeCreateModal(): void {
    this.showCreateModal.set(false);
  }

  createProject(name: string): void {
    this.projects.update(p => [...p, { id: crypto.randomUUID(), name }]);
    this.closeCreateModal();
  }
}

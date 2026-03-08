import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Projectslist } from '../projectslist/projectslist';

@Component({
  selector: 'app-layout',
  imports: [RouterOutlet, Projectslist],
  templateUrl: './layout.html',
  styleUrl: './layout.css',
})
export class Layout {
  sidebarCollapsed = signal(false);

  toggleSidebar(): void {
    this.sidebarCollapsed.update(v => !v);
  }
}

import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Project } from "./shared/components/project/project";

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, Project],
  templateUrl: './app.html',
  styleUrl: './app.css'
})

export class App {
  protected readonly title = signal('web_ui');
}

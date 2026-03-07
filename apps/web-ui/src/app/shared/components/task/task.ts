import { Component, input } from '@angular/core';

@Component({
  selector: 'app-task',
  imports: [],
  templateUrl: './task.html',
  styleUrl: './task.css',
})
export class Task {
  title = input<string>();
  description = input<string>();
  id = input<string>();
}

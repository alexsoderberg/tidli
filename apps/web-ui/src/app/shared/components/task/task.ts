import { Component, input, output } from '@angular/core';

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

  taskDragStarted = output<string>();
  taskDragEnded = output<void>();

  onDragStart(event: DragEvent): void {
    const taskId = this.id();
    if (taskId) {
      event.dataTransfer?.setData('text/plain', taskId);
      event.dataTransfer!.effectAllowed = 'move';
      this.taskDragStarted.emit(taskId);
    }
  }

  onDragEnd(): void {
    this.taskDragEnded.emit();
  }
}

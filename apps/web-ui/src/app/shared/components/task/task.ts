import { Component, input, output } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-task',
  imports: [FormsModule],
  templateUrl: './task.html',
  styleUrl: './task.css',
})
export class Task {
  title = input<string>();
  description = input<string>();
  id = input<string>();
  totalTime = input<number>(0);

  timeAdded = output<number>();

  taskDragStarted = output<string>();
  taskDragEnded = output<void>();

  hours: number | null = null;
  minutes: number | null = null;

  addTime(): void {
    const totalMinutes = (this.hours ?? 0) * 60 + (this.minutes ?? 0);
    if (totalMinutes > 0) {
      this.timeAdded.emit(totalMinutes);
      this.hours = null;
      this.minutes = null;
    }
  }

  formatTime(minutes: number): string {
    const h = Math.floor(minutes / 60);
    const m = minutes % 60;
    return h > 0 ? `${h}h ${m}m` : `${m}m`;
  }

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

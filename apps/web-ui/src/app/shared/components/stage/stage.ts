import { Component, input, output, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Task as TaskComponent } from "../task/task";
import { Stage as StageModel } from './stage.model';

@Component({
  selector: 'app-stage',
  imports: [TaskComponent, FormsModule],
  templateUrl: './stage.html',
  styleUrl: './stage.css',
})
export class Stage {
  stage = input.required<StageModel>();
  stageIndex = input.required<number>();
  totalStages = input.required<number>();

  taskAdded = output<void>();
  taskRemoved = output<string>();
  taskMovedLeft = output<string>();
  taskMovedRight = output<string>();
  taskDropped = output<string>();
  stageDragged = output<string>();
  stageReordered = output<number>();
  taskCreated = output<{ title: string; description: string; totalTime: number }>();

  isDragOver = false;
  isStageDragOver = false;

  showAddTaskModal = signal(false);
  newTaskTitle = '';
  newTaskDescription = '';
  newTaskHours = 0;
  newTaskMinutes = 0;

  canMoveLeft(): boolean {
    return this.stageIndex() > 0;
  }

  canMoveRight(): boolean {
    return this.stageIndex() < this.totalStages() - 1;
  }

  onAddTask(): void {
    this.newTaskTitle = '';
    this.newTaskDescription = '';
    this.newTaskHours = 0;
    this.newTaskMinutes = 0;
    this.showAddTaskModal.set(true);
  }

  closeAddTaskModal(): void {
    this.showAddTaskModal.set(false);
  }

  createTask(): void {
    if (!this.newTaskTitle.trim()) return;

    const totalMinutes = this.newTaskHours * 60 + this.newTaskMinutes;
    this.taskCreated.emit({
      title: this.newTaskTitle,
      description: this.newTaskDescription,
      totalTime: totalMinutes
    });
    this.closeAddTaskModal();
  }

  onRemoveTask(taskId: string): void {
    this.taskRemoved.emit(taskId);
  }

  onMoveLeft(taskId: string): void {
    if (this.canMoveLeft()) {
      this.taskMovedLeft.emit(taskId);
    }
  }

  onMoveRight(taskId: string): void {
    if (this.canMoveRight()) {
      this.taskMovedRight.emit(taskId);
    }
  }

  onDragOver(event: DragEvent): void {
    event.preventDefault();
    event.dataTransfer!.dropEffect = 'move';
    this.isDragOver = true;
  }

  onDragLeave(): void {
    this.isDragOver = false;
  }

  onDrop(event: DragEvent): void {
    event.preventDefault();
    this.isDragOver = false;
    const taskId = event.dataTransfer?.getData('text/plain');
    if (taskId) {
      this.taskDropped.emit(taskId);
    }
  }

  onStageDragStart(event: DragEvent): void {
    event.dataTransfer!.setData('text/plain', this.stage().id);
    event.dataTransfer!.effectAllowed = 'move';
    this.stageDragged.emit(this.stage().id);
  }

  onStageDragOver(event: DragEvent): void {
    event.preventDefault();
    event.dataTransfer!.dropEffect = 'move';
    this.isStageDragOver = true;
  }

  onStageDragLeave(): void {
    this.isStageDragOver = false;
  }

  onStageDrop(event: DragEvent): void {
    event.preventDefault();
    this.isStageDragOver = false;
    const draggedStageId = event.dataTransfer?.getData('text/plain');
    if (draggedStageId && draggedStageId !== this.stage().id) {
      this.stageReordered.emit(this.stageIndex());
    }
  }
}

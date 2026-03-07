import { Component, input, output } from '@angular/core';
import { Task as TaskComponent } from "../task/task";
import { Stage as StageModel } from './stage.model';

@Component({
  selector: 'app-stage',
  imports: [TaskComponent],
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

  canMoveLeft(): boolean {
    return this.stageIndex() > 0;
  }

  canMoveRight(): boolean {
    return this.stageIndex() < this.totalStages() - 1;
  }

  onAddTask(): void {
    this.taskAdded.emit();
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
}

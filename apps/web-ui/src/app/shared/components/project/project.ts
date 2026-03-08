import { Component, inject, Input, signal, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { Stage } from "../stage/stage";
import { Stage as StageModel } from '../stage/stage.model';
import { ProjectService, Project as ProjectModel } from '../../../core/services/project.service';

@Component({
  selector: 'app-project',
  imports: [Stage, FormsModule],
  templateUrl: './project.html',
  styleUrl: './project.css',
})
export class Project implements OnInit {
  private route = inject(ActivatedRoute);
  private projectService = inject(ProjectService);

  @Input() projectId: string | null = null;

  project = signal<ProjectModel | null>(null);
  stages = signal<StageModel[]>([]);

  showAddStageModal = signal(false);
  newStageName = '';
  newStageOrder = 0;
  newStageType = 'Backlog';
  draggedStageIndex: number | null = null;

  ngOnInit(): void {
    if (this.projectId) {
      this.loadProject(this.projectId);
    } else {
      this.route.paramMap.subscribe(params => {
        const id = params.get('id');
        if (id) {
          this.loadProject(id);
        }
      });
    }
  }

  private loadProject(id: string): void {
    const fetched = this.projectService.getProject(id);
    if (fetched) {
      this.project.set(fetched);
      this.stages.set(fetched.stages);
    }
  }

  getStage(stageId: string): StageModel | undefined {
    return this.stages().find(s => s.id === stageId);
  }

  getStageIndex(stageId: string): number {
    return this.stages().findIndex(s => s.id === stageId);
  }

  addTask(stageId: string, taskData?: { title: string; description: string; totalTime: number }): void {
    this.stages.update(stages =>
      stages.map(s =>
        s.id === stageId
          ? { ...s, tasks: [...s.tasks, { 
              id: crypto.randomUUID(), 
              title: taskData?.title ?? 'New Task', 
              description: taskData?.description ?? 'Description', 
              totalTime: taskData?.totalTime ?? 0 
            }] }
          : s
      )
    );
  }

  removeTask(stageId: string, taskId: string): void {
    this.stages.update(stages =>
      stages.map(s =>
        s.id === stageId
          ? { ...s, tasks: s.tasks.filter(t => t.id !== taskId) }
          : s
      )
    );
  }

  moveTaskLeft(stageId: string, taskId: string): void {
    const currentIndex = this.getStageIndex(stageId);
    if (currentIndex > 0) {
      const currentStage = this.getStage(stageId);
      const targetStageId = this.stages()[currentIndex - 1].id;
      if (currentStage) {
        const task = currentStage.tasks.find(t => t.id === taskId);
        if (task) {
          this.stages.update(stages =>
            stages.map(s => {
              if (s.id === stageId) {
                return { ...s, tasks: s.tasks.filter(t => t.id !== taskId) };
              }
              if (s.id === targetStageId) {
                return { ...s, tasks: [...s.tasks, task] };
              }
              return s;
            })
          );
        }
      }
    }
  }

  moveTaskRight(stageId: string, taskId: string): void {
    const currentIndex = this.getStageIndex(stageId);
    if (currentIndex < this.stages().length - 1) {
      const currentStage = this.getStage(stageId);
      const targetStageId = this.stages()[currentIndex + 1].id;
      if (currentStage) {
        const task = currentStage.tasks.find(t => t.id === taskId);
        if (task) {
          this.stages.update(stages =>
            stages.map(s => {
              if (s.id === stageId) {
                return { ...s, tasks: s.tasks.filter(t => t.id !== taskId) };
              }
              if (s.id === targetStageId) {
                return { ...s, tasks: [...s.tasks, task] };
              }
              return s;
            })
          );
        }
      }
    }
  }

  moveTaskToStage(targetStageId: string, taskId: string): void {
    let task: import('../task/task.model').Task | undefined;
    let sourceStageId: string | undefined;

    for (const stage of this.stages()) {
      const found = stage.tasks.find(t => t.id === taskId);
      if (found) {
        task = found;
        sourceStageId = stage.id;
        break;
      }
    }

    if (!task || !sourceStageId) {
      return;
    }

    if (sourceStageId === targetStageId) {
      return;
    }

    this.stages.update(stages =>
      stages.map(s => {
        if (s.id === sourceStageId) {
          return { ...s, tasks: s.tasks.filter(t => t.id !== taskId) };
        }
        if (s.id === targetStageId) {
          return { ...s, tasks: [...s.tasks, task!] };
        }
        return s;
      })
    );
  }

  handleAddStage(): void {
    this.newStageName = '';
    this.newStageOrder = this.stages().length;
    this.newStageType = 'Backlog';
    this.showAddStageModal.set(true);
  }

  closeModal(): void {
    this.showAddStageModal.set(false);
  }

  createStage(): void {
    if (!this.newStageName.trim()) return;

    this.stages.update(stages => [
      ...stages,
      {
        id: crypto.randomUUID(),
        title: this.newStageName,
        tasks: [],
        order: this.newStageOrder,
        type: this.newStageType as 'Backlog' | 'InProgress' | 'Done'
      }
    ]);

    this.closeModal();
  }

  reorderStages(fromIndex: number, toIndex: number): void {
    if (fromIndex === toIndex) return;

    this.stages.update(stages => {
      const result = [...stages];
      const [moved] = result.splice(fromIndex, 1);
      result.splice(toIndex, 0, moved);
      return result;
    });
  }

  onStageDragStart(index: number): void {
    this.draggedStageIndex = index;
  }

  onStageReordered(toIndex: number): void {
    if (this.draggedStageIndex !== null && this.draggedStageIndex !== toIndex) {
      this.reorderStages(this.draggedStageIndex, toIndex);
    }
    this.draggedStageIndex = null;
  }
}

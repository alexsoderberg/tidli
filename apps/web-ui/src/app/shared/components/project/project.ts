import { Component, inject, Input, signal, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Stage } from "../stage/stage";
import { Stage as StageModel } from '../stage/stage.model';
import { ProjectService, Project as ProjectModel } from '../../../core/services/project.service';

@Component({
  selector: 'app-project',
  imports: [Stage],
  templateUrl: './project.html',
  styleUrl: './project.css',
})
export class Project implements OnInit {
  private route = inject(ActivatedRoute);
  private projectService = inject(ProjectService);

  @Input() projectId: string | null = null;

  project = signal<ProjectModel | null>(null);
  stages = signal<StageModel[]>([]);

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

  addTask(stageId: string): void {
    this.stages.update(stages =>
      stages.map(s =>
        s.id === stageId
          ? { ...s, tasks: [...s.tasks, { id: crypto.randomUUID(), title: 'New Task', description: 'Description' }] }
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

  handleAddStage(): void {
    console.log("handleAddStage triggered");
  }
}

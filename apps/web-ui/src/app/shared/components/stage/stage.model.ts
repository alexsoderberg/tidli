import { Task } from "../task/task.model";

export interface Stage {
  id: string;
  title: string;
  tasks: Task[];
  order?: number;
  type?: 'Backlog' | 'InProgress' | 'Done';
}
import { ProjectTaskUpdate } from './project-task-update';

export interface ProjectUpdate {
  key: string;
  tasks: Array<ProjectTaskUpdate>;
}

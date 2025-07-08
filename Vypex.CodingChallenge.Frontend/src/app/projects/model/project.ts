import { ProjectTask } from './project-task';

export interface Project {
  id: number;
  key: string;
  tasks: Array<ProjectTask>;
}

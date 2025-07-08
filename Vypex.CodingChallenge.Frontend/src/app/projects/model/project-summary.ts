import { ProjectTask } from './project-task';

export interface ProjectSummary {
  id: number;
  key: string;
  tasks: Array<ProjectTask>;
  totalPoints: number;
}

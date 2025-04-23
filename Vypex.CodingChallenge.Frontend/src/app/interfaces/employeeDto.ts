import { LeaveDTO } from './leaveDto';

export interface EmployeeListDTO {
  id: string;
  name: string;
  totalLeaveDays: number;
}

export interface EmployeeDTO {
  employeeId: string;
  leaves: LeaveDTO[];
}

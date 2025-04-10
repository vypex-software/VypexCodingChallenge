import { AsyncPipe } from '@angular/common';
import { Component, inject } from '@angular/core';
import { NzButtonComponent } from 'ng-zorro-antd/button';
import { NzTableModule } from 'ng-zorro-antd/table';
import { EditEmployeeModal } from './edit-employee/edit-employee.modal';
import { EmployeeApiService } from './services/employee-api.service';

@Component({
  selector: 'app-employees',
  imports: [
    NzTableModule,
    NzButtonComponent,
    AsyncPipe
  ],
  providers: [
    EditEmployeeModal
  ],
  templateUrl: './employees.component.html',
  styleUrl: './employees.component.scss'
})
export class EmployeesComponent {
  private readonly employeeApiService = inject(EmployeeApiService);
  private readonly editEmployeeModal = inject(EditEmployeeModal);

  public readonly employees$ = this.employeeApiService.getEmployees();

  public edit(employeeId: number) {
    this.editEmployeeModal.open({ id: employeeId })
      .afterClose
      .subscribe(result => {
        if (result === undefined) return; // Modal cancelled.

        // TODO: Handle result
      });
  }
}

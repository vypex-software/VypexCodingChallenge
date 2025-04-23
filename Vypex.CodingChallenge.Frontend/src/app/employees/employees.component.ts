import { NgIf } from '@angular/common';
import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { NzAlertModule } from 'ng-zorro-antd/alert';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzTableModule } from 'ng-zorro-antd/table';
import { finalize } from 'rxjs';
import { EmployeeService } from '../api/services/employee-api.service';
import { EmployeeEditModalComponent } from '../edit-employee/employee-edit-modal.component';
import { EmployeeDTO, EmployeeListDTO } from '../interfaces/employeeDto';
import { NzModalService } from 'ng-zorro-antd/modal';
import { EmployeeEditSignalModalComponent } from '../edit-employee-signal/edit-employee-signal.component';

@Component({
  selector: 'app-employee-list',
  standalone: true,
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [
    NgIf,
    NzTableModule,
    FormsModule,
    NzInputModule,
    EmployeeEditModalComponent,
    NzAlertModule,
  ],
  templateUrl: './employees.component.html',
  styleUrls: ['./employees.component.scss'],
})
export class EmployeeListComponent implements OnInit {
  employees: EmployeeListDTO[] = [];
  filteredEmployees: EmployeeListDTO[] = [];
  searchTerm = '';
  loading = false;
  error: string | null = null;
  selectedEmployeeId: string | null = null;
  selectedEmployee: EmployeeDTO | null = null;

  constructor(private employeeService: EmployeeService, private modal: NzModalService) {}

  ngOnInit() {
    this.loadEmployees();
  }

  loadEmployees() {
    this.loading = true;
    this.error = null;
    this.employeeService
      .getEmployees()
      .pipe(finalize(() => (this.loading = false)))
      .subscribe({
        next: (data) => {
          this.employees = data;
          this.filteredEmployees = data;
          this.loading = false;
        },
        error: (err) => {
          this.error = 'Failed to load employees.';
          this.loading = false;
        },
      });
  }

  onSearch() {
    this.applySearch();
  }

  applySearch() {
    const term = this.searchTerm.toLowerCase();
    this.filteredEmployees = this.employees.filter((e) =>
      e.name.toLowerCase().includes(term)
    );
  }

  reload() {
    this.loadEmployees();
  }

  editEmployee(employeeId: string) {
    this.loadEmployeeDetails(employeeId);
  }

  loadEmployeeDetails(employeeId: string) {
    this.loading = true;
    this.error = null;
    this.employeeService.getEmployeeDetails(employeeId).subscribe({
      next: (employee) => {
        this.selectedEmployee = employee;
        const modalRef = this.modal.create({
          nzTitle: 'Edit Employee',
          nzContent: EmployeeEditSignalModalComponent,
          nzFooter: null
        });
        const contentComponent = modalRef.getContentComponent();
        if (contentComponent) {
          contentComponent.selectedEmployee = employee;
        }
        modalRef.afterClose.subscribe((result) => {
          if (result === 'updated') {
            this.reload();
          }
        });
      },
      error: (err) => {
        this.error = 'Failed to load employee details.';
        this.loading = false;
      },
    });
  }
  onEmployeeUpdated() {
    this.selectedEmployeeId = null;
    this.reload();
  }
}

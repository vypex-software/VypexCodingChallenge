import { CommonModule } from '@angular/common';
import {
  Component,
  computed,
  effect,
  Input,
  OnInit,
  Output,
  EventEmitter,
  signal,
  WritableSignal,
} from '@angular/core';
import { NgIf, NgFor } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { EmployeeDTO } from '../interfaces/employeeDto';
import { LeaveDTO } from '../interfaces/leaveDto';
import { NgZorroModule } from '../modules/NgZorro.module';
import { EMPTY_GUID } from '../utils/constants';
import { EmployeeService } from '../api';

@Component({
  selector: 'edit-employee-signal',
  standalone: true,
  templateUrl: './edit-employee-signal.component.html',
  styleUrls: ['./edit-employee-signal.component.scss'],
  imports: [CommonModule, NgIf, NgFor, NgZorroModule, FormsModule],
})
export class EmployeeEditSignalModalComponent implements OnInit {
  @Input({ required: true }) selectedEmployee!: EmployeeDTO;
  @Output() close = new EventEmitter<void>();

  leaves: WritableSignal<LeaveDTO[]> = signal([]);
  error : WritableSignal<string | null> = signal(null);
  loading : WritableSignal<boolean> = signal(false);

  constructor(
    private employeeService: EmployeeService,
  ) {}

  ngOnInit(): void {
    if (this.selectedEmployee) {
      this.leaves.set(this.selectedEmployee.leaves || []);
    }
  }

  hasOverlap = computed(() => {
    const leaves = this.leaves();
    for (let i = 0; i < leaves.length; i++) {
      for (let j = i + 1; j < leaves.length; j++) {
        const a = leaves[i];
        const b = leaves[j];
        if (
          new Date(a.startDate) <= new Date(b.endDate) &&
          new Date(b.startDate) <= new Date(a.endDate)
        ) {
          return true;
        }
      }
    }
    return false;
  });

  isValidLeave = (leave: LeaveDTO) =>
    leave.startDate && leave.endDate &&
    new Date(leave.startDate) < new Date(leave.endDate);

  addLeave() {
    this.leaves.update((current) => [
      ...current,
      {
        leaveId: EMPTY_GUID,
        startDate: '',
        endDate: '',
      },
    ]);
  }

  removeLeave(index: number) {
    this.leaves.update((current) => {
      const updated = [...current];
      updated.splice(index, 1);
      return updated;
    });
  }

  updateLeave(index: number, key: 'startDate' | 'endDate', value: string) {
    this.leaves.update((leaves) => {
      const copy = [...leaves];
      copy[index] = { ...copy[index], [key]: value };
      return copy;
    });
  }

  onSubmit() {
    const allValid = this.leaves().every(this.isValidLeave);
    if (!allValid || this.hasOverlap()) {
      this.error.set('Fix date errors or overlaps before saving.');
      return;
    }

    this.loading.set(true);

    const payload: EmployeeDTO = {
      employeeId: this.selectedEmployee.employeeId,
      leaves: this.leaves(),
    };

    this.employeeService.updateEmployee(payload).subscribe({
      next: (response) => {
        if (response) this.close.emit();
        else this.error.set('Server rejected the update.');
      },
      error: () => this.error.set('Network or server error.'),
      complete: () => this.loading.set(false),
    });
  }

  onCancel() {
    this.close.emit();
  }
}

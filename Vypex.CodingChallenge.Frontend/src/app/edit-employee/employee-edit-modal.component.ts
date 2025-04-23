import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import {
  AbstractControl,
  FormArray,
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  ValidationErrors,
  Validators,
} from '@angular/forms';
import { EmployeeService } from '../api';
import { EmployeeDTO } from '../interfaces/employeeDto';
import { LeaveDTO } from '../interfaces/leaveDto';
import { NgZorroModule } from '../modules/NgZorro.module';
import { EMPTY_GUID } from '../utils/constants';

@Component({
  selector: 'employee-edit-modal',
  standalone: true,
  templateUrl: './employee-edit-modal.component.html',
  styleUrls: ['./employee-edit-modal.component.scss'],
  imports: [CommonModule, ReactiveFormsModule, NgZorroModule],
})
export class EmployeeEditModalComponent implements OnInit {
  @Input() selectedEmployee!: EmployeeDTO;
  @Output() close = new EventEmitter<void>();
  loading = false;
  error: string | null = null;
  form!: FormGroup;
  constructor(
    private fb: FormBuilder,
    private employeeService: EmployeeService,
  ) {}
  ngOnInit(): void {
    if (this.selectedEmployee) {
      this.setUpForm();
    }
  }

  ngOnChanges() {
    if (this.selectedEmployee) {
      this.setUpForm();
    }
  }
  ngOnDestroy() {
    this.form.reset();
  }

  onCancel() {
    this.close.emit();
  }

  setUpForm() {
    this.form = this.fb.group(
      {
        leaves: this.fb.array(
          this.selectedEmployee.leaves.map((leave) =>
            this.createLeaveGroup(leave)
          )
        ),
      },
      { validators: this.leavesOverlapValidator }
    );
  }

  leavesOverlapValidator(group: AbstractControl): ValidationErrors | null {
    const leavesArray = group.get('leaves') as FormArray;
    const leaves = leavesArray.controls.map((ctrl) => ({
      start: new Date(ctrl.get('startDate')?.value),
      end: new Date(ctrl.get('endDate')?.value),
    }));

    for (let i = 0; i < leaves.length; i++) {
      for (let j = i + 1; j < leaves.length; j++) {
        if (
          leaves[i].start <= leaves[j].end &&
          leaves[j].start <= leaves[i].end
        ) {
          return { leavesOverlap: true };
        }
      }
    }
    return null;
  }

  get leaves(): FormArray {
    return this.form.get('leaves') as FormArray;
  }

  createLeaveGroup(leave?: LeaveDTO): FormGroup {
    return this.fb.group(
      {
        leaveId: leave?.leaveId ?? EMPTY_GUID,
        startDate: [leave?.startDate ?? null, Validators.required],
        endDate: [leave?.endDate ?? null, Validators.required],
      },
      { validators: this.leaveDatesValidator }
    );
  }

  addLeave() {
    this.leaves.push(this.createLeaveGroup());
  }

  removeLeave(index: number) {
    this.leaves.removeAt(index);
  }

  leaveDatesValidator(group: AbstractControl): ValidationErrors | null {
    const start = group.get('startDate')?.value;
    const end = group.get('endDate')?.value;
    if (start && end && new Date(start) >= new Date(end)) {
      return { dateOrderInvalid: true };
    }
    return null;
  }

  hasOverlap(): boolean {
    const dates = this.leaves.controls.map((ctrl) => ({
      start: new Date(ctrl.get('startDate')?.value),
      end: new Date(ctrl.get('endDate')?.value),
    }));

    for (let i = 0; i < dates.length; i++) {
      for (let j = i + 1; j < dates.length; j++) {
        if (dates[i].start <= dates[j].end && dates[j].start <= dates[i].end) {
          return true;
        }
      }
    }
    return false;
  }

  onSubmit() {
    if (this.form.invalid || this.hasOverlap()) {
      this.error = 'Fix date errors or overlaps before saving.';
      return;
    }
    this.loading = true;
    const payload: EmployeeDTO = {
      employeeId: this.selectedEmployee.employeeId,
      leaves: this.form.value.leaves,
    };

    this.employeeService.updateEmployee(payload).subscribe({
      next: (response) => {
        if (response) {
          this.close.emit();
        } else {
          this.error = 'Server rejected the update.';
        }
      },
      error: () => {
        this.error = 'Network or server error.';
      },
      complete: () => {
        this.loading = false;
      },
    });
  }
}

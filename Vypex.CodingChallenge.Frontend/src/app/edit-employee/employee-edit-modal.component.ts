import { OverlayModule } from '@angular/cdk/overlay';
import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import {
  AbstractControl,
  FormArray,
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  ValidationErrors,
  Validators,
} from '@angular/forms';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzDatePickerModule } from 'ng-zorro-antd/date-picker';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzLayoutModule } from 'ng-zorro-antd/layout';
import { NzModalModule, NzModalService } from 'ng-zorro-antd/modal';
import { NzTypographyModule } from 'ng-zorro-antd/typography';
import { EmployeeDTO } from '../interfaces/employeeDto';
import { LeaveDTO } from '../interfaces/leaveDto';
import { EmployeeService } from '../api';

@Component({
  selector: 'employee-edit-modal',
  standalone: true,
  templateUrl: '/employee-edit-modal.component.html',
  styleUrls: ['/employee-edit-modal.component.scss'],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    NzInputModule,
    NzButtonModule,
    NzDatePickerModule,
    NzFormModule,
    NzTypographyModule,
    NzModalModule,
    NzLayoutModule,
    OverlayModule,
  ],
})
export class EmployeeEditModalComponent implements OnInit {
  @Input() selectedEmployee!: EmployeeDTO;
  @Output() close = new EventEmitter<void>();
  isVisible = true;
  loading = false;
  error: string | null = null;
  form!: FormGroup;
  constructor(
    private fb: FormBuilder,
    private modal: NzModalService,
    private employeeService: EmployeeService
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

  setUpForm() {
    this.form = this.fb.group(
      {
        leaves: this.fb.array(
          this.selectedEmployee.leaves.map((leave) =>
            this.createLeaveGroup(leave)
          )
        ),
      },
      { validators: this.leavesDoNotOverlapValidator }
    );
  }

  leavesDoNotOverlapValidator(group: AbstractControl): ValidationErrors | null {
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
      this.modal.error({
        nzTitle: 'Invalid Leave Details',
        nzContent:
          'Please ensure all leave entries are valid and not overlapping.',
      });
      return;
    }

    const result: EmployeeDTO = {
      id: this.selectedEmployee.id,
      leaves: this.form.value.leaves,
    };

    this.employeeService.updateEmployee(result).subscribe({
      next: (response) => {
        if (response) {
          this.modal.success({
            nzTitle: 'Success',
            nzContent: 'Leave details updated successfully.',
          });
          this.isVisible = false;
          this.close.emit();
        } else {
          this.modal.error({
            nzTitle: 'Error',
            nzContent: 'Failed to update leave details.',
          });
        }
      },
      error: (err) => {
        this.modal.error({
          nzTitle: 'Error',
          nzContent: 'Failed to update leave details.',
        });
        this.error = 'Failed to update leave details.';
      },
      complete: () => {
        this.loading = false;
      },
    });
  }
}

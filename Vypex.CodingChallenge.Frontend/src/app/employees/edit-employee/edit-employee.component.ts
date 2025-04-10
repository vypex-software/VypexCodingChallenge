import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NZ_MODAL_DATA, NzModalRef } from 'ng-zorro-antd/modal';
import { validateForm } from '../../common/validateForm';
import { EditEmployeeBindings, EditEmployeeResult } from './edit-employee.modal';

@Component({
  selector: 'app-edit-employee',
  templateUrl: './edit-employee.component.html',
  styleUrls: ['./edit-employee.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    NzFormModule,
    NzButtonModule,
    NzInputModule
  ]
})
export class EditEmployeeComponent {
  private readonly modalRef = inject(NzModalRef<EditEmployeeComponent, EditEmployeeResult>);
  private readonly modalData = inject<EditEmployeeBindings>(NZ_MODAL_DATA);
  private readonly fb = inject(FormBuilder);

  public readonly id = this.modalData.id;

  protected readonly form = this.fb.group({
    name: this.fb.nonNullable.control(null!, Validators.required)
  })

  protected cancel(): void {
    this.modalRef.triggerCancel();
  }

  protected submit(): void {
    if (!validateForm(this.form)) return;

    const formValue = this.form.getRawValue();

    // Handle form submission logic here

    this.modalRef.close({});
  }
}

import { AsyncPipe } from '@angular/common';
import { ChangeDetectionStrategy, Component, DestroyRef, inject, Input, OnInit, signal, viewChild } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { NzAlertComponent } from 'ng-zorro-antd/alert';
import { NzButtonComponent } from 'ng-zorro-antd/button';
import { isNotNil } from 'ng-zorro-antd/core/util';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzInputDirective } from 'ng-zorro-antd/input';
import { NZ_MODAL_DATA, NzModalRef } from 'ng-zorro-antd/modal';
import { filter, shareReplay } from 'rxjs';
import { toFormSignal } from '../../common/toFormSignal';
import { validateForm } from '../../common/validateForm';
import { ProjectTaskUpdate } from '../model';
import { ProjectApiService } from '../services/project-api.service';
import { EditProjectBindings, EditProjectResult } from './edit-project.modal';
import { ProjectTasksFormComponent } from './project-tasks-form/project-tasks-form.component';

@Component({
  selector: 'app-edit-project',
  templateUrl: './edit-project.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
  imports: [
    ProjectTasksFormComponent,
    ReactiveFormsModule,
    NzFormModule,
    NzButtonComponent,
    NzInputDirective,
    NzAlertComponent,
    AsyncPipe
  ]
})
export class EditProjectComponent implements OnInit {
  private readonly modalRef = inject(NzModalRef<EditProjectComponent, EditProjectResult>);
  private readonly modalData = inject<EditProjectBindings>(NZ_MODAL_DATA);
  private readonly projectApiService = inject(ProjectApiService);
  private readonly fb = inject(FormBuilder);
  private readonly destroyRef = inject(DestroyRef);

  @Input({ required: true }) public readonly id = this.modalData.id;

  protected readonly project$ = this.projectApiService.getProject(this.id).pipe(
    shareReplay({ bufferSize: 1, refCount: true })
  );

  protected readonly form = this.fb.group({
    key: this.fb.nonNullable.control<string>(null!, Validators.required),
    tasks: this.fb.nonNullable.control<Array<ProjectTaskUpdate>>([])
  });

  protected readonly tasks = toFormSignal(this.form.controls.tasks);
  protected readonly error = signal<Error | undefined>(undefined);

  private readonly tasksForm = viewChild(ProjectTasksFormComponent);

  public ngOnInit(): void {
    this.project$.pipe(
      filter(isNotNil),
      takeUntilDestroyed(this.destroyRef)
    ).subscribe({
      next: project => this.form.patchValue(project),
      error: error => this.error.set(error)
    });
  }

  protected cancel(): void {
    this.modalRef.triggerCancel();
  }

  protected submit(): void {
    if (!validateForm(this.form) || !this.tasksForm()?.validate()) return;

    const formValue = this.form.getRawValue();

    this.projectApiService.updateProject(this.id, formValue).pipe(
      takeUntilDestroyed(this.destroyRef)
    ).subscribe({
      next: project => this.modalRef.close({ id: project?.id }),
      error: error => this.error.set(error)
    });
  }
}

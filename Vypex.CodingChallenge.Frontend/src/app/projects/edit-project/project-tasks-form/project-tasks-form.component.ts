import { Component, effect, inject, model, untracked } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { NzButtonComponent } from 'ng-zorro-antd/button';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzInputDirective } from 'ng-zorro-antd/input';
import { NzInputNumberComponent } from 'ng-zorro-antd/input-number';
import { toFormSignal } from '../../../common/toFormSignal';
import { validateForm } from '../../../common/validateForm';
import { ProjectTaskUpdate } from '../../model';

interface ProjectTaskControls {
  description: FormControl<string>;
  points: FormControl<number>;
}

@Component({
  selector: 'app-project-tasks-form',
  imports: [
    NzFormModule,
    NzInputDirective,
    ReactiveFormsModule,
    NzButtonComponent,
    NzInputNumberComponent
  ],
  templateUrl: './project-tasks-form.component.html',
})
export class ProjectTasksFormComponent {
  private readonly fb = inject(FormBuilder);

  public readonly tasks = model<Array<ProjectTaskUpdate>>([]);

  protected readonly formArray = this.fb.array<FormGroup<ProjectTaskControls>>([]);
  protected readonly formArrayValue = toFormSignal(this.formArray);

  public constructor() {
    effect(() => {
      const tasks = untracked(this.tasks);
      for (const task of tasks) {
        this.formArray.push(this.createTaskFormGroup(task));
      }
    });

    effect(() => {
      this.tasks.set(this.formArrayValue());
    });
  }

  public validate(): boolean {
    return validateForm(this.formArray);
  }

  protected removeTask(index: number): void {
    this.formArray.removeAt(index);
  }

  protected addTask(): void {
    this.formArray.push(this.createTaskFormGroup());
  }

  private createTaskFormGroup(task?: ProjectTaskUpdate): FormGroup<ProjectTaskControls> {
    return this.fb.group({
      description: this.fb.nonNullable.control(task?.description!, Validators.required),
      points: this.fb.nonNullable.control(task?.points!, [Validators.required, Validators.min(1)]),
    });
  }
}

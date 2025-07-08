import { AsyncPipe } from '@angular/common';
import { Component, inject } from '@angular/core';
import { toObservable } from '@angular/core/rxjs-interop';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { NzAlertComponent } from 'ng-zorro-antd/alert';
import { NzButtonComponent } from 'ng-zorro-antd/button';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzInputDirective } from 'ng-zorro-antd/input';
import { NzTableModule } from 'ng-zorro-antd/table';
import { BehaviorSubject, catchError, combineLatest, defer, map, of, startWith, switchMap } from 'rxjs';
import { toFormSignal } from '../common/toFormSignal';
import { EditProjectModal } from './edit-project/edit-project.modal';
import { ProjectApiService } from './services/project-api.service';

@Component({
  selector: 'app-projects',
  imports: [
    NzTableModule,
    ReactiveFormsModule,
    NzFormModule,
    NzButtonComponent,
    NzAlertComponent,
    NzInputDirective,
    AsyncPipe
  ],
  providers: [
    EditProjectModal
  ],
  templateUrl: './projects.component.html',
})
export class ProjectsComponent {
  private readonly projectApiService = inject(ProjectApiService);
  private readonly editProjectModal = inject(EditProjectModal);

  protected readonly searchControl = new FormControl<string | null>(null);

  protected readonly projects$ = defer(() => {
    return combineLatest([
      this.search$,
      this.reload$
    ]).pipe(
      switchMap(([search]) => this.projectApiService.getProjects(search).pipe(
        map(data => ({ data, loading: false, error: undefined })),
        startWith({ data: undefined, loading: true, error: undefined }),
        catchError(error => of({ data: undefined, loading: false, error }))
      ))
    );
  });

  private readonly search = toFormSignal(this.searchControl);
  private readonly search$ = toObservable(this.search);
  private readonly reload$ = new BehaviorSubject<void>(void 0);

  protected edit(projectId: number) {
    this.editProjectModal.open({ id: projectId })
      .afterClose
      .subscribe(result => {
        if (result === undefined) return; // Modal cancelled.
        else this.reload();
      });
  }

  protected reload(): void {
    this.reload$.next();
  }
}

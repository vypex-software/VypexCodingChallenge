import { inject, Injectable } from '@angular/core';
import { NzModalRef, NzModalService } from 'ng-zorro-antd/modal';
import { EditProjectComponent } from './edit-project.component';

export type EditProjectResult = { id: number | undefined };

export interface EditProjectBindings {
  id: number;
}

@Injectable()
export class EditProjectModal {
  private readonly modalService = inject(NzModalService);

  public open(bindings?: EditProjectBindings): NzModalRef<EditProjectComponent, EditProjectResult> {
    return this.modalService.create({
      nzTitle: 'Edit project',
      nzContent: EditProjectComponent,
      nzData: bindings ?? {},
      nzMaskClosable: false,
      nzFooter: null
    });
  }
}

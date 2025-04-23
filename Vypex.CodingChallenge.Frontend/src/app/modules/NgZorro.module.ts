import { NgModule } from '@angular/core';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzDatePickerModule } from 'ng-zorro-antd/date-picker';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzLayoutModule } from 'ng-zorro-antd/layout';
import { NzModalModule } from 'ng-zorro-antd/modal';
import { NzTypographyModule } from 'ng-zorro-antd/typography';
import { NzAlertModule } from 'ng-zorro-antd/alert';

@NgModule({
  exports: [
    NzIconModule,
    NzButtonModule,
    NzInputModule,
    NzButtonModule,
    NzDatePickerModule,
    NzFormModule,
    NzTypographyModule,
    NzModalModule,
    NzLayoutModule,
    NzAlertModule 
  ],
})
export class NgZorroModule {}

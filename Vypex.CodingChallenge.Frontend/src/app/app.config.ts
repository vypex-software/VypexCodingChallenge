import {
  ApplicationConfig,
  provideExperimentalZonelessChangeDetection,
} from '@angular/core';
import { provideAnimations } from '@angular/platform-browser/animations';
import { provideRouter } from '@angular/router';

import { NZ_I18N, zh_CN } from 'ng-zorro-antd/i18n';
import { provideHttpClient } from '@angular/common/http';
import { routes } from './app.routes';

export const appConfig: ApplicationConfig = {
  providers: [
    provideExperimentalZonelessChangeDetection(),
    provideHttpClient(),
    provideRouter(routes),
    provideAnimations(),
    { provide: NZ_I18N, useValue: zh_CN }
  ],
};

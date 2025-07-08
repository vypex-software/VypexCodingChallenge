import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  template: `
    <div class="flex flex-col py-3">
      <div class="flex flex-row justify-center">
        <img src="/vypex.png" alt="Logo" />
      </div>
      <router-outlet></router-outlet>
    </div>
  `
})
export class AppComponent {
}

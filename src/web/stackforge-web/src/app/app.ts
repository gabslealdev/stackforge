import { Component } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { HeaderComponent } from './layout/header.component/header.component';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, HeaderComponent],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected title = 'stackforge-web';
}

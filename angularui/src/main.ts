import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './app/app.config';
import { AppComponent } from './app/app.component';
import 'jquery';
import 'datatables.net';
import 'datatables.net-bs5';


bootstrapApplication(AppComponent, appConfig)
  .catch((err) => console.error(err));

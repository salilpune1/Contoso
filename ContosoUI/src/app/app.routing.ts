import { RouterModule, Routes } from '@angular/router';
import {LoginComponent} from "./login/login.component";
import {ListContactComponent} from "./contact/list-contact/list-contact.component";
import {ContactComponent} from "./contact/contact/contact.component";

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'list-contact', component: ListContactComponent },
  { path: 'contact', component: ContactComponent },
  {path : '', component : ListContactComponent}
];

export const routing = RouterModule.forRoot(routes);

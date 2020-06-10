import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { ExampleComponent } from './example.component';
import { ExternalOrganizationService } from '../services/external-organization.service';

const routes: Routes = [
  {
    path: '',

    children: [
      {
        path: '',
        pathMatch: 'full',
        redirectTo: 'ex',
      },
      {
        path: 'ex',
        component: ExampleComponent
      }
    ]
  }
]

@NgModule({
  declarations: [ExampleComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule,ExampleComponent],
  providers: [ExternalOrganizationService]
})
export class ExampleModule { }

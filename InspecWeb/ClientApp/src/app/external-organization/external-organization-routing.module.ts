import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { GgcOpmComponent } from './ggc-opm/ggc-opm.component';
import { Opm1111Component } from './opm1111/opm1111.component';
import { OtpsComponent } from './otps/otps.component';

const routes: Routes = [
  {
    path: '',

    children: [
      {
        path: '',
        pathMatch: 'full',
        // redirectTo: 'otps',
      },
      {
        path: 'ggc-opm',
        component: GgcOpmComponent
      },
      {
        path: 'opm-1111',
        component: Opm1111Component
      },
      {
        path: 'otps',
        component: OtpsComponent
      },
    ]
  },
]

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ExternalOrganizationRoutingModule { }

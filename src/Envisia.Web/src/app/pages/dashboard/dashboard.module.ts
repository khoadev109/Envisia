import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { NgxTablePaginationModule } from 'ngx-table-pagination';
import { NgxPaginationModule } from 'ngx-pagination';
import { DashboardComponent } from './dashboard.component';
import { WidgetsModule } from '../../_metronic/partials';

@NgModule({
  declarations: [DashboardComponent],
  imports: [
    CommonModule,
    RouterModule.forChild([
      {
        path: '',
        component: DashboardComponent,
      },
    ]),
    NgxTablePaginationModule,
    NgxPaginationModule,
    WidgetsModule,
  ],
})
export class DashboardModule {}

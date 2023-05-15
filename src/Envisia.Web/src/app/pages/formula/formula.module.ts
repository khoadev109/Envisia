import {NgModule} from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormulaComponent } from './formula.component';
import { WidgetsModule } from '../../_metronic/partials';


@NgModule({
    declarations: [FormulaComponent],
    imports: [
        CommonModule,
        RouterModule.forChild([
            {
                path: '',
                component: FormulaComponent,
            }
        ]),
        WidgetsModule,
    ]
})

export class FormulaModule {}
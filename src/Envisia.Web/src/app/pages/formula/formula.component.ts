import {
  AfterViewInit,
  ChangeDetectorRef,
  Component,
  OnInit,
  ViewChild,
} from '@angular/core';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { CommonService } from 'src/app/services/common.service';
import * as XLSX from 'xlsx';
import 'chartist/dist/index.css';
import { LineChart, easings } from 'chartist';

declare const google: any;

type Tabs = 'Stats' | 'Contacts' | 'ServiceArea';

@Component({
  selector: 'app-formula',
  templateUrl: './formula.component.html',
  styleUrls: ['./formula.component.scss'],
})
export class FormulaComponent implements OnInit, AfterViewInit {
  map: any;
  @ViewChild('mapElement') mapElement: any;
  activeTab: Tabs = 'Stats';

  constructor(
    private sanitizer: DomSanitizer,
    private commonService: CommonService,
    private changeDetectorRef: ChangeDetectorRef
  ) {}

  loadSelectList() {}

  ngOnInit(): void {
    this.loadSelectList();

    const chart = new LineChart(
      '#chart',
      {
        labels: [2018, 2019, 2020, 2021, 2022, 2023],
        series: [[100, 200, 300, 400, 500, 600]],
      },
      {
        width: 500,
        low: 0,
        showArea: true,
        axisX: {
          onlyInteger: true,
        },
        classNames: {
          line: 'ct-line ct-dashed',
        },
        lineSmooth: true,
      }
    );
    chart.on('draw', (data) => {
      if (data.type === 'line' || data.type === 'area') {
        data.element.animate({
          d: {
            begin: 2000 * data.index,
            dur: 2000,
            from: data.path
              .clone()
              .scale(1, 0)
              .translate(0, data.chartRect.height())
              .stringify(),
            to: data.path.clone().stringify(),
            easing: easings.easeOutQuint,
          },
        });
      }
    });
  }

  setTab(tab: Tabs) {
    this.activeTab = tab;
  }

  activeClass(tab: Tabs) {
    return tab === this.activeTab ? 'show active' : '';
  }

  exportToExcel() {
    const fileName = 'example.xlsx';

    const worksheet: XLSX.WorkSheet = XLSX.utils.json_to_sheet([
      { id: 1, name: 'John' },
      { id: 2, name: 'Jane' },
    ]);

    const workbook: XLSX.WorkBook = {
      Sheets: { data: worksheet },
      SheetNames: ['data'],
    };

    XLSX.writeFile(workbook, fileName);
  }

  ngAfterViewInit(): void {}
}

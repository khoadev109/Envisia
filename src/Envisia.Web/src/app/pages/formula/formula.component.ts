import {
  AfterViewInit,
  ChangeDetectorRef,
  Component,
  OnInit,
  ViewChild,
} from '@angular/core';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { CommonService } from 'src/app/services/common.service';

declare const google: any;

type Tabs = 'KeyFigures' | 'Officers' | 'ServiceArea';

@Component({
  selector: 'app-formula',
  templateUrl: './formula.component.html',
  styleUrls: ['./formula.component.scss'],
})
export class FormulaComponent implements OnInit, AfterViewInit {
  map: any;
  @ViewChild('mapElement') mapElement: any;
  activeTab: Tabs = 'KeyFigures';

  constructor(
    private sanitizer: DomSanitizer,
    private commonService: CommonService,
    private changeDetectorRef: ChangeDetectorRef
  ) {}

  loadSelectList() {}

  ngOnInit(): void {
    this.loadSelectList();
  }

  setTab(tab: Tabs) {
    this.activeTab = tab;
  }

  activeClass(tab: Tabs) {
    return tab === this.activeTab ? 'show active' : '';
  }

  ngAfterViewInit(): void {}
}

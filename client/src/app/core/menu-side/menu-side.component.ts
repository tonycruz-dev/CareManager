import { Component, OnInit, AfterContentInit, ViewChild, ElementRef, TemplateRef, HostBinding } from '@angular/core';

declare const $: any;

@Component({
  selector: 'app-menu-side',
  templateUrl: './menu-side.component.html',
  styleUrls: ['./menu-side.component.css']
})
export class MenuSideComponent implements OnInit, AfterContentInit  {

  @HostBinding('class') classes = 'sidebar-nav';

  @ViewChild('el', { static: true, read: ElementRef }) el: ElementRef;

  constructor() { }

  ngOnInit(): void {
  }
  ngAfterContentInit() {
    $(this.el.nativeElement).metisMenu();
  }

}

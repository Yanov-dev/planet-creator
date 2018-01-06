import {Component, EventEmitter, Input, Output} from '@angular/core';
import {Shema} from '../../models/Shema';
import {MatSelectChange, MatSliderChange} from '@angular/material';

@Component({
  selector: 'app-planet2d-properties',
  templateUrl: './planet2d.properties.component.html'
})
export class Planet2dPropertiesComponent {
  @Input() shema: Shema;

  @Output()
  change: EventEmitter<any> = new EventEmitter<any>();

  changeProjection(val: MatSelectChange) {
    this.shema.projectionType = val.value;
    this.change.emit();
  }

  changeLat(val: MatSliderChange) {
    this.shema.lat = val.value;
    this.change.emit();
  }

  changeLng(val: MatSliderChange) {
    this.shema.lng = val.value;
    this.change.emit();
  }
}

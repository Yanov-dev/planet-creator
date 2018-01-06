import {Component, EventEmitter, Input, Output} from '@angular/core';
import {ShemaLayer} from '../../models/shemaLayer';
import {ColorLevel} from '../../models/ColorLevel';

@Component({
  selector: 'app-layers-list',
  templateUrl: './layers.list.component.html'
})
export class LayersListComponent {

  @Input()
  layers: ShemaLayer[];

  @Output() change: EventEmitter<any> = new EventEmitter();

  selectedLayer: ShemaLayer;

  constructor() {
  }

  callChanged() {
    this.change.emit();
  }

  switch(layer: ShemaLayer) {
    this.selectedLayer = layer;
  }
}

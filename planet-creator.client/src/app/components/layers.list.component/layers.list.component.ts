import {Component, Input} from '@angular/core';
import {ShemaLayer} from '../../models/shemaLayer';
import {ColorLevel} from '../../models/ColorLevel';

@Component({
  selector: 'app-layers-list',
  templateUrl: './layers.list.component.html'
})
export class LayersListComponent {

  @Input()
  layers: ShemaLayer[];

  selectedLayer: ShemaLayer;

  constructor() {
  }

  switch(layer: ShemaLayer) {
    this.selectedLayer = layer;
  }
}

import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {PlanetGenerationService} from '../../services/planet-generation.service';
import {ShemaLayer} from '../../Models/ShemaLayer';

@Component({
  selector: 'app-generation-panel',
  templateUrl: './generation-panel.component.html'
})
export class GenerationPanelComponent implements OnInit {

  @Output()
  generate: EventEmitter<ShemaLayer> = new EventEmitter<ShemaLayer>();

  @Output()
  objDepthChange: EventEmitter<number> = new EventEmitter<number>();

  @Output()
  imageDepthChange: EventEmitter<number> = new EventEmitter<number>();

  constructor() {
  }

  ngOnInit() {
  }

  test(any:any)
  {
    console.log(any);
  }
}

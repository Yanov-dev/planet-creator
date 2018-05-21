import {Component, OnInit} from '@angular/core';
import {PlanetGenerationService} from '../../services/planet-generation.service';

@Component({
  selector: 'app-generation-panel',
  templateUrl: './generation-panel.component.html'
})
export class GenerationPanelComponent implements OnInit {

  constructor(private planetGenerationService: PlanetGenerationService) {
  }

  ngOnInit() {
  }

  generate() {
    this.planetGenerationService.generate();
  }
}

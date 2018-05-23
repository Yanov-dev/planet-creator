import {Component, OnInit} from '@angular/core';
import {PlanetGenerationService} from '../../services/planet-generation.service';
import {ShemaLayer} from '../../Models/ShemaLayer';
import {Generate3dRequest} from '../../Models/Generate3dRequest';

@Component({
  selector: 'app-editor-page',
  templateUrl: './editor-page.component.html'
})
export class EditorPageComponent implements OnInit {

  private _layer: ShemaLayer;

  private _objDepth: number = 4;
  private _imageDepth: number = 6;

  constructor(private planetGenerationService: PlanetGenerationService) {
  }

  ngOnInit() {
  }

  layerChanged(layer: ShemaLayer) {
    this._layer = layer;
  }

  generate() {
    const request = new Generate3dRequest();
    request.Layer = this._layer;
    request.Seed = 0;
    request.ObjDepth = this._objDepth;
    request.ImageDepth = this._imageDepth;
    this.planetGenerationService.generate(request);
  }
}

import {Component, Input} from '@angular/core';
import {Shema} from '../../models/Shema';
import {Generator2dService} from '../../services/generator2dService';

@Component({
  selector: 'app-planet2d-render',
  templateUrl: './planet2d.render.component.html'
})
export class Planet2dRenderComponent {

  @Input() shema: Shema;

  planet: string;

  private _renderRequested: boolean;
  private _renderInProgress: boolean;

  constructor(private _generator2dService: Generator2dService) {

  }

  async render() {
    this._renderRequested = true;

    if (this._renderInProgress)
      return;

    this._renderRequested = false;
    this._renderInProgress = true;

    for (let size = 64; size <= 256; size *= 2) {
      if (this._renderRequested) {
        this._renderRequested = false;
        size = 64;
      }

      await this.generateWithSize(size);
    }

    this._renderInProgress = false;
  }

  async generateWithSize(size: number) {
    const res = await this._generator2dService.Generate(this.shema, size).toPromise();
    this.planet = 'data:image/jpeg;base64,' + res;
  }
}

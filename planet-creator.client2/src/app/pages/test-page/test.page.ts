import {Component, ViewChild} from '@angular/core';
import {Generator2dService} from '../../services/generator2dService';
import {Shema} from '../../models/Shema';
import {Planet2dRenderComponent} from '../../components/planet2d.render.component/planet2d.render.component';

@Component({
  templateUrl: './test.page.html',
})
export class TestPageComponent {
  title = 'app';

  planet: string;

  @ViewChild('planet2d') planet2dRender: Planet2dRenderComponent;

  shema: Shema;

  constructor(private _generator2dService: Generator2dService) {
    this.shema = new Shema();
    this.shema.layers.push({
      'colors': [
        {
          'level': 0,
          'color': '#000000'
        },
        {
          'level': 640,
          'color': '#483D8B'
        },
        {
          'level': 675,
          'color': '#8470FF'
        },
        {
          'level': 770,
          'color': '#EEDD82'
        },
        {
          'level': 945,
          'color': '#6B8E23'
        },
        {
          'level': 1275,
          'color': '#000000'
        }
      ],
      'isEnable': true,
      'seed': 0.0
    });
  }

  generate() {
    this.planet2dRender.render();
  }
}

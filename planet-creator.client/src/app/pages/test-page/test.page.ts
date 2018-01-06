import {Component} from '@angular/core';
import {Generator2dService} from '../../services/generator2dService';
import {Shema} from '../../models/Shema';

@Component({
  templateUrl: './test.page.html',
})
export class TestPageComponent {
  title = 'app';

  planet: string;

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
    console.log(this.shema);
  }

  layersChanged() {
    console.log('changed');
    this.generateWithSize(50);
  }

  generate() {
    // this.generateWithSize(100);
    // this.generateWithSize(150);
    this.generateWithSize(200);
    // this.generateWithSize(250);
    // this.generateWithSize(500);
  }

  generateWithSize(size: number) {
    this._generator2dService.Generate(this.shema, size).subscribe(e => {
      this.planet = 'data:image/jpeg;base64,' + e;
    });
  }
}

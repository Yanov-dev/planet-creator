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
          'color': '#FF000000'
        },
        {
          'level': 640,
          'color': '#FF483D8B'
        },
        {
          'level': 675,
          'color': '#FF8470FF'
        },
        {
          'level': 770,
          'color': '#FFEEDD82'
        },
        {
          'level': 945,
          'color': '#FF6B8E23'
        },
        {
          'level': 1275,
          'color': '#FF000000'
        }
      ],
      'isEnable': true,
      'seed': 0.0
    });
    console.log(this.shema);
  }

  generate() {
    this._generator2dService.Generate(this.shema).subscribe(e => {
      this.planet = 'data:image/jpeg;base64,' + e;
    });
  }
}

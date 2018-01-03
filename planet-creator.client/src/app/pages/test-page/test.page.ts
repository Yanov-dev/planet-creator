import { Component } from '@angular/core';
import { Generator2dService } from '../../services/generator2dService';

@Component({
    templateUrl: './test.page.html',
})
export class TestPageComponent {
    title = 'app';

    planet: string;

    constructor(private _generator2dService: Generator2dService) {

    }

    generate() {
        this._generator2dService.Generate().subscribe(e => {
            this.planet = "data:image/jpeg;base64," + e;
        });
    }
}

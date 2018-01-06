import {ShemaLayer} from './shemaLayer';

export class Shema {
  layers: ShemaLayer[];
  projectionType: number;

  constructor() {
    this.layers = [];
    this.projectionType = 0;
  }
}

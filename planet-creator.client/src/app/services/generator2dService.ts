import {Injectable} from '@angular/core';
import {Observable} from 'rxjs/Observable';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../environments/environment';
import {ShemaLayer} from '../models/shemaLayer';
import {Shema} from '../models/Shema';
import {Generate2DRequest} from '../models/Generate2DRequest';

@Injectable()
export class Generator2dService {
  constructor(private _http: HttpClient) {
  }

  Generate(shema: Shema): Observable<any> {
    const obj = new Generate2DRequest();
    obj.shema = shema;
    obj.area = {
      'fullWidth': 200,
      'fullHeight': 200,
      'rect': {
        'x': 0,
        'y': 0,
        'width': 200,
        'height': 200
      }
    };
    return this._http.post(environment.links.generate2d, obj);
  }

  GenerateLayerPreview(layer: ShemaLayer): Observable<any> {
    return this._http.post(environment.links.layerPreview, layer);
  }
}

import {EventEmitter, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {ShemaLayer} from '../Models/ShemaLayer';
import {Generate3dRequest} from '../Models/Generate3dRequest';

@Injectable()
export class PlanetGenerationService {
  public done: EventEmitter<any> = new EventEmitter<any>();

  constructor(private http: HttpClient) {
  }

  public generateBitmap() {
    this.http.post('http://localhost:5000/api/planet/3d/generate/bitmap', {}).subscribe(e => {
      this.done.emit(e);
    });
  }

  public generate(request: Generate3dRequest) {
    this.http.post('http://localhost:5000/api/planet/3d/generate', request).subscribe(e => {
      this.done.emit(e);
    });
  }
}

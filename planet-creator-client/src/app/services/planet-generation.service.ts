import {EventEmitter, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';

@Injectable()
export class PlanetGenerationService {
  public done: EventEmitter<any> = new EventEmitter<any>();

  constructor(private http: HttpClient) {
  }


  public generate() {
    this.http.post('http://localhost:5000/api/planet/3d/generate', {}).subscribe(e => {
      this.done.emit(e);
    });
  }
}

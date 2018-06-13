import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {ShemaLayer} from '../../Models/ShemaLayer';
import {ColorLevel} from '../../Models/ColorLevel';
import {MatTableDataSource} from '@angular/material';
import {HttpClient} from '@angular/common/http';

@Component({
  selector: 'app-layer-editor',
  templateUrl: './layer-editor.component.html'
})
export class LayerEditorComponent implements OnInit {

  displayedColumns = ['level', 'color', 'remove'];
  dataSource = new MatTableDataSource<ColorLevel>();

  preview_data: string;

  @Output()
  change: EventEmitter<ShemaLayer> = new EventEmitter<ShemaLayer>();

  constructor(private http: HttpClient) {
  }

  _layer: ShemaLayer;

  @Input()
  set layer(layer: ShemaLayer) {
    if (!layer) {
      return;
    }
    console.log(layer);
    this._layer = layer;
    this.dataSource.data = this.layer.colors;
    this.updatePreview();
  }

  get layer() {
    return this._layer;
  }

  ngOnInit(): void {
    this.http.get('http://localhost:5000/api/layer').subscribe(e => {
      this.layer = <ShemaLayer>e;
      this.change.emit(this.layer);
      this.updatePreview();
    });
  }

  addColor() {
    this.layer.colors.push(new ColorLevel(0, '#ffffff'));
    this.updateView();
  }

  colorChange() {
    this.updatePreview();
    this.change.emit(this._layer);
  }

  updateView() {
    this.layer.colors = this.layer.colors.sort(a => a.level);
    this.dataSource.data = this.layer.colors;
    this.updatePreview();
    this.change.emit(this._layer);
  }

  removeColor(level: number) {
    this.layer.colors = this.layer.colors.filter(e => e.level !== level);
    this.updateView();
  }

  updatePreview() {
    if (!this.layer) {
      return;
    }

    this.http.post('http://localhost:5000/api/layer/preview', this.layer).subscribe(e => {
      this.preview_data = 'data:image/jpeg;base64,' + e;
    });
  }
}

import {Component, EventEmitter, Output, Input} from '@angular/core';
import {ShemaLayer} from '../../models/shemaLayer';
import {Generator2dService} from '../../services/generator2dService';
import {MatTableDataSource} from '@angular/material';
import {ColorLevel} from '../../models/ColorLevel';
import {OnInit} from '@angular/core/src/metadata/lifecycle_hooks';

@Component({
  selector: 'app-layer-editor',
  templateUrl: './layer.editor.component.html',
})
export class LayerEditorComponent implements OnInit {
  displayedColumns = ['level', 'color', 'remove'];
  dataSource = new MatTableDataSource<ColorLevel>();

  preview_data: string;

  @Output()
  change: EventEmitter<ShemaLayer> = new EventEmitter<ShemaLayer>();

  constructor(private generator2dService: Generator2dService) {
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
    this.updatePreview();
  }

  removeColor(level: number) {
    this.layer.colors = this.layer.colors.filter(e => e.level !== level);
    this.dataSource.data = this.layer.colors;
    this.updatePreview();
  }

  updatePreview() {
    if (!this.layer) {
      return;
    }

    this.generator2dService.GenerateLayerPreview(this.layer).subscribe(e => {
      this.preview_data = 'data:image/jpeg;base64,' + e;
    });
  }
}

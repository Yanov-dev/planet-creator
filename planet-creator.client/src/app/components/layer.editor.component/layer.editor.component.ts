import {Component, EventEmitter, Output} from '@angular/core';
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
    this.layer = new ShemaLayer();
    this.layer.colors = [];
    this.layer.colors.push(new ColorLevel(0, '#ff000000'));
    this.layer.colors.push(new ColorLevel(100, '#ffffffff'));

    this.layer = {
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
    };

    this.dataSource.data = this.layer.colors;
  }

  layer: ShemaLayer;

  ngOnInit(): void {
    this.updatePreview();
  }

  removeColor(level: number) {
    this.layer.colors = this.layer.colors.filter(e => e.level !== level);
    this.dataSource.data = this.layer.colors;
    this.updatePreview();
  }

  updatePreview() {
    this.generator2dService.GenerateLayerPreview(this.layer).subscribe(e => {
      this.preview_data = 'data:image/jpeg;base64,' + e;
    });
  }
}

import {Component} from '@angular/core';
import {MatDialog} from '@angular/material';
import {CreateProjectDialogComponent} from '../../dialogs/create.project.dialog/craete.project.dialog.component';

@Component({
  templateUrl: './overview.page.html',
  styleUrls: ['./overview.page.css']
})

export class OverviewPageComponent {

  list: number[] = [];

  constructor(public dialog: MatDialog) {
    for (var i = 0; i < 100; i++) {
      this.list.push(0);
    }
  }

  createNewProject() {
    const dialogRef = this.dialog.open(CreateProjectDialogComponent, {
      width: '250px',
      data: {}
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
    });
  }
}

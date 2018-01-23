import {Component, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialog, MatDialogRef} from '@angular/material';

@Component({
  selector: 'app-create-project-dialog',
  templateUrl: './create.project.dialog.component.html'
})
export class CreateProjectDialogComponent {

  constructor(public dialogRef: MatDialogRef<CreateProjectDialogComponent>,
              @Inject(MAT_DIALOG_DATA) public data: any) {

  }

  cancelOnClick(): void {
    this.dialogRef.close();
  }
}

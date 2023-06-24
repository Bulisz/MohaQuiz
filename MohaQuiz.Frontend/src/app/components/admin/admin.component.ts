import { Component, ViewEncapsulation } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { QuizService } from 'src/app/services/quiz.service';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss']
})
export class AdminComponent {

  constructor(private _snackBar: MatSnackBar, private qs: QuizService){
    this.qs.hc.on('GetTeamNames', tnl => {
      this.popupTeamNames(tnl)
    })
  }

  popupTeamNames(teamNameList: Array<string>){

    let message = teamNameList.join('\n ')

      this._snackBar.open(message, undefined, {
        duration: 5000,
        horizontalPosition: 'right',
        verticalPosition: 'top',
        panelClass: 'admin-snackbar'
      });
  }
}

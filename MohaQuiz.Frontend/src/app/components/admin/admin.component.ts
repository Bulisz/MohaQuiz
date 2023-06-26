import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { GameProcessStateModel } from 'src/app/models/game-process-state-model';
import { RoundDetailsModel } from 'src/app/models/round-details-model';
import { GameProcessService } from 'src/app/services/game-process.service';
import { QuizService } from 'src/app/services/quiz.service';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss']
})
export class AdminComponent implements OnInit,OnDestroy {

  teamNames: Array<string> = []
  gameProcessState: GameProcessStateModel = { roundNumber: 0, questionNumber: 0, isGameStarted: false }
  roundDetails!: RoundDetailsModel

  constructor(private _snackBar: MatSnackBar, private qs: QuizService, private gps: GameProcessService) {
    this.gps.hc.on('GetTeamNames', tnl => {
      this.teamNames = tnl
      this.popupTeamNames(tnl)
    })
  }

  async ngOnInit() {
    await this.qs.getAllTeamNames()
      .then(tnl => this.teamNames = tnl)

    this.gps.gameProcessState.subscribe({
      next: gp => this.gameProcessState = gp
    })

    await this.gps.getActualGameProcess()

    this.gps.listenForGameProcess()

    await this.refreshRound()
  }

  async startGame() {
    this.gps.hc.off('GetTeamNames')
    await this.gps.startGame()
    await this.refreshRound()
  }

  async nextQuestion(){
    await this.gps.nextQuestion()
  }

  async nextRound(){
    await this.gps.nextRound()
    await this.refreshRound()
  }

  async resetGame() {
    await this.gps.resetGame()
    await this.qs.resetGame()
    window.location.reload();
  }

  async refreshRound(){
    this.roundDetails = await this.qs.GetRoundDetails(this.gameProcessState.roundNumber)
  }

  popupTeamNames(teamNameList: Array<string>) {
    let message = teamNameList.join('\n ')

    this._snackBar.open(message, undefined, {
      duration: 5000,
      horizontalPosition: 'right',
      verticalPosition: 'top',
      panelClass: 'admin-snackbar'
    });
  }

  ngOnDestroy(): void {
    this.gps.hc.off('GetGameProcessState')
  }
}

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
  gameProcessState: GameProcessStateModel = { roundNumber: 0, questionNumber: 0, isGameStarted: false, isScoring: false }
  roundDetails!: RoundDetailsModel

  constructor(private _snackBar: MatSnackBar, private qs: QuizService, private gps: GameProcessService) {
    this.gps.hc.on('GetTeamNames', tn => {
      this.teamNames = tn
      this.popupTeamNames(tn)
    })
  }

  async ngOnInit() {
    await this.qs.getAllTeamNames()
      .then(tn => this.teamNames = tn)

    this.gps.gameProcessState.subscribe({
      next: gp => this.gameProcessState = gp
    })

    await this.gps.getActualGameProcess()

    this.gps.listenForGameProcess()

    await this.refreshRound(this.gameProcessState.roundNumber)
  }

  async startGame() {
    this.gps.hc.off('GetTeamNames')
    await this.gps.startGame()
    await this.qs.randomizeTeamNames()
    await this.refreshRound(1)
  }

  async nextQuestion(){
    await this.gps.nextQuestion()
  }

  async scoring(){
    await this.gps.startScoring()
  }

  async nextRound(){
    await this.gps.nextRound()
    this.gps.hc.invoke('SendRoundDetailsAsync')
    await this.qs.randomizeTeamNames()
    await this.refreshRound(this.gameProcessState.roundNumber)
  }

  async resetGame() {
    await this.gps.resetGame()
    await this.qs.resetGame()
    this.gps.hc.invoke('SendRoundDetailsAsync')
    window.location.reload()
  }

  async refreshRound(roundNumber: number){
    this.roundDetails = await this.qs.getRoundDetails(roundNumber)
  }

  popupTeamNames(teamNames: Array<string>) {
    let message = teamNames.join('\n ')

    this._snackBar.open(message, undefined, {
      duration: 5000,
      horizontalPosition: 'right',
      verticalPosition: 'top',
      panelClass: 'admin-snackbar'
    });
  }

  ngOnDestroy(): void {
    this.gps.stopListenForGameProcess()
    this.gps.hc.off('GetTeamNames')
  }
}

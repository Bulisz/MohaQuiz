import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { GameProcessStateModel } from 'src/app/models/game-process-state-model';
import { RoundDetailsModel } from 'src/app/models/round-details-model';
import { TeamAnswerModel } from 'src/app/models/team-answer-model';
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
  scoringFinished = false

  constructor(private _snackBar: MatSnackBar, private qs: QuizService, private gps: GameProcessService) {
    this.gps.hc.on('GetTeamNames', tn => {
      this.teamNames = tn
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
    this.gps.hc.invoke('SendRoundDetailsAsync')
    await this.refreshRound(1)

    this.gps.hc.on('GetTeamAnswer', ta => {
      this.popupTeamAnswers(ta)
    })
  }

  async nextQuestion(){
    await this.gps.nextQuestion()
  }

  async scoring(){
    await this.gps.startScoring()
  }

  async scoringFinishedM(){
    await this.gps.stopScoring()
    this.scoringFinished = true
  }

  async nextRound(){
    
    await this.gps.nextRound()
    await this.qs.randomizeTeamNames()
    this.gps.hc.invoke('SendRoundDetailsAsync')
    await this.refreshRound(this.gameProcessState.roundNumber)

    this.scoringFinished = false
  }

  async resetGame() {
    await this.gps.resetGame()
    await this.qs.resetGame()
    window.location.reload()
  }

  async refreshRound(roundNumber: number){
    this.roundDetails = await this.qs.getRoundDetails(roundNumber)
  }

  popupTeamAnswers(teamAnswer: TeamAnswerModel) {
    let message = teamAnswer.teamName + '\n\r' + teamAnswer.questionNumber + '. kérdés\n\r' + teamAnswer.teamAnswerText
    this._snackBar.open(message, undefined, {
      duration: 5000,
      horizontalPosition: 'right',
      verticalPosition: 'top',
      panelClass: 'answer-snackbar'
    });
  }

  ngOnDestroy(): void {
    this.gps.stopListenForGameProcess()
    this.gps.hc.off('GetTeamNames')
    this.gps.hc.off('GetTeamAnswer')
  }
}

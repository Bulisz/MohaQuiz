import { Component, OnDestroy, OnInit } from '@angular/core';
import { GameProcessStateModel } from 'src/app/models/game-process-state-model';
import { RoundDetailsModel } from 'src/app/models/round-details-model';
import { TeamAnswerModel } from 'src/app/models/team-answer-model';
import { GameProcessService } from 'src/app/services/game-process.service';
import { QuizService } from 'src/app/services/quiz.service';

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.scss']
})
export class GameComponent implements OnInit,OnDestroy {

  gameProcessState: GameProcessStateModel = {roundNumber: 0, questionNumber: 0, isGameStarted: false, isScoring: false}
  roundDetails!: RoundDetailsModel

  constructor(private qs: QuizService, private gps: GameProcessService){}

  async ngOnInit() {
    this.gps.gameProcessState.subscribe({
      next: gp => this.gameProcessState = gp
    })

    await this.gps.getActualGameProcess()

    this.gps.listenForGameProcess()

    this.gps.hc.on('GetRoundDetails', rd => this.roundDetails = rd)
    this.roundDetails = await this.qs.getRoundDetails(this.gameProcessState.roundNumber)
  }

  async sendAnswer(teamAnswer: TeamAnswerModel){
    await this.qs.sendAnswer(teamAnswer)
  }

  ngOnDestroy(): void {
    this.gps.stopListenForGameProcess()
    this.gps.hc.off('GetRoundDetails')
  }
}

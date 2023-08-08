import { Component, OnDestroy, OnInit } from '@angular/core';
import { GameNameModel } from 'src/app/models/game-name-model';
import { GameProcessStateModel } from 'src/app/models/game-process-state-model';
import { GameSummaryModel } from 'src/app/models/game-summary-model';
import { GameProcessService } from 'src/app/services/game-process.service';
import { QuizService } from 'src/app/services/quiz.service';

@Component({
  selector: 'app-game-summary',
  templateUrl: './game-summary.component.html',
  styleUrls: ['./game-summary.component.scss']
})
export class GameSummaryComponent implements OnInit, OnDestroy {

  gameSummary!: Array<GameSummaryModel>
  gameProcessState: GameProcessStateModel = { gameName: '', roundNumber: 0, questionNumber: 0, isGameStarted: false, isScoring: false, isGameFinished: false}

  constructor(private qs: QuizService, private gps: GameProcessService){}

  async ngOnInit() {
    this.gps.gameProcessState.subscribe({
      next: gp => this.gameProcessState = gp
    })
    
    let model: GameNameModel = { gameName: this.gameProcessState.gameName}
    this.gps.hc.on('GetSummaryOfTeam', async () => this.gameSummary = await this.qs.getSummaryOfGame(model))
    this.gameSummary = await this.qs.getSummaryOfGame(model)
  }

  ngOnDestroy(): void {
    this.gps.hc.off('GetSummaryOfTeam')
  }
}

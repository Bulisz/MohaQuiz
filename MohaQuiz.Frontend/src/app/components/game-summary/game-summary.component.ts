import { Component, OnInit } from '@angular/core';
import { GameSummaryModel } from 'src/app/models/game-summary-model';
import { GameProcessService } from 'src/app/services/game-process.service';
import { QuizService } from 'src/app/services/quiz.service';

@Component({
  selector: 'app-game-summary',
  templateUrl: './game-summary.component.html',
  styleUrls: ['./game-summary.component.scss']
})
export class GameSummaryComponent implements OnInit {

  gameSummary!: Array<GameSummaryModel>

  constructor(private qs: QuizService, private gps: GameProcessService){}

  async ngOnInit() {
    this.gps.hc.on('GetSummaryOfTeam', async () => this.gameSummary = await this.qs.getSummaryOfGame())
    this.gameSummary = await this.qs.getSummaryOfGame()
  }

}

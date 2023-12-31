import { Component, OnDestroy, OnInit } from '@angular/core';
import { TeamAndGameModel } from 'src/app/models/team-and-game-model';
import { TeamScoreSummaryModel } from 'src/app/models/team-score-summary-model';
import { GameProcessService } from 'src/app/services/game-process.service';
import { QuizService } from 'src/app/services/quiz.service';

@Component({
  selector: 'app-score-of-team',
  templateUrl: './score-of-team.component.html',
  styleUrls: ['./score-of-team.component.scss']
})
export class ScoreOfTeamComponent implements OnInit,OnDestroy{

  scoreSummary!: TeamScoreSummaryModel
  total = 0

  constructor(private qs: QuizService, private gps: GameProcessService){}

  async ngOnInit() {
    this.gps.hc.on('GetSummaryOfTeam', async () => await this.refreshScores())
    await this.refreshScores()
  }

  async refreshScores(){
    if(localStorage.getItem('teamName')){
      let model: TeamAndGameModel = {gameName: this.gps.gameProcessState.value.gameName, teamName: localStorage.getItem('teamName') as string}
      this.scoreSummary = await this.qs.getSummaryOfTeam(model)
      if(this.scoreSummary && this.scoreSummary.teamScoresPerRound.length > 0){
        this.total = this.scoreSummary.teamScoresPerRound.reduce((a,b) => a+b)
      }
    }
  }

  ngOnDestroy(): void {
    this.gps.hc.off('GetSummaryOfTeam')
  }
}

import { Component, Input, OnDestroy } from '@angular/core';
import { TeamAndGameModel } from 'src/app/models/team-and-game-model';
import { TeamScoreSummaryModel } from 'src/app/models/team-score-summary-model';
import { GameProcessService } from 'src/app/services/game-process.service';
import { QuizService } from 'src/app/services/quiz.service';

@Component({
  selector: 'app-score-of-teams-admin',
  templateUrl: './score-of-teams-admin.component.html',
  styleUrls: ['./score-of-teams-admin.component.scss']
})
export class ScoreOfTeamsAdminComponent implements OnDestroy {

  @Input() teamNames!: Array<string>
  allTeamScoreSummary: Array<TeamScoreSummaryModel> = []
  allTeamTotal: Array<number> = []

  constructor(private qs: QuizService, private gps: GameProcessService){}

  async ngOnChanges() {
    this.gps.hc.on('GetSummaryOfTeam', async () => await this.refreshScores())
    await this.refreshScores()
  }

  async refreshScores(){
    this.allTeamScoreSummary = []
    this.allTeamTotal = []
    for (let i=0; i<this.teamNames.length; i++){
      let model: TeamAndGameModel = { gameName: this.gps.gameProcessState.value.gameName, teamName: this.teamNames[i]}
      this.allTeamScoreSummary.push(await this.qs.getSummaryOfTeam(model))
      this.allTeamTotal.push(this.allTeamScoreSummary[i].teamScoresPerRound.reduce((a,b) => a+b))
    }
  }
  
  ngOnDestroy(): void {
    this.gps.hc.off('GetSummaryOfTeam');
  }
}

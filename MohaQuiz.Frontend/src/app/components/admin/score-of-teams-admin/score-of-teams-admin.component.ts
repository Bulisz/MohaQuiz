import { Component, Input } from '@angular/core';
import { TeamScoreSummaryModel } from 'src/app/models/team-score-summary-model';
import { GameProcessService } from 'src/app/services/game-process.service';
import { QuizService } from 'src/app/services/quiz.service';

@Component({
  selector: 'app-score-of-teams-admin',
  templateUrl: './score-of-teams-admin.component.html',
  styleUrls: ['./score-of-teams-admin.component.scss']
})
export class ScoreOfTeamsAdminComponent {

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
      this.allTeamScoreSummary.push(await this.qs.getSummaryOfTeam(this.teamNames[i]))
      this.allTeamTotal.push(this.allTeamScoreSummary[i].teamScoresPerRound.reduce((a,b) => a+b))
    }
  }
}

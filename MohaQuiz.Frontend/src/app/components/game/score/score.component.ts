import { Component, Input, OnInit } from '@angular/core';
import { RoundAndTeamModel } from 'src/app/models/round-and-team-model';
import { RoundAnswersOfTeamModel } from 'src/app/models/round-answers-of-team-model';
import { RoundDetailsModel } from 'src/app/models/round-details-model';
import { ScoringModel } from 'src/app/models/scoring-model';
import { QuizService } from 'src/app/services/quiz.service';

@Component({
  selector: 'app-score',
  templateUrl: './score.component.html',
  styleUrls: ['./score.component.scss']
})
export class ScoreComponent implements OnInit {

  @Input() roundDetails!: RoundDetailsModel
  roundAnswersOfTeam!: RoundAnswersOfTeamModel
  isFinished = false

  constructor(private qs: QuizService) { }

  async ngOnInit() {
    if (this.roundDetails) {
      let roundAndTeam: RoundAndTeamModel = {
        roundNumber: this.roundDetails.roundNumber,
        teamName: localStorage.getItem('teamName') as string
      }
      this.roundAnswersOfTeam = await this.qs.getRoundAnswersOfTeam(roundAndTeam)
    }
  }

  async ngOnChanges() {
    if (this.roundDetails) {
      let roundAndTeam: RoundAndTeamModel = {
        roundNumber: this.roundDetails.roundNumber,
        teamName: localStorage.getItem('teamName') as string
      }
      this.roundAnswersOfTeam = await this.qs.getRoundAnswersOfTeam(roundAndTeam)
    }
  }

  scoring(multiple: number, index: number, score: number) {
    (document.getElementById(`score${index}`) as HTMLSpanElement).innerHTML = (multiple * score).toString()
  }

  getAnswerOfQuestionNumber(questionNumber: number): string {
    return this.roundAnswersOfTeam.answers.filter(a => a.questionNumber === questionNumber)[0].teamAnswerText
  }

  async sendScores(){
    let score: ScoringModel
    for(let i = 0; i < this.roundAnswersOfTeam.answers.length; i++) {
      score = {
        teamName: this.roundAnswersOfTeam.teamName,
        roundNumber: this.roundAnswersOfTeam.roundNumber,
        questionNumber: i + 1,
        score: Number((document.getElementById(`score${i}`) as HTMLSpanElement).innerHTML)
      }
      await this.qs.scoringOfAQuestion(score)
    }
    this.isFinished = true
  }
}

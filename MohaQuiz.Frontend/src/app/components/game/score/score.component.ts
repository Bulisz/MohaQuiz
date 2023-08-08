import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { RoundAndTeamModel } from 'src/app/models/round-and-team-model';
import { RoundAnswersOfTeamModel } from 'src/app/models/round-answers-of-team-model';
import { RoundDetailsModel } from 'src/app/models/round-details-model';
import { ScoringModel } from 'src/app/models/scoring-model';
import { GameProcessService } from 'src/app/services/game-process.service';
import { QuizService } from 'src/app/services/quiz.service';

@Component({
  selector: 'app-score',
  templateUrl: './score.component.html',
  styleUrls: ['./score.component.scss']
})
export class ScoreComponent {

  @Input() roundDetails!: RoundDetailsModel
  roundAnswersOfTeam!: RoundAnswersOfTeamModel
  @Output() scoringIsFinished = new EventEmitter()

  constructor(private qs: QuizService, private gps: GameProcessService) { }

  async ngOnChanges() {
    if (this.roundDetails) {
      let roundAndTeam: RoundAndTeamModel = {
        gameName: this.gps.gameProcessState.value.gameName,
        roundNumber: this.roundDetails.roundNumber,
        teamName: localStorage.getItem('teamName') as string
      }
      this.roundAnswersOfTeam = await this.qs.getRoundAnswersOfTeam(roundAndTeam)
    }
  }

  scoring(multiple: number, index: number, score: number) {
    (document.getElementById(`score${index}`) as HTMLSpanElement).innerHTML = (multiple * score).toString()
    if (multiple === 0) {
      (document.getElementById(`btnn${index}`) as HTMLButtonElement).disabled = true;
      (document.getElementById(`btnh${index}`) as HTMLButtonElement).disabled = false;
      (document.getElementById(`btnf${index}`) as HTMLButtonElement).disabled = false;
    } else if (multiple === 0.5) {
      (document.getElementById(`btnn${index}`) as HTMLButtonElement).disabled = false;
      (document.getElementById(`btnh${index}`) as HTMLButtonElement).disabled = true;
      (document.getElementById(`btnf${index}`) as HTMLButtonElement).disabled = false;
    } else if (multiple === 1) {
      (document.getElementById(`btnn${index}`) as HTMLButtonElement).disabled = false;
      (document.getElementById(`btnh${index}`) as HTMLButtonElement).disabled = false;
      (document.getElementById(`btnf${index}`) as HTMLButtonElement).disabled = true;
    }
  }

  getAnswerOfQuestionNumber(questionNumber: number): string {
    let answer = this.roundAnswersOfTeam.answers.find(a => a.questionNumber === questionNumber)
    if (answer) {
      return answer.teamAnswerText
    }
    else return ''
  }

  async sendScores() {
    let score: number
    let scoreModel: ScoringModel
    let questionNumber: number
    for (let i = 0; i < this.roundAnswersOfTeam.answers.length; i++) {
      questionNumber = this.roundAnswersOfTeam.answers[i].questionNumber
      score = Number((document.getElementById(`score${questionNumber}`) as HTMLSpanElement).innerHTML)
      if(score > 0){
        scoreModel = {
          gameName: this.gps.gameProcessState.value.gameName,
          teamName: this.roundAnswersOfTeam.teamName,
          roundNumber: this.roundAnswersOfTeam.roundNumber,
          questionNumber: questionNumber,
          score: Number((document.getElementById(`score${questionNumber}`) as HTMLSpanElement).innerHTML)
        }
        await this.qs.scoringOfAQuestion(scoreModel)
      }
    }

    this.gps.hc.invoke('ScoringReadyAsync', localStorage.getItem('teamName') as string)
    this.scoringIsFinished.emit()
  }
}

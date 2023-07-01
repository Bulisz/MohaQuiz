import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { GameProcessStateModel } from 'src/app/models/game-process-state-model';
import { NullableMessageModel } from 'src/app/models/nullable-message-model';
import { RoundAnswersOfTeamModel } from 'src/app/models/round-answers-of-team-model';
import { RoundDetailsModel } from 'src/app/models/round-details-model';
import { ScoringModel } from 'src/app/models/scoring-model';
import { TeamAnswerModel } from 'src/app/models/team-answer-model';
import { GameProcessService } from 'src/app/services/game-process.service';
import { QuizService } from 'src/app/services/quiz.service';

@Component({
  selector: 'app-nullable-round-admin',
  templateUrl: './nullable-round-admin.component.html',
  styleUrls: ['./nullable-round-admin.component.scss']
})
export class NullableRoundAdminComponent implements OnInit {

  @Input() roundDetails!: RoundDetailsModel
  @Input() gameProcessState!: GameProcessStateModel
  @Input() scoringFinished = false
  @Output() nextQuestionToParent = new EventEmitter();
  @Output() nextRoundToParent = new EventEmitter();
  @Output() scoringToParent = new EventEmitter();

  answersOfRound: Array<RoundAnswersOfTeamModel> = []
  teamAlreadySentAnswer: Array<string> = []
  teamSecondSentAnswer: Array<string> = []

  constructor(private qs: QuizService, private gps: GameProcessService) { }

  ngOnInit() {
    this.gps.hc.off('GetTeamAnswer')
    this.gps.hc.on('GetNullableAnswers', (ans: RoundAnswersOfTeamModel) => {
      if (!this.teamSecondSentAnswer.includes(ans.teamName)) {
        this.answersOfRound.push(ans)
      }
    })
  }

  nextQuestion() {
    this.nextQuestionToParent.emit()
  }

  nextRound() {
    this.scoringToParent.emit()
    this.nextRoundToParent.emit()
  }

  async bad(teamName: string) {
    if (!this.teamAlreadySentAnswer.includes(teamName)) {
      this.teamAlreadySentAnswer.push(teamName);

      let index = this.answersOfRound.findIndex(a => a.teamName == teamName);
      if (index > -1) {
        this.answersOfRound.splice(index, 1);
      }

      let mes: NullableMessageModel = {
        teamName: teamName,
        message: 'Van benne nullázó'
      }
      await this.gps.hc.invoke('SendNullableMessage', mes)
    } else {
      this.teamSecondSentAnswer.push(teamName);

      let index = this.answersOfRound.findIndex(a => a.teamName == teamName);
      if (index > -1) {
        this.answersOfRound.splice(index, 1);
      }

      let mes: NullableMessageModel = {
        teamName: teamName,
        message: 'Volt benne nullázó'
      }
      await this.gps.hc.invoke('SendNullableMessage', mes)
    }
  }

  async good(teamName: string) {
    if (!this.teamAlreadySentAnswer.includes(teamName)) {
      this.teamAlreadySentAnswer.push(teamName);

      let index = this.answersOfRound.findIndex(a => a.teamName == teamName);
      if (index > -1) {
        this.answersOfRound.splice(index, 1);
      }

      let mes: NullableMessageModel = {
        teamName: teamName,
        message: 'Nincs benne nullázó'
      }
      await this.gps.hc.invoke('SendNullableMessage', mes)
    } else {
      this.teamSecondSentAnswer.push(teamName);

      let teamAnswers = this.answersOfRound.find(a => a.teamName == teamName);
      for (let i = 0; i < teamAnswers!.answers.length; i++) {
        await this.sendAnswer(teamAnswers!.answers[i])
      }
      for (let i = 0; i < teamAnswers!.answers.length; i++) {
        if (teamAnswers!.answers[i].teamAnswerText.length > 0) {
          await this.sendScores(teamAnswers!.answers[i])
        }
      }
      
      let index = this.answersOfRound.findIndex(a => a.teamName == teamName);
      if (index > -1) {
        this.answersOfRound.splice(index, 1);
      }

      let mes: NullableMessageModel = {
        teamName: teamName,
        message: 'Nem volt benne nullázó'
      }
      await this.gps.hc.invoke('SendNullableMessage', mes)
    }
  }

  async sendAnswer(answer: TeamAnswerModel) {
    await this.qs.sendAnswer(answer)
  }

  async sendScores(score: TeamAnswerModel) {
    let scoring: ScoringModel = {
      teamName: score.teamName,
      roundNumber: score.roundNumber,
      questionNumber: score.questionNumber,
      score: this.roundDetails.questions.find(q => q.questionNumber === score.questionNumber)?.fullScore as number
    }
    await this.qs.scoringOfAQuestion(scoring)
  }
}

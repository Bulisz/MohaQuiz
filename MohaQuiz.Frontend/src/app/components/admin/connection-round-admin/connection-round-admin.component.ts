import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { GameProcessStateModel } from 'src/app/models/game-process-state-model';
import { RoundDetailsModel } from 'src/app/models/round-details-model';
import { ScoringModel } from 'src/app/models/scoring-model';
import { TeamAnswerModel } from 'src/app/models/team-answer-model';
import { GameProcessService } from 'src/app/services/game-process.service';
import { QuizService } from 'src/app/services/quiz.service';

@Component({
  selector: 'app-connection-round-admin',
  templateUrl: './connection-round-admin.component.html',
  styleUrls: ['./connection-round-admin.component.scss']
})
export class ConnectionRoundAdminComponent implements OnInit,OnDestroy {
  
  @Input() roundDetails!: RoundDetailsModel
  @Input() gameProcessState!: GameProcessStateModel
  @Input() scoringFinished = false
  @Output() nextQuestionToParent = new EventEmitter();
  @Output() nextRoundToParent = new EventEmitter();
  @Output() scoringToParent = new EventEmitter();

  extraAnswers: Array<TeamAnswerModel> = []

  constructor(private qs: QuizService, private gps: GameProcessService){}

  async ngOnInit() {
    this.gps.hc.on('GetExtraAnswer', ans => {
      this.extraAnswers.push(ans)
    })
  }
 
  nextQuestion(){
    this.nextQuestionToParent.emit()
    this.extraAnswers = []
  }

  scoring(){
    this.scoringToParent.emit()
  }

  nextRound(){
    this.nextRoundToParent.emit()
  }

  async sendExtraAnswer(tn: string, ta: string){
    let amswerModel: TeamAnswerModel = {
      teamName: tn,
      roundNumber: this.gameProcessState.roundNumber,
      questionNumber: 0,
      teamAnswerText: ta
    }
    await this.qs.sendAnswer(amswerModel)

    let scoringModel: ScoringModel = {
      teamName: tn,
      roundNumber: this.gameProcessState.roundNumber,
      questionNumber: 0,
      score: (6 - this.gameProcessState.questionNumber)
    }
    await this.qs.scoringOfAQuestion(scoringModel)

    this.gps.hc.invoke('ConfirmExtraAnswer',tn)

    let index = this.extraAnswers.findIndex(e => e.teamName == tn)
    this.extraAnswers.splice(index,1)
  }

  async ngOnDestroy() {
    this.gps.hc.off('GetExtraAnswer')
  }
}

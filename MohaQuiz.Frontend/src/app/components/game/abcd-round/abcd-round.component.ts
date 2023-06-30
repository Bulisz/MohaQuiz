import { Component, EventEmitter, Input, OnDestroy, Output } from '@angular/core';
import { GameProcessStateModel } from 'src/app/models/game-process-state-model';
import { RoundDetailsModel } from 'src/app/models/round-details-model';
import { TeamAnswerModel } from 'src/app/models/team-answer-model';

@Component({
  selector: 'app-abcd-round',
  templateUrl: './abcd-round.component.html',
  styleUrls: ['./abcd-round.component.scss']
})
export class AbcdRoundComponent implements OnDestroy {

  @Input() roundDetails!: RoundDetailsModel
  @Input() gameProcessState!: GameProcessStateModel
  @Output() sendAnswerToParent = new EventEmitter();
  answers: Array<string> = ['','','','','']
 
  sendAnswer(i: number, questionNumber: number){
    let teamAnswer: TeamAnswerModel
    let answerText = this.answers[i]
    if(answerText.length > 0){
      teamAnswer =
        { teamName: localStorage.getItem('teamName') as string,
          roundNumber: this.roundDetails.roundNumber,
          questionNumber: questionNumber,
          teamAnswerText: answerText };
      this.sendAnswerToParent.emit(teamAnswer);
      (document.getElementById(`btn${i}`) as HTMLButtonElement).disabled = true;
    }
  }
  
  async ngOnDestroy(){
    let teamAnswer: TeamAnswerModel
    for(let i= 0; i < this.roundDetails.questions.length; i++){
      if(this.answers[i].length < 1){
        teamAnswer =
        { teamName: localStorage.getItem('teamName') as string,
          roundNumber: this.roundDetails.roundNumber,
          questionNumber: i + 1,
          teamAnswerText: '' };
        this.sendAnswerToParent.emit(teamAnswer);
      }
    }
  }
}

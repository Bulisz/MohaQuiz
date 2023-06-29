import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { GameProcessStateModel } from 'src/app/models/game-process-state-model';
import { RoundDetailsModel } from 'src/app/models/round-details-model';
import { TeamAnswerModel } from 'src/app/models/team-answer-model';
import { GameProcessService } from 'src/app/services/game-process.service';

@Component({
  selector: 'app-connection-round',
  templateUrl: './connection-round.component.html',
  styleUrls: ['./connection-round.component.scss']
})
export class ConnectionRoundComponent implements OnInit,OnDestroy {

  @Input() roundDetails!: RoundDetailsModel
  @Input() gameProcessState!: GameProcessStateModel
  @Output() sendAnswerToParent = new EventEmitter();
  isExtraQuestionConfirmed = false

  constructor (private gps: GameProcessService) {}
 
  ngOnInit(): void {
    this.gps.gameProcessState.subscribe({
      next: () =>{
        if(!this.isExtraQuestionConfirmed && document.getElementById(`btn0`)){
          (document.getElementById(`btn0`) as HTMLButtonElement).disabled = false;
        }
      }
    })
    this.gps.hc.on('GetConfirmationOfExtraAnswer', tn => {
      if(tn === localStorage.getItem('teamName')){
        this.isExtraQuestionConfirmed = true
      }
    })
  }

  sendAnswer(i: number, questionNumber: number){
    let teamAnswer: TeamAnswerModel
    let answerText = (document.getElementById(`answer${i}`) as HTMLInputElement).value
    if(answerText.length > 0){
      teamAnswer =
        { teamName: localStorage.getItem('teamName') as string,
          roundNumber: this.roundDetails.roundNumber,
          questionNumber: questionNumber,
          teamAnswerText: answerText };
      if(questionNumber === 0){
        this.gps.hc.invoke('SendExtraAnswer',teamAnswer)
      } else {
        this.sendAnswerToParent.emit(teamAnswer);
      }
      (document.getElementById(`btn${i}`) as HTMLButtonElement).disabled = true;
    }
  }
  
  async ngOnDestroy(){
    let teamAnswer: TeamAnswerModel
    for(let i= 1; i < this.roundDetails.questions.length; i++){
      if((document.getElementById(`answer${i}`) as HTMLInputElement) && !(document.getElementById(`answer${i}`) as HTMLInputElement).value){
        teamAnswer =
        { teamName: localStorage.getItem('teamName') as string,
          roundNumber: this.roundDetails.roundNumber,
          questionNumber: i,
          teamAnswerText: '' };
        this.sendAnswerToParent.emit(teamAnswer);
      }
    }
  }
}

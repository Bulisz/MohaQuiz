import { Component, EventEmitter, Input, OnDestroy, Output } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { GameProcessStateModel } from 'src/app/models/game-process-state-model';
import { NullableMessageModel } from 'src/app/models/nullable-message-model';
import { RoundAnswersOfTeamModel } from 'src/app/models/round-answers-of-team-model';
import { RoundDetailsModel } from 'src/app/models/round-details-model';
import { TeamAnswerModel } from 'src/app/models/team-answer-model';
import { GameProcessService } from 'src/app/services/game-process.service';

@Component({
  selector: 'app-nullable-round',
  templateUrl: './nullable-round.component.html',
  styleUrls: ['./nullable-round.component.scss']
})
export class NullableRoundComponent implements OnDestroy {
  
  @Input() roundDetails!: RoundDetailsModel
  @Input() gameProcessState!: GameProcessStateModel
  @Output() sendAnswerToParent = new EventEmitter();
  finalSent = false
 
  constructor (private _snackBar: MatSnackBar,private gps: GameProcessService){}

  async sendAnswer(version: string){
    let teamAnswersOfRound: RoundAnswersOfTeamModel
    let answerOfQuestions: Array<TeamAnswerModel> = []
    let answer: TeamAnswerModel

    for(let i=0; i<this.roundDetails.questions.length; i++){
      answer = {
        teamName: localStorage.getItem('teamName') as string,
        roundNumber: this.roundDetails.roundNumber,
        questionNumber: i + 1,
        teamAnswerText: (document.getElementById(`answer${i}`) as HTMLInputElement).value
      }
      answerOfQuestions.push(answer)
    }

    teamAnswersOfRound = {
      teamName: localStorage.getItem('teamName') as string,
      roundNumber: this.roundDetails.roundNumber,
      answers: answerOfQuestions
    }
    await this.gps.hc.invoke('NullableAnswers',teamAnswersOfRound)

    if(version === 't'){
      (document.getElementById(`try`) as HTMLButtonElement).disabled = true;
      this.gps.hc.on('GetNullableMessage', (mes: NullableMessageModel) => {
        if(mes.teamName===localStorage.getItem('teamName') as string){
          this.popupAdminAnswer(mes.message);
          if(!this.finalSent){
            (document.getElementById(`fin`) as HTMLButtonElement).disabled = false;
            this.finalSent = true
          }
        }
      })
    } else if(version === 'f'){
      (document.getElementById(`fin`) as HTMLButtonElement).disabled = true;
    }
  }

  popupAdminAnswer(message: string) {
    this._snackBar.open(message, 'X', {
      duration: 5000,
      horizontalPosition: 'right',
      verticalPosition: 'top',
      panelClass: 'answer-snackbar'
    });
  }

  ngOnDestroy(): void {
    this.gps.hc.off('GetNullableMessage')
  }

}

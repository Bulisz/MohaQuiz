import { Component, EventEmitter, Input, Output } from '@angular/core';
import { GameProcessStateModel } from 'src/app/models/game-process-state-model';
import { RoundDetailsModel } from 'src/app/models/round-details-model';

@Component({
  selector: 'app-warmup-round-admin',
  templateUrl: './warmup-round-admin.component.html',
  styleUrls: ['./warmup-round-admin.component.scss']
})
export class WarmupRoundAdminComponent{

  @Input() roundDetails!: RoundDetailsModel
  @Input() gameProcessState!: GameProcessStateModel
  @Input() scoringFinished = false
  @Output() nextQuestionToParent = new EventEmitter();
  @Output() nextRoundToParent = new EventEmitter();
  @Output() scoringToParent = new EventEmitter();
 
  nextQuestion(){
    this.nextQuestionToParent.emit()
  }

  scoring(){
    this.scoringToParent.emit()
  }

  nextRound(){
    this.nextRoundToParent.emit()
  }
}

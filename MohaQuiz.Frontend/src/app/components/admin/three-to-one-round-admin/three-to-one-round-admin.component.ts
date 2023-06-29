import { Component, EventEmitter, Input, Output } from '@angular/core';
import { GameProcessStateModel } from 'src/app/models/game-process-state-model';
import { RoundDetailsModel } from 'src/app/models/round-details-model';

@Component({
  selector: 'app-three-to-one-round-admin',
  templateUrl: './three-to-one-round-admin.component.html',
  styleUrls: ['./three-to-one-round-admin.component.scss']
})
export class ThreeToOneRoundAdminComponent {

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

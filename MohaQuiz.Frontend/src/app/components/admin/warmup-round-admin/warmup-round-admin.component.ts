import { Component, EventEmitter, Input, Output } from '@angular/core';
import { RoundDetailsModel } from 'src/app/models/round-details-model';

@Component({
  selector: 'app-warmup-round-admin',
  templateUrl: './warmup-round-admin.component.html',
  styleUrls: ['./warmup-round-admin.component.scss']
})
export class WarmupRoundAdminComponent{

  @Input() roundDetails!: RoundDetailsModel
  @Input() questionNumber!: number
  @Output() nextQuestionToParent = new EventEmitter();
  @Output() nextRoundToParent = new EventEmitter();
  isScoring = false
 
  nextQuestion(){
    this.nextQuestionToParent.emit()
  }

  scoring(){
    this.isScoring=true
  }

  nextRound(){
    this.nextRoundToParent.emit()
  }
}

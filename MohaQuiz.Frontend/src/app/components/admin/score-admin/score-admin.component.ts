import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { GameProcessService } from 'src/app/services/game-process.service';

@Component({
  selector: 'app-score-admin',
  templateUrl: './score-admin.component.html',
  styleUrls: ['./score-admin.component.scss']
})
export class ScoreAdminComponent implements OnInit {

  @Input() teamNames!: Array<string>
  @Output() scoringFinished = new EventEmitter()
  readyTeams: Array<string> = []

  constructor(private gps: GameProcessService){}

  ngOnInit() {
    this.gps.hc.on('GetReadyTeamName',tn => {
      this.readyTeams.push(tn)
      if(this.readyTeams.length === this.teamNames.length){
        this.scoringFinished.emit()
      }
    })
  }
}

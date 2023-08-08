import { ChangeDetectorRef, Component, OnDestroy, OnInit } from "@angular/core";
import { MediaMatcher } from '@angular/cdk/layout';
import { QuizService } from "src/app/services/quiz.service";
import { GameProcessService } from "src/app/services/game-process.service";
import { GameProcessStateModel } from "src/app/models/game-process-state-model";

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnDestroy,OnInit {
  
  mobileQuery: MediaQueryList;
  private _mobileQueryListener: () => void;
  teamName: string|null = null
  gameProcessState: GameProcessStateModel = {gameName: '', roundNumber: 0, questionNumber: 0, isGameStarted: false, isScoring: false, isGameFinished: false}


  constructor(changeDetectorRef: ChangeDetectorRef, media: MediaMatcher, private qs: QuizService, private gps: GameProcessService) {
    this.mobileQuery = media.matchMedia('(max-width: 600px)');
    this._mobileQueryListener = () => changeDetectorRef.detectChanges();
    this.mobileQuery.addListener(this._mobileQueryListener);
  }

  async ngOnInit() {
    this.gps.gameProcessState.subscribe({
      next: gp => this.gameProcessState = gp
    })
    
    this.qs.teamName.subscribe({
      next: tn => this.teamName = tn
    })

    if(localStorage.getItem('teamName')){
      await this.qs.isTeamCreated(localStorage.getItem('teamName') as string)
    }
  }

  ngOnDestroy(): void {
    this.mobileQuery.removeListener(this._mobileQueryListener);
    this.gps.hc.stop()
  }
}

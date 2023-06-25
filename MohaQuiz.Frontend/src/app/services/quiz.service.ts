import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.development';
import { TeamNameModel } from '../models/team-name-model';
import { BehaviorSubject, firstValueFrom } from 'rxjs';
import { GameProcessService } from './game-process.service';
import { RoundDetailsModel } from '../models/round-details-model';

@Injectable({
  providedIn: 'root'
})
export class QuizService {

  BASE_URL = environment.apiUrl +'quiz/'
  teamName = new BehaviorSubject<string | null>(null)

  constructor(private http: HttpClient, private gps: GameProcessService) {
  }

  async createTeam(teamName: TeamNameModel) {
    await firstValueFrom(this.http.post<TeamNameModel>(`${this.BASE_URL}createteam`,teamName))
      .then(res => {
        localStorage.setItem('teamName',res.teamName)
        this.teamName.next(res.teamName)
      })
      this.gps.listenForStartGameMessage()
  }

  async isTeamCreated(teamName: string) {
    await firstValueFrom(this.http.get<TeamNameModel>(`${this.BASE_URL}isteamcreated/${teamName}`))
      .then(res => {
        if(res){
          localStorage.setItem('teamName',res.teamName)
          this.teamName.next(res.teamName)
        } else {
          localStorage.removeItem('teamName')
        }
      })
      this.gps.listenForStartGameMessage()
  }

  async getAllTeamNames(): Promise<Array<string>> {
    return await firstValueFrom(this.http.get<Array<string>>(`${this.BASE_URL}getallteamnames`))
  }

  async GetRoundDetails(roundNumber: number): Promise<RoundDetailsModel> {
    return await firstValueFrom(this.http.get<RoundDetailsModel>(`${this.BASE_URL}getrounddetails/${roundNumber}`))
  }

  async resetGame() {
    await firstValueFrom(this.http.get(`${this.BASE_URL}resetgame`))
  }
}

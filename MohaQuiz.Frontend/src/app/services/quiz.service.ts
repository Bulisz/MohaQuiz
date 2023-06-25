import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.development';
import { TeamNameModel } from '../models/team-name-model';
import { BehaviorSubject, firstValueFrom } from 'rxjs';
import { HubConnection } from '@microsoft/signalr';

@Injectable({
  providedIn: 'root'
})
export class QuizService {

  BASE_URL = environment.apiUrl +'quiz/'
  teamName = new BehaviorSubject<string | null>(null)

  constructor(private http: HttpClient, public hc: HubConnection) {
    this.hc.start()
  }

  async createTeam(teamName: TeamNameModel) {
    await firstValueFrom(this.http.post<TeamNameModel>(`${this.BASE_URL}createteam`,teamName))
      .then(res => {
        localStorage.setItem('teamName',res.teamName)
        this.teamName.next(res.teamName)
      })
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
  }
}

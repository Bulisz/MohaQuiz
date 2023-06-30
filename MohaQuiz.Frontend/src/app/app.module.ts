import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HomeComponent } from './components/home/home.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from './material/material.module';
import { NavbarComponent } from './components/navbar/navbar.component';
import { TeamComponent } from './components/team/team.component';
import { AdminComponent } from './components/admin/admin.component';
import { GameComponent } from './components/game/game.component';
import { ScoreComponent } from './components/game/score/score.component';
import { FinalComponent } from './components/final/final.component';
import { WarmupRoundComponent } from './components/game/warmup-round/warmup-round.component';
import { ConnectionRoundComponent } from './components/game/connection-round/connection-round.component';
import { ThreeToOneRoundComponent } from './components/game/three-to-one-round/three-to-one-round.component';
import { AnagramRoundComponent } from './components/game/anagram-round/anagram-round.component';
import { AbcdRoundComponent } from './components/game/abcd-round/abcd-round.component';
import { NullableRoundComponent } from './components/game/nullable-round/nullable-round.component';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { AbcdRoundAdminComponent } from './components/admin/abcd-round-admin/abcd-round-admin.component';
import { AnagramRoundAdminComponent } from './components/admin/anagram-round-admin/anagram-round-admin.component';
import { ConnectionRoundAdminComponent } from './components/admin/connection-round-admin/connection-round-admin.component';
import { WarmupRoundAdminComponent } from './components/admin/warmup-round-admin/warmup-round-admin.component';
import { NullableRoundAdminComponent } from './components/admin/nullable-round-admin/nullable-round-admin.component';
import { ThreeToOneRoundAdminComponent } from './components/admin/three-to-one-round-admin/three-to-one-round-admin.component';
import { ScoreAdminComponent } from './components/admin/score-admin/score-admin.component';
import { ScoreOfTeamsAdminComponent } from './components/admin/score-of-teams-admin/score-of-teams-admin.component';
import { ScoreOfTeamComponent } from './components/game/score-of-team/score-of-team.component';
import { environment } from 'src/environments/environment';
import { GameSummaryComponent } from './components/game-summary/game-summary.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    NavbarComponent,
    TeamComponent,
    AdminComponent,
    GameComponent,
    ScoreComponent,
    FinalComponent,
    WarmupRoundComponent,
    ConnectionRoundComponent,
    ThreeToOneRoundComponent,
    AnagramRoundComponent,
    AbcdRoundComponent,
    NullableRoundComponent,
    AbcdRoundAdminComponent,
    AnagramRoundAdminComponent,
    ConnectionRoundAdminComponent,
    WarmupRoundAdminComponent,
    NullableRoundAdminComponent,
    ThreeToOneRoundAdminComponent,
    ScoreAdminComponent,
    ScoreOfTeamsAdminComponent,
    ScoreOfTeamComponent,
    GameSummaryComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    MaterialModule,
    FormsModule
  ],
  providers: [
    {
      provide: HubConnection,
      useFactory: () => {
        return new HubConnectionBuilder()
          .withUrl(environment.hubUrl)
          .withAutomaticReconnect()
          .build();
      }
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

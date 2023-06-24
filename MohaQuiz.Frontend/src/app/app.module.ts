import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
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
    NullableRoundComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    MaterialModule
  ],
  providers: [
    {
      provide: HubConnection,
      useFactory: () => {
        return new HubConnectionBuilder()
          .withUrl('https://localhost:5001/gamecontrolhub')
          .withAutomaticReconnect()
          .build();
      }
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

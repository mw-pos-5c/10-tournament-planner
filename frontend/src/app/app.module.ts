import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import {AppComponent} from './app.component';
import {HttpClientModule} from "@angular/common/http";
import {FormsModule} from "@angular/forms";
import { PlayersOverviewComponent } from './components/players-overview/players-overview.component';
import { MatchViewComponent } from './components/match-view/match-view.component';
import { MatchesOverviewComponent } from './components/matches-overview/matches-overview.component';

@NgModule({
  declarations: [
    AppComponent,
    PlayersOverviewComponent,
    MatchViewComponent,
    MatchesOverviewComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {
}

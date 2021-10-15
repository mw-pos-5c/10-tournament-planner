import {Component, Input, OnInit} from '@angular/core';
import Match from "../../models/Match";
import {BackendConnectorService} from "../../services/backend-connector.service";
import Player from "../../models/Player";

@Component({
  selector: 'app-match-view',
  templateUrl: './match-view.component.html',
  styleUrls: ['./match-view.component.scss']
})
export class MatchViewComponent implements OnInit {

  constructor(private backend: BackendConnectorService) { }

  @Input() match: Match | undefined;

  player1: Player | null = null;
  player2: Player | null = null;


  getPlayers(): void {
    this.backend.getPlayers().then(value => {
      this.player1 = value.find(p => p.id == this.match?.player1Id) ?? null;
      this.player2 = value.find(p => p.id == this.match?.player2Id) ?? null;
    })
  }

  win(index: number): void {
    if (this.match === undefined || this.match.winner !== 0) return;
    this.backend.setWinner(this.match.id, index).subscribe(success => {
      if (!success) {
        this.backend.reset(false);
        return;
      }
      if (this.match === undefined) return;
      this.match.winner = index;
    });
  }

  ngOnInit(): void {
    this.getPlayers();
  }

}

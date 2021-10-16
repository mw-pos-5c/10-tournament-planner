import {Component, OnInit} from '@angular/core';
import {BackendConnectorService} from "../../services/backend-connector.service";
import Player from "../../models/Player";

@Component({
  selector: 'app-players-overview',
  templateUrl: './players-overview.component.html',
  styleUrls: ['./players-overview.component.scss']
})
export class PlayersOverviewComponent implements OnInit {

  constructor(private backend: BackendConnectorService) {
  }

  players: Player[] = [];

  isWinner(id: number): boolean {
    const alive = this.players.filter(value => !value.isDead);
    if (alive.length !== 1) return false;
    return alive[0].id === id;
  }

  swapGender(id: number, gender: string): void {
    const player = this.players.find(value => value.id === id);
    if (player !== undefined) {
      gender = gender === 'MALE' ? 'FEMALE' : 'MALE';
      this.backend.setPlayerGender(id, gender).subscribe(value => {
          player.gender = gender;
      });
    }
  }

  ngOnInit(): void {
    this.backend.getPlayers(false).then(value => {
      this.players = value;
    })
  }
}

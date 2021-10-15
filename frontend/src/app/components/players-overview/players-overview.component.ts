import { Component, OnInit } from '@angular/core';
import {BackendConnectorService} from "../../services/backend-connector.service";
import Player from "../../models/Player";

@Component({
  selector: 'app-players-overview',
  templateUrl: './players-overview.component.html',
  styleUrls: ['./players-overview.component.scss']
})
export class PlayersOverviewComponent implements OnInit {

  constructor(private backend: BackendConnectorService) { }

  players: Player[] = [];

  ngOnInit(): void {
    this.backend.getPlayers(false).then(value => {
      this.players = value;
    })
  }

}

import { Component, OnInit } from '@angular/core';
import {BackendConnectorService} from "../../services/backend-connector.service";
import Player from "../../models/Player";
import Match from "../../models/Match";

@Component({
  selector: 'app-matches-overview',
  templateUrl: './matches-overview.component.html',
  styleUrls: ['./matches-overview.component.scss']
})
export class MatchesOverviewComponent implements OnInit {

  constructor(private backend: BackendConnectorService) { }

  matches: Match[] = [];

  ngOnInit(): void {
    this.backend.getMatches(true).then(value => {
      this.matches = value;
    })
  }

}

import {Component} from '@angular/core';
import {BackendConnectorService} from "./services/backend-connector.service";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {

  constructor(private backend: BackendConnectorService) { }

  ngOnInit(): void {
    this.backend.loadData();
  }

  reset(): void {
    this.backend.reset();
  }

}

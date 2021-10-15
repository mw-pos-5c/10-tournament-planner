import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import Player from "../models/Player";
import Match from "../models/Match";

@Injectable({
  providedIn: 'root'
})
export class BackendConnectorService {

  backendUrl: string = 'http://localhost:5000';

  playersCache: Player[] = [];
  matchesCache: Match[] = [];

  constructor(private http: HttpClient) {
  }

  public clearCache(): void {
    this.playersCache.length = 0;
    this.matchesCache.length = 0;
  }

  public loadData(): void {
    this.getPlayers(false).then(_ => {
      this.getMatches(false);
    })
  }


  public reset(server = true): void{

    if (server) {
      this.http.delete<never>(this.backendUrl + '/matches').subscribe(_ => {
        this.clearCache();
        this.loadData();
      });
    } else {
      this.clearCache();
      this.loadData();
    }

  }

  public getPlayers(cache = true): Promise<Player[]> {
    return new Promise<Player[]>(resolve => {
      if (cache) {
        resolve(this.playersCache);
        return;
      }
      this.http.get<Player[]>(this.backendUrl + '/players').subscribe(value => {
        this.playersCache.length = 0;
        this.playersCache.push(...value);
        resolve(this.playersCache);
      })
    });

  }

  public getMatches(cache = true): Promise<Match[]> {
    return new Promise<Match[]>(resolve => {
      if (cache) {
        resolve(this.matchesCache);
        return;
      }
      this.http.get<Match[]>(this.backendUrl + '/matches').subscribe(value => {
        this.matchesCache.length = 0;
        this.matchesCache.push(...value);
        resolve(this.matchesCache);
      })
    });


  }

  public setWinner(matchId: number, index: number): Observable<boolean> {
    return this.http.post<boolean>(this.backendUrl + '/matches/winner', {matchId, index});
  }
}

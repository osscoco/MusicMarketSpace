import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SpinnerService {

    private _loadingCount = 0;
    private loadingSubject = new BehaviorSubject<boolean>(false);
    loading$ = this.loadingSubject.asObservable();

    show() {
        this._loadingCount++;
        this.loadingSubject.next(true);
    }

    hide() {
        this._loadingCount = Math.max(0, this._loadingCount - 1);
        if (this._loadingCount === 0) {
        this.loadingSubject.next(false);
        }
    }

    reset() {
        this._loadingCount = 0;
        this.loadingSubject.next(false);
    }

    showFor(durationMs: number) {
        this.show();
        setTimeout(() => this.hide(), durationMs);
    }
}
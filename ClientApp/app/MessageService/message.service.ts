import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Subject } from 'rxjs/Subject';
import { Message } from 'primeng/components/common/api';

@Injectable()
export class MessageService {
    private subject = new Subject<Message>();

    sendSuccessMessage(message: string) {
        this.subject.next({ severity: 'info', summary: 'Success', detail: message });
    }

    sendErrorMessage(message: string) {
        this.subject.next({ severity: 'error', summary: 'Error', detail: message });
    }

    clearMessage() {
        this.subject.next();
    }

    getMessage(): Observable<Message> {
        return this.subject.asObservable();
    }
}
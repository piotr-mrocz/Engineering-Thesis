import { Injectable } from "@angular/core";

@Injectable({
    providedIn: 'root'
})

export class BackendSettings {
    public readonly baseAddress = "https://localhost:7016/";
}
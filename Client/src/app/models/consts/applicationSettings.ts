import { Injectable } from "@angular/core";

@Injectable({
    providedIn: 'root'
})

export class ApplicationSettings {
    public readonly userPhotoBaseAddress = "../../../assets/Images/People/";
    public readonly logoBaseAddress = "../../../assets/Images/";

}
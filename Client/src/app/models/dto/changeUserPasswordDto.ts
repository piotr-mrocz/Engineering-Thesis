export class ChangeUserPasswordDto {
    idUser?: number;
    oldPassword?: string;
    newPassword?: string;
    confirmNewPassword?: string;
}

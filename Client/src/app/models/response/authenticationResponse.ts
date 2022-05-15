export interface AuthenticationResponse {
    isAuthorize: boolean;
    userName?: string;
    role?: string;
    token?: string;
}
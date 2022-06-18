export class BackendResponse<Type> {
    message?: string;
    succeeded?: boolean;
    data?: Type
}
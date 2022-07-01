import { Injectable } from "@angular/core";

@Injectable({
    providedIn: 'root'
})

export class EndpointsUrl {
    public readonly getAllDepartmentsEndpoint = "api/Department/GetAllDepartments";
    public readonly getAllUsersByIdDepartmentEndpoint = "api/User/GetAllUsersByIdDepartment";
    public readonly getAllUsersEndpoint = "api/User/GetAllUsers";
    public readonly loginEndpoint = "api/Account/Login";
    public readonly getUsersPositionsAndDepartmentsAndRolesEndpoint = "api/User/GetUsersPositionsAndDepartmentsAndRoles";
    public readonly addNewUserEndpoint = "api/User/AddNewUser";
    public readonly getAllUserConversationEndpoint = "api/Message/GetConversation";
    public readonly addNewMessageEndpoint = "api/Message/AddNewMessage";
}
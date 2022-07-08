import { Injectable } from "@angular/core";

@Injectable({
    providedIn: 'root'
})

export class EndpointsUrl {
    // user
    public readonly getAllUsersByIdDepartmentEndpoint = "api/User/GetAllUsersByIdDepartment";
    public readonly getAllUsersEndpoint = "api/User/GetAllUsers";
    public readonly getUsersPositionsAndDepartmentsAndRolesEndpoint = "api/User/GetUsersPositionsAndDepartmentsAndRoles";
    public readonly addNewUserEndpoint = "api/User/AddNewUser";
    public readonly getAllUserInDepartmentByIdSupervisorEndpoint = "api/User/GetAllUserInDepartmentByIdSupervisor";
    public readonly releaseUserEndpoint = "api/User/ReleaseUser";
    public readonly updateUserDataEndpoint = "api/User/UpdateUserData";
    public readonly changeUserPasswordEndpoint = "api/User/ChangeUserPassword";
    public readonly resetUserPasswordEndpoint = "api/User/ResetUserPassword";

    // department
    public readonly getAllDepartmentsEndpoint = "api/Department/GetAllDepartments";
    
    // account
    public readonly loginEndpoint = "api/Account/Login";
    
    // message
    public readonly getAllUserConversationEndpoint = "api/Message/GetConversation";
    public readonly addNewMessageEndpoint = "api/Message/AddNewMessage";

    // task
    public readonly getAllUserTasksEndpoint = "api/Task/GetAllUserTasks";
    public readonly addNewTaskEndpoint = "api/Task/AddNewTask";
    public readonly updateTaskEndpoint = "api/Task/UpdateTask";
    public readonly deleteTaskEndpoint = "api/Task/DeleteTask";
    public readonly getAllPriorityEndpoint = "api/Task/GetAllPriority";
    public readonly updateStatusTaskEndpoint = "api/Task/UpdateStatusTask";
    public readonly getUsersTasksForSupervisorEndpoint = "api/Task/GetUsersTasksForSupervisor";
    
    // request for leave
    public readonly createRequestForLeaveEndpoint = "api/RequestForLeave/CreateRequestForLeave";
    public readonly acceptRequestForLeaveEndpoint = "api/RequestForLeave/AcceptRequestForLeave";
    public readonly rejectRequestForLeaveEndpoint = "api/RequestForLeave/RejectRequestForLeave";
    public readonly removeRequestForLeaveEndpoint = "api/RequestForLeave/RemoveRequestForLeave";
}
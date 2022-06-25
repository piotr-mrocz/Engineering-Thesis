import { DepartmentDto } from "./departmentDto";
import { PositionDto } from "./positionDto";
import { RoleDto } from "./roleDto";

export interface PositionsAndDepartmentsAndRoleDto {
   departments: DepartmentDto[],
   positions: PositionDto[],
   roles: RoleDto[]
}

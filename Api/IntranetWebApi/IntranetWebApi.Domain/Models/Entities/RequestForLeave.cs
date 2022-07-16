namespace IntranetWebApi.Domain.Models.Entities;

public class RequestForLeave
{
    public int Id { get; set; }
    public int IdApplicant { get; set; }
    public DateTime CreateDate { get; set; }
    public int IdSupervisor { get; set; }
    public int AbsenceType { get; set; } // I created special enum for this
    public int Status { get; set; } // I created special enum for this
    public DateTime ActionDate { get; set; }
    public string RejectReason { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int DaysAbsence { get; set; }

    public User Applicant { get; set; } = null!;
}

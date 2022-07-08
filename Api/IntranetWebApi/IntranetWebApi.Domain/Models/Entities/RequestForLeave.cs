using IntranetWebApi.Models.Entities;

namespace IntranetWebApi.Domain.Models.Entities;

public class RequestForLeave
{
    public int Id { get; set; }
    public int IdApplicant { get; set; }
    public DateTime CreateDate { get; set; }
    public int IdSupervisor { get; set; }
    public int AbsenceType { get; set; } // I created special enum for this
    public bool? IsAcceptedBySupervisor { get; set; }  // null - when new request
    public DateTime? DateOfAcceptance { get; set; }
    public string RejectionReason { get; set; } = null!;
    public bool IsCancel { get; set; }
    public DateTime? DateOfCancel { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int DaysAbsence { get; set; }

    public User Applicant { get; set; } = null!;
}

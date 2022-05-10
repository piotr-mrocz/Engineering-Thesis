using IntranetWebApi.Models.Entities;

namespace IntranetWebApi.Domain.Models.Entities;

public class RequestForLeave
{
    public int Id { get; set; }
    public int IdApplicant { get; set; }
    public DateTime CreateDate { get; set; }
    public int IdSupervisor { get; set; }
    public int AbsenceType { get; set; }
    public bool IsAcceptedBySupervisor { get; set; }
    public DateTime? DateOfAcceptance { get; set; }
    public string RejectionReason { get; set; } = null;
    public bool IsCancel { get; set; }
    public DateTime? DateOfCancel { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public User Applicant { get; set; } = null!;
}

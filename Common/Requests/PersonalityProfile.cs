namespace Common.Requests;
public class PersonalityProfileRequest
{
    public int ApplicantId { get; set; }
    public bool WellGroomed { get; set; } = false;
    public bool Friendly { get; set; } = false;
    public bool Active { get; set; } = false;
    public bool Confident { get; set; } = false;
    public bool Polite { get; set; } = false;
    public bool SelfControl { get; set; } = false;
    public bool WorksPromptly { get; set; } = false;
    public bool Adaptable { get; set; } = false;
    public bool Outgoing { get; set; } = false;
    public bool Organized { get; set; } = false;
    public bool Creative { get; set; } = false;
    public bool Truthful { get; set; } = false;
    public bool HabituallySilent { get; set; } = false;
    public bool Generous { get; set; } = false;
    public bool Conforming { get; set; } = false;
    public bool Resourceful { get; set; } = false;
    public bool Cautious { get; set; } = false;
    public bool Conscientious { get; set; } = false;
    public bool GoodNatured { get; set; } = false;
    public bool Industrious { get; set; } = false;
    public bool EmotionallyStable { get; set; } = false;
    public bool WorksWillWithOthers { get; set; } = false;
    public bool VolunteersToLead { get; set; } = false;
    public bool PreferredByGroups { get; set; } = false;
    public bool TakesChargeWhenAssigned { get; set; } = false;
    public string? Problems { get; set; }
    public string? ComfortableDiscussing { get; set; }
    public bool Studies { get; set; } = false;
    public bool Family { get; set; } = false;
    public bool Friend { get; set; } = false;
    public bool Self { get; set; } = false;
    public string? Specify { get; set; }
}

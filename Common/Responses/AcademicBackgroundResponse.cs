namespace Common.Responses;
public class AcademicBackgroundResponse
{
    public int Id { get; set; }
    public string SchoolAttended { get; set; }
    public string SchoolAddress { get; set; }
    public string LRN { get; set; }
    public string? Awards { get; set; }
    public int YearGraduated { get; set; }
    public string SchoolSector { get; set; }
    public string ElementarySchoolAttended { get; set; }
    public string ElementaryInclusiveYear { get; set; }
    public string ElementarySchoolAddress { get; set; }
    public string? ElementaryAwards { get; set; }
    public string JuniorSchoolAttended { get; set; }
    public string JuniorInclusiveYear { get; set; }
    public string JuniorSchoolAddress { get; set; }
    public string? JuniorAward { get; set; }
    public string SeniorSchoolAttended { get; set; }
    public string SeniorInclusiveYear { get; set; }
    public string SeniorSchoolAddress { get; set; }
    public string? SeniorAwards { get; set; }
    public string? CollegeSchoolAttended { get; set; }
    public string? CollegeInclusiveYear { get; set; }
    public string? CollegeSchoolAddress { get; set; }
    public string? CollegeAwards { get; set; }
    public string? GraduateSchoolAttended { get; set; }
    public string? GraduateInclusiveYear { get; set; }
    public string? GraduateSchoolAddress { get; set; }
    public string? GraduateAwards { get; set; }
    public string? PostGraduateSchoolAttended { get; set; }
    public string? PostGraduaterInclusiveYear { get; set; }
    public string? PostGraduateSchoolAddress { get; set; }
    public string? PostGraduateAward { get; set; }
    public string? LastAttended { get; set; }
    public string? LastInclusiveYear { get; set; }
    public string? LastSchoolAddress { get; set; }
    public string? LastAwards { get; set; }
    public string? SY { get; set; }
    public string? Semester { get; set; }
    public string? Scholarship { get; set; }
    public string? EducationBenefactor { get; set; }
    public string? BenefactorRelation { get; set; }
    public string? Organization { get; set; }
    public string? PlanAfterCollege { get; set; }
    public string? Motto { get; set; }
    public string? Skills { get; set; }
}

public class LrnResponse
{
    public int Id { get; set; }
    public int ApplicantId { get; set; }
    public string LRN { get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace Common.Requests;

public class AcademicBackgroundRequest
{
    public int ApplicantId { get; set; }

    [Required(ErrorMessage = "Last school attended is required")]
    public string SchoolAttended { get; set; }
    [Required(ErrorMessage = "last school address is required")]
    public string SchoolAddress { get; set; }
    [Required(ErrorMessage = "LRN is required")]
    [StringLength(12, MinimumLength = 12, ErrorMessage = "LRN must be exactly 12 digits")]
    public string LRN { get; set; }
    public string? Awards { get; set; }
    [Required(ErrorMessage = "Last school year graduated is required")]
    public int YearGraduated { get; set; }
    [Required(ErrorMessage = "last school sector is required")]
    public string SchoolSector { get; set; }
    [Required(ErrorMessage = "Elementary school attended is required.")]
    public string ElementarySchoolAttended { get; set; }
    [Required(ErrorMessage = "Elementary school inclusive year is required.")]
    public string ElementaryInclusiveYear { get; set; }
    [Required(ErrorMessage = "Elementary school address is required.")]
    public string ElementarySchoolAddress { get; set; }
    public string? ElementaryAward { get; set; }
    [Required(ErrorMessage = "Junior high school attended is required.")]

    public string JuniorSchoolAttended { get; set; }
    [Required(ErrorMessage = "Junior high school inclusive year is required.")]
    public string JuniorInclusiveYear { get; set; }
    [Required(ErrorMessage = "Junior high school address is required.")]
    public string JuniorSchoolAddress { get; set; }
    public string? JuniorAward { get; set; }
    [Required(ErrorMessage = "Senior high school attended is required.")]
    public string SeniorSchoolAttended { get; set; }
    [Required(ErrorMessage = "Senior high school inclusive year is required.")]
    public string SeniorInclusiveYear { get; set; }
    [Required(ErrorMessage = "Senior high school address is required.")]
    public string SeniorSchoolAddress { get; set; }
    public string? SeniorAward { get; set; }
    [Required(ErrorMessage = "Last school attended is required.")]
    public string LastAttended { get; set; }
    [Required(ErrorMessage = "Last school inclusive year is required.")]
    public string LastInclusiveYear { get; set; }
    [Required(ErrorMessage = "Last school address is required.")]
    public string LastSchoolAddress { get; set; }
    public string? LastAwards { get; set; }

    public string? CollegeSchoolAttended { get; set; }
    public string? CollegeInclusiveYear { get; set; }
    public string? CollegeSchoolAddress { get; set; }
    public string? CollegeAward { get; set; }

    public string? GraduateSchoolAttended { get; set; }
    public string? GraduateInclusiveYear { get; set; }
    public string? GraduateSchoolAddress { get; set; }
    public string? GraduateAward { get; set; }
    public string? PostGraduateSchoolAttended { get; set; }
    public string? PostGraduaterInclusiveYear { get; set; }
    public string? PostGraduateSchoolAddress { get; set; }
    public string? PostGraduateAward { get; set; }
    public string? Scholarship { get; set; }
    public string? EducationBenefactor { get; set; }
    public string? BenefactorRelation { get; set; }
    public string? Organization { get; set; }
    public string? PlanAfterCollege { get; set; }
    public string? Motto { get; set; }
    public string? Skills { get; set; }
}

public class LrnUpdate
{
    public int Id { get; set; }
    public int ApplicantId { get; set; }
    [Required(ErrorMessage = "LRN is required")]
    [StringLength(12, MinimumLength = 12, ErrorMessage = "LRN must be exactly 12 digits")]
    public string LRN { get; set; }
}

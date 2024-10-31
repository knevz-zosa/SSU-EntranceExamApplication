namespace Domain.Enumerations;
public enum Indigenous : byte
{
    Aeta = 0,
    Ati = 1,
    Badjao = 2,
    Bagobo = 3,
    Blaan = 4,
    Bugkalot = 5,
    Bukidnon = 6,
    Dumagat = 7,
    Gaddang = 8,
    Higaonon = 9,
    Ibaloi = 10,
    Ifugao = 11,
    Ilongot = 12,
    Isneg = 13,
    Ivatan = 14,
    Kalinga = 15,
    Kankanaey = 16,
    Mamanwa = 17,
    Mangyan = 18,
    Manobo = 19,
    Maranao = 20,
    Palawan = 21,
    Subanen = 22,
    Tboli = 23,
    Tingguian = 24,
    Tiruray = 25,
    Yakan = 26,
}
public static class IndigenousExtensions
{
    public static string ToFriendlyName(this Indigenous name)
    {
        switch (name)
        {
            case Indigenous.Aeta:
                return "Aeta";
            case Indigenous.Ati:
                return "Ati";
            case Indigenous.Badjao:
                return "Badjao";
            case Indigenous.Bagobo:
                return "Bagobo";
            case Indigenous.Blaan:
                return "B'laan";
            case Indigenous.Bugkalot:
                return "Bugkalot";
            case Indigenous.Bukidnon:
                return "Bukidnon";
            case Indigenous.Dumagat:
                return "Dumagat";
            case Indigenous.Gaddang:
                return "Gaddang";
            case Indigenous.Higaonon:
                return "Higaonon";
            case Indigenous.Ibaloi:
                return "Ibaloi";
            case Indigenous.Ifugao:
                return "Ifugao";
            case Indigenous.Ilongot:
                return "Ilongot";
            case Indigenous.Isneg:
                return "Isneg";
            case Indigenous.Ivatan:
                return "Ivatan";
            case Indigenous.Kalinga:
                return "Kalinga";
            case Indigenous.Kankanaey:
                return "Kankanaey";
            case Indigenous.Mamanwa:
                return "Mamanwa";
            case Indigenous.Mangyan:
                return "Mangyan";
            case Indigenous.Manobo:
                return "Manobo";
            case Indigenous.Maranao:
                return "Maranao";
            case Indigenous.Palawan:
                return "Palawan";
            case Indigenous.Subanen:
                return "Subanen";
            case Indigenous.Tboli:
                return "T'boli";
            case Indigenous.Tingguian:
                return "Tingguian";
            case Indigenous.Tiruray:
                return "Tiruray";
            case Indigenous.Yakan:
                return "Yakan";

            default:
                throw new Exception("Value is invalid");
        }
    }
}
public enum ChronicIllness : byte
{
    Alzheimer = 0,
    Arthritis = 1,
    Asthma = 2,
    Bipolar = 3,
    Cancer = 4,
    ChronicKidney = 5,
    COPD = 6,
    CysticFibrosis = 7,
    Depression = 8,
    Diabetes = 9,
    Endometriosis = 10,
    Epilepsy = 11,
    HeartDisease = 12,
    HighBlood = 13,
    HighCholesterol = 14,
    MultipleSclerosis = 15,
    Obesity = 16,
    OralDisease = 17,
    Osteoporosis = 18,
    ParkinsonDisease = 19,
    Stroke = 20,
    UlcerativeColitis = 21,
}
public static class ChronicIllnessExtensions
{
    public static string ToFriendlyName(this ChronicIllness name)
    {
        switch (name)
        {
            case ChronicIllness.Alzheimer:
                return "Alzheimer";
            case ChronicIllness.Arthritis:
                return "Arthritis";
            case ChronicIllness.Asthma:
                return "Asthma";
            case ChronicIllness.Bipolar:
                return "Bipolar";
            case ChronicIllness.Cancer:
                return "Cancer";
            case ChronicIllness.ChronicKidney:
                return "Chronic Kidney";
            case ChronicIllness.COPD:
                return "COPD";
            case ChronicIllness.CysticFibrosis:
                return "Cystic Fibrosis";
            case ChronicIllness.Depression:
                return "Depression";
            case ChronicIllness.Diabetes:
                return "Diabetes";
            case ChronicIllness.Endometriosis:
                return "Endometriosis";
            case ChronicIllness.Epilepsy:
                return "Epilepsy";
            case ChronicIllness.HeartDisease:
                return "Heart Disease";
            case ChronicIllness.HighBlood:
                return "High Blood";
            case ChronicIllness.HighCholesterol:
                return "High Cholesterol";
            case ChronicIllness.MultipleSclerosis:
                return "Multiple Sclerosis";
            case ChronicIllness.Obesity:
                return "Obesity";
            case ChronicIllness.OralDisease:
                return "Oral Disease";
            case ChronicIllness.Osteoporosis:
                return "Osteoporosis";
            case ChronicIllness.ParkinsonDisease:
                return "Parkinson Disease";
            case ChronicIllness.Stroke:
                return "Stroke";
            case ChronicIllness.UlcerativeColitis:
                return "Ulcerative Colitis";

            default:
                throw new Exception("Value is invalid");
        }
    }
}

public enum Emergency : byte
{
    Parent = 0,
    Sibling = 1,
    Spouse = 2,
    SonDaughter = 3,
    Relatives = 4,
    Partner = 5,
    Friend = 6
}
public static class EmergencyExtensions
{
    public static string ToFriendlyName(this Emergency name)
    {
        switch (name)
        {
            case Emergency.Parent:
                return "Parent";
            case Emergency.Sibling:
                return "Sibling";
            case Emergency.Spouse:
                return "Spouse";
            case Emergency.SonDaughter:
                return "Son/Daughter";
            case Emergency.Relatives:
                return "Relatives";
            case Emergency.Partner:
                return "Partner";
            case Emergency.Friend:
                return "Friend";
            default:
                throw new Exception("Value is invalid");
        }
    }
}
public enum Disabilities : byte
{
    ChronicIllness = 0,
    CommunicationDisability = 1,
    LearningDisability = 2,
    MentalDisability = 3,
    OrthopedicDisability = 4,
    PsychosocialDisability = 5,
    VisualDisability = 6,
    Others = 7
}
public static class DisabilitieseExtensions
{
    public static string ToFriendlyName(this Disabilities name)
    {
        switch (name)
        {
            case Disabilities.ChronicIllness:
                return "Chronic Illness";
            case Disabilities.CommunicationDisability:
                return "Communication Disability";
            case Disabilities.LearningDisability:
                return "Learning Disability";
            case Disabilities.MentalDisability:
                return "Mental Disability";
            case Disabilities.OrthopedicDisability:
                return "Orthopedic Disability";
            case Disabilities.PsychosocialDisability:
                return "Psychosocial Disability";
            case Disabilities.VisualDisability:
                return "Visual Disability";
            case Disabilities.Others:
                return "Others";
            default:
                throw new Exception("Value is invalid");
        }
    }
}
public enum MonthlyIncome : byte
{
    NotApplicable = 0,
    BelowTenThousand = 1,
    TenThousandToTwentyThousand = 2,
    TwentyThousandToThirtyThousand = 3,
    ThirtyThousandToFortyThousand = 4,
    FortyThousandToFiftyThousand = 5,
    AboveFiftyThousand = 6
}
public static class MonthlyIncomeExtensions
{
    public static string ToFriendlyName(this MonthlyIncome name)
    {
        switch (name)
        {
            case MonthlyIncome.NotApplicable:
                return "Not Applicable";
            case MonthlyIncome.BelowTenThousand:
                return "Below P10,000";
            case MonthlyIncome.TenThousandToTwentyThousand:
                return "P10,000 - P20,000";
            case MonthlyIncome.TwentyThousandToThirtyThousand:
                return "P20,000 - P30,000";
            case MonthlyIncome.ThirtyThousandToFortyThousand:
                return "P30,000 - P40,000";
            case MonthlyIncome.FortyThousandToFiftyThousand:
                return "P40,000 - P50,000";
            case MonthlyIncome.AboveFiftyThousand:
                return "Above P50,000";
            default:
                throw new Exception("Value is invalid");
        }
    }
}
public enum EducationLevel : byte
{
    NotApplicable = 0,
    NoFormalEducation = 1,
    ElementaryLevel = 2,
    ElementaryGraduate = 3,
    HighSchoolLevel = 4,
    HighSchoolGraduate = 5,
    CollegeLevel = 6,
    CollegeGraduate = 7
}
public static class EducationLevelExtensions
{
    public static string ToFriendlyName(this EducationLevel name)
    {
        switch (name)
        {
            case EducationLevel.NotApplicable:
                return "Not Applicable";
            case EducationLevel.NoFormalEducation:
                return "No Formal Education";
            case EducationLevel.ElementaryLevel:
                return "Elementary Level";
            case EducationLevel.ElementaryGraduate:
                return "Elementary Graduate";
            case EducationLevel.HighSchoolLevel:
                return "HighSchool Level";
            case EducationLevel.HighSchoolGraduate:
                return "HighSchool Graduate";
            case EducationLevel.CollegeLevel:
                return "College Level";
            case EducationLevel.CollegeGraduate:
                return "College Graduate";
            default:
                throw new Exception("Value is invalid");
        }
    }
}

public enum Sex : byte
{
    Male = 0,
    Female = 1
}
public static class SexExtensions
{
    public static string ToFriendlyName(this Sex name)
    {
        switch (name)
        {
            case Sex.Male:
                return "Male";
            case Sex.Female:
                return "Female";
            default:
                throw new Exception("Value is invalid");
        }
    }
}

public enum ScheduleTime : byte
{
    Morning = 0,
    Afternoon = 1,
}
public static class ScheduleTimeExtensions
{
    public static string ToFriendlyName(this ScheduleTime name)
    {
        switch (name)
        {
            case ScheduleTime.Morning:
                return "8:00am";
            case ScheduleTime.Afternoon:
                return "1:00pm";
            default:
                throw new Exception("Value is invalid");
        }
    }
}


public enum Dialect : byte
{
    Aklanon = 0,
    Bikol = 1,
    Cebuano = 2,
    Chavacano = 3,
    Hiligaynon = 4,
    Ibanag = 5,
    Ilocano = 6,
    Ivatan = 7,
    Kapampangan = 8,
    Kinaraya = 9,
    Maguindanao = 10,
    Maranao = 11,
    Pangasinan = 12,
    Sambal = 13,
    Surigaonon = 14,
    Tagalog = 15,
    Tausug = 16,
    Waray = 17,
    Yakan = 18,
    Others = 19

}
public static class DialectExtensions
{
    public static string ToFriendlyName(this Dialect name)
    {
        switch (name)
        {
            case Dialect.Aklanon:
                return "Aklanon";
            case Dialect.Bikol:
                return "Bikol";
            case Dialect.Cebuano:
                return "Cebuano";
            case Dialect.Chavacano:
                return "Chavacano";
            case Dialect.Hiligaynon:
                return "Hiligaynon";
            case Dialect.Ibanag:
                return "Bikol";
            case Dialect.Ilocano:
                return "Ilocano";
            case Dialect.Ivatan:
                return "Ivatan";
            case Dialect.Kapampangan:
                return "Kapampangan";
            case Dialect.Kinaraya:
                return "Kinaray-a";
            case Dialect.Maguindanao:
                return "Maguindanao";
            case Dialect.Maranao:
                return "Maranao";
            case Dialect.Pangasinan:
                return "Pangasinan";
            case Dialect.Sambal:
                return "Sambal";
            case Dialect.Surigaonon:
                return "Surigaonon";
            case Dialect.Tagalog:
                return "Tagalog";
            case Dialect.Tausug:
                return "Tausug";
            case Dialect.Waray:
                return "Waray";
            case Dialect.Yakan:
                return "Yakan";
            case Dialect.Others:
                return "Others";
            default:
                throw new Exception("Value is invalid");
        }
    }
}


public enum Religion : byte
{
    Aglipay = 0,
    BBC = 1,
    COC = 2,
    IFI = 3,
    INC = 4,
    Islam = 5,
    JW = 6,
    RC = 7,
    SDA = 8,
    UCCP = 9,
    None = 10,
    Others = 11

}
public static class ReligionExtensions
{
    public static string ToFriendlyName(this Religion name)
    {
        switch (name)
        {
            case Religion.Aglipay:
                return "Aglipay(PIC)";
            case Religion.BBC:
                return "BBC";
            case Religion.COC:
                return "CoC";
            case Religion.IFI:
                return "IFI";
            case Religion.INC:
                return "INC";
            case Religion.Islam:
                return "Islam";
            case Religion.JW:
                return "JW";
            case Religion.RC:
                return "RC";
            case Religion.SDA:
                return "SDA";
            case Religion.UCCP:
                return "UCCP";
            case Religion.None:
                return "None";
            case Religion.Others:
                return "Others";

            default:
                throw new Exception("Value is invalid");
        }
    }
}

public enum CivilStatus : byte
{
    Single = 0,
    Married = 1,
    Widowed_Widower = 2,
    Separated = 3,
    LivingIn = 4
}
public static class CivilStatusExtensions
{
    public static string ToFriendlyName(this CivilStatus name)
    {
        switch (name)
        {
            case CivilStatus.Single:
                return "Single";
            case CivilStatus.Married:
                return "Married";
            case CivilStatus.Widowed_Widower:
                return "Widowed / Widower";
            case CivilStatus.Separated:
                return "Separated";
            case CivilStatus.LivingIn:
                return "Living-In";
            default:
                throw new Exception("Value is invalid");
        }
    }
}

public enum SchoolSector : byte
{
    Government = 0,
    Private = 1
}
public static class SchoolSectorExtensions
{
    public static string ToFriendlyName(this SchoolSector name)
    {
        switch (name)
        {
            case SchoolSector.Government:
                return "Government";
            case SchoolSector.Private:
                return "Private";
            default:
                throw new Exception("Value is invalid");
        }
    }
}

public enum StudenStatus : byte
{
    New = 0,
    Shifter = 1,
    Transferee = 2,
    Returnee = 3,
    SecondCourser = 4
}
public static class StudenStatusExtensions
{
    public static string ToFriendlyName(this StudenStatus name)
    {
        switch (name)
        {
            case StudenStatus.New:
                return "New";
            case StudenStatus.Shifter:
                return "Shifter";
            case StudenStatus.Transferee:
                return "Transferee";
            case StudenStatus.Returnee:
                return "Returnee";
            case StudenStatus.SecondCourser:
                return "Second Courser";

            default:
                throw new Exception("Value is invalid");
        }
    }
}

public enum TracksAndStrands : byte
{
    ABM = 0,
    STEM = 1,
    HUMSS = 2,
    GAS = 3,
    ArtsandDesignTrack = 4,
    SportsTrack = 5,
    AFA = 6,
    HE = 7,
    IA = 8,
    ICT = 9
}
public static class TracksAndStrandsExtensions
{
    public static string ToFriendlyName(this TracksAndStrands name)
    {
        switch (name)
        {
            case TracksAndStrands.ABM:
                return "Academic Track (ABM)";
            case TracksAndStrands.STEM:
                return "Academic Track (STEM)";
            case TracksAndStrands.HUMSS:
                return "Academic Track (HUMSS)";
            case TracksAndStrands.GAS:
                return "Academic Track (GAS)";
            case TracksAndStrands.ArtsandDesignTrack:
                return "Arts and Design Track";
            case TracksAndStrands.SportsTrack:
                return "Sports Track";
            case TracksAndStrands.AFA:
                return "Technical Vocational Livelihood Track (AFA)";
            case TracksAndStrands.HE:
                return "Technical Vocational Livelihood Track (HE)";
            case TracksAndStrands.IA:
                return "Technical Vocational Livelihood Track (IA)";
            case TracksAndStrands.ICT:
                return "Technical Vocational Livelihood Track (ICT)";

            default:
                throw new Exception("Value is invalid");
        }
    }
}

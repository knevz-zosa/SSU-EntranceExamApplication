using AutoMapper;
using Common.Requests;
using Common.Responses;
using Domain.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ApplicationLayer.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserResponse>();
        CreateMap<UserResponse, UserUpdateProfile>();
        CreateMap<UserResponse, UserUpdateAccessRole>();
        CreateMap<UserResponse, UserUpdateCredential>();

        CreateMap<Campus, CampusResponse>()
              .ForMember(x => x.Departments, opt => opt.MapFrom(src => src.Departments))
              .ForMember(x => x.Courses, opt => opt.MapFrom(src => src.Courses));
        CreateMap<CampusResponse, CampusUpdate>();

        CreateMap<Department, DepartmentResponse>()
              .ForMember(x => x.Campus, opt => opt.MapFrom(src => src.Campus))
              .ForMember(x => x.Courses, opt => opt.MapFrom(src => src.Courses));
        CreateMap<DepartmentResponse, DepartmentUpdate>();

        CreateMap<Course, CourseResponse>()
               .ForMember(x => x.Campus, opt => opt.MapFrom(src => src.Campus))
               .ForMember(x => x.Department, opt => opt.MapFrom(src => src.Department));
        CreateMap<CourseResponse, CourseUpdate>();

        CreateMap<Schedule, ScheduleResponse>()
              .ForMember(x => x.Campus, opt => opt.MapFrom(src => src.Campus))
              .ForMember(x => x.Applicants, opt => opt.MapFrom(src => src.Applicants));

        CreateMap<Applicant, ApplicantResponse>()
               .ForMember(x => x.Schedule, opt => opt.MapFrom(src => src.Schedule))
               .ForPath(x => x.Schedule.Campus, opt => opt.MapFrom(src => src.Schedule.Campus))
               .ForPath(x => x.Schedule.Campus.Courses, opt => opt.MapFrom(src => src.Schedule.Campus.Courses))
               .ForMember(x => x.PersonalInformation, opt => opt.MapFrom(src => src.PersonalInformation))
               .ForMember(x => x.Spouse, opt => opt.MapFrom(src => src.Spouse))
               .ForMember(x => x.SoloParent, opt => opt.MapFrom(src => src.SoloParent))
               .ForMember(x => x.AcademicBackground, opt => opt.MapFrom(src => src.AcademicBackground))
               .ForMember(x => x.ParentGuardianInformation, opt => opt.MapFrom(src => src.ParentGuardianInformation))
               .ForMember(x => x.FamilyRelations, opt => opt.MapFrom(src => src.FamilyRelations))
               .ForMember(x => x.PersonalityProfile, opt => opt.MapFrom(src => src.PersonalityProfile))
               .ForMember(x => x.PhysicalHealth, opt => opt.MapFrom(src => src.PhysicalHealth))
               .ForMember(x => x.PsychiatristConsultation, opt => opt.MapFrom(src => src.PsychiatristConsultation))
               .ForMember(x => x.PsychologistConsultation, opt => opt.MapFrom(src => src.PsychologistConsultation))
               .ForMember(x => x.CouncelorConsultation, opt => opt.MapFrom(src => src.CouncelorConsultation))
               .ForMember(x => x.EmergencyContact, opt => opt.MapFrom(src => src.EmergencyContact))
               .ForMember(x => x.Transfers, opt => opt.MapFrom(src => src.Transfers))
               .ForMember(x => x.Interviews, opt => opt.MapFrom(src => src.Interviews))
               .ForMember(x => x.Examination, opt => opt.MapFrom(src => src.Examination))
               .ForMember(x => x.FirstApplicationInfo, opt => opt.MapFrom(src => src.FirstApplicationInfo))
               .ForMember(x => x.Registered, opt => opt.MapFrom(src => src.Registered));
        CreateMap<ApplicantResponse, ApplicantTransfer>();
        CreateMap<ApplicantResponse, ApplicantUpdateStudentId>();
        CreateMap<ApplicantResponse, ApplicantUpdateGwaStatusTrack>();

        CreateMap<PersonalInformation, PersonalInformationResponse>();
        CreateMap<PersonalInformationResponse, PersonalInformationUpdate>();

        CreateMap<AcademicBackground, AcademicBackgroundResponse>();
        CreateMap<LrnResponse, LrnUpdate>();

        CreateMap<Examination, ExaminationResponse>();
        CreateMap<ExaminationResponse, ExaminationUpdate>();

        CreateMap<Interview, InterviewResponse>();
        CreateMap<InterviewResponse, InterviewActiveUpdate>();
        CreateMap<InterviewResponse, InterviewRatingUpdate>();

       
        CreateMap<FirstApplicationInfo, FirstApplicationInfoResponse>();
        CreateMap<Registered, RegisteredResponse>();
        CreateMap<Spouse, SpouseResponse>();       
        CreateMap<PhysicalHealth, PhysicalHealthResponse>();
        CreateMap<CouncelorConsultation, CouncelorConsultationResponse>();
        CreateMap<PsychiatristConsultation, PsychiatristConsultationResponse>();
        CreateMap<PsychologistConsultation, PsychologistConsultationResponse>();
        CreateMap<FamilyRelation, FamilyRelationResponse>();
        CreateMap<ParentGuardianInformation, ParentGuardianInformationResponse>();
        CreateMap<EmergencyContact, EmergencyContactResponse>();
        CreateMap<PersonalityProfile, PersonalityProfileResponse>();
        

    }
}


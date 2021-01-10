using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.EntityHelpers;
using Core.Loops;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<JobToRequest, JobToRequestToReturnDto>()
              .ForMember(d => d.TimeDetail, o => o.MapFrom(s => s.TimeDetail.Hour))
              .ForMember(d => d.ClientLocation, o => o.MapFrom(s => s.ClientLocation.Address1 + ' ' + s.ClientLocation.Address2))
              .ForMember(d => d.AttributeDetail, o => o.MapFrom(s => s.AttributeDetail.AttributeName))
              .ForMember(d => d.ShiftState, o => o.MapFrom(s => s.ShiftState.ShiftDetails))
              .ForMember(d => d.JobType, o => o.MapFrom(s => s.JobType.JobName))
              .ForMember(d => d.PaymentType, o => o.MapFrom(s => s.PaymentType.Name))
              .ForMember(d => d.Agency, o => o.MapFrom(s => s.Agency.Name))
              .ForMember(d => d.Grade, o => o.MapFrom(s => s.Grade.GradeName))
              .ForMember(d => d.Aria, o => o.MapFrom(s => s.Aria.Borough))
              .ForMember(d => d.AppUser, o => o.MapFrom(s => s.AppUser.NickName));

            CreateMap<InvitedCandidate, InvitedCandidateReturnDto>()
              .ForMember(d => d.TimeDetail, o => o.MapFrom(s => s.TimeDetail.Hour))
              .ForMember(d => d.ClientLocation, o => o.MapFrom(s => s.ClientLocation.Address1 + ' ' + s.ClientLocation.Address2))
              .ForMember(d => d.AttributeDetail, o => o.MapFrom(s => s.AttributeDetail.AttributeName))
              .ForMember(d => d.ShiftState, o => o.MapFrom(s => s.ShiftState.ShiftDetails))
              .ForMember(d => d.JobType, o => o.MapFrom(s => s.JobType.JobName))
              .ForMember(d => d.PaymentType, o => o.MapFrom(s => s.PaymentType.Name))
              .ForMember(d => d.Agency, o => o.MapFrom(s => s.Agency.Name))
              .ForMember(d => d.Aria, o => o.MapFrom(s => s.Aria.Borough))
              .ForMember(d => d.Grade, o => o.MapFrom(s => s.Grade.GradeName))
              .ForMember(d => d.Candidate, o => o.MapFrom(s => s.Candidate.FirstName + ' '+ s.Candidate.LastName ))
              .ForMember(d => d.Candidate, o => o.MapFrom(s => s.Candidate.FirstName + ' ' + s.Candidate.LastName))
              .ForMember(d => d.AppUserCandidate, o => o.MapFrom(s => s.AppUserCandidate.NickName))
              .ForMember(d => d.AppUserPosted, o => o.MapFrom(s => s.AppUserPosted.NickName));

            CreateMap<InvitedCandidate, InvitedCandidateFromCandidateDto>()
             .ForMember(d => d.AvatarAgencyUrl, o => o.MapFrom(s => s.Agency.LogoUrl))
             .ForMember(d => d.AttributeDetail, o => o.MapFrom(s => s.AttributeDetail.AttributeName))
             .ForMember(d => d.ShiftState, o => o.MapFrom(s => s.ShiftState.ShiftDetails))
             .ForMember(d => d.JobType, o => o.MapFrom(s => s.JobType.JobName))
             .ForMember(d => d.PaymentType, o => o.MapFrom(s => s.PaymentType.Name))
             .ForMember(d => d.Agency, o => o.MapFrom(s => s.Agency.Name))
             .ForMember(d => d.Aria, o => o.MapFrom(s => s.Aria.Borough))
             .ForMember(d => d.Grade, o => o.MapFrom(s => s.Grade.GradeName))
             .ForMember(d => d.Candidate, o => o.MapFrom(s => s.Candidate.FirstName + ' ' + s.Candidate.LastName))
             .ForMember(d => d.Candidate, o => o.MapFrom(s => s.Candidate.FirstName + ' ' + s.Candidate.LastName));

            CreateMap<JobConfirmed, JobConfirmedToReturnDto>()
             .ForMember(d => d.TimeDetail, o => o.MapFrom(s => s.TimeDetail.Hour))
             .ForMember(d => d.ClientLocation, o => o.MapFrom(s => s.ClientLocation.Address1 + ' ' + s.ClientLocation.Address2))
             .ForMember(d => d.AttributeDetail, o => o.MapFrom(s => s.AttributeDetail.AttributeName))
             .ForMember(d => d.ShiftState, o => o.MapFrom(s => s.ShiftState.ShiftDetails))
             .ForMember(d => d.JobType, o => o.MapFrom(s => s.JobType.JobName))
             .ForMember(d => d.PaymentType, o => o.MapFrom(s => s.PaymentType.Name))
             .ForMember(d => d.Agency, o => o.MapFrom(s => s.Agency.Name))
             .ForMember(d => d.AgencyPhotoUrl, o => o.MapFrom(s => s.Agency.LogoUrl))
             .ForMember(d => d.Aria, o => o.MapFrom(s => s.Aria.Borough))
             .ForMember(d => d.Grade, o => o.MapFrom(s => s.Grade.GradeName))
             .ForMember(d => d.Candidate, o => o.MapFrom(s => s.Candidate.FirstName + ' ' + s.Candidate.LastName))
             .ForMember(d => d.Candidate, o => o.MapFrom(s => s.Candidate.FirstName + ' ' + s.Candidate.LastName))
             .ForMember(d => d.AppUserCandidate, o => o.MapFrom(s => s.AppUserCandidate.NickName))
             .ForMember(d => d.AppUserPosted, o => o.MapFrom(s => s.AppUserPosted.NickName));

            CreateMap<JobConfirmed, JobFinishToReturnDto>()
            .ForMember(d => d.AgencyPhotoUrl, o => o.MapFrom(s => s.Agency.LogoUrl))
            .ForMember(d => d.HourlyRate, o => o.MapFrom(s => s.Grade.HourlyRate));


            CreateMap<Candidate, CandidateDto>()
              .ForMember(d => d.Grade, o => o.MapFrom(s => s.Grade.GradeName)).ReverseMap();
            CreateMap<Candidate, CandidateForInvitationDto>()
              .ForMember(d => d.Grade, o => o.MapFrom(s => s.Grade.GradeName));
            
            CreateMap<TimeDetail, TimeDetailDto>().ReverseMap();
            CreateMap<ClientLocation, ClientLocationDto>().ReverseMap();
            CreateMap<AttributeDetail, AttributeDetailDto>().ReverseMap();
            CreateMap<ShiftState, ShiftStateDto>().ReverseMap();
            CreateMap<JobType, JobTypeDto>().ReverseMap();
            CreateMap<PaymentType, PaymentTypeDto>().ReverseMap();
            CreateMap<Agency, PaymentTypeDto>().ReverseMap();
            CreateMap<Grade, GradeDto>().ReverseMap();
            CreateMap<Aria, AriaDto>().ReverseMap();
            CreateMap<JobToRequestInsertDto, JobToRequest>();
            CreateMap<InvitedCandidateInserOrUpdateDto, InvitedCandidate>();
            CreateMap<JobConfirmedInserOrUpdateDto, JobConfirmed>();
            CreateMap<RetunAgencyLoop, RetunAgencyLoopDto>();
            CreateMap<AgencyListLoop, RetunAgencyLoopDto>().ReverseMap();

            CreateMap<CandidatePhoto, CandidatePhotoToReturnDto>().ReverseMap();
            CreateMap<CandidatePhotoForCreationDto, CandidatePhoto>();

            CreateMap<CandidateDocument, CandidateDocumentToReturnDto>().ReverseMap();
            CreateMap<CandidateDocumentForCreation, CandidateDocument>();

            CreateMap<AgencyPhoto, AgencyPhotoToReturnDto>().ReverseMap();
            CreateMap<AgencyPhotoForCreation, AgencyPhoto>();
            CreateMap<HRDashboard, HRDashboardDto>();

            CreateMap<AgencyDocument, AgencyDocumentToReturnDto>().ReverseMap();
            CreateMap<AgencyDocumentForCreationDto, AgencyDocument>();
            CreateMap<CandidateResponded, CandidateRespondedDto>();
            CreateMap<CandidateBooked, CandidateBookedDto>();
            CreateMap<ConfirmeFinalDto, ConfirmeFinal>();
            CreateMap<Agency, AgencyDto>().ReverseMap();
        }
    }
}

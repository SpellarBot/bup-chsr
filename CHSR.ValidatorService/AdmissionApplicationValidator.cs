using CHSR.Domain;
using FluentValidation;


namespace CHSR.ValidatorService
{
    public class AdmissionApplicationValidator : AbstractValidator<AdmissionApplication>
    {
        public AdmissionApplicationValidator()
        {
            RuleFor(admissionApplication => admissionApplication.CandidateNameEnglish).NotNull();
        }
    }
}

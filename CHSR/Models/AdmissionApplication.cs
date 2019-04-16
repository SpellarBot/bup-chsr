using System;
using System.ComponentModel.DataAnnotations;


namespace CHSR.Models
{
    public class AdmissionApplication : Entity
    {

        [Required]
        [Display(Name = "Program")]
        public string ProgramName { get; set; }
        public string CandidateNameBangla { get; set; }

        public string CandidateNameEnglish { get; set; }

        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string SpouseName { get; set; }
        public string PermanentAddress { get; set; }
        public string PresentAddress { get; set; }
        public string Nationality { get; set; }
        public string Religion { get; set; }
        public DateTime DOB { get; set; }
        public string NationalIdNo { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string EmergencyContactNo { get; set; }
        public EducationalInfo SSC { get; set; }
        public EducationalInfo HSC { get; set; }
        public EducationalInfo BachelorDegree { get; set; }
        public EducationalInfo MasterDegree { get; set; }
        public EducationalInfo MPhilDegree { get; set; }
        public EducationalInfo AnyOther { get; set; }
        public string TitleOfResearch { get; set; }
        public string NumberOfPublishedJournal { get; set; }
        public string PassportNumber { get; set; }
        public string PassportIssuingAuthority { get; set; }
        public string VisaType { get; set; }
        public string VisaDuration { get; set; }
        public string EmbassyContactName { get; set; }
        public string EmbassyContactDesignation { get; set; }
        public string EmbassyContactTelephoneNumber { get; set; }
        public string EmbassyContactMobileNumber { get; set; }
        public string EmbassyContactEmail { get; set; }
        public decimal PaymentAmountBDT { get; set; }
        public string PaymentTraceId { get; set; }
        public string PaymentNameOfBranch { get; set; }
        public string Signature { get; set; }
        public OrganizationExperience ResearchInformationOne { get; set; }
        public OrganizationExperience ResearchInformationTwo { get; set; }
        public OrganizationExperience ResearchInformationThree { get; set; }
        public OrganizationExperience ResearchInformationFour { get; set; }
        public OrganizationExperience ResearchInformationFive { get; set; }
    }
}

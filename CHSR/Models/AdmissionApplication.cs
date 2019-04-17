using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [Required]
        public string Email { get; set; }
        public string EmergencyContactNo { get; set; }
        public string TitleOfResearch { get; set; }
        public int NumberOfPublishedJournal { get; set; }
        public string PassportNumber { get; set; }
        public string PassportIssuingAuthority { get; set; }

        // need to discuss
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
        // Admin Input
        public bool IsPaid { get; set; }
        public bool IsShortListed { get; set; }


        // Educational Information
        public int SSC_PassingYear { get; set; }
        public string SSC_Group { get; set; }
        public string SSC_Institute { get; set; }
        public decimal SSC_GPA { get; set; }
        public decimal SSC_TotalNumber { get; set; }

        public int HSC_PassingYear { get; set; }
        public string HSC_Group { get; set; }
        public string HSC_Institute { get; set; }
        public decimal HSC_GPA { get; set; }
        public decimal HSC_TotalNumber { get; set; }

        public int BachelorDegree_PassingYear { get; set; }
        public string BachelorDegree_Group { get; set; }
        public string BachelorDegree_Institute { get; set; }
        public decimal BachelorDegree_GPA { get; set; }
        public decimal BachelorDegree_TotalNumber { get; set; }

        public int MasterDegree_PassingYear { get; set; }
        public string MasterDegree_Group { get; set; }
        public string MasterDegree_Institute { get; set; }
        public decimal MasterDegree_GPA { get; set; }
        public decimal MasterDegree_TotalNumber { get; set; }

        public int MPhilDegree_PassingYear { get; set; }
        public string MPhilDegree_Group { get; set; }
        public string MPhilDegree_Institute { get; set; }
        public decimal MPhilDegree_GPA { get; set; }
        public decimal MPhilDegree_TotalNumber { get; set; }

        public int AnyOther_PassingYear { get; set; }
        public string AnyOther_Group { get; set; }
        public string AnyOther_Institute { get; set; }
        public decimal AnyOther_GPA { get; set; }
        public decimal AnyOther_TotalNumber { get; set; }
        public string ResearchInformationOneName { get; set; }
        public DateTime ResearchInformationOneFrom { get; set; }
        public DateTime ResearchInformationOneTo { get; set; }
        public string ResearchInformationOneTotalDuration { get; set; }
        public string ResearchInformationOneDesignation { get; set; }
        public string ResearchInformationOneResponsibility { get; set; }

        public string ResearchInformationTwoName { get; set; }
        public DateTime ResearchInformationTwoFrom { get; set; }
        public DateTime ResearchInformationTwoTo { get; set; }
        public string ResearchInformationTwoTotalDuration { get; set; }
        public string ResearchInformationTwoDesignation { get; set; }
        public string ResearchInformationTwoResponsibility { get; set; }

        public string ResearchInformationThreeName { get; set; }
        public DateTime ResearchInformationThreeFrom { get; set; }
        public DateTime ResearchInformationThreeTo { get; set; }
        public string ResearchInformationThreeTotalDuration { get; set; }
        public string ResearchInformationThreeDesignation { get; set; }
        public string ResearchInformationThreeResponsibility { get; set; }

        public string ProfilePictureId { get; set; }

        [NotMapped]
        public IFormFile ProfilePicture { get; set; }

        public string TraceId { get; set; }
        public bool IsDraft { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechLineCaseAPI.Model
{
    public class ResultMessage
    {
        public string Status { get; set; }
        public string Message { get; set; }
    }
    public class Operation
    {
        public string OUT_COMMANDCODE { get; set; }
        public string OUT_COMMANDDESC { get; set; }
        public string OUT_SERVICE_TYPE { get; set; }
        public string OUT_OPTCODE { get; set; }
        public string OUT_OPT_DESC { get; set; }
        public string OUT_EXPENSE_TYPE { get; set; }
    }
    public class RocodeOperation
    {
        public string rocode { get; set; }
        public string dealercode { get; set; }
        public string offdealercode { get; set; }
        public string requestno { get; set; }
    }

    public class Authen
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class ResultModel
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public object Result { get; set; }
    }
    public class ROSubjectModel
    {
        public int Id { get; set; }
        public Guid? SubjectId { get; set; }
        public string Subject { get; set; }
        public string Name { get; set; }
        public string EnglishName { get; set; }

        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public string StatusCode { get; set; }
    }

    public class RODealerModel
    {
        public int Id { get; set; }

        public string Code { get; set; }
        public string Name { get; set; }
        public string EnglishName { get; set; }

        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public string StatusCode { get; set; }
    }
    public class ACodeModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Desc { get; set; }

        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public string StatusCode { get; set; }
    }

    public class BCodeModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Desc { get; set; }

        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public string StatusCode { get; set; }
    }

    public class CCodeModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Desc { get; set; }

        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public string StatusCode { get; set; }
    }

    public class vROMessageModel
    {
        public int? Id { get; set; }
        public int? CaseId { get; set; }
        public int? SenderId { get; set; }
        public string SenderName { get; set; }
        public string Text { get; set; }
        public int AttachFileId { get; set; }

        //public DateTime Time { get; set; }

        public HttpPostedFile File { get; set; }

        public string Category { get; set; }

        //public string ObjectId { get; set; } // CaseId

        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public string StatusCode { get; set; }
    }

    public class ROCaseModel
    {
        public int? Id { get; set; }
        public string CaseId { get; set; }
        public string Dealer { get; set; }
        public string Out_offcde { get; set; }
        public string Out_cmpcde { get; set; }
        public string Out_rocode { get; set; }
        public string Out_cust_date { get; set; }
        public string Out_ro_status { get; set; }
        public string Out_rodate { get; set; }
        public string Out_rotime { get; set; }
        public string Out_warranty_date { get; set; }
        public string Out_expiry_date { get; set; }
        public string Out_license { get; set; }
        public string Out_prdcde { get; set; }
        public string Out_chasno { get; set; }
        public string Out_engno { get; set; }
        public string Out_model { get; set; }
        public string Out_kilo_last { get; set; }
        public string Out_last_date { get; set; }
        public string Out_idno { get; set; }
        public string Out_cusname { get; set; }
        public string Out_mobile { get; set; }
        public string Out_address { get; set; }
        public string Out_province { get; set; }
        public string Out_zipcode { get; set; }
        public string Out_custype { get; set; }
        public string A_code { get; set; }
        public string B_code { get; set; }
        public string C_code { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string StatusCode { get; set; }
        public string LevelofProblem { get; set; }
        public string CaseTitle { get; set; }
        public string CaseType { get; set; }
        public string CaseSubject { get; set; }
        public string CaseDescription { get; set; }
        public string OUT_SYS_CODE { get; set; }
        public string OUT_SYS_STS { get; set; }
        public string OUT_SYS_MSG { get; set; }
        public string StatusCodeText { get; set; }
        
        public IList<Operation> operation { get; set; }
    }

    public class Rocode
    {
        public string rocode { get; set; }
    }

    public class ROCaseLogModel
    {
        public int? Id { get; set; }
        public string CaseId { get; set; }
        public string StatusCodeFrom { get; set; }
        public string StatusCodeTo { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }

    public class UserMitsu
    {
        public string id { get; set; }
        public string user_mail { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string profile_photo_url { get; set; }
        public string token { get; set; }
        public string id_token { get; set; }
        public string refresh_token { get; set; }
        public string dealer { get; set; }
    }

    public class Incident
    {
        public string id { get; set; }
        public string CaseId { get; set; }
        public string Dealer { get; set; }
        public string Out_offcde { get; set; }
        public string Out_cmpcde { get; set; }
        public string Out_rocode { get; set; }
        public string Out_cust_date { get; set; }
        public string Out_ro_status { get; set; }
        public string Out_rodate { get; set; }
        public string Out_rotime { get; set; }
        public string Out_warranty_date { get; set; }
        public string Out_expiry_date { get; set; }
        public string Out_license { get; set; }
        public string Out_prdcde { get; set; }
        public string Out_chasno { get; set; }
        public string Out_engno { get; set; }
        public string Out_model { get; set; }
        public string Out_kilo_last { get; set; }
        public string Out_last_date { get; set; }
        public string Out_idno { get; set; }
        public string Out_cusname { get; set; }
        public string Out_mobile { get; set; }
        public string Out_address { get; set; }
        public string Out_province { get; set; }
        public string Out_zipcode { get; set; }
        public string Out_custype { get; set; }
        public string A_code { get; set; }
        public string B_code { get; set; }
        public string C_code { get; set; }
        public string CreatedBy { get; set; }
        public string LevelofProblem { get; set; }
        public string CaseTitle { get; set; }
        public string CaseType { get; set; }
        public string CaseSubject { get; set; }
        public string CaseDescription { get; set; }
        public string createdBy { get; set; }
        public string createdOn { get; set; }
        public string modifiedOn { get; set; }
        public string modifiedBy { get; set; }
        public string statusCode { get; set; }
        public string statusCodeText { get; set; }
    }
    public class RatingModel
    {
        public int? Id { get; set; }
        public string Category { get; set; }
        public string Comment { get; set; }
        public int? ROCaseId { get; set; }
        public string CASEID { get; set; }

        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string StatusCode { get; set; }
    }

    public class RatingSubjectModel
    {
        public int? Id { get; set; }
        public string Subject { get; set; }
        public decimal? Score { get; set; }
        public decimal? MaxScore { get; set; }
        public int? RatingId { get; set; }

        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string StatusCode { get; set; }
    }

    public class RatingMasterModel
    {
        public int? Id { get; set; }
        public string Subject { get; set; }
        public decimal? MaxScore { get; set; }

        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string StatusCode { get; set; }
    }
}
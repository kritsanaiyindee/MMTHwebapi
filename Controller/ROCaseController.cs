using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;
using System.Xml;
using TechLineCaseAPI.MMThApi;
using TechLineCaseAPI.Model;
using static TechLineCaseAPI.MMThApi.WS_InterfaceCRMSoapClient;


namespace TechLineCaseAPI.Controller
{
    public class ROCaseController : ApiController
    {
        public IOrganizationService _service = null;

        public void ConnectToMSCRM()
        {
            try
            {
                string UserName = TechLineCaseAPI.Properties.Settings.Default.UserName;
                string Password = TechLineCaseAPI.Properties.Settings.Default.Password;
                string SoapOrgServiceUri = TechLineCaseAPI.Properties.Settings.Default.SoapOrgServiceUri;//   "https://thcrmweb01/mmthqas/XRMServices/2011/Organization.svc";

                ClientCredentials credentials = new ClientCredentials();
                credentials.UserName.UserName = UserName;
                credentials.UserName.Password = Password;
                Uri serviceUri = new Uri(SoapOrgServiceUri);
                OrganizationServiceProxy proxy = new OrganizationServiceProxy(serviceUri, null, credentials, null);
                proxy.EnableProxyTypes();
                _service = (IOrganizationService)proxy;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while connecting to CRM " + ex.Message);
                Console.ReadKey();
            }
        }

        [HttpGet]
        [Route("api/rocaselog/all/{caseid}")]
        public IHttpActionResult GetLogAll(string caseid)
        {
            using (mmthapiEntities entity = new mmthapiEntities())
            {
                List<ROCaseLogModel> models = new List<ROCaseLogModel>();
                var list = entity.ro_case_log.Where(o => o.CASEID.Contains(caseid)).ToList();

                foreach (var item in list)
                {
                    ROCaseLogModel model = new ROCaseLogModel()
                    {
                        Id = item.id,
                        CaseId = item.CASEID,
                        StatusCodeFrom = item.STATUS_CODE_FROM,
                        StatusCodeTo = item.STATUS_CODE_TO,
                        CreatedBy = item.CREATED_BY,
                        CreatedOn = item.CREATED_ON,
                        ModifiedBy = item.MODIFIED_BY,
                        ModifiedOn = item.MODIFIED_ON,
                    };

                    models.Add(model);
                }

                return Json(models);
            }
        }

        [HttpGet]
        [Route("api/rocaselog/status/pending")]
        public IHttpActionResult GetListCaseByStatus()
        {
            //var PendingStatus = new string[] { "0", "1", "2","3","4" };
            using (mmthapiEntities entity = new mmthapiEntities())
            {
                List<ROCaseLogModel> models = new List<ROCaseLogModel>();
                var list = entity.vRoCases.Where(o => o.STATUS_CODE == "0" || o.STATUS_CODE == "1"|| o.STATUS_CODE == "2" || o.STATUS_CODE == "3"|| o.STATUS_CODE == "4" ).ToList();



                return Json(list);
            }
        }
        [HttpGet]
        [Route("api/rocaselog/status/completed")]
        public IHttpActionResult GetListCaseByStatusCompleted()
        {
            //var PendingStatus = new string[] { "6", "5" };
            using (mmthapiEntities entity = new mmthapiEntities())
            {
                
                var list = entity.vRoCases.Where(o => o.STATUS_CODE=="5" || o.STATUS_CODE == "6").ToList();
                return Json(list);
            }
        }
        // GET api/blog/5
        [HttpPost]
        [Route("api/ROCase/rocheck")]
        public ResultModel GetRONumber([FromBody] Rocode  rcode)
        {


            //WS_InterfaceCRMSoapClient soapClient = new WS_InterfaceCRMSoapClient(TechLineCaseAPI.Properties.Settings.Default.RO_WS);
            //WS_InterfaceCRMSoapClient ws = new WS_InterfaceCRMSoapClient();
            WS_InterfaceCRMSoapClient soapClient = new WS_InterfaceCRMSoapClient("WS_InterfaceCRMSoap", "http://pre-mmth.dms-ccp.com/WS_InterfaceCRM/WS_InterfaceCRM.asmx");

            using (new OperationContextScope(soapClient.InnerChannel))
            {
                //Create message header containing the credentials
                //var header = MessageHeader.CreateHeader("SC_Credentials",
                // "http://soapservice.com", credentials, new CFMessagingSerializer(typeof(SC_Credentials)));
                //Add the credentials message header to the outgoing request
                // OperationContext.Current.OutgoingMessageHeaders.Add(header);

                try
                {
                    WSCheckHistoryRORequest wr = new WSCheckHistoryRORequest();
                    wr.CMPCDE = "110059";
                    wr.OFFCDE = "110059";
                    wr.RONO = rcode.rocode;
                    wr.REQUEST_NO = "123456789012345678901234567890";
                    WSCheckHistoryROResponse  resp = soapClient.WSCheckHistoryRO(wr);
                    // var result = Task.Run(async () => soapClient.WSCheckHistoryROAsync(wr)).GetAwaiter().GetResult();

                    DataTable cv = resp.WSCheckHistoryROResult;
                    



                         IList <ROCaseModel> items = cv.AsEnumerable().Select(row =>
                    new ROCaseModel
                    {
                        Out_offcde = row.Field<string>("OUT_CMPCDE"),
                        Out_cmpcde = row.Field<string>("OUT_OFFCDE"),
                        Out_rocode = row.Field<string>("OUT_ROCODE"),
                        Out_cust_date = row.Field<string>("OUT_CUST_DATE"),
                         Out_ro_status = row.Field<string>("OUT_RO_STATUS"),
                         Out_rodate = row.Field<string>("OUT_RODATE"),
                         Out_rotime = row.Field<string>("OUT_ROTIME"),
                         Out_warranty_date = row.Field<string>("OUT_WARRANTY_DATE"),
                         Out_expiry_date = row.Field<string>("OUT_EXPIRY_DATE"),
                         Out_license = row.Field<string>("OUT_LICENSE"),
                         Out_prdcde = row.Field<string>("OUT_PRDCDE"),
                         Out_chasno = row.Field<string>("OUT_CHASNO"),
                         Out_engno = row.Field<string>("OUT_ENGNO"),
                         Out_model = row.Field<string>("OUT_MODEL"),
                         Out_kilo_last = row.Field<string>("OUT_KILO_LAST"),
                         Out_last_date = row.Field<string>("OUT_LAST_DATE"),
                         Out_idno = row.Field<string>("OUT_IDNO"),
                         Out_cusname = row.Field<string>("OUT_CUSNAME"),
                         Out_mobile = row.Field<string>("OUT_MOBILE"),
                         Out_address = row.Field<string>("OUT_ADDRESS"),
                         Out_province = row.Field<string>("OUT_PROVINCE"),
                         Out_zipcode = row.Field<string>("OUT_ZIPCODE"),
                         Out_custype = row.Field<string>("OUT_CUSTYPE"),

                        OUT_SYS_CODE = row.Field<string>("OUT_SYS_CODE"),
                        OUT_SYS_STS = row.Field<string>("OUT_SYS_STS"),
                        OUT_SYS_MSG = row.Field<string>("OUT_SYS_MSG"),

                         }).ToList();
                           if(items[0].OUT_SYS_CODE== "E001")
                    {
                        return new ResultModel()
                        {
                            Status = "S",
                            Message = items[0].OUT_SYS_MSG,
                            Result = items,
                        };
                    }
                    else
                    {
                        return new ResultModel()
                        {
                            Status = "E",
                            Message = items[0].OUT_SYS_MSG,
                            Result = items,
                        };
                    }

                }
                catch (Exception ex)
                {
                    return new ResultModel()
                    {
                        Status = "S",
                        Message = "Create Completed",
                        
                    };
                    //throw;
                }
            }




        }


        private bool IsExistingRO(string rcode)
        {
            bool bExist = false;
            try
            {
                string conString = TechLineCaseAPI.Properties.Settings.Default.ConString;
                using (SqlConnection connection = new SqlConnection(conString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(
                        "SELECT TOP 1 * FROM ro_case  where out_rocode='" + rcode + "' ",
                        connection))
                    {
                        
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            bExist= reader.HasRows;                            
                        }
                       
                    }
                    connection.Close();
                    return bExist;


                }
            }
            catch (Exception e)
            {
                return false;
            }


        }


        [HttpPost]
        [Route("api/ROCase/techlinevalid")]
        public ResultModel techlinevalid([FromBody] Rocode rcode)
        {



                try
                {
                  
                    if (IsExistingRO(rcode.rocode))
                    {

                    return new ResultModel()
                    {
                        Status = "E",
                        Message = "Duplicate RO",

                    };
                }
                    else
                    {
                    return new ResultModel()
                    {
                        Status = "S",
                        Message = "Can Create",

                    };
                }

                }
                catch (Exception ex)
                {
                    return new ResultModel()
                    {
                        Status = "S",
                        Message = "Create Completed",

                    };
                    //throw;
                }
            




        }

        [HttpPost]
        [Route("api/rocase/create")]
        public ResultModel Post([FromBody] ROCaseModel model)
        {
            try
            {
                var js = new JavaScriptSerializer();
                //var json = HttpContext.Current.Request.Form["da"];

               // ROCaseModel model = js.Deserialize<ROCaseModel>(json);

                if (model.CaseId == null) new ResultMessage() { Status = "E", Message = "Require Case Id" };
                if (model.Dealer == null) new ResultMessage() { Status = "E", Message = "Require Dealer" };
                if (model.CreatedBy == null) new ResultMessage() { Status = "E", Message = "Require Created By" };

                int? rocaseid = CreateROCase(model);

                if (rocaseid != null)
                {
                    if (TechLineCaseAPI.Properties.Settings.Default.DEV)
                    {
                        var crmCase = CreateCRMCase(model);
                        return new ResultModel()
                        {
                            Status = "S",

                            Message = "Create Completed",
                            Result = "DEV0000"+ rocaseid,
                        };
                    }
                    else
                    {
                        var crmCase = CreateCRMCase(model);
                        return new ResultModel()
                        {
                            Status = "S",

                            Message = "Create Completed",
                            Result = crmCase.Result.ToString(),
                        };
                    }
                    
                    
                }
                else
                {
                    return new ResultModel()
                    {
                        Status = "E",
                        Message = "Create Incompleted",
                        Result = 0,
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResultModel()
                {
                    Status = "E",
                    Message = "Create Incompleted (" + ex.Message + ")",
                    Result = 0,
                };
            }
        }

        [HttpPost]
        [Route("api/rocase/update")]
        public ResultMessage Update()
        {
            try
            {
                var js = new JavaScriptSerializer();
                var json = HttpContext.Current.Request.Form["Model"];

                ROCaseModel model = js.Deserialize<ROCaseModel>(json);

                if (model.Id == null) new ResultMessage() { Status = "E", Message = "Require RO Case Id" };
                if (model.CaseId == null) new ResultMessage() { Status = "E", Message = "Require Case Id" };
                if (model.Dealer == null) new ResultMessage() { Status = "E", Message = "Require Dealer" };
                if (model.ModifiedBy == null) new ResultMessage() { Status = "E", Message = "Require Modified By" };

                if (UpdateROCase(model))
                {
                    return new ResultMessage()
                    {
                        Status = "S",
                        Message = "Update Completed"
                    };
                }
                else
                {
                    return new ResultMessage()
                    {
                        Status = "E",
                        Message = "Update Incompleted"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResultMessage()
                {
                    Status = "E",
                    Message = "Update Incompleted (" + ex.Message + ")"
                };
            }
        }

        [HttpPost]
        [Route("api/rocase/delete")]
        public ResultMessage Delete()
        {
            var id = HttpContext.Current.Request.Params["id"];

            if (id == null) return new ResultMessage() { Status = "E", Message = "Require Id" };

            if (DeleteROCase(id))
            {
                return new ResultMessage()
                {
                    Status = "S",
                    Message = "Delete Completed"
                };
            }
            else
            {
                return new ResultMessage()
                {
                    Status = "E",
                    Message = "Delete Incompleted"
                };
            }
        }

        public static int? CreateROCase(ROCaseModel model)
        {
            try
            {
                int? rocaseid;

              //  if (model.CaseId == null) return null;
                if (model.Dealer == null) return null;
                if (model.CreatedBy == null) return null;

                using (mmthapiEntities entity = new mmthapiEntities())
                {
                    var record = new ro_case()
                    {
                        CASEID = model.CaseId,
                        DEALER = model.Dealer,
                        OUT_OFFCDE = model.Out_offcde,
                        OUT_CMPCDE = model.Out_cmpcde,
                        OUT_ROCODE = model.Out_rocode,
                        OUT_CUST_DATE = model.Out_cust_date,
                        OUT_RO_STATUS = model.Out_ro_status,
                        OUT_RODATE = model.Out_rodate,
                        OUT_ROTIME = model.Out_rotime,
                        OUT_WARRANTY_DATE = model.Out_warranty_date,
                        OUT_EXPIRY_DATE = model.Out_expiry_date,
                        OUT_LICENSE = model.Out_license,
                        OUT_PRDCDE = model.Out_prdcde,
                        OUT_CHASNO = model.Out_chasno,
                        OUT_ENGNO = model.Out_engno,
                        OUT_MODEL = model.Out_model,
                        OUT_KILO_LAST = model.Out_kilo_last,
                        OUT_LAST_DATE = model.Out_last_date,
                        OUT_IDNO = model.Out_idno,
                        OUT_CUSNAME = model.Out_cusname,
                        OUT_MOBILE = model.Out_mobile,
                        OUT_ADDRESS = model.Out_address,
                        OUT_PROVINCE = model.Out_province,
                        OUT_ZIPCODE = model.Out_zipcode,
                        OUT_CUSTYPE = model.Out_custype,
                        A_CODE = model.A_code,
                        B_CODE = model.B_code,
                        C_CODE = model.C_code,

                        CREATED_BY = model.CreatedBy,
                        CREATED_ON = DateTime.Now,
                        MODIFIED_BY = model.CreatedBy,
                        MODIFIED_ON = DateTime.Now,
                        STATUS_CODE = "1",

                        LevelofProblem = model.LevelofProblem,
                        CaseTitle = model.CaseTitle,
                        CaseType = model.CaseType,
                        CaseSubject = model.CaseSubject,
                        CaseDescription = model.CaseDescription,
                    };

                    entity.ro_case.AddObject(record);
                    entity.SaveChanges();
                    entity.Refresh(RefreshMode.StoreWins, record);

                    rocaseid = record.id;
                }

                return rocaseid;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static bool UpdateROCase(ROCaseModel model)
        {
            try
            {
                if (model.Id == null) return false;
                if (model.CaseId == null) return false;
                if (model.Dealer == null) return false;
                if (model.ModifiedBy == null) return false;

                using (mmthapiEntities entity = new mmthapiEntities())
                {
                    var record = entity.ro_case.Where(o => o.id == model.Id).FirstOrDefault();

                    record.CASEID = AssignStringData(record.CASEID, model.CaseId);
                    record.DEALER = AssignStringData(record.DEALER, model.Dealer);
                    record.OUT_OFFCDE = AssignStringData(record.OUT_OFFCDE, model.Out_offcde);
                    record.OUT_CMPCDE = AssignStringData(record.OUT_CMPCDE, model.Out_cmpcde);
                    record.OUT_ROCODE = AssignStringData(record.OUT_ROCODE, model.Out_rocode);
                    record.OUT_CUST_DATE = AssignStringData(record.OUT_CUST_DATE, model.Out_cust_date);
                    record.OUT_RO_STATUS = AssignStringData(record.OUT_RO_STATUS, model.Out_ro_status);
                    record.OUT_RODATE = AssignStringData(record.OUT_RODATE, model.Out_rodate);
                    record.OUT_ROTIME = AssignStringData(record.OUT_ROTIME, model.Out_rotime);
                    record.OUT_WARRANTY_DATE = AssignStringData(record.OUT_WARRANTY_DATE, model.Out_warranty_date);
                    record.OUT_EXPIRY_DATE = AssignStringData(record.OUT_EXPIRY_DATE, model.Out_expiry_date);
                    record.OUT_LICENSE = AssignStringData(record.OUT_LICENSE, model.Out_license);
                    record.OUT_PRDCDE = AssignStringData(record.OUT_PRDCDE, model.Out_prdcde);
                    record.OUT_CHASNO = AssignStringData(record.OUT_CHASNO, model.Out_chasno);
                    record.OUT_ENGNO = AssignStringData(record.OUT_ENGNO, model.Out_engno);
                    record.OUT_MODEL = AssignStringData(record.OUT_MODEL, model.Out_model);
                    record.OUT_KILO_LAST = AssignStringData(record.OUT_KILO_LAST, model.Out_kilo_last);
                    record.OUT_LAST_DATE = AssignStringData(record.OUT_LAST_DATE, model.Out_last_date);
                    record.OUT_IDNO = AssignStringData(record.OUT_IDNO, model.Out_idno);
                    record.OUT_CUSNAME = AssignStringData(record.OUT_CUSNAME, model.Out_cusname);
                    record.OUT_MOBILE = AssignStringData(record.OUT_MOBILE, model.Out_mobile);
                    record.OUT_ADDRESS = AssignStringData(record.OUT_ADDRESS, model.Out_address);
                    record.OUT_PROVINCE = AssignStringData(record.OUT_PROVINCE, model.Out_province);
                    record.OUT_ZIPCODE = AssignStringData(record.OUT_ZIPCODE, model.Out_zipcode);
                    record.OUT_CUSTYPE = AssignStringData(record.OUT_CUSTYPE, model.Out_custype);
                    record.A_CODE = AssignStringData(record.A_CODE, model.A_code);
                    record.B_CODE = AssignStringData(record.B_CODE, model.B_code);
                    record.C_CODE = AssignStringData(record.C_CODE, model.C_code);

                    string sourceStatus = record.STATUS_CODE;
                    //CREATED_BY = model.CreatedBy,
                    //CREATED_ON = DateTime.Now,
                    record.MODIFIED_BY = model.ModifiedBy;
                    record.MODIFIED_ON = DateTime.Now;
                    record.STATUS_CODE = AssignStringData(sourceStatus, model.StatusCode);

                    record.LevelofProblem = AssignStringData(record.LevelofProblem, model.LevelofProblem);
                    record.CaseTitle = AssignStringData(record.CaseTitle, model.CaseTitle);
                    record.CaseType = AssignStringData(record.CaseType, model.CaseType);
                    record.CaseSubject = AssignStringData(record.CaseSubject, model.CaseSubject);
                    record.CaseDescription = AssignStringData(record.CaseDescription, model.CaseDescription);

                    //entity.ro_case.Attach(record);
                    //entity.ObjectStateManager.ChangeObjectState(record, System.Data.EntityState.Modified);
                    entity.SaveChanges();
                    entity.Refresh(RefreshMode.StoreWins, record);

                    if (sourceStatus != model.StatusCode)
                    {
                        CreateROCaseLog(record.CASEID, sourceStatus, model.StatusCode, model.ModifiedBy);
                    }
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool DeleteROCase(string id)
        {
            try
            {
                if (id == null) return false;

                using (mmthapiEntities entity = new mmthapiEntities())
                {
                    int rocaseid = int.Parse(id);
                    var record = entity.ro_case.Where(o => o.id == rocaseid).FirstOrDefault();

                    if (record != null)
                    {
                        entity.ro_case.Attach(record);
                        entity.ObjectStateManager.ChangeObjectState(record, System.Data.EntityState.Deleted);
                        entity.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }



        private Entity getAccount(string accountCode)

        {
            try
            {
                if (_service == null)
                {
                    ConnectToMSCRM();
                }

                var columns = new ColumnSet();
                columns.AllColumns = true;

                FilterExpression filter = new FilterExpression(LogicalOperator.Or);

                FilterExpression filter1 = new FilterExpression
                {
                    FilterOperator = LogicalOperator.And,
                    Conditions =
                    {
                        new ConditionExpression("hms_code", ConditionOperator.Equal, accountCode),
                        new ConditionExpression("hms_code", ConditionOperator.NotNull)
                    },
                };



                filter.AddFilter(filter1);
                //filter.AddFilter(filter2);
                //filter.AddFilter(filter3);

                var query = new QueryExpression("account");
                query.ColumnSet = columns;
                query.Criteria = filter;
                query.TopCount = 1;

                EntityCollection result = _service.RetrieveMultiple(query);

                return (result.Entities.Count > 0) ? result.Entities[0] : null;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        private Entity getCase(Guid caseid)

        {
            try
            {
                if (_service == null)
                {
                    ConnectToMSCRM();
                }

                var columns = new ColumnSet();
                columns.AllColumns = true;

                FilterExpression filter = new FilterExpression(LogicalOperator.Or);

                FilterExpression filter1 = new FilterExpression
                {
                    FilterOperator = LogicalOperator.And,
                    Conditions =
                    {
                        new ConditionExpression("incidentid", ConditionOperator.Equal, caseid),

                    },
                };



                filter.AddFilter(filter1);
                //filter.AddFilter(filter2);
                //filter.AddFilter(filter3);

                var query = new QueryExpression("incident");
                query.ColumnSet = columns;
                query.Criteria = filter;
                query.TopCount = 1;

                EntityCollection result = _service.RetrieveMultiple(query);

                return (result.Entities.Count > 0) ? result.Entities[0] : null;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        private ResultModel CreateCRMCase(ROCaseModel inc)
        {
            try
            {
                if (_service == null)
                {
                    ConnectToMSCRM();
                }

                Entity entity = new Entity("incident");
                //var model = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, object>>>(myentity);
                var account = getAccount(inc.Out_offcde);
                if (account != null)
                {
                    entity["customerid".ToLower()] = new EntityReference("account", account.Id);
                    entity["hms_outOffcde".ToLower()] = inc.Out_offcde;
                    entity["hms_outCmpcde".ToLower()] = inc.Out_cmpcde;
                    entity["hms_outRocode".ToLower()] = inc.Out_rocode;
                    entity["hms_outCustDate".ToLower()] = inc.Out_cust_date;
                    entity["hms_outRoStatus".ToLower()] = inc.Out_ro_status;
                    entity["hms_outRodate".ToLower()] = inc.Out_rodate;
                    entity["hms_outRotime".ToLower()] = inc.Out_rotime;
                    entity["hms_outWarrantyDate".ToLower()] = inc.Out_warranty_date;
                    entity["hms_outExpiryDate".ToLower()] = inc.Out_expiry_date;
                    entity["hms_outLicense".ToLower()] = inc.Out_license;
                    entity["hms_outChasno".ToLower()] = inc.Out_chasno;
                    entity["hms_outEngno".ToLower()] = inc.Out_engno;
                    entity["hms_outModel".ToLower()] = inc.Out_model;

                    entity["hms_outKiloLast".ToLower()] = inc.Out_kilo_last;
                    entity["hms_outIdno".ToLower()] = inc.Out_idno;
                    entity["hms_outCusname".ToLower()] = inc.Out_cusname;
                    entity["hms_outMobile".ToLower()] = inc.Out_mobile;
                    entity["hms_outAddress".ToLower()] = inc.Out_address;
                    entity["hms_outProvince".ToLower()] = inc.Out_province;
                    entity["hms_outZipcode".ToLower()] = inc.Out_zipcode;
                    entity["hms_outCustype".ToLower()] = inc.Out_custype;
                    entity["hms_aCode".ToLower()] = inc.A_code;
                    entity["hms_bCode".ToLower()] = inc.B_code;
                    entity["hms_cCode".ToLower()] = inc.C_code;
                    entity["hms_levelofProblem".ToLower()] = inc.LevelofProblem;
                    entity["hms_caseTypetext".ToLower()] = inc.CaseType;
                    //entity["hms_caseSubject".ToLower()] = inc.CaseSubject;
                    // entity["hms_statusCodeText".ToLower()] = inc.statusCodeText;
                    entity["hms_caseDescription".ToLower()] = inc.CaseDescription;
                    
                    //entity["hms_statusCode".ToLower()] = inc.StatusCode;
                    //    entity["hms_statusCodeText".ToLower()] = inc.statusCodeText;
                    entity["hms_levelofProblem".ToLower()] = inc.LevelofProblem;
                    
                    //entity["hms_caseType".ToLower()] = inc.CaseType;
                    entity["hms_caseSubject".ToLower()] = inc.CaseSubject;
                    // entity["hms_statusCodeText".ToLower()] = inc.statusCodeText;
                    //entity["hms_caseDescription".ToLower()] = inc.CaseDescription;

                    entity["title".ToLower()] = inc.CaseTitle;

                    entity["hms_background".ToLower()] = inc.CaseDescription;
                    int value = int.Parse(inc.LevelofProblem == null ? "177980000" : inc.LevelofProblem);
                    entity["hms_lop".ToLower()] = new OptionSetValue(value);
                    
                    //   inc.hms_levelofProblem   Options  ;



                    //entity["createdby"] = "SYSTEM";
                    // entity["createdon"] = DateTime.Now;
                    //  entity["modifiedby"] = "SYSTEM";
                    //   entity["modifiedon"] = DateTime.Now;

                    Guid id = _service.Create(entity);

                    var getcase = getCase(id);
                    if (UpdateROCase(getcase["ticketnumber"].ToString(), inc.Out_rocode))
                    {
                        return new ResultModel() { Status = "S", Message = "Create Complete", Result = getcase["ticketnumber"] };
                    }
                    else
                    {
                        return new ResultModel() { Status = "E", Message = "ไม่มี Dealer code" };
                    }

                    return new ResultModel() { Status = "S", Message = "Create Complete", Result = getcase["ticketnumber"] };
                }
                else
                {
                    return new ResultModel() { Status = "E", Message = "ไม่มี Dealer code" };
                }

            }
            catch (Exception ex)
            {
                return new ResultModel() { Status = "E", Message = ex.Message };
            }
        }
        private bool UpdateROCase(string caseID, string ROCode)
        {
            try
            {
                string conString = TechLineCaseAPI.Properties.Settings.Default.ConString;
                using (SqlConnection connection = new SqlConnection(conString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(
                        "UPDATE ro_case  set caseid='" + caseID + "'  where out_rocode='" + ROCode + "'",
                        connection))
                    {

                        var retId = command.ExecuteNonQuery();
                        return true;

                    }
                }
            }
            catch (Exception e)
            {
                return false;
            }


        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////

        private static int? CreateROCaseLog(string caseId, string source, string data, string createdBy)
        {
            try
            {
                int? rologid;

                if (caseId == null) return null;
                if (source == null) return null;
                if (data == null) return null;
                if (createdBy == null) return null;

                using (mmthapiEntities entity = new mmthapiEntities())
                {
                    var record = new ro_case_log()
                    {
                        CASEID = caseId,
                        STATUS_CODE_FROM = source,
                        STATUS_CODE_TO = data,

                        CREATED_BY = createdBy,
                        CREATED_ON = DateTime.Now,
                        MODIFIED_BY = createdBy,
                        MODIFIED_ON = DateTime.Now,
                    };

                    entity.ro_case_log.AddObject(record);
                    entity.SaveChanges();
                    entity.Refresh(RefreshMode.StoreWins, record);

                    rologid = record.id;
                }

                return rologid;
            }
            catch (Exception)
            {
                return null;
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////
        
        private static string AssignStringData(string source, string data)
        {
            switch (data)
            {
                case "": return null;
                case null: return source;
                default: return data;
            }
        }
    }
}

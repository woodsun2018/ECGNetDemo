using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShareCode
{

    public enum StudyListType { ForDiagnose, All };

    //检查记录档案客户端
    public class StudyWebClient
    {
        private string ServerUri = "http://localhost:5000/";

        private static StudyWebClient _instance = new StudyWebClient();
        public static StudyWebClient Instance { get { return _instance; } }

        private StudyWebClient()
        {
        }

        //初始化检查记录档案客户端
        public void Init(string serverUrl)
        {
            ServerUri = serverUrl;
        }

        //查询记录
        public async Task<List<Study>> GetStudyListByType(StudyListType studyType, string doctorName)
        {
            string requestStr = "api/Studies?";
            if (studyType == StudyListType.ForDiagnose)
            {
                //待诊断记录
                requestStr += $"$filter=Diagnose eq ''";
            }
            else
            {
                //我诊断的记录
                if (!string.IsNullOrWhiteSpace(doctorName))
                    requestStr += $"$filter=DoctorName eq '{doctorName}'";
            }
            requestStr += "&$orderby=ID asc";

            //执行查询记录
            var studyList = await ExecGetStudyList(requestStr);
            return studyList;
        }

        //查询记录
        public async Task<List<Study>> GetStudyListByPatientName(string patientName)
        {
            string requestStr = "api/Studies?";
            requestStr += $"$filter=PatientName eq '{patientName}'";
            requestStr += "&$orderby=ID asc";

            //执行查询记录
            var studyList = await ExecGetStudyList(requestStr);
            return studyList;
        }

        //执行查询记录
        public async Task<List<Study>> ExecGetStudyList(string requestStr)
        {
            var restClient = new RestClient(ServerUri);
            restClient.Timeout = 10000;//设置10秒超时
            var restRequest = new RestRequest(requestStr, Method.GET);

            //var restResponse = await restClient.ExecuteTaskAsync<List<Study>>(restRequest);
            var restResponse = await restClient.ExecuteGetTaskAsync(restRequest);

            if (restResponse.ErrorException != null)
                throw new ApplicationException(restResponse.ErrorMessage, restResponse.ErrorException);

            var studyList = JsonConvert.DeserializeObject<List<Study>>(restResponse.Content);
            return studyList;
        }

        //添加记录
        public async Task<bool> AddStudy(Study study)
        {
            string requestStr = "api/Studies";

            var restClient = new RestClient(ServerUri);
            restClient.Timeout = 10000;//设置10秒超时
            var restRequest = new RestRequest(requestStr, Method.POST);

            var json = restRequest.JsonSerializer.Serialize(study);
            restRequest.AddParameter("application/json; charset=utf-8", json, ParameterType.RequestBody);

            var restResponse = await restClient.ExecuteTaskAsync(restRequest);

            if (restResponse.ErrorException != null)
                throw new ApplicationException(restResponse.ErrorMessage, restResponse.ErrorException);

            return true;
        }

        //修改记录
        public async Task<bool> Modify(Study study)
        {
            string requestStr = $"api/Studies/{study.ID}";

            var restClient = new RestClient(ServerUri);
            restClient.Timeout = 10000;//设置10秒超时
            var restRequest = new RestRequest(requestStr, Method.PUT);

            var json = restRequest.JsonSerializer.Serialize(study);
            restRequest.AddParameter("application/json; charset=utf-8", json, ParameterType.RequestBody);

            var restResponse = await restClient.ExecuteTaskAsync(restRequest);

            if (restResponse.ErrorException != null)
                throw new ApplicationException(restResponse.ErrorMessage, restResponse.ErrorException);

            return true;
        }

    }
}

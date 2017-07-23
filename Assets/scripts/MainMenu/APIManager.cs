using System.IO;
using System.Net;

public class APIManager
{
    private static string baseUrl = "http://ec2-54-76-133-2.eu-west-1.compute.amazonaws.com:5050";
    //private static string baseUrl = "http://localhost:5050";

    public static APIResponseModel MakeRequest(string endpoint, string method = "POST", string json = "{}")
    {
        string result = "{}";
        APIResponseModel response = new APIResponseModel();

        var httpWebRequest = (HttpWebRequest)WebRequest.Create(APIManager.baseUrl + endpoint);
        if(method == "POST")
        {
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = method;
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }
        }

        httpWebRequest.GetRequestStream();

        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        {
            result = streamReader.ReadToEnd();
            response = APIResponseModel.CreateFromJSON(result);
        }

        return response;
    }
}
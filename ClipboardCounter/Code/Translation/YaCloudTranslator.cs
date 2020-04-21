using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace ClipboardCounter
{
    public class YaCloudTranslator : ITranslator
    {
        private class TranslationRequest
        {
            public string sourceLanguageCode;
            public string targetLanguageCode;
            // "format": "string",
            public string[] texts = new string[1];
            // "folderId": "string",
            // "model": "string",
            public GlossaryConfig glossaryConfig;
        }

        private class GlossaryConfig { }

        private class TranslationResponse
        {
            public class Translation
            {
                public string text;
                public string detectedLanguageCode;
            }
            
            public Translation[] translations;
        }

        private const string IAM_AUTH_HEADER_TEMPLATE = "Authorization: Bearer {0}"; 
        private const string API_KEY_AUTH_HEADER_TEMPLATE = "Authorization: Api-Key {0}";
        private string ApiAuthHeader => string.Format(API_KEY_AUTH_HEADER_TEMPLATE, settings.YandexCloudApiKey);
        
        private const string TRANSLATION_API_ENDPOINT = "https://translate.api.cloud.yandex.net/translate/v2/translate";
        private Settings settings => Program.Settings;
        public string YandexIamToken = "";


        public string Fetch(string text)
        {
            var response = RequestTranslation(text);
            var result = ParseResponse(response);
            return result;
        }

        private string RequestTranslation(string text)
        {
            var translationRequest = new TranslationRequest
            {
                sourceLanguageCode = settings.SourceLanguageCode,
                targetLanguageCode = settings.TargetLanguageCode,
                texts = {[0] = text},
            };
            var request = SendRequest(translationRequest);

            WebResponse response = null;
            bool requestFailed = false;
            try
            {
                // Get the response.
                response = request.GetResponse();
            }
            catch (WebException webException)
            {
                Settings.WriteLog(webException.Message);
                Console.WriteLine(webException);
                response = webException.Response as HttpWebResponse;
                requestFailed = true;
            }

            if (response == null)
            {
                return null;
            }
            
            // Display the status.
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            // Get the stream containing content returned by the server.
            // The using block ensures the stream is automatically closed.
            string translationResponse;
            using (var dataStream = response.GetResponseStream())
            {
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();
                translationResponse = responseFromServer;
                Console.WriteLine(responseFromServer);
            }
            // Close the response.
            response.Close();

            if (requestFailed)
            {
                Settings.WriteLog(translationResponse);
                translationResponse = null;
            }
            
            return translationResponse;
        }

        private WebRequest SendRequest(TranslationRequest translationRequest)
        {
            var json = JsonConvert.SerializeObject(translationRequest);
            // Create POST data and convert it to a byte array.
            string postData = "This is a test that posts this string to a Web server.";
            byte[] byteArray = Encoding.UTF8.GetBytes(json);
            //
            WebRequest request = WebRequest.Create(TRANSLATION_API_ENDPOINT);
            request.Method = "POST";
            // Auth header
            request.Headers.Add(ApiAuthHeader);
            // Set the ContentType property of the WebRequest.
            request.ContentType = "application/x-www-form-urlencoded";
            // Set the ContentLength property of the WebRequest.
            request.ContentLength = byteArray.Length;
            // Get the request stream.
            Stream dataStream = request.GetRequestStream();
            // Write the data to the request stream.
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Close the Stream object.
            dataStream.Close();
            return request;
        }

        private string ParseResponse(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                return "Web request failed";
            }
            string result;
            if (JsonConvert.DeserializeObject(json, typeof(TranslationResponse)) is TranslationResponse parsed)
            {
                var text = parsed?.translations?.FirstOrDefault()?.text;
                result = text;
            }
            else
            {
                result = $"Failed to parse: {json}";
            }
            return result;
        }
    }
}
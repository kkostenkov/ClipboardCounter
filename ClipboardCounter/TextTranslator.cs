using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace ClipboardCounter
{
    /// <summary>
    /// Yandex translate docs
    /// https://yandex.ru/dev/translate/doc/dg/reference/translate-docpage/
    /// </summary>
    public class TextTranslator
    {
        private class Response
        {
            public int code;
            public string lang;
            public List<string> text;
        }
        
        private const string YA_TRANSLATE_KEY = 
            "trnsl.1.1.20200417T193608Z.e558a4f693138226.36639068feda928e772c02ac89b0faf607db2ed2";
        private const string YA_TRANSLATE_ENDPOINT = "https://translate.yandex.net/api/v1.5/tr.json/translate";

        private readonly Dictionary<int, string> responseCodes = new Dictionary<int, string>()
        {
            {200, "Success"},
            {401, "Invalid API key"},
            {402, "API key is blocked"},
            {404, "Daily translation char limit exceeded"},
            {413, "Max text size exceeded"},
            {422, "Can't translate this text"},
            {501, "Translation direction is unsupported"},
        };
        
        private string translationDirection = "de-ru";

        public string Fetch(string text)
        {
            var response = RequestTranslation(text);
            var result = ParseResponse(response);
            return result;
        }

        private string RequestTranslation(string text)
        {
            var escapedText = Uri.EscapeUriString(text);
            var request = $"{YA_TRANSLATE_ENDPOINT}?key={YA_TRANSLATE_KEY}&text={escapedText}&lang={translationDirection}";
            string responseString;
            using (var client = new WebClient())
            {
                var bytes = client.DownloadData(request);
                responseString = Encoding.UTF8.GetString(bytes);
            }
            return responseString;
        }
        
        private string ParseResponse(string json)
        {
            string result;
            if (JsonConvert.DeserializeObject(json, typeof(Response)) is Response parsed)
            {
                responseCodes.TryGetValue(parsed.code, out var codeText);
                result = $"Error #{parsed.code}: {codeText}";
                var text = parsed.text?.FirstOrDefault();
                if (!string.IsNullOrEmpty(text))
                {
                    result = text;    
                }
            }
            else
            {
                result = $"Failed to parse: {json}";
            }
            return result;
        }
    }
}
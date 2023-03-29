 public class GptService
    {
        private static readonly HttpClient httpClient = new HttpClient();
        private static string apiKey = "your-api-key";
        private static string apiUrl = "https://api.openai.com/v1/chat/completions";
        public GptService()
        {

        }

        public async Task<string> GetGptResponse(string prompt)
        {
            using var request = new HttpRequestMessage(HttpMethod.Post, apiUrl);
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);
            request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var payload = new
            {
                model = "gpt-3.5-turbo",
                messages = new[] { new { role = "user", content = "Tell me a joke" } }
            };

            request.Content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");

            using var response = await httpClient.SendAsync(request);
            
            string responseBody = await response.Content.ReadAsStringAsync();
            var gptApiResponse = JsonConvert.DeserializeObject<ChatGPTResponse>(responseBody);
            string gptResponse = gptApiResponse.Choices[0].Message.Content.Trim();

            return gptResponse;
        }
    }

    public class ChatGPTResponse
    {
        public List<Choice> Choices { get; set; }
    }

    public class Choice
    {
        public Message Message { get; set; }
    }

    public class Message
    {
        public string Role { get; set; }
        public string Content { get; set; }
    }

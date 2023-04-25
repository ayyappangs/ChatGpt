using Azure.AI.OpenAI;
using System;
using System.Threading.Tasks;

namespace Azure.ChatGpt
{
    public class AzureOpenAiService 
    {
        public AzureOpenAiService()
        {
            
        }

        public async Task<string> GetChats(string prompt)
        {
            try
            {
                string story = string.Empty;
                string endpoint = "YOUR AZURE API URL";
                string key = "YOUR AZURE API KEY";
                string engine = "gpt-35-turbo";

                OpenAIClient client = new(new Uri(endpoint), new AzureKeyCredential(key));

                ChatCompletionsOptions chatCompletionsOptions = new ChatCompletionsOptions();
                chatCompletionsOptions.Messages.Add(new ChatMessage(new ChatRole("user"), prompt));

                Response<ChatCompletions> completionsResponse = await client.GetChatCompletionsAsync(engine, chatCompletionsOptions);
                story = completionsResponse.Value.Choices[0].Message.Content;
                return story;
            }
            catch (Exception ex)
            {
                throw;
            }           
        }
    }
}

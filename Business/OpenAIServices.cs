namespace ChatOpenAi.Business
{
    public class OpenAIServices
    {
        private readonly HttpClient _httpClient;

        public OpenAIServices(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("OpenAI");
        }

        public async Task<string> GetChatResponse(string prompt)
        {
            var request = new
            {
                model = "text-davinci-003",
                prompt = prompt,
                max_tokens = 100
            };

            var response = await _httpClient.PostAsJsonAsync("completions", request);
            response.EnsureSuccessStatusCode();

            var responseData = await response.Content.ReadFromJsonAsync<OpenAiResponse  >();
            return responseData.Choices.FirstOrDefault()?.Text.Trim();
        }
    }
}


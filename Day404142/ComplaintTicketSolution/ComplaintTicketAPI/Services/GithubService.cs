using System.Text;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace ComplaintTicketAPI.Services
{

    public class GitHubService
    {
        private readonly string _githubToken = "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"; // Replace with your GitHub token
        private readonly string _repoOwner = "niharikaluna11"; // Your GitHub username or organization
        private readonly string _repoName = "my-image-repo"; // Your repository name
       // private readonly string _folderPath ; // The folder in your repo where images should be uploaded

        private readonly HttpClient _httpClient;

        public GitHubService()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"token {_githubToken}");
        }
        public async Task<string> SaveFileToGitHub(string filePath, string fileName, string folderPath)
        {
            if (string.IsNullOrWhiteSpace(folderPath) || string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentException("Folder path and file name must be provided.");

            fileName = string.Concat(fileName.Split(Path.GetInvalidFileNameChars())); // Remove invalid characters
            folderPath = folderPath.Trim('/'); // Remove leading or trailing slashes

            // Read the file content as base64
            byte[] fileBytes = await File.ReadAllBytesAsync(filePath);
            string base64FileContent = Convert.ToBase64String(fileBytes);

            var requestUrl = $"https://api.github.com/repos/{_repoOwner}/{_repoName}/contents/{Uri.EscapeUriString(folderPath)}/{Uri.EscapeDataString(fileName)}";

            var body = new
            {
                message = "Upload image",
                content = base64FileContent,
                branch = "main"
            };

            var jsonContent = JsonConvert.SerializeObject(body);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // GitHub requires a custom user agent header
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "MyBackendApp");

            var response = await _httpClient.PutAsync(requestUrl, content);
            if (response.IsSuccessStatusCode)
            {
                return fileName;
            }
            else
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error uploading file: {errorResponse}");
            }
        }
        

    }
}




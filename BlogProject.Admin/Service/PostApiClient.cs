using BlogProject.ViewModel.Catalog.Posts;
using BlogProject.ViewModel.Common;
using BlogProject.ViewModel.System.Users;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using BaseProject.Ultilities;

namespace BlogProject.Admin.Service
{
    public class PostApiClient : BaseApiClient, IPostApiClient
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;


        public PostApiClient(IHttpClientFactory httpClientFactory,
                    IHttpContextAccessor httpContextAccessor,
                     IConfiguration configuration)
             : base(httpClientFactory, httpContextAccessor, configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ApiResult<bool>> CreatePost(PostRequest request)
        {
            var sessions = _httpContextAccessor
                .HttpContext
                .Session
                .GetString(SystemConstants.AppSettings.Token);

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var requestContent = new MultipartFormDataContent();

            if (request.Image != null)
            {
                byte[] data;
                using (var br = new BinaryReader(request.Image.OpenReadStream()))
                {
                    data = br.ReadBytes((int)request.Image.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(data);
                requestContent.Add(bytes, "Image", request.Image.FileName);
            }
            if (request.Title != null)
            {
                requestContent.Add(new StringContent(request.Title.ToString()), "Title");
            }
            if (request.Desprition != null)
            {
                requestContent.Add(new StringContent(request.Desprition.ToString()), "Desprition");
            }
            if (request.Content != null)
            {
                requestContent.Add(new StringContent(request.Content.ToString()), "Content");
            }
            requestContent.Add(new StringContent(request.CategoryId.ToString()), "CategoryId");

            //string dateOfBirth = request.Dob?.ToString("dd-MM-yyyy") ?? string.Empty;
            //requestContent.Add(new StringContent(dateOfBirth), "Dob");

            //requestContent.Add(new StringContent(request.Gender?.ToString()), "Gender");

            //requestContent.Add(new StringContent(request.PhoneNumber?.ToString()), "PhoneNumber");

            //string UserAddress = request.Address?.ToString() ?? string.Empty;
            //requestContent.Add(new StringContent(UserAddress), "Address");

            

            string Image = request.ImageFileName?.ToString() ?? string.Empty;
            requestContent.Add(new StringContent(Image), "ImageFileName");
            var response = await client.PostAsync($"/api/posts", requestContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);

            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
        }

        public async Task<ApiResult<bool>> DeletePost(int id)
        {
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await client.DeleteAsync($"/api/posts/{id}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiResult<bool>>(body);

            return JsonConvert.DeserializeObject<ApiResult<bool>>(body);

        }

       

        public async Task<ApiResult<PostVm>> GetById(int id)
        {
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await client.GetAsync($"/api/posts/{id}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiResult<PostVm>>(body);

            return JsonConvert.DeserializeObject<ApiResult<PostVm>>(body);
        }

      

        public async Task<PagedResult<PostVm>> GetPagings(GetUserPagingRequest request)
        {
          var data= await  GetAsync<PagedResult<PostVm>>($"/api/Posts/role?pageIndex=" +
                $"{request.PageIndex}&pageSize={request.PageSize}&keyword={request.Keyword}");
            return data;
        }

        public async Task<ApiResult<bool>> UpdatePost(PostUpdateRequest request, int id)
        {
            var sessions = _httpContextAccessor
                .HttpContext
                .Session
                .GetString(SystemConstants.AppSettings.Token);

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var requestContent = new MultipartFormDataContent();

            if (request.Image != null)
            {
                byte[] data;
                using (var br = new BinaryReader(request.Image.OpenReadStream()))
                {
                    data = br.ReadBytes((int)request.Image.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(data);
                requestContent.Add(bytes, "Image", request.Image.FileName);
            }
            if (request.Title != null)
            {
                requestContent.Add(new StringContent(request.Title.ToString()), "Title");
            }
            if (request.Desprition != null)
            {
                requestContent.Add(new StringContent(request.Desprition.ToString()), "Desprition");
            }
            if (request.Content != null)
            {
                requestContent.Add(new StringContent(request.Content.ToString()), "Content");
            }
            requestContent.Add(new StringContent(request.CategoryId.ToString()), "CategoryId");

            //string dateOfBirth = request.Dob?.ToString("dd-MM-yyyy") ?? string.Empty;
            //requestContent.Add(new StringContent(dateOfBirth), "Dob");

            //requestContent.Add(new StringContent(request.Gender?.ToString()), "Gender");

            //requestContent.Add(new StringContent(request.PhoneNumber?.ToString()), "PhoneNumber");

            //string UserAddress = request.Address?.ToString() ?? string.Empty;
            //requestContent.Add(new StringContent(UserAddress), "Address");

            requestContent.Add(new StringContent(request.Id.ToString()), "Id");

            string Image = request.ImageFileName?.ToString() ?? string.Empty;
            requestContent.Add(new StringContent(Image), "ImageFileName");
            var response = await client.PutAsync($"/api/Posts/{id}", requestContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);

            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
        }
    }
}

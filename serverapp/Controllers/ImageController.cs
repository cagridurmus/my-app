using Microsoft.AspNetCore.Mvc;
using Firebase.Storage;

namespace serverapp.Controllers;

[ApiController]
[Route("[controller]")]
public class ImageController : ControllerBase
{
    private static string Bucket = "htkmobile-c7487.appspot.com";

        [HttpGet("getimage")]
        public async Task<string> GetImage(string userId, string filePath)
        {
            try {
                //var auth = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));
                //var a = await auth.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPassword);
                
                var cancellation = new CancellationTokenSource();
                // Get any Stream — it can be FileStream, MemoryStream or any other type of Stream

                // var stream = System.IO.File.Open(filePath, FileMode.Open);

                // Construct FirebaseStorage with path to where you want to upload the file and put it there
                var task = new FirebaseStorage(
                 Bucket,
                 new FirebaseStorageOptions {
                     //AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                     ThrowOnCancel = true
                 })
                 //.Child("Data")
                 .Child("Images")
                 .Child(userId)
                 .Child(filePath)
                 .GetDownloadUrlAsync();

                // Track progress of the upload
                // task.Progress.ProgressChanged += (s, e) => Console.WriteLine($"Progress: {e.Percentage} %");

                // Await the task to wait until upload is completed and get the download url
                var downloadUrl = await task;

                return await Task.FromResult(downloadUrl); ;
            }
            catch (Exception e) {
                string fail = e.ToString();
                return fail;
            }
        } 
}
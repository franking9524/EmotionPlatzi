using EmotionPlatzi.Web.Data;
using EmotionPlatzi.Web.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EmotionPlatzi.Web.Controllers
{
    public class EmoUploaderController : Controller
    {
        string serverFolderPath;
        EmotionHelper emoHelper;
        string key;
        EmotionPlatziWebContext db = new EmotionPlatziWebContext();
        public EmoUploaderController()
        {
            serverFolderPath = ConfigurationManager.AppSettings["UPLOAD_DIR"];
            key = ConfigurationManager.AppSettings["EMOTION_KEY"];
            emoHelper = new EmotionHelper(key);
        }
        // GET: EmoUploader
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Index(HttpPostedFileBase file)
        {
            //file != null es lo mismo que decir => file?
            if (file?.ContentLength > 0)
            {
                var pictureName = Guid.NewGuid().ToString();
                pictureName += Path.GetExtension(file.FileName);

                var route = Server.MapPath(serverFolderPath);
                route = route + "/" + pictureName;
                file.SaveAs(route);
                var emoPicture = await emoHelper.DetectAndExtractFacesAsync(file.InputStream);

                emoPicture.Nombre = file.FileName;
                //forma de concatenar con llaves, reemplaza a serverfolder + / + picture
                emoPicture.Path = $"{ serverFolderPath} / { pictureName}";
                db.EmoPictures.Add(emoPicture);
                await db.SaveChangesAsync();
                return RedirectToAction("Details", "EmoPictures", new { Id = emoPicture.Id});
            }
            return View();
        }
    }
}
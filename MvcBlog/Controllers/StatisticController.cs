using DataAccessLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcBlog.Controllers
{
    public class StatisticController : Controller
    {
        // GET: Statistic
        Context c = new Context();
        public ActionResult Index()
        {
            //Toplam kategori sayısı :
            var CategoryCount = c.Categories.Count().ToString();
            ViewBag.CategoryCount = CategoryCount;

            //Başlık tablosunda "Yazılım" kategorisine ait başlık sayısı : 
            var SoftwareTitleCount = c.Headings.Count(x => x.CategoryID==8);
            ViewBag.SoftwareTitleCount = SoftwareTitleCount;

            //Yazar adında 'a' harfi geçen yazarın/yazarların sayısı :
            var lettera = (from x in c.Writers select x.WriterName.IndexOf("a")).Distinct().Count().ToString();
            ViewBag.lettera = lettera;

            //En fazla başlığa sahip kategorinin adı :
            var headings = c.Categories.Where(u => u.CategoryID == c.Headings.GroupBy(x => x.CategoryID).OrderByDescending(x => x.Count()).Select(x => x.Key).FirstOrDefault()).Select(x => x.CategoryName).FirstOrDefault();
            ViewBag.headings = headings;

            // Kategori tablosunda durumu true olan kategoriler ile false olan kategoriler arasındaki fark : 
            var statu = c.Categories.Count(x => x.CategoryStatus == true) - c.Categories.Count(x => x.CategoryStatus == false);
            ViewBag.statu = statu;

            return View();
        }
    }
}
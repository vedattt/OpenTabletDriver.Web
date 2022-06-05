using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using OpenTabletDriver.Web.Core.Services;
using OpenTabletDriver.Web.Models;
using OpenTabletDriver.Web.Utilities;

namespace OpenTabletDriver.Web.Controllers
{
    public class TabletsController : Controller
    {
        private ITabletService tabletService;

        public TabletsController(ITabletService tabletService)
        {
            this.tabletService = tabletService;
        }

        [ResponseCache(Duration = 300)]
        public async Task<IActionResult> Index(string search = null)
        {
            var markdown = await tabletService.GetMarkdownRaw();
            var html = Markdown.ToHtml(markdown);
            var patchedHtml = html.Replace(
                "<table>",
                "<table id=\"tablets\" class=\"table table-hover\">"
            );

            var model = new ContentSearchViewModel
            {
                Content = new HtmlString(patchedHtml),
                Search = search
            };
            return View(model);
        }
    }
}
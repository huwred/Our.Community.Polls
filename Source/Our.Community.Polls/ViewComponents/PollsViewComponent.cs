using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Our.Community.Polls.Models;

namespace Our.Community.Polls.ViewComponents
{
    public class PollsViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(Question Model)
        {

            return await Task.FromResult((IViewComponentResult)View(Model));
        }
    }
}

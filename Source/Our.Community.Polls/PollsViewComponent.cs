using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Community.Polls.Models;

namespace Umbraco.Community.Polls
{
    public class PollsViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(Question Model)
        {

            return await Task.FromResult((IViewComponentResult)View(Model));
        }
    }
}

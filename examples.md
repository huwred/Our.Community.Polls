# Examples
Example using the provided Viewcomponent.
## ViewComponent
```csharp
   @await Component.InvokeAsync("Polls", Model.MyPoll)
```

Example using a controller and view.

## Controller
```csharp
    public class PollSurfaceController : SurfaceController
    {
        private readonly IPollService _pollService;
        public PollSurfaceController(IUmbracoContextAccessor umbracoContextAccessor, IUmbracoDatabaseFactory databaseFactory, ServiceContext services, AppCaches appCaches, IProfilingLogger profilingLogger, IPublishedUrlProvider publishedUrlProvider,
            IPollService pollService) : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
        {
            _pollService = pollService;
        }

        public ActionResult Get(int id)
        {
            var poll = _pollService.GetQuestion(id);
            return this.View(poll);
        }

        public ActionResult Post([FromBody] int questionId, [FromBody] int answerId)
        {
            var poll = _pollService.Vote(questionId, answerId);
            return this.View("partials/poll", poll);
        }
    }
```

## View
```csharp
@inherits UmbracoViewPage<Our.Community.Polls.Models.Question>

<p>@Model.Name</p>
<ul>
    @using (Html.BeginUmbracoForm("Post", "PollSurface"))
    {
        @Html.Hidden("questionId", Model.Id)

        foreach (var answer in Model.Answers)
        {
            <li>
                @Html.RadioButton("answerId", answer.Id, false)
                @answer.Percentage% - @answer.Value
            </li>
        }

        <button type="submit">post</button>
    }
</ul>
```

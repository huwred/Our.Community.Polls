# Developers Guide

## Contents

* [Introduction](#introduction)
* [Get value](#get-value)
* [Service](#poll-service)
* [Examples](#examples)

---

### Introduction

**Our.Community.Polls** is a backoffice extension for umbraco, it adds functionality to create, edit and delete polls in Umbraco. The polls can be selected using a content picker inside the content using a custom propery editor which is included.



---

### Get Value
The value stored in the property is the `id` of the question.

#### Value Conveter
```csharp
@Model.GetPropertyValue<Our.Community.Polls.Question>("poll");
```

#### Dynamic

```csharp
var question =  PollService.GetQuestion(Model.content.poll)
```

##### Response
```json
{
   "Id":3,
   "Name":"How do you like the Poll",
   "StartDate":null,
   "EndDate":null,
   "CreatedDate":"\/Date(1499371861200)\/",
   "Responses":9,
   "Answers":[
      {
         "Id":7,
         "Value":"Yes",
         "Index":1,
	      "Percentage":35,
         "Responses":[
            {
               "Id":25,
               "ResponseDate":"\/Date(1499299200000)\/"
            }
         ]
      },
      {
         "Id":8,
         "Value":"No",
         "Index":2,
	      "Percentage":11,
         "Responses":[
            {
               "Id":22,
               "ResponseDate":"\/Date(1499299200000)\/"
            },
            ...
         ]
      },
      ...
   ]
}
```

### Poll Service
The service has 2 methods, one to get the value and one for voting.


```csharp
    public interface IPollService
    {
        Question GetQuestion(int questionId);
        Question Vote(int questionId, int answerId);
    }
```
You receive the same model as when you call the `getQuestion` method

### Examples


Example using the provided Viewcomponent.
```csharp
   @await Component.InvokeAsync("Polls", Model.MyPoll)
```

Example using a controller and view.

#### Controller
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

#### View
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

# Developers Guide

## Contents

* [Introduction](#introduction)
* [Get value](#get-value)
* [Service](#poll-service)

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

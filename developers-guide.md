# Developers Guide

## Contents

* [Introduction](#introduction)
* [Settings](#settings)
* [Get the value](#get-the-value)
* [Service](#service)

---

### Introduction

**Our.Community.Polls** is a backoffice extension for umbraco, it adds the functionality to umbraco to create, edit and delete polls inside a custom section. The polls can be selected using a content picker inside the content with a custom propery editor which is included.



---

### Settings

#### Amount of answers
You can specify the number of answers per question. _If the value isn't defined, the default value will be:_ `6`
```xml
<add key="pollIt:AmountOfAnswers" value="6"/>
```

---

### Get the value
The value stored in the property is the `id` of the question wich is selected in the content editor.

#### Value Conveter
```csharp
@Model.GetPropertValue<Our.Community.Polls.Question>("poll");
```

#### Dynamic

```csharp
var question = PollService.GetQuestion(Model.content.poll)
```

##### Result
```javascript
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

### Service
The service has 2 methods, one to get the value and one for voting.

#### GetQuestion
```csharp
Question GetQuestion(Model.content.poll)
```
You receive the same model as the example above

#### Vote
```csharp
Question Vote(questionId, answerId);
```
You receive the same model as when you call the `getQuestion` method

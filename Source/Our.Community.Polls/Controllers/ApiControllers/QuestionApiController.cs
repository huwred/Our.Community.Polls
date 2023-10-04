using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Our.Community.Polls.Models;
using Our.Community.Polls.Repositories;
using Umbraco.Cms.Web.Common.Controllers;

namespace Our.Community.Polls.Controllers.ApiControllers
{

    public class QuestionApiController : UmbracoApiController 
    {
        private readonly IQuestions _questions;
        private readonly ILogger<QuestionApiController> _logger;

        public QuestionApiController(IQuestions questions,ILogger<QuestionApiController> logger)
        {
            _questions = questions;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Question> Get()
        {
            return _questions.Get();
        }

        [HttpGet]
        public Question GetById(int id)
        {
            var result = _questions.GetById(id);

            return result;
        }

        [HttpPost]
        public Question Post([FromBody]JObject question)
        {
            try
            {
                var check = (Question)JsonConvert.DeserializeObject(question.ToString(),typeof(Question));

                if (check != null)
                {
                    return _questions.Save(check);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e,"Unable to save Question");
                throw;
            }
            
            return null;
        }

        [HttpDelete]
        public bool Delete(int id)
        {
            try
            {
                if (_questions.Delete(id))
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e,"Unable to delete question");
                throw;
            }

            return false;
        }

        [HttpGet]
        public IEnumerable<Answer> GetAnswers(int id)
        {
            return _questions.GetAnswers(id);
        }

        [HttpPost]
        public Answer PostAnswer(int id, Answer answer)
        {
            try
            {
                var result = _questions.PostAnswer(id, answer);

                if (result != null)
                {
                    return result;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e,"Unable to add answer to question");
                throw;
            }

            return null; 
        }

        [HttpGet]
        public IEnumerable<Response> GetResponses(int id)
        {
            return _questions.GetResponses(id);
        }
    }
}

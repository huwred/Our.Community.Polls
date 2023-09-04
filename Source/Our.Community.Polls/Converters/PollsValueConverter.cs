using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.PropertyEditors;
using Umbraco.Cms.Core.PublishedCache;
using Umbraco.Community.Polls.Models;
using Umbraco.Community.Polls.Models.Repositories;
using Umbraco.Community.Polls.PollConstants;

namespace Umbraco.Community.Polls.Converters
{

    public class PollsValueConverter : IPropertyValueConverter
    {
        private readonly IQuestions _questions;
        private readonly IPublishedSnapshotAccessor _publishedSnapshotAccessor;
        private IPublishedModelFactory PublishedModelFactory;
        public PollsValueConverter(IPublishedSnapshotAccessor publishedSnapshotAccessor, IPublishedModelFactory publishedModelFactory,IQuestions questions)
        {
            _questions = questions;
            _publishedSnapshotAccessor = publishedSnapshotAccessor;
            PublishedModelFactory = publishedModelFactory;
        }
        public bool IsConverter(IPublishedPropertyType propertyType)
        {
            return ApplicationConstants.PropertyEditorAlias.Equals(propertyType.EditorAlias);
        }

        public bool? IsValue(object value, PropertyValueLevel level)
        {
            var test = value;
            return true;
        }

        public Type GetPropertyValueType(IPublishedPropertyType propertyType)
        {
            return typeof(Question);
        }

        public PropertyCacheLevel GetPropertyCacheLevel(IPublishedPropertyType propertyType)
        {
            return PropertyCacheLevel.Element;
        }

        public object ConvertSourceToIntermediate(IPublishedElement owner, IPublishedPropertyType propertyType, object source,
            bool preview)
        {
             if (!string.IsNullOrWhiteSpace(source?.ToString()))
             {
                 if (int.TryParse(source.ToString(), out var id))
                 {
                     return _questions.GetById(id);
                 }
             }
             return source?.ToString();

        }

        public object ConvertIntermediateToObject(IPublishedElement owner, IPublishedPropertyType propertyType,
            PropertyCacheLevel referenceCacheLevel, object inter, bool preview)
        {

            return inter;
        }

        public object ConvertIntermediateToXPath(IPublishedElement owner, IPublishedPropertyType propertyType,
            PropertyCacheLevel referenceCacheLevel, object inter, bool preview)
        {
            throw new NotImplementedException();
        }

        //protected IPublishedElement ConvertToElement(JObject sourceObject, PropertyCacheLevel referenceCacheLevel, bool preview)
        //{
        //    var elementTypeAlias = "question";// sourceObject[PollsValueConverter.ContentTypeAliasPropertyKey]?.ToObject<string>();
        //    if (string.IsNullOrEmpty(elementTypeAlias))
        //        return null;

        //    // only convert element types - content types will cause an exception when PublishedModelFactory creates the model
        //    _publishedSnapshotAccessor.TryGetPublishedSnapshot(out var publishedContentType);

        //    var contentType = publishedContentType.Content.GetContentType(elementTypeAlias);
        //    if (contentType == null || contentType.IsElement == false)
        //        return null;

        //    var propertyValues = sourceObject.ToObject<Dictionary<string, object>>();

        //    if (!propertyValues.TryGetValue("key", out var keyo)
        //        || !Guid.TryParse(keyo.ToString(), out var key))
        //        key = Guid.Empty;

        //    IPublishedElement element = new PublishedElement(contentType, key, propertyValues, preview, referenceCacheLevel, _publishedSnapshotAccessor);
        //    element = PublishedModelFactory.CreateModel(element);
        //    return element;
        //}
    }
}

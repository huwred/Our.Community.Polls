using System;
using Our.Community.Polls.Models;
using Our.Community.Polls.PollConstants;
using Our.Community.Polls.Repositories;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.PropertyEditors;
using Umbraco.Cms.Core.PublishedCache;

namespace Our.Community.Polls.Converters
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

    }
}

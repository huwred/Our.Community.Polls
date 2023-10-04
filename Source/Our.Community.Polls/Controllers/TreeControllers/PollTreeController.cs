using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Our.Community.Polls.PollConstants;
using Our.Community.Polls.Repositories;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Actions;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Models.Trees;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Trees;
using Umbraco.Cms.Web.BackOffice.Trees;
using Umbraco.Cms.Web.Common.Attributes;
using Umbraco.Cms.Web.Common.ModelBinders;
using Umbraco.Extensions;

namespace Our.Community.Polls.Controllers.TreeControllers
{
    [Tree(Constants.Applications.Settings, TreeConstants.TreeAlias, TreeTitle = TreeConstants.TreeTitle)]
    [PluginController(ApplicationConstants.SectionAlias)]
    public class PollTreeController : TreeController
    {
        //private readonly string rootId = Constants.System.Root.ToString(CultureInfo.InvariantCulture);
        private readonly ILocalizedTextService _localizedTextService;
        private readonly IMenuItemCollectionFactory _menuItemCollectionFactory;
        private readonly IQuestions _questions;
        public PollTreeController(ILocalizedTextService localizedTextService, UmbracoApiControllerTypeCollection umbracoApiControllerTypeCollection, IEventAggregator eventAggregator, IMenuItemCollectionFactory menuItemCollectionFactory,IQuestions questions) : base(localizedTextService, umbracoApiControllerTypeCollection, eventAggregator)
        {
            _localizedTextService = localizedTextService;
            _menuItemCollectionFactory = menuItemCollectionFactory ?? throw new ArgumentNullException(nameof(menuItemCollectionFactory));
            _questions = questions;
        }
        protected override ActionResult<TreeNode> CreateRootNode(FormCollection queryStrings)
        {
            var rootResult = base.CreateRootNode(queryStrings);
            if (!(rootResult.Result is null))
            {
                return rootResult;
            }

            var root = rootResult.Value;

            //optionally setting a routepath would allow you to load in a custom UI instead of the usual behaviour for a tree
            root.RoutePath = string.Format("{0}/{1}/{2}", Constants.Applications.Settings, "poll", "overview");
            // set the icon
            root.Icon = "icon-poll";
            // set to false for a custom tree with a single node.
            root.HasChildren = true;
            //url for menu
            root.MenuUrl = null;

            return root;
        }
        protected override ActionResult<TreeNodeCollection> GetTreeNodes(string id, [ModelBinder(typeof(HttpQueryStringModelBinder))] FormCollection queryStrings)
        {
            var nodes = new TreeNodeCollection();

            if (id.Equals(Constants.System.Root.ToInvariantString()))
            {
                var questions = _questions.Get();

                foreach (var question in questions)
                {
                    nodes.Add(this.CreateTreeNode(question.Id.ToString(), id, queryStrings, question.Name, "icon-poll", false));
                }
            }

            return nodes;
        }

        protected override ActionResult<MenuItemCollection> GetMenuForNode(string id, [ModelBinder(typeof(HttpQueryStringModelBinder))] FormCollection queryStrings)
        {
            var collection = _menuItemCollectionFactory.Create();

            if (id.Equals(Constants.System.Root.ToInvariantString()))
            {
                // root actions, perhaps users can create new items in this tree, or perhaps it's not a content tree, it might be a read only tree, or each node item might represent something entirely different...
                // add your menu item actions or custom ActionMenuItems
                collection.Items.Add(new CreateChildEntity(LocalizedTextService));

            }
            else
            {
                collection.Items.Add<ActionDelete>(LocalizedTextService, true, opensDialog: true);

            }

            // add refresh menu item (note no dialog)
            collection.Items.Add(new RefreshNode(LocalizedTextService, true));

            return collection;
        }

    }


}

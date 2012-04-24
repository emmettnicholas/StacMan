using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using StackExchange.StacMan.Tests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace StackExchange.StacMan.Tests
{
    [TestClass]
    public class FilterRegistrationTests
    {
        [TestMethod]
        public void Loose_filter_registration_throws_exception()
        {
            var client = new StacManClient(FilterBehavior.Loose);
            Assert2.Throws<StacMan.Exceptions.FilterException>(() => client.RegisterFilters("foo", "bar", "baz"));
        }
        
        [TestMethod]
        public void Strict_filter_registration()
        {
            var mock = new Mock<StacManClient>(FilterBehavior.Strict, null);

            // http://api.stackexchange.com/2.0/filters/!UhRibfDA*9;-pFCia1g)g1T
            mock.FakeFetchForRegex("filters", response: @"{""items"":[{""filter"":""!UhRibfDA*9"",""included_fields"":["".items"",""post.body"",""post.comments"",""post.creation_date"",""post.down_vote_count"",""post.last_activity_date"",""post.last_edit_date"",""post.owner"",""post.post_id"",""post.post_type"",""post.score"",""post.up_vote_count""],""filter_type"":""safe""},{""filter"":""-pFCia1g)g1T"",""included_fields"":["".items"",""post.body"",""post.comments"",""post.creation_date"",""post.down_vote_count"",""post.last_activity_date"",""post.last_edit_date"",""post.owner"",""post.post_id"",""post.post_type"",""post.score"",""post.up_vote_count""],""filter_type"":""unsafe""}],""quota_remaining"":277,""quota_max"":300,""has_more"":false}");

            var client = mock.Object;
            client.RegisterFilters("!UhRibfDA*9", "-pFCia1g)g1T");
            
            Assert.IsTrue(client.IsFilterRegistered("!UhRibfDA*9"));
            Assert.IsTrue(client.IsFilterRegistered("-pFCia1g)g1T"));
            Assert.IsFalse(client.IsFilterRegistered("asdfasdfasdf"));

            // http://api.stackexchange.com/2.0/users?site=stackoverflow&filter=!UhRibfDA*9
            mock.FakeFetchForRegex("users", response: @"{""items"":[{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{}]}");

            var usersResponse = client.Users.GetAll("stackoverflow", filter: "!UhRibfDA*9").Result;
            Assert.IsTrue(usersResponse.Success);

            Assert2.ThrowsArgumentException(() => client.Users.GetAll("stackoverflow", filter: "asdfasdfasdf"), "filter");
        }
        
        [TestMethod]
        public void Is_Default_filter_registered()
        {
            var client = new StacManClient(FilterBehavior.Strict);
            Assert.IsTrue(client.IsFilterRegistered("default"));
        }

        [TestMethod]
        public void Prevent_access_to_filtered_out_fields()
        {
            var mock = new Mock<StacManClient>(FilterBehavior.Strict, null);

            // http://api.stackexchange.com/2.0/filters/!-q2RZj5_
            mock.FakeFetchForRegex("filters", response: @"{""items"":[{""filter"":""!-q2RZj5_"",""included_fields"":["".backoff"","".error_id"","".error_message"","".error_name"","".has_more"","".items"","".quota_max"","".quota_remaining"",""access_token.access_token"",""access_token.account_id"",""access_token.expires_on_date"",""access_token.scope"",""answer.answer_id"",""answer.community_owned_date"",""answer.creation_date"",""answer.is_accepted"",""answer.last_activity_date"",""answer.last_edit_date"",""answer.locked_date"",""answer.owner"",""answer.question_id"",""answer.score"",""badge.award_count"",""badge.badge_id"",""badge.badge_type"",""badge.link"",""badge.name"",""badge.rank"",""badge.user"",""badge_count.bronze"",""badge_count.gold"",""badge_count.silver"",""comment.comment_id"",""comment.creation_date"",""comment.edited"",""comment.owner"",""comment.post_id"",""comment.reply_to_user"",""comment.score"",""error.description"",""error.error_id"",""error.error_name"",""event.creation_date"",""event.event_id"",""event.event_type"",""filter.filter"",""filter.filter_type"",""filter.included_fields"",""inbox_item.answer_id"",""inbox_item.comment_id"",""inbox_item.creation_date"",""inbox_item.is_unread"",""inbox_item.item_type"",""inbox_item.link"",""inbox_item.question_id"",""inbox_item.site"",""inbox_item.title"",""info.answers_per_minute"",""info.api_revision"",""info.badges_per_minute"",""info.new_active_users"",""info.questions_per_minute"",""info.total_accepted"",""info.total_answers"",""info.total_badges"",""info.total_comments"",""info.total_questions"",""info.total_unanswered"",""info.total_users"",""info.total_votes"",""migration_info.on_date"",""migration_info.other_site"",""migration_info.question_id"",""network_user.account_id"",""network_user.answer_count"",""network_user.badge_counts"",""network_user.creation_date"",""network_user.last_access_date"",""network_user.question_count"",""network_user.reputation"",""network_user.site_name"",""network_user.site_url"",""network_user.user_id"",""post.creation_date"",""post.last_activity_date"",""post.last_edit_date"",""post.owner"",""post.post_id"",""post.post_type"",""post.score"",""post.up_vote_count"",""privilege.description"",""privilege.reputation"",""privilege.short_description"",""question.accepted_answer_id"",""question.answer_count"",""question.bounty_amount"",""question.bounty_closes_date"",""question.closed_date"",""question.closed_reason"",""question.community_owned_date"",""question.creation_date"",""question.is_answered"",""question.last_activity_date"",""question.last_edit_date"",""question.link"",""question.locked_date"",""question.migrated_from"",""question.migrated_to"",""question.owner"",""question.protected_date"",""question.question_id"",""question.score"",""question.tags"",""question.title"",""question.view_count"",""question_timeline.comment_id"",""question_timeline.creation_date"",""question_timeline.down_vote_count"",""question_timeline.owner"",""question_timeline.post_id"",""question_timeline.question_id"",""question_timeline.revision_guid"",""question_timeline.timeline_type"",""question_timeline.up_vote_count"",""question_timeline.user"",""related_site.api_site_parameter"",""related_site.name"",""related_site.relation"",""related_site.site_url"",""reputation.on_date"",""reputation.post_id"",""reputation.post_type"",""reputation.reputation_change"",""reputation.user_id"",""reputation.vote_type"",""revision.comment"",""revision.creation_date"",""revision.is_rollback"",""revision.last_tags"",""revision.last_title"",""revision.post_id"",""revision.post_type"",""revision.revision_guid"",""revision.revision_number"",""revision.revision_type"",""revision.set_community_wiki"",""revision.tags"",""revision.title"",""revision.user"",""shallow_user.accept_rate"",""shallow_user.display_name"",""shallow_user.link"",""shallow_user.profile_image"",""shallow_user.reputation"",""shallow_user.user_id"",""shallow_user.user_type"",""site.aliases"",""site.api_site_parameter"",""site.audience"",""site.closed_beta_date"",""site.favicon_url"",""site.icon_url"",""site.launch_date"",""site.logo_url"",""site.markdown_extensions"",""site.name"",""site.open_beta_date"",""site.related_sites"",""site.site_state"",""site.site_type"",""site.site_url"",""site.styling"",""site.twitter_account"",""styling.link_color"",""styling.tag_background_color"",""styling.tag_foreground_color"",""suggested_edit.approval_date"",""suggested_edit.comment"",""suggested_edit.creation_date"",""suggested_edit.post_id"",""suggested_edit.post_type"",""suggested_edit.proposing_user"",""suggested_edit.rejection_date"",""suggested_edit.suggested_edit_id"",""suggested_edit.tags"",""suggested_edit.title"",""tag.count"",""tag.has_synonyms"",""tag.is_moderator_only"",""tag.is_required"",""tag.name"",""tag.user_id"",""tag_score.post_count"",""tag_score.score"",""tag_score.user"",""tag_synonym.applied_count"",""tag_synonym.creation_date"",""tag_synonym.from_tag"",""tag_synonym.last_applied_date"",""tag_synonym.to_tag"",""tag_wiki.body_last_edit_date"",""tag_wiki.excerpt"",""tag_wiki.excerpt_last_edit_date"",""tag_wiki.tag_name"",""top_tag.answer_count"",""top_tag.answer_score"",""top_tag.question_count"",""top_tag.question_score"",""top_tag.tag_name"",""user.accept_rate"",""user.account_id"",""user.age"",""user.badge_counts"",""user.creation_date"",""user.display_name"",""user.is_employee"",""user.last_access_date"",""user.last_modified_date"",""user.link"",""user.location"",""user.profile_image"",""user.reputation"",""user.reputation_change_day"",""user.reputation_change_month"",""user.reputation_change_quarter"",""user.reputation_change_week"",""user.reputation_change_year"",""user.timed_penalty_date"",""user.user_id"",""user.user_type"",""user.website_url"",""user_timeline.badge_id"",""user_timeline.comment_id"",""user_timeline.creation_date"",""user_timeline.detail"",""user_timeline.post_id"",""user_timeline.post_type"",""user_timeline.suggested_edit_id"",""user_timeline.timeline_type"",""user_timeline.title"",""user_timeline.user_id""],""filter_type"":""safe""}],""quota_remaining"":273,""quota_max"":300,""has_more"":false}");

            // http://api.stackexchange.com/2.0/posts?pagesize=2&site=stackoverflow&filter=!-q2RZj5_
            mock.FakeFetchForRegex("posts", response: @"{""items"":[{""post_id"":10077683,""post_type"":""question"",""owner"":{""user_id"":1322429,""display_name"":""user1322429"",""reputation"":1,""user_type"":""registered"",""profile_image"":""http://www.gravatar.com/avatar/c6dcfd1e7c05de64068790702c083f16?d=identicon&r=PG"",""link"":""http://stackoverflow.com/users/1322429/user1322429""},""creation_date"":1333995173,""last_activity_date"":1333997493,""last_edit_date"":1333997493,""score"":0,""up_vote_count"":0},{""post_id"":10078150,""post_type"":""question"",""owner"":{""user_id"":127776,""display_name"":""John M"",""reputation"":1587,""user_type"":""registered"",""profile_image"":""http://www.gravatar.com/avatar/c7bc4e6decb8fa63db610c86a1862110?d=identicon&r=PG"",""link"":""http://stackoverflow.com/users/127776/john-m"",""accept_rate"":96},""creation_date"":1333997487,""last_activity_date"":1333997487,""score"":0,""up_vote_count"":0}],""quota_remaining"":274,""quota_max"":300,""has_more"":true}");

            var client = mock.Object;
            client.RegisterFilters("!-q2RZj5_");

            var response = client.Posts.GetAll("stackoverflow", filter: "!-q2RZj5_", pagesize: 2).Result;
            var post = response.Data.Items.First();

            // the "!-q2RZj5_" filter contains up_vote_count, but not down_vote_count
            Assert.AreEqual(0, post.UpVoteCount);
            Assert2.Throws<Exceptions.FilterException>(() => { var c = post.DownVoteCount; });
        }

        [TestMethod]
        public void Loose_filter_behavior()
        {
            var mock = new Mock<StacManClient>(FilterBehavior.Loose, null);

            //http://api.stackexchange.com/2.0/comments?page=1&pagesize=1&order=desc&sort=creation&site=stackoverflow&filter=!67oYenEe
            mock.FakeFetch(response: @"{""items"":[{""comment_id"":13063519}],""has_more"":true}");

            var client = mock.Object;

            var result = client.Comments.GetAll("stackoverflow", page: 1, pagesize: 1, order: Order.Desc, sort: Comments.Sort.Creation, filter: "!6e8DFsY9").Result;
            Assert.IsTrue(result.Success);

            var wrapper = result.Data;
            Assert.IsTrue(wrapper.HasMore);

            var comment = wrapper.Items.Single();
            Assert.AreEqual(13063519, comment.CommentId);

            // the following fields aren't included in the filter, so expect default values:
            Assert.AreEqual(0, comment.PostId);
            Assert.AreEqual(DateTime.MinValue, comment.CreationDate);
            Assert.IsNull(comment.Body);
            Assert.IsNull(comment.Owner);
            Assert.IsFalse(comment.Edited);
        }
    }
}

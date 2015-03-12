
var postApiUrl = '/api/ApiCommets/';

function Post(data) {
    var self = this;
    data = data || {};
    self.Id = data.Id;
    self.comment = ko.observable(data.comment || "");
    self.Email = ko.observable(data.Email || "");
    self.Date = getTimeAgo(data.Date);
    self.error = ko.observable();
    self.newCommentMessage = ko.observable();

    self.addComment = function () {
        var comments = new Comment();
        comments.Id = self.Id;
        comments.comment(self.newCommentMessage());
        return $.ajax({
            url: commentApiUrl,
            dataType: "json",
            contentType: "application/json",
            cache: false,
            type: 'POST',
            data: ko.toJSON(comments)
        })
       .done(function (result) {
           self.PostComments.push(new Comment(result));
           self.newCommentMessage('');
       })
       .fail(function () {
           error('unable to add post');
       });


    }

    if (data.PostComments) {
        var mappedPosts = $.map(data.PostComments, function (item) { return new Comment(item); });
        self.PostComments(mappedPosts);
    }
}

function Comment(data) {
    var self = this;
    data = data || {};
    self.Id = data.Id;
    self.Email = ko.observable(data.Email || "");
    self.comment = ko.observable(data.comment || "");
    self.Date = getTimeAgo(data.Date);
    self.error = ko.observable();
}

function getTimeAgo(varDate) {
    if (varDate) { return $.timeago(varDate.toString().slice(-1) == 'Z' ? varDate : varDate + 'Z'); } else { return ''; }
}

function viewModel() {
    var self = this;
    self.posts = ko.observableArray();
    self.newMessage = ko.observable();
    self.error = ko.observable();

    self.loadPosts = function () {

        $.ajax({
            url: postApiUrl,
            dataType: "json",
            contentType: "application/json",
            cache: false,
            type: 'GET'
        })
            .done(function (data) {
                var mappedPosts = $.map(data, function (item) { return new Post(item); });
                self.posts(mappedPosts);
            })
            .fail(function () {
                error('unable to load posts');
            });
    }

    self.addPost = function () {
        var post = new Post();
        post.comment(self.newMessage());
        return $.ajax({
            url: postApiUrl,
            dataType: "json",
            contentType: "application/json",
            cache: false,
            type: 'POST',
            data: ko.toJSON(post)
        })
       .done(function (result) {
           self.posts.splice(0, 0, new Post(result));
           self.newMessage('');
       })
       .fail(function () {
           error('unable to add post');
       });
    }

    self.loadPosts();

    return self;
};

var viewModel = new viewModel();
ko.applyBindings(viewModel);
## Features

* Supports [**all** Stack Exchange API V2 methods](http://api.stackexchange.com/docs) through version 2.1
* **Easy to use**: one-to-one mapping between StacMan and API methods/params
* **Async is easy**: methods return [`Task<T>`](http://msdn.microsoft.com/en-us/library/dd321424.aspx) so they're [ready for C# 5's `await`](http://msdn.microsoft.com/en-us/vstudio/hh533273)
* Adheres to the [API's throttling rules](http://api.stackexchange.com/docs/throttle)

## Get StacMan

[StacMan is available on NuGet](https://nuget.org/packages/StacMan):

    PM> Install-Package StacMan

## Example Usage

    using StackExchange.StacMan;
    ...
    var client = new StacManClient(key: "my-app-key", version: "2.1");

**Synchronous**

    var response = client.Questions.GetAll("stackoverflow",
        page: 1,
        pagesize: 10,
        sort: Questions.AllSort.Creation,
        order: Order.Desc,
        filter: "!mDO35lQRaz").Result;

    foreach (var question in response.Data.Items)
    {
        Console.WriteLine(question.Title);
    }

**Asynchronous**

    var task = client.Questions.GetAll("stackoverflow",
        page: 1,
        pagesize: 10,
        sort: Questions.AllSort.Creation,
        order: Order.Desc,
        filter: "!mDO35lQRaz");

    task.ContinueWith(t =>
        {
            foreach (var user in t.Result.Data.Items)
            {
                Console.WriteLine(question.Title);
            }
        });

**Asynchronous (C# 5)**

    var response = await client.Questions.GetAll("stackoverflow",
        page: 1,
        pagesize: 10,
        sort: Questions.AllSort.Creation,
        order: Order.Desc,
        filter: "!mDO35lQRaz");

    foreach (var user in response.Data.Items)
    {
        Console.WriteLine(question.Title);
    }

## Filters

StacMan supports the Stack Exhchange API's concept of [filters](http://api.stackexchange.com/docs/filters), which allow applications to specify which fields are included/excluded in the API response.

When a field is excluded, the property returned by StacMan corresponding to the excluded field assumes the *default value* of the type. For example, when the "default" filter is used, the `AnswerCount` property of the User object returned by StacMan will be 0, since `user.answer_count` is not included by the "default" filter.
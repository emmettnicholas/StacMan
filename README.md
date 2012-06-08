## Features

* Supports [**all** Stack Exchange API V2 methods](http://api.stackexchange.com/docs)
* **Easy to use**: one-to-one mapping between StacMan and API methods/params
* **Async is easy**: methods return [`Task<T>`](http://msdn.microsoft.com/en-us/library/dd321424.aspx) so they're [ready for C# 5's `await`](http://msdn.microsoft.com/en-us/vstudio/hh533273)
* Validates that fields are included in the [filter](http://api.stackexchange.com/docs/filters) *(optional &ndash; see [FilterBehavior](#filter-behavior) below)*
* Adheres to the [API's throttling rules](http://api.stackexchange.com/docs/throttle)

## Get StacMan

[StacMan is available on NuGet](https://nuget.org/packages/StacMan):

    PM> Install-Package StacMan

## Example Usage

    using StackExchange.StacMan;
    ...
    var client = new StacManClient(FilterBehavior.Strict, key: "my-app-key");
    client.RegisterFilters("!mDO35lQRaz");

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

<a name="filter-behavior" />
## FilterBehavior

The `StacManClient` constructor takes a `FilterBehavior` enum, which can be either `Strict` or `Loose`:

**FilterBehavior.Strict**
* Getting a property not included in the [filter](http://api.stackexchange.com/docs/filters) throws a `FilterException`.
* All filters must be "registered" (with the `RegisterFilters` method) prior to being used.
  * IMPORTANT: `RegisterFilters` incurs one [API call](http://api.stackexchange.com/docs/read-filter) (per 20 unregistered filters) each time it's called, so for best performance, it should be called sparingly and at most once per filter, e.g. once only when your app starts. (Built-in filters such as "default" do not need to be registered.)

**FilterBehavior.Loose**
* Getting a property not included in the [filter](http://api.stackexchange.com/docs/filters) returns that property type's default value.
* Filters do not need to be "registered".
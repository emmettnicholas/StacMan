# Example Usage

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
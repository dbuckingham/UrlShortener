# UrlShortener

Here are some notes, and stuff...

TODO

+ Append index state info to Url/Index (is stale, last updated, etc.)
+ Auto-refresh Url/Index (http://stackoverflow.com/questions/7064982/asynchronous-auto-refresh-using-asp-net-mvc-3)
+ Add "Delete All" action to UrlController.
+ Add data layer to separate view models from data models, and abstract data access from MVC app.
+ Format (and localize) date and times in Views.
+ Validate data entry (keys do not exist, valid url is given, etc.)
+ Show some sort of "empty" message when no links are present/returned.
+ Show a N/A tag when a Timestamp is default (LastAccessed = 1/1/0001...)
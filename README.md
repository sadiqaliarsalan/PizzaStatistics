1. WebPizzaEventSimulator: It generates mock events and send them to over tcp
2. WebPizzaStatistics: It recieves any events over tcp. use handlers to handle the event and use managers to update in-memory objects
3. WebPizzaCommon: It contains all the common functionality being used by different apps. Contain different managers, handlers, models, enums and common utilities
4. WebPizzaCommon.Tests: Written some unit tests to cover business logic

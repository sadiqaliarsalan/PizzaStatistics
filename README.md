WebPizzaEventSimulator: It generates mock events and send them to tcp on localhost and port 8888
WebPizzaStatistics: It recieves any events from tcp on localhost and port 8888. and then update the stats
WebPizzaCommon: It contains all the common functionality being used by both above apps. Contain different managers, models, enums and common utilities
WebPizzaCommon.Tests: Written some unit tests for demostration (whole code is not covered yet)

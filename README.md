1. WebPizzaEventSimulator: It generates mock events and send them to over tcp
2. WebPizzaStatistics: It recieves any events over tcp. use handlers to handle the event and use managers to update in-memory objects
3. WebPizzaCommon: It contains all the common functionality being used by different apps. Contain different managers, handlers, models, enums and common utilities
4. WebPizzaCommon.Tests: Written some unit tests to cover business logic

Old Architecure

![Screenshot 2024-08-15 at 11 17 49](https://github.com/user-attachments/assets/eb7fb1b2-6b54-40cb-a01a-6d8d5ae471b1)

New Architecture

![Screenshot 2024-08-15 at 11 18 07](https://github.com/user-attachments/assets/a0193a76-5c7a-4208-84de-ba42993e61ef)

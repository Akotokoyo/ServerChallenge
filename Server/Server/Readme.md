Hello! Thanks a lot for this opportunity! 
I must say why I did some decisions in this project.
- I choosed to use relational DB because i'm afraid about the time limit(8 hours), I should have studied non-relational databases to use them with C#, but I would have wasted too much time
- I choosed to NOT use Identity or scaffolding, because the test was about see how I code, so I decided to do everything by my own hands.
- This project Hit a lot of time the Database, I should have to use some kind of Cache system(maybe redis), but as I said before, I never used redis with C#, only with Erlang language.
- I decided to treat cash and jems as items, because I thought about what will happen if we will add new coin system? Even for the rewards I choosed to treat it like an item, for the same reason.
- I decided to treat the items like a configuration sheet, to give us the opportunity to access to them directly and without hit the database.

Unfortunately I can't test all the features. I would have liked to add some unittests/integration tests.
I tried to simulate the architecture that I used in my previous company, we used Controllers to evaluate the client request, with the services we do DB operations.
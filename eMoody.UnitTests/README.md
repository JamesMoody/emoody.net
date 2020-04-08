# Overview

This is focused on various unit test, for now. More to come... hopefully! ;P


##### Info for GUI Tests
* Longest chapter is Psalms 119 
    * select b, c, count(v) as VerseCount from t_kjv group by b, c having count(v) > 100;

* Longest verse is Esther 8:9
    * select * from t_kjv where length(t) > 500 order by length(t) desc;


use Task_Management;
-- Task1 
select * from Customer;
-- Task2
select * from Task where Priority = 2 ;
 -- Task3
select * from Worker where Position='senior' and Salary = 800;
 -- Task4
select Salary , Name from Worker;
 -- Task5
select Surname as Lastname, Position as Post from Worker;
 -- Task6
 select concat(Name, ' ', Surname) as Name from Worker;
 -- Task7
 select concat(Title, ' ', priority) as Project,
case
when Priority >= 3 then 'This goes first'
when Priority = 2 then 'Secondly'
else 'Sometime but not now'
end as Recomendations
from Task;
-- Task8
SELECT CompanyName as Company FROM Customer where id > 5 limit 2;
-- Task9
use Task_Management;
SELECT CompanyName as Company FROM Customer where id > 5 limit 2;
-- It's impossible to get concrete number of rows in MYSQL workbench
-- Task10
select * from Task where Description = null;
-- BTW, all the rows in my tables are not null, so it won't work
-- Task11
select * from Task where Title like '%2';
-- Task12
select Name from Worker order by salary;
-- Task13
select * from Task order by Id desc, Priority desc;
-- Task14
select * from Customer order by substring(CompanyName, 1,1);
 -- Task15
 select * from Customer order by CompanyName is null, CompanyName desc;
 -- Task16
 select * from Task order by Id desc;
 -- Task17
select name as 'worker and company', position as 'postitions + rating' from Worker
union all
select name, rating from Team;
 -- Task18
select t.title as 'Task name', r.name as 'Repository name'
from task t, repository r
where r.TaskId = t.Id;
 -- Task19
 select t.title as 'Task name', r.name as 'Repository name'
from task t join repository r
on t.Id = r.taskid;
 -- Task20
 select t.title as 'Task name', r.name as 'Repository name'
from task t left join repository r
on t.Id = r.taskid;
-- Task21
select t.title as 'Task name', r.name as 'Repository name'
from task t left join repository r
on t.Id = r.taskid;
-- Task22
select w.name as 'worker name', c.companyname as 'cutomer name' from worker w
right join team t on w.id = t.workerid
left join customer c on t.customerid = c.id;
-- Task23
select sum(w.salary) as 'total amount', 
avg(w.salary) as 'average amount', 
r.name as 'repositories' from worker w
right join working wk on w.id = wk.workerid
left join repository r on wk.repositoryid = r.id
group by r.name;
-- Task24
select sum(w.salary) as 'not spent money per month', t.name as 'project team'
from team t left outer join worker w on w.id = t.workerid
group by t.name;
-- Task25
select w.name as 'workers', t.name as 'project team', c.companyname as 'company'
from team t 
join customer c on c.id = t.customerid
right outer join worker w on w.id = t.workerid;
-- Task26
select w.name as 'workers', coalesce(t.name,'no team') as 'project team', 
coalesce(c.companyname,'no company')as 'company'
from team t 
join customer c on c.id = t.customerid
right outer join worker w on w.id = t.workerid;
-- Task33
select min(salary) as 'mininal salary',
max(salary) as 'max salary'
from Worker;
-- Task34
select count(*) as 'Amount of combinations'
from Team
where name = 'Kpi team';
-- Task35
select count(*) as 'Amount of combinations'
from Team
where name = 'Kpi team';
-- Task36
select sum(salary) as 'total juniors salary'
from Worker 
where position = 'junior';
-- Task37
select datediff(finishdate,startdate) as 'Days to work'
from ( select finishdate from task where id = 23) x,
(select startdate from task  where id = 23) y;
-- Task39
select day(
last_day(
	date_add(
	date_add(
	date_add(current_date,
		interval -dayofyear(current_date) day),
	interval 1 day),
	interval 1 month))) dy
from Task;
-- Task40
select date_add(current_date,
	interval -day(current_date)+1 day) firstday,
     last_day(current_date) lastday
from Task;
-- Task41
select max(case dw when 2 then dm end) as Mo,
          max(case dw when 3 then dm end) as Tu,
          max(case dw when 4 then dm end) as We,
          max(case dw when 5 then dm end) as Th,
          max(case dw when 6 then dm end) as Fr,
          max(case dw when 7 then dm end) as Sa,
          max(case dw when 1 then dm end) as Su
 from (
  select date_format(dy,'%u') wk,
         date_format(dy,'%d') dm,
         date_format(dy,'%w')+1 dw
 from (
  select adddate(x.dy,football.t500.id-1) dy,
 x.mth
 from (
  select adddate(current_date,-dayofmonth(current_date)+1) dy,
      date_format(
          adddate(current_date,
                  -dayofmonth(current_date)+1),
                  '%m') mth
from task )x, football.t500
where t500.id <= 31
and date_format(adddate(x.dy,football.t500.id-1),'%m') = x.mth )y
)z 
group by wk 
order by wk;


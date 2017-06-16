@echo off

git status
git add -A
git status
pause

set h=%TIME:~0,2%
set m=%TIME:~3,2%
set s=%TIME:~6,2%
set ms=%TIME:~9,2%
set curtime=%h%:%m%:%s%:%ms%
set dd=%DATE:~0,2%
set mm=%DATE:~3,2%
set yyyy=%DATE:~6,4%
set curdate=%dd%-%mm%-%yyyy%
set curdatetime=%curdate% %curtime%
echo ******************* Commiting *******************

git commit -m "Update %curdatetime%"
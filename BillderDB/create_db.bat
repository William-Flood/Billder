echo off

rem batch file to run a script to create a db
rem 2016-09-14

sqlcmd -S localhost -E -i BezierDB.sql


ECHO if no error messages appear DB was created
PAUSE
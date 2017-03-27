----------------------------------------------------------------------
Package Name   nvLocked Patch Utility     

Supported Models   
			

Operating Systems  Windows 7, Windows 8, Windows 10

Languages	   English

Version            1.1

Issue Date         09/16/2015

  


----------------------------------------------------------------------
WHAT THIS PACKAGE DOES
----------------------------------------------------------------------
The utility checks to see if the nvLocked TPM permanent flag is set.  

If the nvLocked permanent flag is not set, the utility will automatically 
set the flag and check to see if any protected data areas have been defined 
in the TPM's NV Storage area. (For purposes of this utility, a protected 
area means that one or more TPM NV Storage indices were found that had 
an access permission set in the Index attribute field. This means that 
data may have been stored in this area and may not have been secure 
until this utility was run).

If the nvLocked permanent flag is already set, the utility takes no further
action.
 
 
----------------------------------------------------------------------
Version History
----------------------------------------------------------------------

Summary of Changes

  Where: <    /    >	Version number / Lenovo Build number
	[Important]	Important update
	(New)		New function or added enhancement
	(Fix)		Correction to existing function
        (Update)        Add new funtion


< 1.0 >
	- (New) Initial Release
< 1.1 > 
	- (Update) Add checking for NV areas in use if nvLocked is not set


----------------------------------------------------------------------
INSTALLATION INSTRUCTIONS
----------------------------------------------------------------------

- Run nvPatchPackage.exe to extract the utility.  You may place the utility 
  in any directory you choose


----------------------------------------------------------------------
Running the utility
----------------------------------------------------------------------

Running from File explorer:

 - locate the directory with the utility 
 - right click on nvLocked.exe
 - select "Run as administrator"

Running from a command window:

 - open a command window with elevated privilege
 - change to the directory that contains nvLocked.exe
 - run 'nvlocked.exe' for normal output
 - run 'nvlocked.exe -s' for silent operation

Return codes:
 0 - nvlocked flag was already set 
 1 - nvlocked flag was not set, but was set by the utility
 any other return - could not find compatible TPM 1.2
   


----------------------------------------------------------------------
TRADEMARKS
----------------------------------------------------------------------

* ThinkCentre, ThinkPad and ThinkServer are registered trademarks of Lenovo.


Other company, product, and service names may be registered trademarks,
trademarks or service marks of others.
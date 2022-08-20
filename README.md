# ToyRobot

This is a small C# application in which the aim is to help navigate a Robot around a table, without the Robot falling off.

The Table is defaulted to 5x5; meaning the Robot can move frmo index 0 to index 4 on the X and Y axis. 
The point of origin (0,0) on the table is considered to be the SouthWest corner.

*** VALID COMMAND LIST ***

The Robot can receive the following commands:

MOVE - Moves the Robot one point in the direction it is facing
REPORT - The Robot reports its current location and facing in the format X, Y, F
LEFT - Rotates the Robot left
RIGHT - Rotates the Robot right 
PLACE - Places the Robot on the table. A valid PLACE command must be in the format: PLACE X,Y,F where X and Y represent coordinates and F is the facing

NOTE - All commands prior to a valid PLACE command will be ignored by the Robot.

The Robot can receive the following facings: NORTH, EAST, SOUTH, WEST

The application takes input in two different ways - Command Line or File Based; the default is command line input. Instructions are printed out to the user regardless.

*** RUNNING THE APPLICATION *** 

You can run the application by navigating to the .EXE file located in the bin folder. 
There are 4 default command sets, however, you may create your own command sets. You must place them within the bin folder.

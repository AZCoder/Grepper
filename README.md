# Grepper
Grep UI Tool for Windows

A UI to search all files in a directory and optionally its subdirectories by file extension patterns. Searches may be conducted by
literal text or regular expression patterns. This tool has been developed and maintained since 2010. The code author originally developed
it to work with projects without an IDE (specifically PHP projects and Arma 2 SQF scripting) so that key words could easily be found
in all files fitting the extension pattern. This fulfills the duties that are carried out by major IDE's such as Visual Studio that
can do the same thing internally, although in some cases it may prove easier to do via this tool.

The UI display results in 2 grids. The upper grid shows one row per file were a match was found, including the number of matches.
Clicking on that row will populate the lower grid with each line that contains the search term. Double clicking on an upper grid
line will cause the app to attempt opening the default app for the file type, so a txt file would open in Notepad, Notepadd++, or whatever
default is set. Right clicking on a row in the lower grid will copy that line to the clipboard. This can be useful when you are
trying to see how you resolved a coding problem before, and just want to copy and paste the line into your editor to make it easier
to work on the current piece.

Grepper uses the Windows registry to allow its invocation within Windows Explorer, so that you can right-click on a directory and bring
up the tool for that location. All settings are saved as a json structured file.
